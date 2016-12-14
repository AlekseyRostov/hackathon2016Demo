using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Media;
using Feedback.Core.Services;
using Feedback.UI.Services;
using Java.IO;

// Ported from: http://www.edumobile.org/android/audio-recording-in-wav-format-in-android-programming/

namespace Feedback.Droid.Services
{
    public class AudioRecorderService : IAudioRecorderService
    {
        private readonly IDeviceService _deviceService;
        private const int RecorderBpp = 16;
        private int _bufferSize;
        private readonly Guid _instanceId;
        private readonly ChannelIn _recorderChannels = ChannelIn.Stereo;
        private readonly Encoding _recorderAudioEncoding = Encoding.Pcm16bit;

        private AudioRecord _recorder;
        private bool _isRecording;
        private string _cacheFolder;

        public AudioRecorderService(IDeviceService deviceService)
        {
            _deviceService = deviceService;
            _instanceId = Guid.NewGuid();
            Initialize();
        }

        public void StartRecording()
        {
            _recorder?.Release();

            _recorder = new AudioRecord(AudioSource.Mic, _deviceService.AudioSampleRate, _recorderChannels, _recorderAudioEncoding, _bufferSize);
            _recorder.StartRecording();
            _isRecording = true;

            Task.Run(() => WriteAudioDataToFile());
        }

        void WriteAudioDataToFile()
        {
            byte[] data = new byte[_bufferSize];
            var filename = GetTempFilename();
            FileOutputStream outputStream = null;

            System.Diagnostics.Debug.WriteLine(filename);

            try
            {
                outputStream = new FileOutputStream(filename);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            if(outputStream != null)
            {
                while(_isRecording)
                {
                    _recorder.Read(data, 0, _bufferSize);
                    try
                    {
                        outputStream.Write(data);
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }

                try
                {
                    outputStream.Close();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public string StopRecording()
        {
            if(_recorder != null)
            {
                _isRecording = false;
                _recorder.Stop();
                _recorder.Release();
                _recorder = null;
            }

            var file = GetFilename(Guid.NewGuid().ToString());
            CopyWaveFile(GetTempFilename(), file);
            return file;
        }

        private string GetFilename(string id)
        {
            return System.IO.Path.Combine(_cacheFolder, $"{id}.wav");
        }

        private string GetTempFilename()
        {
            return System.IO.Path.Combine(_cacheFolder, $"{_instanceId}.raw");
        }

        private void CopyWaveFile(string tempFile, string permanentFile)
        {
            long longSampleRate = _deviceService.AudioSampleRate;
            var channels = 2;
            long byteRate = RecorderBpp*longSampleRate*channels/8;

            byte[] data = new byte[_bufferSize];

            try
            {
                var input = new FileInputStream(tempFile);
                var output = new FileOutputStream(permanentFile);
                var totalAudioLen = input.Channel.Size();
                var totalDataLen = totalAudioLen + 36;

                System.Diagnostics.Debug.WriteLine($"File Size: {totalDataLen}");

                WriteWaveFileHeader(output, totalAudioLen, totalDataLen, longSampleRate, channels, byteRate);

                while(input.Read(data) != -1)
                {
                    output.Write(data);
                }

                input.Close();
                output.Close();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void WriteWaveFileHeader(FileOutputStream output, long totalAudioLen, long totalDataLen, long longSampleRate, int channels, long byteRate)
        {
            byte[] header = new byte[44];

            header[0] = Convert.ToByte('R'); // RIFF/WAVE header
            header[1] = Convert.ToByte('I'); //  (byte)'I';
            header[2] = Convert.ToByte('F');
            header[3] = Convert.ToByte('F');
            header[4] = (byte) (totalDataLen & 0xff);
            header[5] = (byte) ((totalDataLen >> 8) & 0xff);
            header[6] = (byte) ((totalDataLen >> 16) & 0xff);
            header[7] = (byte) ((totalDataLen >> 24) & 0xff);
            header[8] = Convert.ToByte('W');
            header[9] = Convert.ToByte('A');
            header[10] = Convert.ToByte('V');
            header[11] = Convert.ToByte('E');
            header[12] = Convert.ToByte('f'); // 'fmt ' chunk
            header[13] = Convert.ToByte('m');
            header[14] = Convert.ToByte('t');
            header[15] = (byte) ' ';
            header[16] = 16; // 4 bytes: size of 'fmt ' chunk
            header[17] = 0;
            header[18] = 0;
            header[19] = 0;
            header[20] = 1; // format = 1
            header[21] = 0;
            header[22] = Convert.ToByte(channels);
            header[23] = 0;
            header[24] = (byte) (longSampleRate & 0xff);
            header[25] = (byte) ((longSampleRate >> 8) & 0xff);
            header[26] = (byte) ((longSampleRate >> 16) & 0xff);
            header[27] = (byte) ((longSampleRate >> 24) & 0xff);
            header[28] = (byte) (byteRate & 0xff);
            header[29] = (byte) ((byteRate >> 8) & 0xff);
            header[30] = (byte) ((byteRate >> 16) & 0xff);
            header[31] = (byte) ((byteRate >> 24) & 0xff);
            header[32] = 2*16/8; // block align
            header[33] = 0;
            header[34] = Convert.ToByte(RecorderBpp); // bits per sample
            header[35] = 0;
            header[36] = Convert.ToByte('d');
            header[37] = Convert.ToByte('a');
            header[38] = Convert.ToByte('t');
            header[39] = Convert.ToByte('a');
            header[40] = (byte) (totalAudioLen & 0xff);
            header[41] = (byte) ((totalAudioLen >> 8) & 0xff);
            header[42] = (byte) ((totalAudioLen >> 16) & 0xff);
            header[43] = (byte) ((totalAudioLen >> 24) & 0xff);

            output.Write(header, 0, 44);
        }

        private void Initialize()
        {
            var sampleRates = new[] {8000, 11025, 16000, 22050, 44100};
            _cacheFolder = MainActivity.Instance.CacheDir.AbsolutePath;
            foreach(int rate in sampleRates.Reverse())
            {
                _bufferSize = AudioRecord.GetMinBufferSize(rate, _recorderChannels, _recorderAudioEncoding);
                _deviceService.AudioSampleRate = rate;
                if(_bufferSize > 0)
                {
                    return;
                }
            }
            throw new Exception("Could not find valid sample rate and buffer size configuration.");
        }
    }
}
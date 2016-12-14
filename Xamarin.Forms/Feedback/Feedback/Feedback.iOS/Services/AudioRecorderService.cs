using System;
using AVFoundation;
using Feedback.UI.Services;
using Foundation;
using PCLStorage;

namespace Feedback.iOS.Services
{
    public class AudioRecorderService : IAudioRecorderService
    {
        private AVAudioRecorder _recorder;
        private NSError _error;
        private NSUrl _currentRecordUrl;
        private NSDictionary _settings;
        private Guid _recordId;
        private readonly string _localStorage;

        public AudioRecorderService()
        {
            _localStorage = FileSystem.Current.LocalStorage.Path;
        }

        public void StartRecording()
        {
            _recordId = Guid.NewGuid();
            if(_recorder == null) InitializeRecorder();

            _recorder.Record();
        }

        public string StopRecording()
        {
            if(_recorder == null)
                throw new Exception("You must first start recording.");

            _recorder.Stop();
            return _currentRecordUrl.Path;
        }

        void InitializeRecorder()
        {
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if(err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return;
            }
            err = audioSession.SetActive(true);
            if(err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return;
            }

            var audioFilePath = PortablePath.Combine(_localStorage, $"{_recordId}.wav");

            Console.WriteLine("Audio File Path: " + audioFilePath);

            _currentRecordUrl = NSUrl.FromFilename(audioFilePath);

            //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
            var values = new NSObject[]
                         {
                             NSNumber.FromFloat(44100.0f), //Sample Rate
                             NSNumber.FromInt32((int) AudioToolbox.AudioFormatType.LinearPCM), //AVFormat
                             NSNumber.FromInt32(2), //Channels
                             NSNumber.FromInt32(16), //PCMBitDepth
                             NSNumber.FromBoolean(false), //IsBigEndianKey
                             NSNumber.FromBoolean(false) //IsFloatKey
                         };

            //Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
            var keys = new NSObject[]
                       {
                           AVAudioSettings.AVSampleRateKey,
                           AVAudioSettings.AVFormatIDKey,
                           AVAudioSettings.AVNumberOfChannelsKey,
                           AVAudioSettings.AVLinearPCMBitDepthKey,
                           AVAudioSettings.AVLinearPCMIsBigEndianKey,
                           AVAudioSettings.AVLinearPCMIsFloatKey
                       };

            //Set Settings with the Values and Keys to create the NSDictionary
            _settings = NSDictionary.FromObjectsAndKeys(values, keys);

            //Set recorder parameters
            _recorder = AVAudioRecorder.Create(_currentRecordUrl, new AudioSettings(_settings), out _error);
            //Set Recorder to Prepare To Record
            _recorder.PrepareToRecord();
        }
    }
}
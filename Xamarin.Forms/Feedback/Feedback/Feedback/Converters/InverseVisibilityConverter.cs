using System;
using Xamarin.Forms;

namespace Feedback.UI.Core.Converters
{
    public class InverseVisibilityConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !VisibilityConverter.IsVisible(value);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
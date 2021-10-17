using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OtakuLib.View.XamlConverter
{
    [ValueConversion(typeof(int), typeof(WindowState))]
    public sealed class WindowStateConverter : IValueConverter
    {
        /// <summary> Gets the default instance </summary>
        public static WindowStateConverter Default { get; } = new WindowStateConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int val ? (WindowState)val : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is WindowState val ? (int)val : value;
        }
    }
}

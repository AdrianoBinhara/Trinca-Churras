using System;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TrincaChurras.Helpers.Converters
{
    public class DataConverter : IValueConverter
    {
        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value?.ToString();
            if (data == null)
                return string.Empty;

            var sB = new StringBuilder(5);
            char[] delimiterChars = { 'T', '-' };
            var split = data?.Split(delimiterChars);

            sB.Append($"{split[1]}/{split[2]}");

            return sB;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

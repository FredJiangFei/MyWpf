using System;
using System.Globalization;
using System.Windows.Data;

namespace PlayWpf.Core.Converter
{
    public class SingleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] parray = parameter.ToString().Split(':');
            if (value == null)
            {
                return null;
            }

            return parray[0].Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase) ? parray[1] : parray[2];  //单值比较
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnValue = "otherValue";
            string[] parray = parameter.ToString().Split(':');
            if (value == null)
                return returnValue;
            var valueStr = value.ToString();
            if (valueStr != parray[1])
                return returnValue;
            else
                return parray[0].Contains('|') ? parray[0].Split('|')[0] : parray[0];
        }
    }
}

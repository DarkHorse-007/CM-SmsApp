using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MessageApp.Views.Converters
{
    class MessageLengthToColourConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsMultipart = (bool)values[0];
            string Text = (null == values[1]) ? String.Empty : values[1].ToString();
            if (!IsMultipart && Text.Length > 160)
            {
                return Brushes.Red;
            }

            return parameter;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

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
    class MessageLengthToTextConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsMultipart = (bool)values[0];
            string Text = (null == values[1])? String.Empty : values[1].ToString();
            if (!IsMultipart)
            {
                return String.Format("{0} / 160",Text.Length);
            }
            else
            {
                return String.Format("{0} Message(s)", 1 + (Text.Length / 153));
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

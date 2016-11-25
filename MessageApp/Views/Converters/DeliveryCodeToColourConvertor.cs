using MessageApp.Models;
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
    class DeliveryCodeToColourConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DeliveryStatusCode code;
            if(!Enum.TryParse<DeliveryStatusCode>(value.ToString(), true, out code))
            {
                throw new ArgumentException();
            }

            switch (code)
            {
                case DeliveryStatusCode.Delivered:
                    return Brushes.Green;
                default:
                    return Brushes.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

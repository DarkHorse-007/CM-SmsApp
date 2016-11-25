using MessageApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MessageApp.Views.Converters
{
    public class DeliveryCodeToTextConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DeliveryStatusCode code;
            if (!Enum.TryParse<DeliveryStatusCode>(value.ToString(), true, out code))
            {
                throw new ArgumentException();
            }

            switch (code)
            {
                case DeliveryStatusCode.Delivered:
                    return "Delivered successfully.";
                default:
                    return "Delivery failed.";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MessageApp.Views.Converters
{
    class OperationResultToImageConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imagesrc = @"/Views/Resources/{0}.png";
            bool IsError = (bool)value;
            if(IsError)
            {
                return string.Format(imagesrc, "Error");
            }
            else
            {
                return string.Format(imagesrc, "Success");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

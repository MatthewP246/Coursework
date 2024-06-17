using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Coursework_UI
{
    class ColourCodeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            string v;
            if ((string)value == "0")
            {
                v = "red";
            }
            else v = "Yellow";
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

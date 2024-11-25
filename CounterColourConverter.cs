using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Coursework_UI
{
    class CounterColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            string path;
            if ((string)value == "0")
            {
                path = "{x:null}";
            }
            else if ((string)value == "1")
            {
                path = "/Images/Red.png";
            }
            else path= "/Images/Yellow.png";

            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

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

            if ((string)value == "R")
            {
                path = "/Images/Red.png";
            }
            else if ((string)value == "RH")
            {
                path = "/Images/Red H.png";
            }
            else if ((string)value == "RV")
            {
                path = "/Images/Red V.png";
            }
            else if ((string)value == "RD1")
            {
                path = "/Images/Red D1.png";
            }
            else if ((string)value == "RD2")
            {
                path = "/Images/Red D2.png";
            }
            else if ((string)value == "Y")
            {
                path = "/Images/Yellow.png";
            }
            else if ((string)value == "YH")
            {
                path = "/Images/Yellow H.png";
            }
            else if ((string)value == "YV")
            {
                path = "/Images/Yellow V.png";
            }
            else if ((string)value == "YD1")
            {
                path = "/Images/Yellow D1.png";
            }
            else if((string)value == "YD2")
            {
                path = "/Images/Yellow D2.png";
            }
            else
            {
                path = "{x:null}";
            }


            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

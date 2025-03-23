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

        public object Convert(object Value, Type targetType, object paramater, CultureInfo culture)
        {
            string Path;

            if ((string)Value == "R")
            {
                Path = "/Images/Red.png";
            }
            else if ((string)Value == "RH")
            {
                Path = "/Images/Red H.png";
            }
            else if ((string)Value == "RV")
            {
                Path = "/Images/Red V.png";
            }
            else if ((string)Value == "RD1")
            {
                Path = "/Images/Red D1.png";
            }
            else if ((string)Value == "RD2")
            {
                Path = "/Images/Red D2.png";
            }
            else if ((string)Value == "Y")
            {
                Path = "/Images/Yellow.png";
            }
            else if ((string)Value == "YH")
            {
                Path = "/Images/Yellow H.png";
            }
            else if ((string)Value == "YV")
            {
                Path = "/Images/Yellow V.png";
            }
            else if ((string)Value == "YD1")
            {
                Path = "/Images/Yellow D1.png";
            }
            else if((string)Value == "YD2")
            {
                Path = "/Images/Yellow D2.png";
            }
            else
            {
                Path = "{x:null}";
            }


            return new BitmapImage(new Uri(Path, UriKind.Relative));
        }
        public object ConvertBack(object Value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Coursework_UI
{
    class IntegerToStringConverter : IValueConverter
    {
        // converts the data in your model to a form that can be subtituted into the relevant property in
        // your xaml
        public object Convert(object value, Type targetType, object paramater, CultureInfo culture)
        {
            string v;
            v = "" + value;

            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

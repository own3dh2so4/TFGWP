using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrTab.View.convert
{
    public class BoolToHeart : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool actual = false;
            bool.TryParse(value.ToString(), out actual);
            if (actual)
                return  "/View/icons/heart.red.png";
            else
                return  "/View/icons/heart.white.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

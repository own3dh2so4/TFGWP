
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrTab.View.convert
{
    public class NumeroMesToNombreConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";
            switch((string)value)
            {
                case "1": result = "Enero";
                    break;
                case "2": result = "Febrero";
                    break;
                case "3": result = "Marzo";
                    break;
                case "4": result = "Abril";
                    break;
                case "5": result = "Mayo";
                    break;
                case "6": result = "Junio";
                    break;
                case "7": result = "Julio";
                    break;
                case "8": result = "Agosto";
                    break;
                case "9": result = "Septiembre";
                    break;
                case "10": result = "Octubre";
                    break;
                case "11": result = "Noviembre";
                    break;
                case "12": result = "Diciembre";
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}

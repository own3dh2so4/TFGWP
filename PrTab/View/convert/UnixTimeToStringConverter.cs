using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrTab.View.convert
{
    public class UnixTimeToStringConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ret = "";
            int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int messageTime = (int) value;
            //int.TryParse(value.ToString(),out messageTime);
            int valor = unixTimestamp - messageTime;
            valor = valor / 60;
            if (valor < 60)
                ret = "Enviado hace " + valor + " min";
            else
            {
                valor = valor / 60;
                if (valor < 24)
                    ret = "Enviado hace " + valor + " horas";
                else
                {
                    valor = valor / 24;
                    if (valor < 31)
                        ret = "Enviado hace " + valor + " dias";
                    else
                    {
                        valor = valor / 30;
                        if (valor < 12)
                            ret = "Enviado hace " + valor + "meses";
                        else
                        {
                            valor = valor / 12;
                            ret = "Enviado hace " + valor + " años";
                        }
                    }
                }
            }

            return ret;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

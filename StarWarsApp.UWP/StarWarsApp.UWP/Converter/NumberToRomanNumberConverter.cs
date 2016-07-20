using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Core.Utils.Extentions;

namespace StarWarsApp.UWP.Converter
{
    public class NumberToRomanNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = String.Empty;
            int number;
            if (Int32.TryParse(value.ToString(), out number))
            {
                if (number >= 10) result = "X" + ExtensionMethods.ToRoman(number - 10);
                else if (number >= 9) result = "IX" + ExtensionMethods.ToRoman(number - 9);
                else if (number >= 5) result = "V" + ExtensionMethods.ToRoman(number - 5);
                else if (number >= 4) result = "IV" + ExtensionMethods.ToRoman(number - 4);
                else if (number >= 1) result = "I" + ExtensionMethods.ToRoman(number - 1);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

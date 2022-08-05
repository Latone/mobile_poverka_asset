using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mobile_poverka_asset.ViewModels
{
    public class YesNoMaybeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "true":
                    return Color.Green;
                case "false":
                    return Color.Red;
                case "maybe":
                    return Color.Orange;
            }

            return Color.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // You probably don't need this, this is used to convert the other way around
            // so from color to yes no or maybe
            throw new NotImplementedException();
        }
    }
}

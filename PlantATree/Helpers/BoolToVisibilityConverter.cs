using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace PlantATree
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        Visibility falseValue = Visibility.Collapsed;
        public Visibility FalseValue
        {
            get { return falseValue; }
            set { falseValue = value; }
        }

        Visibility trueValue = Visibility.Visible;
        public Visibility TrueValue
        {
            get { return trueValue; }
            set { trueValue = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool))
                return FalseValue;

            return ((bool)value) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}

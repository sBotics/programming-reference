using System;
using System.Linq;
using System.Collections.Generic;

namespace sBotics
{
    namespace CodeUtils
    {
        public class Color
        {
            private byte red, green, blue;

            public double Red
            {
                get => (double) red;
                set => red = (byte) ClampColor(value);
            }

            public double Green
            {
                get => (double) green;
                set => green = (byte) ClampColor(value);
            }

            public double Blue
            {
                get => (double) blue;
                set => blue = (byte) ClampColor(value);
            }

            public double Brightness
            {
                // Rec. 601 Luma
                get => (Red * 0.299 + Green * 0.587 + Blue * 0.114);
            }

            public Color(double r, double g, double b)
            {
                this.Red = r;
                this.Green = g;
                this.Blue = b;
            }

            static double ClampColor(double value) => 
                Math.Floor(Utils.Clamp(value, 0, 255));

            static string GetColorName(Colors color)
            {   
                // Other languages
                if(__sBotics__SpecialCodeUtils.Locale == enums.REducLanguages.pt_BR.ToString())
                    return __sBotics__SpecialCodeUtils.ColorsPTBR[color];

                // English
                else return color.ToString();
            }

            public double DistanceTo(Color compareWith) =>
                Math.Abs(compareWith.Red - this.Red) + Math.Abs(compareWith.Green - this.Green) + Math.Abs(compareWith.Blue - this.Blue);

            public static Color ToColor(string _color, Colors _default = Colors.Black)
            {
                Colors color;

                // Other languages
                if(__sBotics__SpecialCodeUtils.Locale == enums.REducLanguages.pt_BR.ToString())
                    return ToColor(__sBotics__SpecialCodeUtils.ColorsPTBR.FirstOrDefault(x => x.Value == _color).Key);

                // English
                else if(Enum.TryParse<Colors>(_color, out color))
                    return ToColor(color);
                    
                else return ToColor(_default);
            }
        
            public static Color ToColor(Colors color)
            {
                Dictionary<Colors, Color> colorValues = new Dictionary<Colors, Color>
                {
                    { Colors.Black, new Color(0, 0, 0) },
                    { Colors.White, new Color(255, 255, 255) },
                    { Colors.Green, new Color(0, 255, 0) },
                    { Colors.Red, new Color(255, 0, 0) },
                    { Colors.Blue, new Color(0, 0, 255) },
                    { Colors.Yellow, new Color(255, 255, 0) },
                    { Colors.Magenta, new Color(255, 0, 255) },
                    { Colors.Cyan, new Color(0, 255, 255) },
                };

                return colorValues[color];
            }

            public Colors Closest()
            {
                double distance = int.MaxValue;
                Colors closest = Colors.Black;

                foreach (Colors color in (Colors[]) Enum.GetValues(typeof(Colors)))
                {
                    double _distance = this.DistanceTo(ToColor(color));
                    if(_distance < distance)
                    {
                        distance = _distance;
                        closest = color;
                    }
                }

                return closest;
            }

            public override string ToString() => GetColorName(Closest());

            public static Color operator +(Color a, Color b) => new Color(a.red + b.red, a.green + b.green, a.blue + b.blue);
            public static Color operator +(Color a, double b) => new Color(a.red + b, a.green + b, a.blue + b);
            public static Color operator -(Color a, Color b) => new Color(a.red - b.red, a.green - b.green, a.blue - b.blue);
            public static Color operator -(Color a, double b) => new Color(a.red - b, a.green - b, a.blue - b);
            public static Color operator *(Color a, Color b) => new Color(a.red * b.red, a.green * b.green, a.blue * b.blue);
            public static Color operator *(Color a, double b) => new Color(a.red * b, a.green * b, a.blue * b);
            public static Color operator /(Color a, Color b) => new Color(a.red / b.red, a.green / b.green, a.blue / b.blue);
            public static Color operator /(Color a, double b) => new Color(a.red / b, a.green / b, a.blue / b);

            public static bool operator >(Color a, Color b) => a.Brightness > b.Brightness;
            public static bool operator <(Color a, Color b) => a.Brightness < b.Brightness;
            public static bool operator >=(Color a, Color b) => a.Brightness >= b.Brightness;
            public static bool operator <=(Color a, Color b) => a.Brightness <= b.Brightness;
        }
    }
}

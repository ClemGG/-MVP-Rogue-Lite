using UnityEngine;

namespace Project.Colors
{
    public static class ColorLibrary
    {
        //Default
        public static Color32 None = Swatch.None;
        public static Color32 OutOfFOV = Swatch.Grey;   //In case we want a common color for explored, invisible Cells


        //Floor Colors
        public static Color32 FloorBackground = Swatch.Black;
        public static Color32 Floor = Swatch.AlternateDarkest;
        public static Color32 FloorBackgroundFov = Swatch.DbDark;
        public static Color32 FloorFov = Swatch.Alternate;

        //Wall Colors
        public static Color32 WallBackground = Swatch.SecondaryDarkest;
        public static Color32 Wall = Swatch.Secondary;
        public static Color32 WallBackgroundFov = Swatch.SecondaryDarker;
        public static Color32 WallFov = Swatch.SecondaryLighter;

        //Player Colors
        public static Color32 ActorBackground = Swatch.DbLight;
        public static Color32 Actor = Swatch.DbLight;
        public static Color32 ActorBackgroundFov = Swatch.DbLight;
        public static Color32 ActorFov = Swatch.DbLight;


        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }



        public static string ColoredChar(this char caracter, Color32 textColor)
        {
            string coloredChar = $"<color=#{ColorToHex(textColor)}>{caracter}</color>";
            return coloredChar;
        }


        public static string ColoredCharAndBackground(this char caracter, Color32 textColor, Color32 backgroundColor)
        {
            //Si la typo ne fonctionne pas, rajouter "My Fonts/Consolas-Font/"
            string coloredBackground = $"<font=\"My Fonts/Consolas-Font/CONSOLA SDF\"><mark=#{ColorToHex(backgroundColor)}>{ColoredChar(caracter, textColor)}</mark></font>";
            return coloredBackground;
        }
    }
}

using UnityEngine;

namespace Project.Colors
{
    public static class ColorLibrary
    {
        //Default
        [field: SerializeField] public static Color32 None { get; } = Swatch.None;
        [field: SerializeField] private static Color32 OutOfFOV { get; } = Swatch.Grey;   //In case we want a common color for explored, invisible Cells


        //Floor Colors
        [field: SerializeField] private static Color32 FloorBackground { get; } = Swatch.Black;
        [field: SerializeField] private static Color32 Floor { get; } = Swatch.AlternateDarkest;
        [field: SerializeField] private static Color32 FloorBackgroundFov { get; } = Swatch.DbDark;
        [field: SerializeField] private static Color32 FloorFov { get; } = Swatch.Alternate;

        //Wall Colors
        [field: SerializeField] private static Color32 WallBackground { get; } = Swatch.SecondaryDarkest;
        [field: SerializeField] private static Color32 Wall { get; } = Swatch.Secondary;
        [field: SerializeField] private static Color32 WallBackgroundFov { get; } = Swatch.SecondaryDarker;
        [field: SerializeField] private static Color32 WallFov { get; } = Swatch.SecondaryLighter;

        //Player Colors
        [field: SerializeField] private static Color32 ActorBackground { get; } = Swatch.DbLight;
        [field: SerializeField] private static Color32 Actor { get; } = Swatch.DbLight;
        [field: SerializeField] private static Color32 ActorBackgroundFov { get; } = Swatch.DbLight;
        [field: SerializeField] private static Color32 ActorFov { get; } = Swatch.DbLight;

        //Enemy Colors
        [field: SerializeField] private static Color32 Rat { get; } = Swatch.DbBrightWood;


        //Door Colors
        [field: SerializeField] private static Color32 DoorBackground { get; } = Swatch.ComplimentDarkest;
        [field: SerializeField] private static Color32 Door { get; } = Swatch.ComplimentLighter;
        [field: SerializeField] private static Color32 DoorBackgroundFov { get; } = Swatch.ComplimentDarker;
        [field: SerializeField] private static Color32 DoorFov { get; } = Swatch.ComplimentLightest;


        //Stairs Colors
        [field: SerializeField] private static Color32 StairsBackground { get; } = Swatch.Black;
        [field: SerializeField] private static Color32 Stairs { get; } = Swatch.DbLight;
        [field: SerializeField] private static Color32 StairsBackgroundFov { get; } = Swatch.DbGrass;
        [field: SerializeField] private static Color32 StairsFov { get; } = Swatch.Black;



        //Item Colors
        [field: SerializeField] private static Color32 FinalItem { get; } = Swatch.Red;
        [field: SerializeField] private static Color32 HealthPotion { get; } = Swatch.Green;


        //Log
        //For all info to write on the console logs on screen
        [field: SerializeField] public static Color32 Gold { get; } = Swatch.DbSun;
        [field: SerializeField] public static Color32 DisplayedTileName { get; } = Swatch.DbBrightWood;
        [field: SerializeField] public static Color32 InventoryChar { get; } = Swatch.Green;


        //Debug
        [field: SerializeField] public static Color32 debug_BackgroundHighlight { get; } = Swatch.Yellow;  //To highlight Tiles if we want to debug something




        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }



        public static string ColoredText(string text, Color32 textColor)
        {
            string coloredChar = $"<color=#{ColorToHex(textColor)}>{text}</color>";
            return coloredChar;
        }

        public static string ColoredChar(char caracter, Color32 textColor)
        {
            string coloredChar = $"<color=#{ColorToHex(textColor)}>{caracter}</color>";
            return coloredChar;
        }


        public static string ColoredCharAndBackground(char caracter, Color32 textColor, Color32 backgroundColor, bool debug = false)
        {
            //Si la typo ne fonctionne pas, rajouter "My Fonts/Consolas-Font/CONSOLA SDF"
            //Si la typo ne fonctionne pas, rajouter "My Fonts/Source Code Pro/SourceCodePro SDF"
            string coloredBackground = $"<font=\"My Fonts/Consolas-Font/CONSOLA SDF\"><mark=#{ColorToHex(backgroundColor)}>{ColoredChar(caracter, textColor)}</mark></font>";
            return coloredBackground;
        }
    }
}

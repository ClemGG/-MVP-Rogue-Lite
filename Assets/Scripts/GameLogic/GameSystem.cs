namespace Project.Logic
{
    /// <summary>
    /// Used to store informations relative to the current game session (map seed, randoms, etc.)
    /// </summary>
    public class GameSystem
    {
        #region Flags

        public static bool s_IsGameOver { get; set; } = false;
        public static int s_FloorLevel { get; set; } = 1;

        #endregion


        #region Display

        public const string c_ShowHelpText = "Press h to toggle the commans menu.";

        // Define the maximum number of lines in the MessageLog our resolution allows
        public const int c_MaxLines = 5;
        // Define the maximum number of visible healthbars in the InspectorLog our resolution alllows
        public const int c_MaxHealthBars = 15;

        #endregion
    }
}
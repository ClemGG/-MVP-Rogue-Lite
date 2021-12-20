
namespace Project.Logic
{
    /// <summary>
    /// Used to store informations relative to the current game session (map seed, randoms, etc.)
    /// </summary>
    public class GameSystem
    {
        #region Flags

        public static bool s_IsGameOver { get; set; } = false;
        public static bool s_IsGoalReached { get; set; } = false;   //The final item the Player must reach at the last level. Used to unlock stairs and make enemies more difficult.
        public static int s_PreviousFloorLevel { get; set; } = 0;   // Used to determine if we should spawn the Player on the Upstairs or npt
        public static int s_FloorLevel { get; set; } = 1;           // Incremented/Decremented in the Stairs' TileBehaviour

        public const int c_MaxFloorLevel = 20;           // The deepest level the Player can go

        #endregion


        #region Display

        public const string c_ShowHelpText = "Press h to toggle the commands menu.";

        // Define the maximum number of lines in the MessageLog our resolution allows
        public const int c_MaxLines = 5;
        // Define the maximum number of visible healthbars in the InspectorLog our resolution alllows
        public const int c_MaxHealthBars = 15;

        #endregion
    }
}
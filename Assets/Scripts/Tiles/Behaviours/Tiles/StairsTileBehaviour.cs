using Project.Display;
using Project.Logic;
using Project.Tiles;
using UnityEngine;

namespace Project.Behaviours.Tiles
{
    [CreateAssetMenu(fileName = "Stairs Behaviour", menuName = "Rogue/Actors/Behaviours/Tile/Stairs")]
    public class StairsTileBehaviour : TileBehaviour
    {
        [field: SerializeField] private bool IsUpstairs { get; set; }

        public override void OnActorInteracted(PlayerTile player, Cell thisCell, Tile thisTile)
        {
            //If the Player tries to climb back up without reaching the goal, OR tries to go back down after reching the goal,
            //the corresponding Stairs will be locked.
            if (IsUpstairs ^ !GameSystem.s_IsGoalReached)
            {

                // When the Player presses the Interact button while on a Stairs Tile, we generate a new level...
                GameSystem.s_FloorLevel += IsUpstairs ? -1 : 1;
                GameObject.Find("GameManager").GetComponent<GameManager>().GenerateNewDungeon();

                //And the change the MapLog's title to reflect the new level.
                MapLog.ChangeTitle();
            }
            else
            {
                MessageLog.Print("You cannot go back now!");
            }
        }
    }
}
using Project.Display;
using Project.Logic;
using Project.Tiles;
using UnityEngine;

namespace Project.Behaviours.Tiles
{
    [CreateAssetMenu(fileName = "Final Item Behaviour", menuName = "Rogue/Actors/Behaviours/Tile/Item/Final Item Behaviour")]
    public class FinalItemTileBehaviour : TileBehaviour
    {
        public override void OnActorInteracted(PlayerTile player, Cell thisCell, Tile thisTile)
        {
            //The Player has collected the final item, which means he is now able to go upstairs and leave the dungeon.
            GameSystem.s_IsGoalReached = true;
            MessageLog.Print($"You have retrieved the {thisTile.TileName}! You can leave the dungeon now!");
        }
    }
}
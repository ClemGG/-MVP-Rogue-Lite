using Project.Display;
using Project.Tiles;
using UnityEngine;

namespace Project.Behaviours.Tiles
{
    [CreateAssetMenu(fileName = "Health Item Behaviour", menuName = "Rogue/Actors/Behaviours/Tile/Item/Health Item Behaviour")]
    public class HealthItemTileBehaviour : ItemTileBehaviour
    {
        [field: SerializeField] private int HealthGain { get; set; }


        public override void OnItemConsumed(PlayerTile player, Cell thisCell, Tile thisTile)
        {
            base.OnItemConsumed(player, thisCell, thisTile);

            //Heal Player by [HealthGain] amount
            player.Stats.Health = Mathf.Clamp(player.Stats.Health + HealthGain, player.Stats.Health, player.Stats.MaxHealth);
            MessageLog.Print($"You consumed the {thisTile.TileName}. You regain {HealthGain} HP.");
        }
    }
}
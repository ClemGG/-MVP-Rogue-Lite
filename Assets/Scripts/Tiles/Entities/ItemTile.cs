using Project.Tiles;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Rogue/Entities/Item")]
public class ItemTile : Tile
{
    [field: Space(10)]
    [field: Header("Item Settings:")]
    [field: Space(10)]

    [field: SerializeField, Tooltip("Can the Player have multiple instances of the same Item in a single slot?")]
    public bool IsStackable { get; set; }
    [field: SerializeField, Tooltip("Should the Item be destroyed after usage?")]
    public bool IsConsumable { get; set; }



    public ItemTile()
    {
        SeeThrough = true;
        Walkable = true;
    }
}

using Project.Tiles;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Rogue/Entities/Item")]
public class ItemTile : Tile
{
     [Space(10)]
     [Header("Item Settings:")]
     [Space(10)]

    [field: SerializeField, Tooltip("Can the Player have multiple instances of the same Item in a single slot?")] 
    private bool _isStackable;
    [field: SerializeField, Tooltip("Should the Item be destroyed after usage?")] 
    private bool _isConsumable;



    public ItemTile()
    {
        SeeThrough = true;
        Walkable = true;
    }
}

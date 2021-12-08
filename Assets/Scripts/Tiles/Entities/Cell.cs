using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Tiles
{
    /// <summary>
    /// Contains info on each slot of our map.
    /// </summary>
    

    [Serializable]
    public class Cell
    {
        public Vector2Int Position { get; set; }    //The index of this cell in the map array.
        public List<Tile> Tiles { get; set; } = new List<Tile>();       //The list of tiles contained in this cell. Each time the games creates a Tile,
                                                                        //it is added on top of the others. Only the last Tile is displayed.

        public bool IsExplored { get; set; }        //Has the Player already encountered this Cell?
        public bool IsInPlayerFov { get; set; }     //Is this Cell visible by the player?


        //Can the player see through this Cell?
        //(If any of the Tiles in a Cell is not see-through, the player cannot see through the Cell at all.)
        public bool SeeThrough
        {
            get
            {
                for (int i = 0; i < Tiles.Count; i++)
                {
                    if (!Tiles[i].SeeThrough)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        
        //Can the player walk on this Cell?
        //(If any of the Tiles in a Cell is not walkable, the player cannot walk on the Cell at all.)
        public bool Walkable
        {
            get
            {
                for (int i = 0; i < Tiles.Count; i++)
                {
                    if (!Tiles[i].Walkable)
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        //If we don't call Init() to keep the old dimensions and preserve memory, we have to clean the previous
        //cells in order to use them again.
        //Cell.Clear() allows us to reset the Cell manually in case we add more lists in the future
        public void Clear()
        {
            Tiles.Clear();
            IsExplored = false;
            IsInPlayerFov = false;
        }


        public bool Contains<TileType>() where TileType : Tile
        {
            bool contains = false;
            for (int i = 0; i < Tiles.Count; i++)
            {
                if (Tiles[i] is TileType)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }
        public bool Contains<TileType>(string tileName) where TileType : Tile
        {
            bool contains = false;
            for (int i = 0; i < Tiles.Count; i++)
            {
                if (Tiles[i] is TileType && Tiles[i].TileName == tileName)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        public void OnActorEntered(ActorTile actor)
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].OnActorEntered(actor, this);
            }
        }
        public void OnActorCollided(ActorTile actor)
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].OnActorCollided(actor, this);
            }
        }
    }
}
using UnityEngine;

namespace Project.Pathfinding.AStar
{
    public class AStarNode
    {
        public Vector2Int position;
        public int fCost;
        public int gCost;
        public int hCost;
        public AStarNode parent;
        public bool isOnClosedList = false;
        public bool isOnOpenList = false;
    }
}
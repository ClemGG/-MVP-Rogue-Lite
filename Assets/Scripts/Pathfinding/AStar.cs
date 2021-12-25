using Project.Generation;
using Project.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Pathfinding.AStar
{
    public static class AStar
    {
        static List<AStarNode> openList;
        static List<AStarNode> closedList;
        static AStarNode[,] allNodes;

        public static List<Vector2Int> CalculatePath(Vector2Int start, Vector2Int target)
        {

            List<Vector2Int> path = new List<Vector2Int>();

            openList = new List<AStarNode>();
            closedList = new List<AStarNode>();
            allNodes = new AStarNode[DungeonInfo.s_Size.x, DungeonInfo.s_Size.y];
            for (int y = 0; y < DungeonInfo.s_Size.y; y++)
            {
                for (int x = 0; x < DungeonInfo.s_Size.x; x++)
                {
                    allNodes[x, y] = new AStarNode();
                }
            }

            AStarNode firstNode = new AStarNode() { position = start, gCost = 0, isOnClosedList = true };
            closedList.Add(firstNode);

            foreach (Vector2Int position in GetNeighbours(firstNode.position))
            {
                AStarNode node = new AStarNode() { position = position, parent = firstNode, hCost = CalculateHCost(position, target), gCost = CalculateGCost(firstNode, position) };
                node.fCost = node.gCost + node.hCost;
                node.isOnOpenList = true;
                allNodes[position.x, position.y] = node;
                openList.Add(node);
            }


            AStarNode lastNode = null;

            while (openList.Count > 0)
            {
                AStarNode node = openList[0];

                //We take the cheapest Node in the list (aka the closest to the target)
                foreach (AStarNode pathNode in openList)
                {
                    if (node.fCost > pathNode.fCost || (node.fCost == pathNode.fCost && node.hCost > pathNode.hCost))
                    {
                        node = pathNode;
                    } 
                }

                if (node.position == target) 
                {
                    lastNode = node;
                    break;
                }

                closedList.Add(node);
                allNodes[node.position.x, node.position.y].isOnClosedList = true;
                openList.Remove(node);
                allNodes[node.position.x, node.position.y].isOnOpenList = false;

                bool foundTarget = false;

                foreach (Vector2Int position in GetNeighbours(node.position))
                {
                    if (position == target)
                    {
                        foundTarget = true;
                        break;
                    }


                    AStarNode neighbour = new AStarNode() { position = position, parent = node, gCost = CalculateGCost(node, position), hCost = CalculateHCost(position, target), isOnOpenList = true };
                    neighbour.fCost = neighbour.gCost + neighbour.hCost;
                    allNodes[position.x, position.y] = neighbour;
                    openList.Add(neighbour);
                }

                if (foundTarget)
                {
                    openList.Clear();
                    lastNode = node;
                    break;
                }
            }

            path.Add(target);
            RetracePath(path, start, lastNode);

            return path;
        }

        static void RetracePath(List<Vector2Int> path, Vector2Int start, AStarNode lastNode)
        {
            AStarNode current = lastNode;

            while (current.position != start)
            {
                path.Add(current.position);
                current = current.parent;
            }

            path.Reverse();
        }

        static List<Vector2Int> GetNeighbours(Vector2Int parent)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();

            for (int y = parent.y - 1; y <= parent.y + 1; y++)
            {
                for (int x = parent.x - 1; x <= parent.x + 1; x++)
                {
                    if (x >= 0 && y >= 0 && x < DungeonInfo.s_Size.x && y < DungeonInfo.s_Size.y)
                    {
                        Cell c = DungeonInfo.GetCellAt(x, y);
                        if (c.Walkable || c.Contains<PlayerTile>())
                        {
                            if (!allNodes[x, y].isOnClosedList && !allNodes[x, y].isOnOpenList)
                            {
                                neighbours.Add(new Vector2Int(x, y));
                            }
                        }
                    }
                }
            }

            return neighbours;
        }

        static int CalculateHCost(Vector2Int position, Vector2Int target)
        {
            int hCost = 0;

            int x = Mathf.Abs(position.x - target.x);
            int y = Mathf.Abs(position.y - target.y);

            hCost = x + y;

            return hCost;
        }

        static int CalculateGCost(AStarNode parent, Vector2Int position)
        {
            int localG;

            if (position.x != parent.position.x && position.y != parent.position.y) localG = 14;
            else localG = 10;

            int gCost = parent.fCost + localG;

            return gCost;
        }


    }
}
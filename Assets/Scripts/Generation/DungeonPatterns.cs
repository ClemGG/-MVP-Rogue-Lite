using UnityEngine;
using Project.ValueTypes;
using System.Linq;
using Project.Tiles;

namespace Project.Generation
{
    /// <summary>
    /// Stores all dungeon generation algorithms in one place for ease of use
    /// instead of storing them in the DungeonGenerator class
    /// </summary>
    public static class DungeonPatterns
    {
        #region Private Constructors

        /// <summary>
        /// Generates a Feature and adds it on the Map.
        /// </summary>
        /// <param name="position">The starting generation position (top left corner of the Feature).</param>
        /// <param name="size">The size of the Feature to generate.</param>
        /// <param name="type">The type of the Feature to generate (Room, Corridor, etc.)</param>
        private static void GenerateRectangularFeature(Vector2Int position, Vector2Int size, FeatureType type)
        {
            Feature newFeature = new Feature
            {
                Type = type,
                Bounds = new Rectangle(position, size),
            };

            Vector2Int startPos = position;
            Vector2Int endPos = position + size;

            //Populates each Cell with exactly 1 Floor Tile
            for (int y = startPos.y; y < endPos.y - 1; y++)
            {
                for (int x = startPos.x; x < endPos.x - 1; x++)
                {
                    Cell currentCell = DungeonMap.s_Map[x, y];
                    Tile newTile = TileLibrary.Floor;

                    //Removes the Wall Tile to replace it by a Floor Tile
                    currentCell.Clear();

                    currentCell.Tiles.Add(newTile);
                    newFeature.Cells.Add(currentCell);  //Stores the generated Feature in case we need to get its content later
                }

            }

            DungeonInfo.s_AllFeatures.Add(newFeature);

            switch (type)
            {
                case FeatureType.Room:
                    DungeonInfo.s_AllRooms.Add(newFeature);
                    break;

                case FeatureType.Corridor:
                    DungeonInfo.s_AllCorridors.Add(newFeature);
                    break;

                default:
                    break;
            }
        }




        // Carve a tunnel out of the map parallel to the x-axis
        private static void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            Vector2Int position = new Vector2Int(Mathf.Min(xStart, xEnd), yPosition);
            Vector2Int size = new Vector2Int(Mathf.Abs(xStart - xEnd) + 2, 2);

            GenerateRectangularFeature(position, size, FeatureType.Corridor);
        }

        // Carve a tunnel out of the map parallel to the y-axis
        private static void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
    {
            Vector2Int position = new Vector2Int(xPosition, Mathf.Min(yStart, yEnd));
            Vector2Int size = new Vector2Int(2, Mathf.Abs(yStart - yEnd) + 2);

            GenerateRectangularFeature(position, size, FeatureType.Corridor);
        }

        #endregion


        #region Public Constructors

        /// <summary>
        /// Instantiates a single room the size of the dungeon, with only 4 walls and a floor.
        /// </summary>
        public static void GenerateOneRoom()
        {
            GenerateRectangularFeature(Vector2Int.one, DungeonMap.s_Size - Vector2Int.one, FeatureType.Room);
        }


        /// <summary>
        /// Creates a standard dungeon, with random rooms and object placements.
        /// </summary>
        /// <param name="doubleCorridors"> Should we generate a single L-shaped Corridor or two ?</param>
        public static void GenerateRandomDungeon(Vector2Int minMaxFeatureSize, int maxRooms, bool doubleCorridors = false)
        {

            // Try to place as many rooms as the specified maxRooms
            for (int r = 0; r < maxRooms; r++)
            {
                // Determine the size and position of the room randomly
                int roomWidth = Random.Range(minMaxFeatureSize.x, minMaxFeatureSize.y + 1);
                int roomHeight = Random.Range(minMaxFeatureSize.x, minMaxFeatureSize.y + 1);
                int roomXPosition = Random.Range(1, DungeonMap.s_Size.x - roomWidth - 1);
                int roomYPosition = Random.Range(1, DungeonMap.s_Size.y - roomHeight - 1);
                                               //^1 instead of 0 to avoid creating the Room on the outer limits of the dungeon

                // All of our rooms can be represented as Rectangles
                var newRoom = new Rectangle(roomXPosition, roomYPosition,
                  roomWidth, roomHeight);

                // Check to see if the room rectangle intersects with any other rooms
                bool newRoomIntersects = DungeonInfo.s_AllFeatures.Any(feature => newRoom.Intersects(feature.Bounds));

                // As long as it doesn't intersect add it to the list of rooms
                if (!newRoomIntersects)
                {
                    GenerateRectangularFeature(newRoom.Position, newRoom.Size, FeatureType.Room);
                }


            }


            //Adding Corridors between Rooms
            // Iterate through each room that was generated
            // Don't do anything with the first room, so start at i = 1 instead of i = 0
            for (int i = 1; i < DungeonInfo.s_AllRooms.Count; i++)
            {
                // For all remaing rooms get the center of the room and the previous room
                int previousRoomCenterX = DungeonInfo.s_AllRooms[i - 1].Bounds.Center.x;
                int previousRoomCenterY = DungeonInfo.s_AllRooms[i - 1].Bounds.Center.y;
                int currentRoomCenterX = DungeonInfo.s_AllRooms[i].Bounds.Center.x;
                int currentRoomCenterY = DungeonInfo.s_AllRooms[i].Bounds.Center.y;


                //If we generate a single L-shaped connection, create 2 Corridors
                if (!doubleCorridors)
                {

                    // Give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
                    if (Random.Range(0, 2) == 0)
                    {
                        CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                        CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                    }
                    else
                    {
                        CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                        CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                    }
                }
                //Otherwise, create 4 Corridors
                else
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }



        }

        #endregion
    }
}
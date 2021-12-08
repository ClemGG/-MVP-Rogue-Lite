using Project.Logic;
using System.Reflection;
using UnityEngine;

namespace Project.Combat
{
    /// <summary>
    /// Retrieves an instance of the stats to avoid modifying the original.
    /// This is where we also set random numbers to those stats for more variety between similar Actors.
    /// This allows us to avoid randomizing them in the TileLibrary class instead.
    /// </summary>
    public static class StatsLibrary
    {
        //TODO : Use Object Pooling instead of those Instantiate() to recycle the Tiles instead of wasting memory on recreating them.
        public static ActorStats PlayerStats { get { return Object.Instantiate(Resources.Load<ActorStats>("Stats/PlayerStats")); } }
        public static ActorStats RatStats
        {
            //RatStats set explicitely in case we want to modify its stats before returning it
            //We use the floorLevel so that the Enemy becomes stronger the deeper the player goes into the dungeon
            get
            {
                //If the Player hasn't reached the goal yet, we progressively increase the difficulty when he goes down a level
                //Otherwise, he's climbing back up; so we set all the monsters in the floor to maximum difficulty
                int difficultyLevel = GameSystem.s_IsGoalReached ? GameSystem.c_MaxFloorLevel : GameSystem.s_FloorLevel;


                ActorStats ratStats = Object.Instantiate(Resources.Load<ActorStats>("Stats/RatStats"));
                ratStats.Attack += difficultyLevel / 3;
                ratStats.Defense += difficultyLevel / 3;
                ratStats.Gold = Random.Range(0, ratStats.Gold + 1);
                return ratStats;
            }
        }


        private static PropertyInfo[] _fieldsNames
        {
            get
            {
                return _fields ??= typeof(StatsLibrary).GetProperties();
            }
        }
        private static PropertyInfo[] _fields;


        //This method uses the actor's name to retrieve the actual property field sharing the name name in the format {name}Stats
        public static ActorStats GetStats(string actorName)
        {
            string statsName = $"{actorName}Stats";
            for (int i = 0; i < _fieldsNames.Length; i++)
            {
                if (statsName == _fieldsNames[i].Name)
                {
                    return (ActorStats)_fieldsNames[i].GetValue(_fieldsNames[i]);
                }
            }

            return null;
        }
    }
}
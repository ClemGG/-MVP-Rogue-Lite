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
        public static ActorStats PlayerStats { get { return Object.Instantiate(Resources.Load<ActorStats>("Stats/PlayerStats")); } }
        public static ActorStats RatStats
        {
            //RatStats set explicitely in case we want to modify its stats before returning it
            //We use the floorLevel so that the Enemy becomes stronger the deeper the player goes into the dungeon
            get
            {
                ActorStats ratStats = Object.Instantiate(Resources.Load<ActorStats>("Stats/RatStats"));
                ratStats.Attack += GameSystem.s_FloorLevel/3;
                ratStats.Defense += GameSystem.s_FloorLevel/3;
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
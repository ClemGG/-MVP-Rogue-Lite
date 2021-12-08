using System.Reflection;
using UnityEngine;

namespace Project.Behaviours.FOV
{

    /// <summary>
    /// Retrieves an instance of the stats to avoid modifying the original.
    /// This is where we also set random numbers to those stats for more variety between similar Actors.
    /// This allows us to avoid randomizing them in the TileLibrary class instead.
    /// </summary>
    public static class FOVLibrary
    {
        //TODO : Use Object Pooling instead of those Instantiate() to recycle the Tiles instead of wasting memory on recreating them.
        public static FOV PlayerFOV { get { return Object.Instantiate(Resources.Load<FOV>("Behaviours/FOV/PlayerFOV")); } }
        public static FOV RatFOV { get { return Object.Instantiate(Resources.Load<FOV>("Behaviours/FOV/RatFOV")); } }


        private static PropertyInfo[] _fieldsNames
        {
            get
            {
                return _fields ??= typeof(FOVLibrary).GetProperties();
            }
        }
        private static PropertyInfo[] _fields;


        //This method uses the actor's name to retrieve the actual property field sharing the name name in the format {name}FOV
        public static FOV GetFov(string actorName)
        {
            string statsName = $"{actorName}FOV";
            for (int i = 0; i < _fieldsNames.Length; i++)
            {
                if (statsName == _fieldsNames[i].Name)
                {
                    return (FOV)_fieldsNames[i].GetValue(_fieldsNames[i]);
                }
            }

            return null;
        }

    }
}
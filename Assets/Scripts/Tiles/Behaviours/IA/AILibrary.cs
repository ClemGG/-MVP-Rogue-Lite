using Project.Behaviours.AI;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Retrieves an instance of the ai to avoid modifying the original.
/// This is where we also set random numbers to those stats for more variety between similar Actors.
/// This allows us to avoid randomizing them in the TileLibrary class instead.
/// </summary>
public static class AILibrary
{

    //TODO : Use Object Pooling instead of those Instantiate() to recycle the Tiles instead of wasting memory on recreating them.
    //Creates a new EnemyAI for the Player if it doesn't already have one.
    public static EnemyAI RatAI { get { return Object.Instantiate(Resources.Load<EnemyAI>("Behaviours/AI/RatAI")); } }
    

    private static PropertyInfo[] _fieldsNames
    {
        get
        {
            return _fields ??= typeof(AILibrary).GetProperties();
        }
    }
    private static PropertyInfo[] _fields;


    //This method uses the actor's name to retrieve the actual property field sharing the name name in the format {name}Stats
    public static EnemyAI GetAI(string actorName)
    {
        string statsName = $"{actorName}AI";
        for (int i = 0; i < _fieldsNames.Length; i++)
        {
            if (statsName == _fieldsNames[i].Name)
            {
                return (EnemyAI)_fieldsNames[i].GetValue(_fieldsNames[i]);
            }
        }

        return null;
    }
}

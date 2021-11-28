using UnityEngine;

namespace Project.Combat
{
    [CreateAssetMenu(fileName = "New Stats", menuName = "Rogue/Actors/Combat/Stats")]
    public class ActorStats : ScriptableObject
    {
        #region Private Fields

        [SerializeField] private int _attack;
        [SerializeField] private int _attackChance;
        [SerializeField] private int _awareness;
        [SerializeField] private int _defense;
        [SerializeField] private int _defenseChance;
        [SerializeField] private int _gold;
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] int _speed; //How fast the actor is. This determines the rate at which they perform actions.
                            //A lower number is faster. An actor with a speed of 10 will perform twice as many actions in the same time as an actor with a speed of 20.
        #endregion


        #region Accessors

        /* We won’t be doing auto-properties this time because later we’ll want to update the getters 
         * on the each property to account for additional stats from items that we equip. 
         * For instance the Defense stat might have a base value of 2, 
         * but then have an additional +2 bonus from armor that is being worn. 
         */

        public int Attack
        {
            get
            {
                return _attack;
            }
            set
            {
                _attack = value;
            }
        }

        public int AttackChance
        {
            get
            {
                return _attackChance;
            }
            set
            {
                _attackChance = value;
            }
        }

        public int Awareness
        {
            get
            {
                return _awareness;
            }
            set
            {
                _awareness = value;
            }
        }

        public int Defense
        {
            get
            {
                return _defense;
            }
            set
            {
                _defense = value;
            }
        }

        public int DefenseChance
        {
            get
            {
                return _defenseChance;
            }
            set
            {
                _defenseChance = value;
            }
        }

        public int Gold
        {
            get
            {
                return _gold;
            }
            set
            {
                _gold = value;
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return _maxHealth;
            }
            set
            {
                _maxHealth = value;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        #endregion
    }
}
using TMPro;
using UnityEngine;
using Project.Combat;
using System;
using Project.Colors;

namespace Project.Display
{
    //Class in charge of displaying the player's stats on the UI
    public static class PlayerLog
    {
        #region Private Fields

        private static TextMeshProUGUI _nameTextField;
        private static TextMeshProUGUI _healthTextField;
        private static TextMeshProUGUI _atkTextField;
        private static TextMeshProUGUI _defTextField;
        private static TextMeshProUGUI _goldTextField;

        #endregion

        #region Accessors

        //These retrieve the Text Fields automatically
        private static TextMeshProUGUI _nameField
        {
            get
            {
                return _nameTextField ??= GameObject.Find("name").GetComponent<TextMeshProUGUI>();
            }
        }

        private static TextMeshProUGUI __healthField
        {
            get
            {
                return _healthTextField ??= GameObject.Find("health").GetComponent<TextMeshProUGUI>();
            }
        }

        private static TextMeshProUGUI _atkField
        {
            get
            {
                return _atkTextField ??= GameObject.Find("atk").GetComponent<TextMeshProUGUI>();
            }
        }

        private static TextMeshProUGUI _defField
        {
            get
            {
                return _defTextField ??= GameObject.Find("def").GetComponent<TextMeshProUGUI>();
            }
        }

        private static TextMeshProUGUI _goldField
        {
            get
            {
                if (!_goldTextField)
                {
                    _goldTextField = GameObject.Find("gold").GetComponent<TextMeshProUGUI>();
                    _goldTextField.color = ColorLibrary.Gold;
                }
                return _goldTextField;
            }
        }

        #endregion

        #region Public Methods

        //Each textField has its own public method in oreder to avoid redrawing the entire layout

        public static void DisplayPlayerStats(string playerName, ActorStats stats)
        {
            _nameField.text = $"Name : {playerName}";

            DisplayHealth(stats.Health, stats.MaxHealth);
            DisplayAtk(stats.Attack, stats.AttackChance);
            DisplayDef(stats.Defense, stats.DefenseChance);
            DisplayGold(stats.Gold);
        }

        public static void DisplayHealth(int health, int maxHealth)
        {
            __healthField.text = $"Health : {health}/{maxHealth}";
        }

        public static void DisplayAtk(int attack, int attackChance)
        {
            _atkField.text = $"Atk : {attack} ({attackChance}%)";
        }

        public static void DisplayDef(int defense, int defenseChance)
        {
            _defField.text = $"Def : {defense} ({defenseChance}%)";

        }

        public static void DisplayGold(int gold)
        {
            _goldField.text = $"Gold : {gold}";
        }

        #endregion
    }
}
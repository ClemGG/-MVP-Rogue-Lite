using Project.Colors;
using Project.Logic;
using Project.Tiles;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Display
{
    public static class InspectorLog
    {
        #region Private Accessors

        private static RectTransform HealthBarsParent
        {
            get
            {
                return _p = _p != null ? _p : GameObject.Find("content").GetComponent<RectTransform>();
            }
        }
        private static RectTransform _p;

        private static TextMeshProUGUI DescriptionText
        {
            get
            {
                return _dt = _dt != null ? _dt : GameObject.Find("examine description").GetComponent<TextMeshProUGUI>();
            }
        }
        private static TextMeshProUGUI _dt;

        private static GameObject[] HealthBarsInUI 
        {
            get
            {
                if(_hbs == null)
                {
                    _hbs = new GameObject[GameSystem.c_MaxHealthBars];
                    for (int i = 0; i < GameSystem.c_MaxHealthBars; i++)
                    {
                        _hbs[i] = ObjectPooler.instance.SpawnFromPool("healthbar", Vector3.zero, Quaternion.identity, HealthBarsParent);
                        _hbs[i].SetActive(false);
                    }
                }

                return _hbs;
            }
        }
        private static GameObject[] _hbs;

        private static int _nbVisibleActors = 0;

        #endregion



        #region Public Methods



        internal static void ClearDescription()
        {
            DescriptionText.text = "";
        }

        internal static void ClearHealthbarsList()
        {
            //Hide all healthbars aat the start of a turn before only showing the ones we need
            if (_nbVisibleActors > 0)
            {
                _nbVisibleActors = 0;
                for (int i = 0; i < HealthBarsInUI.Length; i++)
                {
                    HealthBarsInUI[i].SetActive(false);
                }
            }
        }

        /// <summary>
        /// Used to describe the Tile currently hovered during the Examine Mode, as well as its eventual status effects if it is an Actor.
        /// </summary>
        internal static void DisplayTileDescription(Tile tile)
        {
            
            DescriptionText.text = $"<b>{ColorLibrary.ColoredText(tile.TileName, tile.TextColorInFOV.Color)}</b>\n\n{tile.Description}";
        }


        //What we want is to reset all the healthbars before relinking them to all visible actors and setting once their Health value
        //before doing it again on the next turn.
        //Maybe not the most performance or memory friendly, but it works well and we can easily change it later if we want.
        internal static void DisplayHealth(ActorTile actor)
        {
            _nbVisibleActors++;
            if (_nbVisibleActors >= GameSystem.c_MaxHealthBars) return;

            //Get the corresponding healthbar and set its text and fillAmount to match the actor's name and health
            Transform healthBar = HealthBarsInUI[_nbVisibleActors].transform;
            healthBar.gameObject.SetActive(true);

            Image fill = healthBar.GetChild(1).GetComponent<Image>();
            TextMeshProUGUI symbol = healthBar.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nameField = healthBar.GetChild(3).GetComponent<TextMeshProUGUI>();

            fill.fillAmount = (float)actor.Stats.Health / (float)actor.Stats.MaxHealth;
            symbol.text = actor.Symbol.ToString();
            nameField.text = actor.TileName;

        }

        #endregion
    }
}
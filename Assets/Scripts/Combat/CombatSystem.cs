using Project.Display;
using Project.Generation;
using Project.Logic;
using Project.DiceRandom;
using Project.Tiles;
using System.Text;
using UnityEngine;

/// <summary>
/// Handles all the combat calculations between two Actors.
/// </summary>
public static class CombatSystem
{
    #region Fields

    private static StringBuilder _atkSB { get; } = new StringBuilder(100);
    private static StringBuilder _defSB { get; } = new StringBuilder(100);

    #endregion


    internal static void ConductAttack(ActorTile attacker, ActorTile defender)
    {
        _atkSB.Clear();
        _defSB.Clear();

        int hits = ResolveAttack(attacker, defender, _atkSB);

        int blocks = ResolveDefense(defender, hits, _atkSB, _defSB);

        MessageLog.Print(_atkSB.ToString());
        if (!string.IsNullOrWhiteSpace(_defSB.ToString()))
        {
            MessageLog.Print(_defSB.ToString());
        }

        int damage = hits - blocks;

        ResolveDamage(attacker, defender, damage);
    }

    // The attacker rolls based on his stats to see if he gets any hits
    private static int ResolveAttack(ActorTile attacker, ActorTile defender, StringBuilder attackMessage)
    {
        int hits = 0;

        //attackMessage.AppendFormat("{0} attacks {1} and rolls: ", attacker.TileName, defender.TileName);
        attackMessage.AppendFormat("The {0} attacks {1}, ", attacker.TileName, defender.TileName);

        // Roll a number of 100-sided dice equal to the Attack value of the attacking actor
        int[] results = new int[attacker.Stats.Attack];
        Dice.Roll(attacker.Stats.Attack, 100, results);

        // Look at the face value of each single die that was rolled
        foreach (int result in results)
        {
            //attackMessage.Append(result + ", ");
            // Compare the value to 100 minus the attack chance and add a hit if it's greater
            if (result >= 100 - attacker.Stats.AttackChance)
            {
                hits++;
            }
        }

        return hits;
    }

    // The defender rolls based on his stats to see if he blocks any of the hits from the attacker
    private static int ResolveDefense(ActorTile defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
    {
        int blocks = 0;

        if (hits > 0)
        {
            attackMessage.AppendFormat("scoring {0} hits. ", hits);
            //defenseMessage.AppendFormat("  {0} defends and rolls: ", defender.TileName);
            defenseMessage.AppendFormat("The {0} defends, ", defender.TileName);

            // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
            int[] results = new int[defender.Stats.Defense];
            Dice.Roll(defender.Stats.Defense, 100, results);

            // Look at the face value of each single die that was rolled
            foreach (int result in results)
            {
                //defenseMessage.Append(result + ", ");
                // Compare the value to 100 minus the defense chance and add a block if it's greater
                if (result >= 100 - defender.Stats.DefenseChance)
                {
                    blocks++;
                }
            }
            defenseMessage.AppendFormat("resulting in {0} blocks. ", blocks);
        }
        else
        {
            attackMessage.Append("and misses completely. ");
        }

        return blocks;
    }

    // Apply any damage that wasn't blocked to the defender
    private static void ResolveDamage(ActorTile attacker, ActorTile defender, int damage)
    {
        if (damage > 0)
        {
            defender.Stats.Health = Mathf.Clamp(defender.Stats.Health - damage, 0, defender.Stats.MaxHealth);

            MessageLog.Print($"  The {defender.TileName} was hit for {damage} damage.");

            if (defender.Stats.Health <= 0)
            {
                ResolveDeath(attacker, defender);
            }
        }
        else
        {
            MessageLog.Print($"  The {defender.TileName} blocked all damage.");
        }
    }

    // Remove the defender from the map and add some messages upon death.
    private static void ResolveDeath(ActorTile attacker, ActorTile defender)
    {
        if (defender is PlayerTile)
        {
            GameSystem.s_IsGameOver = true;
            MessageLog.Print("  You died. GameOver.");
        }
        else
        {
            DungeonInfo.RemoveActor(defender);
            MessageLog.Print($"  The {defender.TileName} died and dropped {defender.Stats.Gold} gold.");

            if(attacker is PlayerTile)
            {
                attacker.Stats.Gold += defender.Stats.Gold;
            }
        }
    }
}

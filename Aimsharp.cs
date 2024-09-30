using System;
using System.Collections.Generic;
using System.Drawing;
using AS = AimsharpWow.API.Aimsharp;

namespace SafeSharp
{
    /// <summary>
    /// Provides exception-safe wrappers around the Aimsharp API methods.
    /// </summary>
    public static class Aimsharp
    {
        private static readonly int _defaultInt = -1;
        private static readonly float _defaultFloat = -1f;
        private static readonly bool _defaultBool = false;

        /// <summary>
        /// Executes a function with exception handling.
        /// </summary>
        /// <typeparam name="T">The return type of the function.</typeparam>
        /// <param name="func">The function to execute.</param>
        /// <param name="errorMessage">The error message to log in case of an exception.</param>
        /// <param name="defaultValue">The default value to return if an exception occurs.</param>
        /// <returns>The result of the function, or <paramref name="defaultValue"/> if an exception occurs.</returns>
        private static T ExecuteWithExceptionHandling<T>(Func<T> func, string errorMessage, T defaultValue = default(T))
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Logging.LogError(errorMessage);
                Logging.LogError(ex.ToString());
                return defaultValue;
            }
        }

        /// <summary>
        /// Executes an action with exception handling.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="errorMessage">The error message to log in case of an exception.</param>
        private static void ExecuteWithExceptionHandling(Action action, string errorMessage)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logging.LogError(errorMessage);
                Logging.LogError(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if a spell can be cast on a specified unit.
        /// </summary>
        /// <param name="spellName">The name of the spell to check.</param>
        /// <param name="unit">The unit on which to check the spell. Defaults to "target".</param>
        /// <param name="checkRange">Specifies whether to check the range of the spell. Defaults to true.</param>
        /// <param name="checkCasting">Specifies whether to check if the player is currently casting. Defaults to false.</param>
        /// <returns><c>true</c> if the spell can be cast on the unit; otherwise, <c>false</c>.</returns>
        public static bool CanCast(string spellName, string unit = "target", bool checkRange = true, bool checkCasting = false)
        {
            return ExecuteWithExceptionHandling(
                () => AS.CanCast(spellName, unit, checkRange, checkCasting),
                $"Failed to check if '{spellName}' can be cast on '{unit}'.",
                _defaultBool);
        }

        /// <summary>
        /// Prints a message to the bot's console box.
        /// Should only be used in Initialize or for debugging since printing every tick can be slow.
        /// </summary>
        /// <param name="text">The text to print.</param>
        /// <param name="color">The color of the text.</param>
        public static void PrintMessage(string text, Color color)
        {
            ExecuteWithExceptionHandling(
                () => AS.PrintMessage(text, color),
                $"Failed to print message: '{text}'.");
        }

        /// <summary>
        /// Tries to cast the specified spell from the Spellbook or macro from the Macro list.
        /// </summary>
        /// <param name="name">The name of the spell or macro to cast.</param>
        /// <param name="quickDelay">If true, the bot will spam the key faster instead of waiting for the normal key delay.</param>
        public static void Cast(string name, bool quickDelay = false)
        {
            ExecuteWithExceptionHandling(
                () => AS.Cast(name, quickDelay),
                $"Failed to cast '{name}' with QuickDelay: {quickDelay}.");
        }

        /// <summary>
        /// Returns the cooldown remaining of an ability in milliseconds, including any GCD or interrupted time.
        /// </summary>
        /// <param name="spell">The name of the spell.</param>
        /// <returns>The cooldown remaining in milliseconds, or _defaultInt if an error occurs.</returns>
        public static int SpellCooldown(string spell)
        {
            return ExecuteWithExceptionHandling(
                () => AS.SpellCooldown(spell),
                $"Failed to get cooldown for spell: '{spell}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the number of current charges of an ability that has charges.
        /// Always 0 if the ability does not use charges.
        /// </summary>
        /// <param name="spell">The name of the spell.</param>
        /// <returns>The number of current charges, or _defaultInt if an error occurs.</returns>
        public static int SpellCharges(string spell)
        {
            return ExecuteWithExceptionHandling(
                () => AS.SpellCharges(spell),
                $"Failed to get charges for spell: '{spell}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the maximum number of charges an ability can have.
        /// Always 0 if the ability does not use charges.
        /// </summary>
        /// <param name="spell">The name of the spell.</param>
        /// <returns>The maximum number of charges, or _defaultInt if an error occurs.</returns>
        public static int MaxCharges(string spell)
        {
            return ExecuteWithExceptionHandling(
                () => AS.MaxCharges(spell),
                $"Failed to get max charges for spell: '{spell}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the time remaining for an ability to gain another charge.
        /// Always 0 if the ability does not use charges.
        /// </summary>
        /// <param name="spell">The name of the spell.</param>
        /// <returns>The recharge time in milliseconds, or _defaultInt if an error occurs.</returns>
        public static int RechargeTime(string spell)
        {
            return ExecuteWithExceptionHandling(
                () => AS.RechargeTime(spell),
                $"Failed to get recharge time for spell: '{spell}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns false if the spell is active and the cooldown will begin as soon as the spell is used or cancelled; otherwise, true.
        /// </summary>
        /// <param name="spell">The name of the spell.</param>
        /// <returns><c>true</c> if the spell is enabled; otherwise, <c>false</c>.</returns>
        public static bool SpellEnabled(string spell)
        {
            return ExecuteWithExceptionHandling(
                () => AS.SpellEnabled(spell),
                $"Failed to check if spell '{spell}' is enabled.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the unit is in range of the ability; otherwise, false.
        /// </summary>
        /// <param name="spell">The name of the spell.</param>
        /// <param name="unit">The unit to check.</param>
        /// <returns><c>true</c> if the unit is in range; otherwise, <c>false</c>.</returns>
        public static bool SpellInRange(string spell, string unit)
        {
            return ExecuteWithExceptionHandling(
                () => AS.SpellInRange(spell, unit),
                $"Failed to check if spell '{spell}' is in range for unit '{unit}'.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the current UI map ID for the player.
        /// </summary>
        /// <returns>The UI map ID, or _defaultInt if an error occurs.</returns>
        public static int GetMapID()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetMapID(),
                "Failed to get map ID.",
                _defaultInt);
        }

        /// <summary>
        /// Returns true if the player is PvP enabled.
        /// </summary>
        /// <returns><c>true</c> if the player is PvP enabled; otherwise, <c>false</c>.</returns>
        public static bool PlayerIsPvP()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerIsPvP(),
                "Failed to check if player is PvP enabled.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player is dead or a ghost.
        /// </summary>
        /// <returns><c>true</c> if the player is dead; otherwise, <c>false</c>.</returns>
        public static bool PlayerIsDead()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerIsDead(),
                "Failed to check if player is dead.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player is mounted.
        /// </summary>
        /// <returns><c>true</c> if the player is mounted; otherwise, <c>false</c>.</returns>
        public static bool PlayerIsMounted()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerIsMounted(),
                "Failed to check if player is mounted.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player has a pet.
        /// </summary>
        /// <returns><c>true</c> if the player has a pet; otherwise, <c>false</c>.</returns>
        public static bool PlayerHasPet()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerHasPet(),
                "Failed to check if player has a pet.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player is in a vehicle.
        /// </summary>
        /// <returns><c>true</c> if the player is in a vehicle; otherwise, <c>false</c>.</returns>
        public static bool PlayerInVehicle()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerInVehicle(),
                "Failed to check if player is in a vehicle.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player is moving.
        /// </summary>
        /// <returns><c>true</c> if the player is moving; otherwise, <c>false</c>.</returns>
        public static bool PlayerIsMoving()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerIsMoving(),
                "Failed to check if player is moving.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player is outdoors.
        /// </summary>
        /// <returns><c>true</c> if the player is outdoors; otherwise, <c>false</c>.</returns>
        public static bool PlayerIsOutdoors()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerIsOutdoors(),
                "Failed to check if player is outdoors.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player is in a raid.
        /// </summary>
        /// <returns><c>true</c> if the player is in a raid; otherwise, <c>false</c>.</returns>
        public static bool InRaid()
        {
            return ExecuteWithExceptionHandling(
                () => AS.InRaid(),
                "Failed to check if player is in a raid.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the player is in a party or group.
        /// </summary>
        /// <returns><c>true</c> if the player is in a party; otherwise, <c>false</c>.</returns>
        public static bool InParty()
        {
            return ExecuteWithExceptionHandling(
                () => AS.InParty(),
                "Failed to check if player is in a party.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the target is hostile to the player.
        /// </summary>
        /// <returns><c>true</c> if the target is an enemy; otherwise, <c>false</c>.</returns>
        public static bool TargetIsEnemy()
        {
            return ExecuteWithExceptionHandling(
                () => AS.TargetIsEnemy(),
                "Failed to check if target is an enemy.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the player's level.
        /// </summary>
        /// <returns>The player's level, or _defaultInt if an error occurs.</returns>
        public static int GetPlayerLevel()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetPlayerLevel(),
                "Failed to get player level.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the player's race.
        /// </summary>
        /// <returns>The player's race, or an empty string if an error occurs.</returns>
        public static string GetPlayerRace()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetPlayerRace(),
                "Failed to get player race.",
                string.Empty);
        }

        /// <summary>
        /// Returns true if the specified unit is in combat.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <returns><c>true</c> if the unit is in combat; otherwise, <c>false</c>.</returns>
        public static bool InCombat(string unit = "player")
        {
            return ExecuteWithExceptionHandling(
                () => AS.InCombat(unit),
                $"Failed to check if '{unit}' is in combat.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the specialization of the specified unit.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <returns>The specialization, or an empty string if an error occurs.</returns>
        public static string GetSpec(string unit = "player")
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetSpec(unit),
                $"Failed to get specialization for unit '{unit}'.",
                string.Empty);
        }

        /// <summary>
        /// Returns the health percentage of the specified unit.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <returns>The health percentage, or _defaultInt if an error occurs.</returns>
        public static int Health(string unit = "player")
        {
            return ExecuteWithExceptionHandling(
                () => AS.Health(unit),
                $"Failed to get health for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the power (rage, energy, etc.) of the specified unit.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <returns>The power value, or _defaultInt if an error occurs.</returns>
        public static int Power(string unit = "player")
        {
            return ExecuteWithExceptionHandling(
                () => AS.Power(unit),
                $"Failed to get power for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the target's current HP in thousands (not percentage).
        /// </summary>
        /// <returns>The target's current HP in thousands, or _defaultInt if an error occurs.</returns>
        public static int TargetCurrentHP()
        {
            return ExecuteWithExceptionHandling(
                () => AS.TargetCurrentHP(),
                "Failed to get target's current HP.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the target's maximum HP in thousands (not percentage).
        /// </summary>
        /// <returns>The target's maximum HP in thousands, or _defaultInt if an error occurs.</returns>
        public static int TargetMaxHP()
        {
            return ExecuteWithExceptionHandling(
                () => AS.TargetMaxHP(),
                "Failed to get target's max HP.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the player's current secondary power (e.g., combo points, chi).
        /// </summary>
        /// <returns>The secondary power value, or _defaultInt if an error occurs.</returns>
        public static int PlayerSecondaryPower()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerSecondaryPower(),
                "Failed to get player's secondary power.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the number of hostile enemies in melee range.
        /// </summary>
        /// <returns>The number of enemies in melee range, or _defaultInt if an error occurs.</returns>
        public static int EnemiesInMelee()
        {
            return ExecuteWithExceptionHandling(
                () => AS.EnemiesInMelee(),
                "Failed to get enemies in melee range.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the player's maximum power (max energy, max rage, etc.).
        /// </summary>
        /// <returns>The maximum power value, or _defaultInt if an error occurs.</returns>
        public static int PlayerMaxPower()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PlayerMaxPower(),
                "Failed to get player's max power.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the mana percentage of the specified unit.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <returns>The mana percentage, or _defaultInt if an error occurs.</returns>
        public static int Mana(string unit = "player")
        {
            return ExecuteWithExceptionHandling(
                () => AS.Mana(unit),
                $"Failed to get mana for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the range to the specified unit.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        /// <returns>The range in yards, or _defaultInt if an error occurs.</returns>
        public static int Range(string unit = "target")
        {
            return ExecuteWithExceptionHandling(
                () => AS.Range(unit),
                $"Failed to get range to unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the number of players including the player in the current group or raid.
        /// </summary>
        /// <returns>The group size, or _defaultInt if an error occurs.</returns>
        public static int GroupSize()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GroupSize(),
                "Failed to get group size.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the name of the last successfully casted ability by the player if it is in the bot's Spellbook.
        /// </summary>
        /// <returns>The name of the last casted spell, or an empty string if an error occurs.</returns>
        public static string LastCast()
        {
            return ExecuteWithExceptionHandling(
                () => AS.LastCast(),
                "Failed to get last casted spell.",
                string.Empty);
        }

        /// <summary>
        /// Returns the name of the last successfully casted ability by any hostile enemies if it is in the bot's EnemySpells list.
        /// </summary>
        /// <returns>The name of the enemy's last casted spell, or an empty string if an error occurs.</returns>
        public static string EnemySpellCast()
        {
            return ExecuteWithExceptionHandling(
                () => AS.EnemySpellCast(),
                "Failed to get enemy's last casted spell.",
                string.Empty);
        }

        /// <summary>
        /// Returns the WoW spell ID the unit is currently casting or channeling.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        /// <returns>The spell ID, or _defaultInt if an error occurs.</returns>
        public static int CastingID(string unit = "target")
        {
            return ExecuteWithExceptionHandling(
                () => AS.CastingID(unit),
                $"Failed to get casting ID for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns true if the unit is currently interruptible.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        /// <returns><c>true</c> if the unit is interruptible; otherwise, <c>false</c>.</returns>
        public static bool IsInterruptable(string unit = "target")
        {
            return ExecuteWithExceptionHandling(
                () => AS.IsInterruptable(unit),
                $"Failed to check if unit '{unit}' is interruptible.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the unit is currently channeling.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        public static bool IsChanneling(string unit = "target")
        {
            return ExecuteWithExceptionHandling(
                () => AS.IsChanneling(unit),
                $"Failed to check if unit '{unit}' is channeling.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the elapsed time in milliseconds of the unit's current spell cast or channel.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        public static int CastingElapsed(string unit = "target")
        {
            return ExecuteWithExceptionHandling(
                () => AS.CastingElapsed(unit),
                $"Failed to get casting elapsed time for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the remaining time in milliseconds of the unit's current spell cast or channel.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        public static int CastingRemaining(string unit = "target")
        {
            return ExecuteWithExceptionHandling(
                () => AS.CastingRemaining(unit),
                $"Failed to get casting remaining time for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the player's haste percentage.
        /// </summary>
        public static float Haste()
        {
            return ExecuteWithExceptionHandling(
                () => AS.Haste(),
                "Failed to get player's haste.",
                _defaultFloat);
        }

        /// <summary>
        /// Returns the player's critical strike chance percentage.
        /// </summary>
        public static float Crit()
        {
            return ExecuteWithExceptionHandling(
                () => AS.Crit(),
                "Failed to get player's critical strike chance.",
                _defaultFloat);
        }

        /// <summary>
        /// Returns true if the last spell cast by the player got a line of sight error.
        /// </summary>
        public static bool LineOfSighted()
        {
            return ExecuteWithExceptionHandling(
                () => AS.LineOfSighted(),
                "Failed to check if last spell cast had line of sight error.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the last spell cast by the player got a not facing error.
        /// </summary>
        public static bool NotFacing()
        {
            return ExecuteWithExceptionHandling(
                () => AS.NotFacing(),
                "Failed to check if last spell cast had not facing error.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the remaining time in milliseconds of the player's current active global cooldown.
        /// </summary>
        public static int GCD()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GCD(),
                "Failed to get global cooldown.",
                _defaultInt);
        }

        /// <summary>
        /// Returns true if the unit has the specified buff.
        /// </summary>
        /// <param name="buffName">The name of the buff.</param>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <param name="byPlayer">If true, only buffs applied by the player are considered.</param>
        /// <param name="type">Optional type to filter buffs (e.g., "magic", "disease").</param>
        public static bool HasBuff(string buffName, string unit = "player", bool byPlayer = true, string type = "")
        {
            return ExecuteWithExceptionHandling(
                () => AS.HasBuff(buffName, unit, byPlayer, type),
                $"Failed to check if unit '{unit}' has buff '{buffName}'.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the number of stacks of a buff the unit has.
        /// </summary>
        /// <param name="buffName">The name of the buff.</param>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <param name="byPlayer">If true, only buffs applied by the player are considered.</param>
        public static int BuffStacks(string buffName, string unit = "player", bool byPlayer = true)
        {
            return ExecuteWithExceptionHandling(
                () => AS.BuffStacks(buffName, unit, byPlayer),
                $"Failed to get buff stacks for '{buffName}' on unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the remaining duration in milliseconds of a buff on a unit.
        /// </summary>
        /// <param name="buffName">The name of the buff.</param>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        /// <param name="byPlayer">If true, only buffs applied by the player are considered.</param>
        /// <param name="type">Optional type to filter buffs.</param>
        public static int BuffRemaining(string buffName, string unit = "player", bool byPlayer = true, string type = "")
        {
            return ExecuteWithExceptionHandling(
                () => AS.BuffRemaining(buffName, unit, byPlayer, type),
                $"Failed to get buff remaining time for '{buffName}' on unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns true if the unit has the specified debuff.
        /// </summary>
        /// <param name="debuffName">The name of the debuff.</param>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        /// <param name="byPlayer">If true, only debuffs applied by the player are considered.</param>
        /// <param name="type">Optional type to filter debuffs.</param>
        public static bool HasDebuff(string debuffName, string unit = "target", bool byPlayer = true, string type = "")
        {
            return ExecuteWithExceptionHandling(
                () => AS.HasDebuff(debuffName, unit, byPlayer, type),
                $"Failed to check if unit '{unit}' has debuff '{debuffName}'.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the number of stacks of a debuff the unit has.
        /// </summary>
        /// <param name="debuffName">The name of the debuff.</param>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        /// <param name="byPlayer">If true, only debuffs applied by the player are considered.</param>
        public static int DebuffStacks(string debuffName, string unit = "target", bool byPlayer = true)
        {
            return ExecuteWithExceptionHandling(
                () => AS.DebuffStacks(debuffName, unit, byPlayer),
                $"Failed to get debuff stacks for '{debuffName}' on unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the remaining duration in milliseconds of a debuff on a unit.
        /// </summary>
        /// <param name="debuffName">The name of the debuff.</param>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        /// <param name="byPlayer">If true, only debuffs applied by the player are considered.</param>
        /// <param name="type">Optional type to filter debuffs.</param>
        public static int DebuffRemaining(string debuffName, string unit = "target", bool byPlayer = true, string type = "")
        {
            return ExecuteWithExceptionHandling(
                () => AS.DebuffRemaining(debuffName, unit, byPlayer, type),
                $"Failed to get debuff remaining time for '{debuffName}' on unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns true if the specified talent is selected.
        /// </summary>
        /// <param name="talentId">The talent ID to check.</param>
        public static bool Talent(int talentId)
        {
            return ExecuteWithExceptionHandling(
                () => AS.Talent(talentId),
                $"Failed to check if talent ID '{talentId}' is selected.",
                _defaultBool);
        }

        /// <summary>
        /// Returns a list of selected PvP talent IDs.
        /// </summary>
        public static List<int> PvpTalentIDs()
        {
            return ExecuteWithExceptionHandling(
                () => AS.PvpTalentIDs(),
                "Failed to get PvP talent IDs.",
                new List<int>());
        }

        /// <summary>
        /// Returns true if the item is ready to be used.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="checkIfEquipped">If true, checks if the item is equipped.</param>
        public static bool CanUseItem(string itemName, bool checkIfEquipped = true)
        {
            return ExecuteWithExceptionHandling(
                () => AS.CanUseItem(itemName, checkIfEquipped),
                $"Failed to check if item '{itemName}' can be used.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the item's cooldown remaining in milliseconds.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        public static int ItemCooldown(string itemName)
        {
            return ExecuteWithExceptionHandling(
                () => AS.ItemCooldown(itemName),
                $"Failed to get cooldown for item '{itemName}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns true if the item is equipped.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        public static bool IsEquipped(string itemName)
        {
            return ExecuteWithExceptionHandling(
                () => AS.IsEquipped(itemName),
                $"Failed to check if item '{itemName}' is equipped.",
                _defaultBool);
        }

        /// <summary>
        /// Targets the player.
        /// </summary>
        public static void TargetSelf()
        {
            ExecuteWithExceptionHandling(
                () => AS.TargetSelf(),
                "Failed to target self.");
        }

        /// <summary>
        /// Targets the specified party member.
        /// </summary>
        /// <param name="memberNumber">The party member number (1-4).</param>
        public static void TargetPartyMember(int memberNumber)
        {
            ExecuteWithExceptionHandling(() =>
            {
                switch (memberNumber)
                {
                    case 1:
                        AS.TargetParty1();
                        break;
                    case 2:
                        AS.TargetParty2();
                        break;
                    case 3:
                        AS.TargetParty3();
                        break;
                    case 4:
                        AS.TargetParty4();
                        break;
                    default:
                        Logging.LogError("Invalid party member number.");
                        break;
                }
            },
            $"Failed to target party member {memberNumber}.");
        }

        /// <summary>
        /// Targets the specified arena opponent.
        /// </summary>
        /// <param name="arenaNumber">The arena opponent number (1-3).</param>
        public static void TargetArena(int arenaNumber)
        {
            ExecuteWithExceptionHandling(() =>
            {
                switch (arenaNumber)
                {
                    case 1:
                        AS.TargetArena1();
                        break;
                    case 2:
                        AS.TargetArena2();
                        break;
                    case 3:
                        AS.TargetArena3();
                        break;
                    default:
                        Logging.LogError("Invalid arena number.");
                        break;
                }
            },
            $"Failed to target arena {arenaNumber}.");
        }

        /// <summary>
        /// Targets the specified boss.
        /// </summary>
        /// <param name="bossNumber">The boss number (1-4).</param>
        public static void TargetBoss(int bossNumber)
        {
            ExecuteWithExceptionHandling(() =>
            {
                switch (bossNumber)
                {
                    case 1:
                        AS.TargetBoss1();
                        break;
                    case 2:
                        AS.TargetBoss2();
                        break;
                    case 3:
                        AS.TargetBoss3();
                        break;
                    case 4:
                        AS.TargetBoss4();
                        break;
                    default:
                        Logging.LogError("Invalid boss number.");
                        break;
                }
            },
            $"Failed to target boss {bossNumber}.");
        }

        /// <summary>
        /// Targets the specified raid member.
        /// </summary>
        /// <param name="index">The raid member index (1-40).</param>
        public static void TargetRaid(int index)
        {
            ExecuteWithExceptionHandling(
                () => AS.TargetRaid(index),
                $"Failed to target raid member {index}.");
        }

        /// <summary>
        /// Stops the current cast or channel.
        /// </summary>
        public static void StopCasting()
        {
            ExecuteWithExceptionHandling(
                () => AS.StopCasting(),
                "Failed to stop casting.");
        }

        /// <summary>
        /// Finds the max HP value (in thousands) for a unit.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        public static int UnitMaxHP(string unit = "player")
        {
            return ExecuteWithExceptionHandling(
                () => AS.UnitMaxHP(unit),
                $"Failed to get max HP for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Finds the current HP value (in thousands) for a unit.
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "player".</param>
        public static int UnitCurrentHP(string unit = "player")
        {
            return ExecuteWithExceptionHandling(
                () => AS.UnitCurrentHP(unit),
                $"Failed to get current HP for unit '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the current Dampening percentage in arena.
        /// </summary>
        public static int Dampening()
        {
            return ExecuteWithExceptionHandling(
                () => AS.Dampening(),
                "Failed to get dampening value.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the time since current combat first started in milliseconds.
        /// </summary>
        public static int CombatTime()
        {
            return ExecuteWithExceptionHandling(
                () => AS.CombatTime(),
                "Failed to get combat time.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the time in milliseconds until the rune at the specified index is ready.
        /// Only works for Death Knights.
        /// </summary>
        /// <param name="runeIndex">The rune index (1-6).</param>
        public static int RuneCooldown(int runeIndex)
        {
            return ExecuteWithExceptionHandling(
                () => AS.RuneCooldown(runeIndex),
                $"Failed to get rune cooldown for index {runeIndex}.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the time in milliseconds until the specified number of runes are ready.
        /// </summary>
        /// <param name="count">The number of runes.</param>
        public static int TimeUntilRunes(int count)
        {
            return ExecuteWithExceptionHandling(
                () => AS.TimeUntilRunes(count),
                $"Failed to get time until {count} runes are ready.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the number of enemies near the target's range (includes the target).
        /// </summary>
        public static int EnemiesNearTarget()
        {
            return ExecuteWithExceptionHandling(
                () => AS.EnemiesNearTarget(),
                "Failed to get number of enemies near target.",
                _defaultInt);
        }

        /// <summary>
        /// Returns true if the equipped trinket is ready to be used.
        /// </summary>
        /// <param name="slot">The trinket slot (0 for top slot, 1 for bottom slot).</param>
        public static bool CanUseTrinket(int slot)
        {
            return ExecuteWithExceptionHandling(
                () => AS.CanUseTrinket(slot),
                $"Failed to check if trinket in slot {slot} can be used.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the diminishing returns level of a CC category on an enemy unit.
        /// </summary>
        /// <param name="unit">The enemy unit (e.g., "arena1").</param>
        /// <param name="category">The CC category (e.g., "Stuns").</param>
        public static int EnemyDR(string unit, string category)
        {
            return ExecuteWithExceptionHandling(
                () => AS.EnemyDR(unit, category),
                $"Failed to get diminishing returns for unit '{unit}' and category '{category}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the time remaining in milliseconds until the arena unit can use their interrupt ability.
        /// </summary>
        /// <param name="arenaNumber">The arena unit number (1-3).</param>
        public static int ArenaKickTimer(int arenaNumber)
        {
            return ExecuteWithExceptionHandling(
                () => AS.ArenaKickTimer(arenaNumber),
                $"Failed to get kick timer for arena {arenaNumber}.",
                _defaultInt);
        }

        /// <summary>
        /// Sends the space key to make the character jump.
        /// </summary>
        public static void Jump()
        {
            ExecuteWithExceptionHandling(
                () => AS.Jump(),
                "Failed to make character jump.");
        }

        /// <summary>
        /// Returns true if the target is a boss unit.
        /// </summary>
        public static bool TargetIsBoss()
        {
            return ExecuteWithExceptionHandling(
                () => AS.TargetIsBoss(),
                "Failed to check if target is a boss.",
                _defaultBool);
        }

        /// <summary>
        /// Returns true if the target is the specified unit.
        /// </summary>
        /// <param name="unit">The unit to compare with the target.</param>
        public static bool TargetIsUnit(string unit)
        {
            return ExecuteWithExceptionHandling(
                () => AS.TargetIsUnit(unit),
                $"Failed to check if target is unit '{unit}'.",
                _defaultBool);
        }

        /// <summary>
        /// Returns the target's exact current HP.
        /// </summary>
        public static int TargetExactCurrentHP()
        {
            return ExecuteWithExceptionHandling(
                () => AS.TargetExactCurrentHP(),
                "Failed to get target's exact current HP.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the target's exact maximum HP.
        /// </summary>
        public static int TargetExactMaxHP()
        {
            return ExecuteWithExceptionHandling(
                () => AS.TargetExactMaxHP(),
                "Failed to get target's exact max HP.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the remaining duration of the specified totem.
        /// </summary>
        /// <param name="totemName">The name of the totem.</param>
        public static int TotemRemaining(string totemName)
        {
            return ExecuteWithExceptionHandling(
                () => AS.TotemRemaining(totemName),
                $"Failed to get remaining time for totem '{totemName}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the unit ID from UnitGUID().
        /// </summary>
        /// <param name="unit">The unit to check. Defaults to "target".</param>
        public static int UnitID(string unit = "target")
        {
            return ExecuteWithExceptionHandling(
                () => AS.UnitID(unit),
                $"Failed to get unit ID for '{unit}'.",
                _defaultInt);
        }

        /// <summary>
        /// Returns an estimate of the number of group or raid members near the target.
        /// </summary>
        public static int AlliesNearTarget()
        {
            return ExecuteWithExceptionHandling(
                () => AS.AlliesNearTarget(),
                "Failed to get number of allies near target.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the name of the currently loaded addon.
        /// </summary>
        public static string GetAddonName()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetAddonName(),
                "Failed to get addon name.",
                string.Empty);
        }

        /// <summary>
        /// Manually sends a hotkey press with or without modifiers.
        /// </summary>
        /// <param name="index">The key index (0-9 for keys 0 through 9).</param>
        /// <param name="alt">If true, the Alt modifier is used.</param>
        /// <param name="ctrl">If true, the Ctrl modifier is used.</param>
        /// <param name="shift">If true, the Shift modifier is used.</param>
        public static void SendHotkey(string index, bool alt, bool ctrl, bool shift)
        {
            ExecuteWithExceptionHandling(
                () => AS.SendHotkey(index, alt, ctrl, shift),
                $"Failed to send hotkey '{index}' with modifiers Alt: {alt}, Ctrl: {ctrl}, Shift: {shift}.");
        }

        /// <summary>
        /// Returns the current empower stage for Evoker.
        /// </summary>
        public static int GetEmpowerStage()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetEmpowerStage(),
                "Failed to get empower stage.",
                _defaultInt);
        }

        /// <summary>
        /// Returns the string for a user queued up spell using the "/xxxxx queue spellname" macro.
        /// </summary>
        public static string GetSpellQueue()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetSpellQueue(),
                "Failed to get spell queue.",
                string.Empty);
        }

        /// <summary>
        /// Returns the integer value of a defined custom WoW API Lua function.
        /// </summary>
        /// <param name="customFunctionName">The name of the custom function.</param>
        public static int CustomFunction(string customFunctionName)
        {
            return ExecuteWithExceptionHandling(
                () => AS.CustomFunction(customFunctionName),
                $"Failed to execute custom function '{customFunctionName}'.",
                _defaultInt);
        }

        /// <summary>
        /// Sets the debug mode, which will print all attempts to cast an ability onto the console.
        /// </summary>
        /// <param name="debug">If true, debug mode is enabled.</param>
        public static void DebugMode(bool debug = true)
        {
            ExecuteWithExceptionHandling(
                () => AS.DebugMode(debug),
                $"Failed to set debug mode to '{debug}'.");
        }

        /// <summary>
        /// Grabs an ID used for rotation license generation.
        /// </summary>
        public static string GetAimsharpID()
        {
            return ExecuteWithExceptionHandling(
                () => AS.GetAimsharpID(),
                "Failed to get Aimsharp ID.",
                string.Empty);
        }

        /// <summary>
        /// Returns a list of detailed buff information.
        /// </summary>
        /// <param name="unit">The unit to check.</param>
        /// <param name="buffName">The name of the buff.</param>
        /// <param name="byPlayer">If true, only buffs applied by the player are considered.</param>
        public static List<Dictionary<string, int>> BuffInfoDetailed(string unit, string buffName, bool byPlayer)
        {
            return ExecuteWithExceptionHandling(
                () => AS.BuffInfoDetailed(unit, buffName, byPlayer),
                $"Failed to get detailed buff info for '{buffName}' on unit '{unit}'.",
                new List<Dictionary<string, int>>());
        }

        /// <summary>
        /// Returns a list of detailed debuff information.
        /// </summary>
        /// <param name="unit">The unit to check.</param>
        /// <param name="debuffName">The name of the debuff.</param>
        /// <param name="byPlayer">If true, only debuffs applied by the player are considered.</param>
        public static List<Dictionary<string, int>> DebuffInfoDetailed(string unit, string debuffName, bool byPlayer)
        {
            return ExecuteWithExceptionHandling(
                () => AS.DebuffInfoDetailed(unit, debuffName, byPlayer),
                $"Failed to get detailed debuff info for '{debuffName}' on unit '{unit}'.",
                new List<Dictionary<string, int>>());
        }
    }
}

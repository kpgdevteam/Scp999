using System;
using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using CustomRoleLib.API;
using LabApi.Features;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using Scp999.Features;
using Scp999.Features.Components;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999;

public class Scp999 : Plugin<Config>
{
    public override string Name => "Scp999";
    public override string Description =>
        "Adds SCP-999, the tickling monster, as a custom role with unique abilities and features.";
    public override string Author => "MedveMarci";
    public override Version Version => new(1, 2, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);

    public static Scp999 Singleton { get; private set; }
    public static ICustomRole<object> Role { get; private set; }

    private const string RoleNamespaceKey = "scp999:scp999";

    private const int WaveId = 9992;
    private const int HealId = 9993;
    private const int DanceId = 9994;

    private const float WaveCooldown = 10f;
    private const float HealCooldown = 20f;
    private const float DanceCooldown = 15f;

    // key = (settingId << 16) | playerId — tracks per-player per-ability cooldown
    private readonly Dictionary<int, float> _cooldowns = new();

    public override void Enable()
    {
        Singleton = this;
        CustomRoleManager.RegisterAllRoles(typeof(Scp999Role).Assembly);
        CustomRoleManager.TryGetRole(ObjectNamespace.Get(RoleNamespaceKey), out var role);
        Role = role;

        CustomSpawnManager.SetGroupMaxTokens(RoleNamespaceKey, 1);
        CustomSpawnManager.SetGroupTokenReset(
            RoleNamespaceKey,
            CustomSpawnManager.TokenResetType.RoundRestart
        );

        var settings = ServerSpecificSettingsSync.DefinedSettings?.ToList() ?? new List<ServerSpecificSettingBase>();
        if (!settings.Any(s => s.SettingId == WaveId))
            settings.Add(new SSKeybindSetting(WaveId, "SCP-999 Wave", KeyCode.G));
        if (!settings.Any(s => s.SettingId == HealId))
            settings.Add(new SSKeybindSetting(HealId, "SCP-999 Heal", KeyCode.F));
        if (!settings.Any(s => s.SettingId == DanceId))
            settings.Add(new SSKeybindSetting(DanceId, "SCP-999 Dance", KeyCode.T));
        ServerSpecificSettingsSync.DefinedSettings = settings.ToArray();
        ServerSpecificSettingsSync.SendToAll();

        ServerSpecificSettingsSync.ServerOnSettingValueReceived += OnSSSReceived;
    }

    public override void Disable()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= OnSSSReceived;

        var settings = ServerSpecificSettingsSync.DefinedSettings?.ToList() ?? new List<ServerSpecificSettingBase>();
        settings.RemoveAll(s => s.SettingId is WaveId or HealId or DanceId);
        ServerSpecificSettingsSync.DefinedSettings = settings.ToArray();
        ServerSpecificSettingsSync.SendToAll();

        _cooldowns.Clear();
        CustomRoleManager.UnregisterAllRoles(typeof(Scp999Role).Assembly);
        Role = null;
        Singleton = null;
    }

    private void OnSSSReceived(ReferenceHub hub, ServerSpecificSettingBase setting)
    {
        if (setting is not SSKeybindSetting { SyncIsPressed: true } keybind) return;

        var player = Player.Get(hub);
        if (player == null || Role?.Check(player) != true) return;

        switch (keybind.SettingId)
        {
            case WaveId:   TryActivate(player, WaveId,  WaveCooldown,  () => DoWave(player));  break;
            case HealId:   TryActivate(player, HealId,  HealCooldown,  () => DoHeal(player));  break;
            case DanceId:  TryActivate(player, DanceId, DanceCooldown, () => DoDance(player)); break;
        }
    }

    private void TryActivate(Player player, int abilityId, float cooldown, Action action)
    {
        var key = (abilityId << 16) | player.PlayerId;
        if (_cooldowns.TryGetValue(key, out var end) && Time.time < end) return;
        _cooldowns[key] = Time.time + cooldown;
        action();
    }

    private static void DoWave(Player player)
    {
        Scp999SchematicComponent.PlayAnimation(player, "HelloAnimation");
        player.EnableEffect<Ensnared>(duration: 5f);
    }

    private static void DoHeal(Player player)
    {
        Scp999SchematicComponent.PlayAnimation(player, "HealthAnimation");
        foreach (var target in Player.GetAll())
        {
            if (target == player || target.IsSCP) continue;
            if (Vector3.Distance(player.Position, target.Position) > Singleton.Config.Distance) continue;
            target.Heal(Singleton.Config.HealAmount);
        }
    }

    private static void DoDance(Player player)
    {
        var rand = UnityEngine.Random.Range(0, 100) + 1;
        var animName = rand switch
        {
            <= 15 => "FunAnimation1",
            <= 60 => "FunAnimation2",
            <= 90 => "FunAnimation3",
            _ => "FunAnimation4"
        };
        Scp999SchematicComponent.PlayAnimation(player, animName);
    }
}

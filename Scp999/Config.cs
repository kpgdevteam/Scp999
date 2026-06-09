using System.ComponentModel;

namespace Scp999;

public class Config
{
    [Description("Enable debug messages in the console.")]
    public bool Debug { get; set; } = false;

    [Description("Distance in which the heal ability can heal players.")]
    public float Distance { get; set; } = 3f;

    [Description("The amount of healing for the Heal Ability.")]
    public float HealAmount { get; set; } = 100f;

    [Description("The message shown when a player becomes SCP-999.")]
    public string SpawnBroadcast { get; set; } =
        "<color=#ffa500>😄 You are SCP-999 - The tickle monster! 😄\n" +
        "Heal Humans, dance and calm down SCPs in facility</color>";

    [Description("How long (seconds) the spawn broadcast is shown.")]
    public ushort SpawnBroadcastDuration { get; set; } = 10;

    [Description("A folder under ~/.config/SCPSL/LabAPI/configs/{port}/Scp999/ where all clips should be cached.")]
    public string ShortClipsPath { get; set; } = "Audio/";
}

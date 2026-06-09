using CustomAbilityLib.API;
using CustomPlayerEffects;
using CustomRoleLib.API;
using CustomRoleLib.API.DefaultComponents.Generic;
using ProjectMER.Features.Objects;
using Scp999.Features.Abilities;
using Scp999.Features.Components;
using SecretLabNAudio.Core;

namespace Scp999.Features;

public class Scp999RoleInstance : RoleInstanceBase, IInitializable
{
    public void OnInitialized()
    {
        Owner.MaxHealth = 1500f;
        Owner.Health = 1500f;

        Owner.EnableEffect<Fade>(255);
        Owner.EnableEffect<Slowness>(25);
        Owner.EnableEffect<SilentWalk>(255);
        Owner.EnableEffect<Ghostly>(255);

        Owner.SendHint(
            Scp999.Singleton.Config.SpawnBroadcast,
            Scp999.Singleton.Config.SpawnBroadcastDuration
        );

        CustomAbilityManager.TryGiveAbility<Scp999WaveAbility>(Owner);
        CustomAbilityManager.TryGiveAbility<Scp999HealAbility>(Owner);
        CustomAbilityManager.TryGiveAbility<Scp999DanceAbility>(Owner);
        CustomAbilityManager.TryGiveAbility<Scp999YippeeAbility>(Owner);
    }

    public SchematicObject Schematic { get; set; }
    public AudioPlayer Audio { get; set; }
    public AudioPlayer JumpAudio { get; set; }
}

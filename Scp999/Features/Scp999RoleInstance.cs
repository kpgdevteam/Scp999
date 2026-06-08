using CustomAbilityLib.API;
using CustomPlayerEffects;
using CustomRoleLib.API;
using CustomRoleLib.API.DefaultComponents.Generic;
using Scp999.Features.Abilities;

namespace Scp999.Features;

public class Scp999RoleInstance : RoleInstanceBase, IInitializable
{
    public void OnInitialized()
    {
        Owner.MaxHealth = 1500f;
        Owner.Health = 1500f;

        Owner.EnableEffect<Fade>(255, 0f);
        Owner.EnableEffect<Slowness>(25, 0f);
        Owner.EnableEffect<SilentWalk>(255, 0f);
        Owner.EnableEffect<Ghostly>(255, 0f);

        Owner.CustomInfo = "<color=#960018>Other Alive</color>";

        Owner.SendHint(
            Scp999.Singleton.Config.SpawnBroadcast,
            Scp999.Singleton.Config.SpawnBroadcastDuration
        );

        CustomAbilityManager.TryGiveAbility<Scp999WaveAbility>(Owner);
        CustomAbilityManager.TryGiveAbility<Scp999HealAbility>(Owner);
        CustomAbilityManager.TryGiveAbility<Scp999DanceAbility>(Owner);
    }
}

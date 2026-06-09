using CustomAbilityLib.API;
using CustomPlayerEffects;
using CustomRoleLib.API;
using LabApi.Features.Wrappers;
using Scp999.Features.Components;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class Scp999YippeeAbility : ServerSpecificSettingAbility<Scp999YippeeAbilityInstance>
{
    public override string Name => "SCP-999 Yippee";
    public override string Description => "Play the yippee sound.";
    public override string Id => "yippee";
    protected override double Cooldown => 1;
    protected override KeyCode SuggestedKey => KeyCode.F;
}

public class Scp999YippeeAbilityInstance : AbilityInstanceBase
{
    public override void Create(Player player) { }
    public override void Destroy() { }

    public override bool Execute(out string response)
    {
        response = null;
        var rand = Random.Range(0, 100) + 1;
        var clipName = rand switch
        {
            <= 60 => "yippee-tbh1",
            _ => "yippee-tbh2",
        };
        Scp999AudioComponent.PlaySound(Owner, clipName);
        return true;
    }
}

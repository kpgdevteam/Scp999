using CustomAbilityLib.API;
using CustomRoleLib.API;
using LabApi.Features.Wrappers;
using Scp999.Features.Components;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class Scp999DanceAbility : ServerSpecificSettingAbility<Scp999DanceAbilityInstance>
{
    public override string Name => "SCP-999 Dance";
    public override string Description => "Dance and spread joy!";
    public override string Id => "dance";
    protected override double Cooldown => 15;
    protected override KeyCode SuggestedKey => KeyCode.T;
}

public class Scp999DanceAbilityInstance : AbilityInstanceBase
{
    public override void Create(Player player) { }
    public override void Destroy() { }

    public override bool Execute(out string response)
    {
        response = null;
        var rand = Random.Range(0, 100) + 1;
        var animName = rand switch
        {
            <= 15 => "FunAnimation1",
            <= 60 => "FunAnimation2",
            <= 90 => "FunAnimation3",
            _ => "FunAnimation4"
        };
        var clipName = rand switch
        {
            <= 15 => "circus",
            <= 60 => "funnytoy",
            <= 90 => "funnytoy",
            _ => "uwu"
        };
        Scp999SchematicComponent.PlayAnimation(Owner, animName);
        return true;
    }
}

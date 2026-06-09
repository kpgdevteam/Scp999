using CustomAbilityLib.API;
using CustomPlayerEffects;
using CustomRoleLib.API;
using LabApi.Features.Wrappers;
using Scp999.Features.Components;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class Scp999WaveAbility : ServerSpecificSettingAbility<Scp999WaveAbilityInstance>
{
    public override string Name => "SCP-999 Wave";
    public override string Description => "Wave at nearby players, rooting yourself briefly.";
    public override string Id => "wave";
    protected override double Cooldown => 10;
    protected override KeyCode SuggestedKey => KeyCode.G;
}

public class Scp999WaveAbilityInstance : AbilityInstanceBase
{
    public override void Create(Player player) { }
    public override void Destroy() { }

    public override bool Execute(out string response)
    {
        response = null;
        var clipName = Random.Range(0, 2) == 0 ? "hello" : "hi";
        Scp999SchematicComponent.PlayAnimation(Owner, "HelloAnimation");
        Scp999AudioComponent.PlaySound(Owner, clipName);
        Owner.EnableEffect<Ensnared>(duration: 3.5f);
        return true;
    }
}

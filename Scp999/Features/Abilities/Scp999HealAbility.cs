using CustomAbilityLib.API;
using CustomRoleLib.API;
using LabApi.Features.Wrappers;
using Scp999.Features.Components;
using UnityEngine;

namespace Scp999.Features.Abilities;

public class Scp999HealAbility : ServerSpecificSettingAbility<Scp999HealAbilityInstance>
{
    public override string Name => "SCP-999 Heal";
    public override string Description => "Heal nearby humans.";
    public override string Id => "heal";
    protected override double Cooldown => 20;
    protected override KeyCode SuggestedKey => KeyCode.F;
}

public class Scp999HealAbilityInstance : AbilityInstanceBase
{
    public override void Create(Player player) { }
    public override void Destroy() { }

    public override bool Execute(out string response)
    {
        response = null;
        Scp999SchematicComponent.PlayAnimation(Owner, "HealthAnimation");
        Scp999AudioComponent.PlaySound(Owner, "health");
        foreach (var target in Player.GetAll())
        {
            if (target == Owner || target.IsSCP) continue;
            if (Vector3.Distance(Owner.Position, target.Position) > Scp999.Singleton.Config.Distance) continue;
            target.Heal(Scp999.Singleton.Config.HealAmount);
        }
        return true;
    }
}

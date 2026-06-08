using CustomRoleLib.API;
using CustomRoleLib.API.Attributes;
using CustomRoleLib.API.DefaultComponents.Generic;
using PlayerRoles;
using Scp999.Features.Components;

namespace Scp999.Features;

[CustomRole(RoleTypeId.Tutorial)]
[CustomRoleAttributeBase(typeof(InitializerComponent<Scp999RoleInstance>))]
[CustomRoleAttributeBase(typeof(Scp999BehaviorComponent))]
[CustomRoleAttributeBase(typeof(Scp999SchematicComponent))]
public class Scp999Role : CustomRoleBase<Scp999RoleInstance>
{
    public override string Name => "<color=#960018>SCP-999</color>";
    public override string Description => "SCP-999, the tickle monster! Heals and befriends everyone.";
    public override string Id => "scp999";

    public override bool NaturallySpawnable => true;
    public override float RoleSpawnWeight => 5f;
    public override float RoleNotSpawnWeight => 95f;

    public override RoleTypeId[] RoleSpawnOriginalRoleIds =>
        [RoleTypeId.FacilityGuard, RoleTypeId.Scientist, RoleTypeId.ClassD];
}

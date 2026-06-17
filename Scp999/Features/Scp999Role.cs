using CustomRoleLib;
using CustomRoleLib.API;
using CustomRoleLib.API.Attributes;
using CustomRoleLib.API.DefaultComponents;
using CustomRoleLib.API.DefaultComponents.Generic;
using PlayerRoles;
using Scp999.Features.Components;

namespace Scp999.Features;

[CustomRole(RoleTypeId.Tutorial)]
[CustomRoleAttributeBase(typeof(InitializerComponent<Scp999RoleInstance>))]
[CustomRoleAttributeBase(typeof(Scp999BehaviorComponent))]
[CustomRoleAttributeBase(typeof(Scp999SchematicComponent))]
[CustomRoleAttributeBase(typeof(Scp999AudioComponent))]
[CustomRoleAttributeBase(typeof(RoleNameDisplayComponent))]
[CustomRoleAttributeBase(typeof(RoleReceivedHintComponent))]
[CustomSpawnpointRole<Scp999RoleInstance>(RoleTypeId.ClassD)]
public class Scp999Role : CustomRoleBase<Scp999RoleInstance>
{
    public override string Name => "SCP-999";
    public override string Description => "SCP-999, the tickle monster! Heals and befriends everyone.";
    public override string Id => "scp999";

    public override bool NaturallySpawnable => true;
    public override float RoleSpawnWeight => Scp999.Singleton.Config.Scp999SpawnWeight;
    public override float RoleNotSpawnWeight => Scp999.Singleton.Config.NotSpawnWeight;

    public override RoleTypeId[] RoleSpawnOriginalRoleIds =>
        [RoleTypeId.FacilityGuard, RoleTypeId.Scientist, RoleTypeId.ClassD];
}

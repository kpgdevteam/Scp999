using System.Collections.Generic;
using CustomRoleLib.API.DefaultComponents;
using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999.Features.Components;

public class Scp999SchematicComponent : ComponentBase<Scp999RoleInstance>
{
    private const string SchematicName = "SCP999";
    private static readonly Vector3 PositionOffset = new(0f, -0.75f, 0f);

    public static readonly Dictionary<Player, SchematicObject> PlayerSchematics = new();

    private readonly Dictionary<Scp999RoleInstance, SchematicObject> _schematics = new();
    private readonly Dictionary<Scp999RoleInstance, CoroutineHandle> _coroutines = new();

    public override void OnCreatedInstance(Scp999RoleInstance instance)
    {
        var schematic = ObjectSpawner.SpawnSchematic(SchematicName, PositionOffset, Quaternion.identity);
        if (schematic == null) return;
        
        schematic.transform.SetParent(instance.Owner.GameObject!.transform, false);
        PlayerSchematics[instance.Owner] = schematic;
    }

    public override void OnDestroyedInstance(Scp999RoleInstance instance)
    {
        if (_coroutines.TryGetValue(instance, out var handle))
        {
            Timing.KillCoroutines(handle);
            _coroutines.Remove(instance);
        }

        if (_schematics.TryGetValue(instance, out var schematic))
        {
            if (schematic != null)
                NetworkServer.Destroy(schematic.gameObject);
            _schematics.Remove(instance);
        }

        PlayerSchematics.Remove(instance.Owner);
    }

    public static void PlayAnimation(Player player, string animationName)
    {
        if (PlayerSchematics.TryGetValue(player, out var schematic) && schematic != null)
            schematic.GetComponentInChildren<Animator>()?.Play(animationName);
    }

    private IEnumerator<float> FollowPlayer(Scp999RoleInstance instance)
    {
        while (_schematics.TryGetValue(instance, out var schematic) && schematic != null && instance.Owner != null)
        {
            var yaw = instance.Owner.ReferenceHub.transform.eulerAngles.y;
            schematic.transform.SetPositionAndRotation(
                instance.Owner.Position + PositionOffset,
                Quaternion.Euler(0f, yaw, 0f)
            );
            yield return Timing.WaitForOneFrame;
        }
    }
}

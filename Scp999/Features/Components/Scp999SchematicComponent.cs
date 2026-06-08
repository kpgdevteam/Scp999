using System;
using System.Collections.Generic;
using CustomRoleLib.API;
using CustomRoleLib.API.DefaultComponents;
using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999.Features.Components;

public interface ISchematic
{
    public SchematicObject Schematic { get; set; }
}

public class Scp999SchematicComponent : ComponentBase<Scp999RoleInstance>
{
    private const string SchematicName = "SCP999";
    private static readonly Vector3 PositionOffset = new(0f, -0.75f, 0f);
    private static ICustomRole<Scp999RoleInstance> Scp999Role => CustomRoleManager.TryGetRole<Scp999RoleInstance>(ObjectNamespace.Get("scp999:scp999"), out var role) ? role : throw new Exception("Could not find SCP-999 role.");

    public override void OnCreatedInstance(Scp999RoleInstance instance)
    {
        if (instance.Schematic != null)
        {
            try { instance.Schematic.Destroy(); }
            catch (NullReferenceException) {}
        }
        instance.Schematic = ObjectSpawner.SpawnSchematic(SchematicName, PositionOffset, Quaternion.identity);
        if (instance.Schematic == null) return;

        instance.Schematic.transform.SetParent(instance.Owner.GameObject!.transform, false);
    }

    public override void OnDestroyedInstance(Scp999RoleInstance instance)
    {
        if (instance.Schematic == null) return;

        try { instance.Schematic.Destroy(); }
        catch (NullReferenceException) {}
    }

    public static void PlayAnimation(Player player, string animationName)
    {
        if (!Scp999Role.Check(player, out var instanceU) || instanceU is not Scp999RoleInstance instance) return;
        if (instance.Schematic == null) return;
        instance.Schematic.GetComponentInChildren<Animator>()?.Play(animationName);
    }
}

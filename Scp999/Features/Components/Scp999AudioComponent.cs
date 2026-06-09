using System;
using System.Collections.Generic;
using CustomRoleLib.API;
using CustomRoleLib.API.DefaultComponents;
using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using SecretLabNAudio.Core;
using SecretLabNAudio.Core.Extensions;
using SecretLabNAudio.Core.FileReading;
using UnityEngine;

namespace Scp999.Features.Components;

public class Scp999AudioComponent : ComponentBase<Scp999RoleInstance>
{
    private const float Volume = 1f;
    private static readonly Vector3 PositionOffset = Vector3.zero;
    private static ICustomRole<Scp999RoleInstance> Scp999Role => CustomRoleManager.TryGetRole<Scp999RoleInstance>(ObjectNamespace.Get("scp999:scp999"), out var role) ? role : throw new Exception("Could not find SCP-999 role.");

    public override void OnCreatedInstance(Scp999RoleInstance instance)
    {
        if (instance.Audio == null)
        {
            instance.Audio = AudioPlayer.Create(
                SpeakerSettings.Default with { Volume = Volume },
                PositionOffset, instance.Owner.GameObject!.transform);
        }
        else
        {
            instance.Audio.Volume = Volume;
            instance.Audio.Speaker.Transform.SetParent(instance.Owner.GameObject!.transform, false);
            instance.Audio.Speaker.Base.transform.localPosition = PositionOffset;
        }

        if (instance.JumpAudio == null)
        {
            instance.JumpAudio = AudioPlayer.Create(
                SpeakerSettings.Default with { Volume = Volume },
                PositionOffset, instance.Owner.GameObject!.transform);
        }
        else
        {
            instance.JumpAudio.Volume = Volume;
            instance.JumpAudio.Speaker.Transform.SetParent(instance.Owner.GameObject!.transform, false);
            instance.JumpAudio.Speaker.Base.transform.localPosition = PositionOffset;
        }
    }

    public override void OnDestroyedInstance(Scp999RoleInstance instance)
    {
        if (instance.Audio == null) return;

        instance.Audio.Destroy();
    }

    public static void PlaySound(Player player, string clipName)
    {
        if (!Scp999Role.Check(player, out var instanceU) || instanceU is not Scp999RoleInstance instance) return;
        if (instance.Audio == null) return;
        instance.Audio.UseShortClip(clipName);
    }

    public static void PlayJumpSound(Player player)
    {
        if (!Scp999Role.Check(player, out var instanceU) || instanceU is not Scp999RoleInstance instance) return;
        if (instance.JumpAudio == null) return;
        instance.JumpAudio.UseShortClip("jump");
    }
}

using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Wrappers;
using Mirror;
using PlayerRoles;
using PlayerStatsSystem;
using UnityEngine;

namespace Scp999.Patches;

[HarmonyPatch(typeof(DoorCrusherExtension), nameof(DoorCrusherExtension.OnTriggerEnter))]
public class DoorCrusherPatch
{
    [HarmonyPrefix]
    public static bool OnTriggerEnter(DoorCrusherExtension __instance, Collider other)
    {
        if (!NetworkServer.active || !ReferenceHub.TryGetHub(other.transform.root.gameObject, out var hub))
            return false;

        var player = Player.Get(hub);
        var currentRole = hub.roleManager.CurrentRole;

        if ((player != null && Scp999.Role?.Check(player) == true) || currentRole.RoleTypeId == RoleTypeId.Scp106)
            return false;

        var flag = hub.GetTeam() == Team.SCPs;
        if (__instance.IgnoreScps & flag)
            return false;

        var damage = flag ? __instance.ScpCrushDamage : -1f;
        hub.playerStats.DealDamage(new UniversalDamageHandler(damage, DeathTranslations.Crushed));
        return false;
    }
}

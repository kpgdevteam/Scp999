using System;
using CustomAbilityLib.API;
using CustomRoleLib.API;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using Scp999.Features;

namespace Scp999;

public class Scp999 : Plugin<Config>
{
    public override string Name => "Scp999";
    public override string Description =>
        "Adds SCP-999, the tickling monster, as a custom role with unique abilities and features.";
    public override string Author => "MedveMarci";
    public override Version Version => new(1, 2, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);

    public static Scp999 Singleton { get; private set; }
    public static ICustomRole<object> Role { get; private set; }

    private const string RoleNamespaceKey = "scp999:scp999";

    public override void Enable()
    {
        Singleton = this;
        CustomRoleManager.RegisterAllRoles(typeof(Scp999Role).Assembly);
        CustomAbilityManager.RegisterAllAbilities(typeof(Scp999Role).Assembly);
        CustomRoleManager.TryGetRole(ObjectNamespace.Get(RoleNamespaceKey), out var role);
        Role = role;

        CustomSpawnManager.SetGroupMaxTokens(RoleNamespaceKey, 1);
        CustomSpawnManager.SetGroupTokenReset(
            RoleNamespaceKey,
            CustomSpawnManager.TokenResetType.RoundRestart
        );
    }

    public override void Disable()
    {
        CustomAbilityManager.UnregisterAllAbilities(typeof(Scp999Role).Assembly);
        CustomRoleManager.UnregisterAllRoles(typeof(Scp999Role).Assembly);
        Role = null;
        Singleton = null;
    }
}

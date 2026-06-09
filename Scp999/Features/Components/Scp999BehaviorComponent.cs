using CustomPlayerEffects;
using CustomRoleLib.API.DefaultComponents;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp3114Events;
using LabApi.Events.Handlers;
using PlayerRoles;

namespace Scp999.Features.Components;

public class Scp999BehaviorComponent : ComponentBase<Scp999RoleInstance>
{
    public override void SubscribeEvents(Scp999RoleInstance instance)
    {
        PlayerEvents.SearchingPickup += GetLabEvent<PlayerSearchingPickupEventArgs>(instance, OnSearchingPickup);
        PlayerEvents.InteractingDoor += GetLabEvent<PlayerInteractingDoorEventArgs>(instance, OnInteractingDoor);
        PlayerEvents.InteractingWarheadLever += GetLabEvent<PlayerInteractingWarheadLeverEventArgs>(instance, OnInteractingWarheadLever);
        PlayerEvents.Hurt += GetLabEvent<PlayerHurtEventArgs>(instance, OnPlayerHurt);
        PlayerEvents.Hurting += GetLabEvent<PlayerHurtingEventArgs>(instance, OnPlayerHurting);
        PlayerEvents.EnteringPocketDimension += GetLabEvent<PlayerEnteringPocketDimensionEventArgs>(instance, OnEnteringPocketDimension);
        PlayerEvents.Cuffing += GetLabEvent<PlayerCuffingEventArgs>(instance, OnPlayerCuffing);
        PlayerEvents.Jumped += GetLabEvent<PlayerJumpedEventArgs>(instance, OnPlayerJumped);
        Scp096Events.AddingTarget += GetLabEvent<Scp096AddingTargetEventArgs>(instance, OnScp096AddingTarget);
        Scp049Events.ResurrectingBody += GetLabEvent<Scp049ResurrectingBodyEventArgs>(instance, OnScp049ResurrectingBody);
        Scp049Events.UsingSense += GetLabEvent<Scp049UsingSenseEventArgs>(instance, OnScp049UsingSense);
        Scp173Events.AddingObserver += GetLabEvent<Scp173AddingObserverEventArgs>(instance, OnScp173AddingObserver);
        Scp3114Events.StrangleStarting += GetLabEvent<Scp3114StrangleStartingEventArgs>(instance, OnScp3114StrangleStarting);
    }

    public override void UnsubscribeEvents(Scp999RoleInstance instance)
    {
        PlayerEvents.SearchingPickup -= GetLabEvent<PlayerSearchingPickupEventArgs>(instance, OnSearchingPickup);
        PlayerEvents.InteractingDoor -= GetLabEvent<PlayerInteractingDoorEventArgs>(instance, OnInteractingDoor);
        PlayerEvents.InteractingWarheadLever -= GetLabEvent<PlayerInteractingWarheadLeverEventArgs>(instance, OnInteractingWarheadLever);
        PlayerEvents.Hurt -= GetLabEvent<PlayerHurtEventArgs>(instance, OnPlayerHurt);
        PlayerEvents.Hurting -= GetLabEvent<PlayerHurtingEventArgs>(instance, OnPlayerHurting);
        PlayerEvents.EnteringPocketDimension -= GetLabEvent<PlayerEnteringPocketDimensionEventArgs>(instance, OnEnteringPocketDimension);
        PlayerEvents.Cuffing -= GetLabEvent<PlayerCuffingEventArgs>(instance, OnPlayerCuffing);
        PlayerEvents.Jumped -= GetLabEvent<PlayerJumpedEventArgs>(instance, OnPlayerJumped);
        Scp096Events.AddingTarget -= GetLabEvent<Scp096AddingTargetEventArgs>(instance, OnScp096AddingTarget);
        Scp049Events.ResurrectingBody -= GetLabEvent<Scp049ResurrectingBodyEventArgs>(instance, OnScp049ResurrectingBody);
        Scp049Events.UsingSense -= GetLabEvent<Scp049UsingSenseEventArgs>(instance, OnScp049UsingSense);
        Scp173Events.AddingObserver -= GetLabEvent<Scp173AddingObserverEventArgs>(instance, OnScp173AddingObserver);
        Scp3114Events.StrangleStarting -= GetLabEvent<Scp3114StrangleStartingEventArgs>(instance, OnScp3114StrangleStarting);
    }

    private void OnPlayerJumped(PlayerJumpedEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Player != instance.Owner) return;
        Scp999AudioComponent.PlayJumpSound(ev.Player);
    }

    private void OnSearchingPickup(PlayerSearchingPickupEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Player == instance.Owner) ev.IsAllowed = false;
    }

    private void OnInteractingDoor(PlayerInteractingDoorEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Player == instance.Owner) ev.IsAllowed = false;
    }

    private void OnInteractingWarheadLever(PlayerInteractingWarheadLeverEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Player == instance.Owner) ev.IsAllowed = false;
    }

    private void OnPlayerHurt(PlayerHurtEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Player != instance.Owner || ev.Player.HasEffect<MovementBoost>()) return;
        if (ev.Player.TryGetEffect<Slowness>(out var slowness))
            ev.Player.EnableEffect<MovementBoost>((byte)(slowness.Intensity + 50), 10f);
        else
            ev.Player.EnableEffect<MovementBoost>(50, 10f);
    }

    private void OnPlayerHurting(PlayerHurtingEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Player != instance.Owner || ev.Attacker is not { IsSCP: true }) return;
        if (ev.Attacker.Role is RoleTypeId.Scp049)
            ev.Player.DisableEffect<CardiacArrest>();
        ev.IsAllowed = false;
    }

    private void OnEnteringPocketDimension(PlayerEnteringPocketDimensionEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Player == instance.Owner) ev.IsAllowed = false;
    }

    private void OnPlayerCuffing(PlayerCuffingEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Target == instance.Owner) ev.IsAllowed = false;
    }

    private void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Target == instance.Owner) ev.IsAllowed = false;
    }

    private void OnScp049ResurrectingBody(Scp049ResurrectingBodyEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Target == instance.Owner) ev.IsAllowed = false;
    }

    private void OnScp049UsingSense(Scp049UsingSenseEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Target == instance.Owner) ev.IsAllowed = false;
    }

    private void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Target == instance.Owner) ev.IsAllowed = false;
    }

    private void OnScp3114StrangleStarting(Scp3114StrangleStartingEventArgs ev, Scp999RoleInstance instance)
    {
        if (ev.Target == instance.Owner) ev.IsAllowed = false;
    }
}

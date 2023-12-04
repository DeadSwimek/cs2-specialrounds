using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Menu;
using CounterStrikeSharp.API.Core.Attributes;



namespace SpecialRounds;
[MinimumApiVersion(55)]

public static class GetUnixTime
{
    public static int GetUnixEpoch(this DateTime dateTime)
    {
        var unixTime = dateTime.ToUniversalTime() -
                       new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        return (int)unixTime.TotalSeconds;
    }
}
public partial class SpecialRounds : BasePlugin, IPluginConfig<ConfigSpecials>
{
    public override string ModuleName => "SpecialRounds";
    public override string ModuleAuthor => "DeadSwim";
    public override string ModuleDescription => "Simple Special rounds.";
    public override string ModuleVersion => "V. 1.0.2";
    private static readonly int?[] IsVIP = new int?[65];



    public ConfigSpecials Config { get; set; }
    public int Round;
    public bool EndRound;
    public bool IsRound;
    public int IsRoundNumber;
    public string NameOfRound = "";
    public bool isset = false;

    public void OnConfigParsed(ConfigSpecials config)
    {
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        WriteColor("Special round is [*Loaded*]", ConsoleColor.Green);
        RegisterListener<Listeners.OnMapStart>(name =>
        {
            EndRound = false;
            IsRound = false;
            NameOfRound = "";
            IsRoundNumber = 0;
            Round = 0;

        });
        RegisterListener<Listeners.OnTick>(() =>
        {
            for (int i = 1; i < Server.MaxPlayers; i++)
            {
                var ent = NativeAPI.GetEntityFromIndex(i);
                if (ent == 0)
                    continue;

                var client = new CCSPlayerController(ent);
                if (client == null || !client.IsValid)
                    continue;
                if (IsRound)
                {
                    client.PrintToCenterHtml(
                    $"<font color='gray'>----</font> <font class='fontSize-l' color='green'>Special Rounds</font><font color='gray'>----</font><br>" +
                    $"<font color='gray'>Now playing</font> <font class='fontSize-m' color='green'>[{NameOfRound}]</font>"
                    );
                }
                OnTick(client);
            }
        });
    }
    public static SpecialRounds It;
    public SpecialRounds()
    {
        It = this;
    }
    public static void OnTick(CCSPlayerController controller)
    {
        if (!controller.PawnIsAlive)
            return;
        var pawn = controller.Pawn.Value;
        var flags = (PlayerFlags)pawn.Flags;
        var client = controller.Index;
        var buttons = controller.Buttons;


        if (It.IsRoundNumber != 6)
            return;
        if (buttons == PlayerButtons.Attack2)
            return;
        if (buttons == PlayerButtons.Zoom)
            return;
        
    }
    [GameEventHandler]
    public HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {
        if (EndRound)
        {
            WriteColor($"SpecialRound - [*SUCCESS*] I turning off the special round.", ConsoleColor.Green);
            if(IsRoundNumber == 1)
            {
                change_cvar("mp_buytime", "15");
            }
            if (IsRoundNumber == 2)
            {
                change_cvar("sv_autobunnyhopping", "false");
                change_cvar("sv_enablebunnyhopping", "false");
            }
            if (IsRoundNumber == 3)
            {
                change_cvar("sv_gravity", "800");
            }
            if (IsRoundNumber == 4)
            {
                change_cvar("mp_buytime", "15");
            }
            if (IsRoundNumber == 5)
            {
                change_cvar("mp_buytime", "15");
            }
            if (IsRoundNumber == 6)
            {
                change_cvar("mp_buytime", "15");
            }
            IsRound = false;
            EndRound = false;
            isset = false;
            IsRoundNumber = 0;
            NameOfRound = "";

        }
        if (IsRound)
        {
            WriteColor($"SpecialRound - [*WARNING*] I cannot start new special round, its now.", ConsoleColor.Yellow);
            return HookResult.Continue;
        }
        if (Round < 0)
        {
            WriteColor("SpecialRound - [*WARNING*] I cannot start new special round, its round < 5.", ConsoleColor.Yellow);
            return HookResult.Continue;
        }
        Random rnd = new Random();
        int random = rnd.Next(36, 37);
        if (random == 1 || random == 2)
        {
            if (Config.AllowKnifeRound)
            {
                IsRound = true;
                IsRoundNumber = 1;
                NameOfRound = "Knife only";

                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);
            }
        }
        if (random == 6 || random == 7)
        {
            if (Config.AllowBHOPRound)
            {
                IsRound = true;
                IsRoundNumber = 2;
                NameOfRound = "Auto BHopping";

                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);
            }
        }
        if (random == 14 || random == 15)
        {
            if (Config.AllowGravityRound)
            {
                IsRound = true;
                IsRoundNumber = 3;
                NameOfRound = "Gravity round";

                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);
            }
        }
        if (random == 21 || random == 22)
        {
            if (Config.AllowAWPRound)
            {
                IsRound = true;
                IsRoundNumber = 4;
                NameOfRound = "Only AWP";

                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);
            }
        }
        if (random == 29 || random == 30)
        {
            if (Config.AllowP90Round)
            {
                IsRound = true;
                IsRoundNumber = 5;
                NameOfRound = "Only P90";

                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);
            }
        }
        if (random == 36 || random == 37)
        {
            if (Config.AllowANORound)
            {
                IsRound = true;
                IsRoundNumber = 6;
                NameOfRound = "Only AWP + NOSCOPE";

                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);
            }
        }
        //Server.PrintToConsole($" Settings : {NameOfRound} / IsRound {IsRound} / IsRoundNumber {IsRoundNumber} / Random number {random}");

        return HookResult.Continue;
    }
    [GameEventHandler]
    public HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        foreach (var l_player in Utilities.GetPlayers())
        {
            CCSPlayerController player = l_player;
            var client = player.Index;
            if (IsRoundNumber == 1)
            {
                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);

                if (IsRound)
                {
                    if (!is_alive(player))
                        return HookResult.Continue;
                    foreach (var weapon in player.PlayerPawn.Value.WeaponServices!.MyWeapons)
                    {
                        if (weapon is { IsValid: true, Value.IsValid: true })
                        {

                            if (weapon.Value.DesignerName.Contains("bayonet") || weapon.Value.DesignerName.Contains("knife"))
                            {
                                continue;
                            }
                            change_cvar("mp_buytime", "0");
                            weapon.Value.Remove();
                        }
                    }
                    if (!EndRound)
                    {
                        EndRound = true;
                    }
                }
            }
            if (IsRoundNumber == 2)
            {
                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);

                if (IsRound)
                {
                    change_cvar("sv_autobunnyhopping", "true");
                    change_cvar("sv_enablebunnyhopping", "true");
                    if (!EndRound)
                    {
                        EndRound = true;
                    }
                }
            }
            if (IsRoundNumber == 3)
            {
                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);

                if (IsRound)
                {
                    change_cvar("sv_gravity", "400");
                    if (!EndRound)
                    {
                        EndRound = true;
                    }
                }
            }
            if (IsRoundNumber == 4)
            {
                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);

                if (IsRound)
                {
                    if (!is_alive(player))
                        return HookResult.Continue;
                    foreach (var weapon in player.PlayerPawn.Value.WeaponServices!.MyWeapons)
                    {
                        if (weapon is { IsValid: true, Value.IsValid: true })
                        {
                            change_cvar("mp_buytime", "0");
                            weapon.Value.Remove();
                        }
                    }
                    player.GiveNamedItem("weapon_awp");
                    if (!EndRound)
                    {
                        EndRound = true;
                    }
                }
            }
            if (IsRoundNumber == 5)
            {
                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);

                if (IsRound)
                {
                    if (!is_alive(player))
                        return HookResult.Continue;
                    foreach (var weapon in player.PlayerPawn.Value.WeaponServices!.MyWeapons)
                    {
                        if (weapon is { IsValid: true, Value.IsValid: true })
                        {
                            change_cvar("mp_buytime", "0");
                            weapon.Value.Remove();
                        }
                    }
                    player.GiveNamedItem("weapon_p90");
                    if (!EndRound)
                    {
                        EndRound = true;
                    }
                }
            }
            if (IsRoundNumber == 6)
            {
                WriteColor($"SpecialRound - [*ROUND START*] Starting special round {NameOfRound}.", ConsoleColor.Green);

                if (IsRound)
                {
                    if (!is_alive(player))
                        return HookResult.Continue;
                    foreach (var weapon in player.PlayerPawn.Value.WeaponServices!.MyWeapons)
                    {
                        if (weapon is { IsValid: true, Value.IsValid: true })
                        {
                            change_cvar("mp_buytime", "0");
                            weapon.Value.Remove();
                        }
                    }
                    player.GiveNamedItem("weapon_awp");
                    if (!EndRound)
                    {
                        EndRound = true;
                    }
                }
            }

        }
        isset = false;
        return HookResult.Continue;
    }
    [GameEventHandler]
    public HookResult OnWeaponZoom(EventWeaponZoom @event, GameEventInfo info)
    {
        if (IsRoundNumber != 6) { return HookResult.Continue; }
        var player = @event.Userid;
        var weaponservices = player.PlayerPawn.Value.WeaponServices!;
        var currentWeapon = weaponservices.ActiveWeapon.Value.DesignerName;

        weaponservices.ActiveWeapon.Value.Remove();
        player.GiveNamedItem(currentWeapon);


        return HookResult.Continue;
    }
}

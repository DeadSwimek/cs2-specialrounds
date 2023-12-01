using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;



namespace SpecialRounds
{
    public partial class SpecialRounds
    {
        static void WriteColor(string message, ConsoleColor color)
        {
            var pieces = Regex.Split(message, @"(\[[^\]]*\])");

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                Console.Write(piece);
                Console.ResetColor();
            }

            Console.WriteLine();
        }
        static public int get_hp(CCSPlayerController? player)
        {
            if (player == null || !player.PawnIsAlive)
            {
                return 100;
            }
            return player.PlayerPawn.Value.Health;
        }
        static public bool is_alive(CCSPlayerController? player)
        {
            if (!player.PawnIsAlive)
            {
                return false;
            }
            return true;
        }

        static public bool change_cvar(string cvar, string value)
        {
                var find_cvar = ConVar.Find($"{cvar}");
                if (find_cvar == null)
                {
                    WriteColor($"SpecialRound - [*ERROR*] Canno't set {cvar} to {value}.", ConsoleColor.Red);
                    return false;
                }
                Server.ExecuteCommand($"{cvar} {value}");
                return true;
        }
    }
}
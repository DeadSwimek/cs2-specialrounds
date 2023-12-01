using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using System.Text.Json.Serialization;

namespace SpecialRounds;


public class ConfigSpecials : BasePluginConfig
{
    [JsonPropertyName("Prefix")] public string Prefix { get; set; } = $" {ChatColors.Default}[{ChatColors.Green}MadGames.eu{ChatColors.Default}]";
    [JsonPropertyName("AllowKnifeRound")] public bool AllowKnifeRound { get; set; } = true;
    [JsonPropertyName("AllowBHOPRound")] public bool AllowBHOPRound { get; set; } = true;
    [JsonPropertyName("AllowGravityRound")] public bool AllowGravityRound { get; set; } = true;
    [JsonPropertyName("AllowAWPRound")] public bool AllowAWPRound { get; set; } = true;
    [JsonPropertyName("AllowP90Round")] public bool AllowP90Round { get; set; } = true;

}
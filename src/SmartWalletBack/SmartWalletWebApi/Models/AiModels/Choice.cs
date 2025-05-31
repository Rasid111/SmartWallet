using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static SmartWalletWebApi.Service.OpenRouterService;

namespace SmartWalletWebApi.Models.AiModels;

public class Choice
{
    [JsonPropertyName("message")]
    public Message Message { get; set; }
}
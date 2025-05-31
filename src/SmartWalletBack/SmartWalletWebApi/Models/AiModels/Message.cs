using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartWalletWebApi.Models.AiModels;

public class Message
{
    [JsonPropertyName("content")]
    public string Content { get; set; }
}


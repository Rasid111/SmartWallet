using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SmartWalletWebApi.Models.AiModels;

namespace SmartWalletWebApi.Service;

public class OpenRouterService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey =
        "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";
    private const string ApiUrl = "https://openrouter.ai/api/v1/chat/completions";

    public OpenRouterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            ApiKey
        );
    }

    public async Task<string> SendMessage(string message)
    {
        var requestBody = new
        {
            model = "anthropic/claude-3-haiku",
            messages = new[] { new { role = "user", content = message } },
            max_tokens = 300,
            temperature = 0.7,
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(ApiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Exception API OpenRouter: {response.StatusCode} | {result}");
        }

        var data = JsonSerializer.Deserialize<OpenRouterResponse>(result);

        return data?.Choices?.FirstOrDefault()?.Message?.Content ?? "Нет ответа от AI.";
    }
}

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

    private const string ApiKey ="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";

    private const string ApiUrl = "https://openrouter.ai/api/v1/chat/completions";

    public OpenRouterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

    }

    public async Task<string> SendMessage(string message)
    {
        var requestBody = new
        {
            model = "anthropic/claude-3-haiku",
            messages = new[]
            {
                new { role = "user", content = message }
            },
            max_tokens = 100,
            temperature = 0.7
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
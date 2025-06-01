using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AiAnalysisController : ControllerBase
{
    private readonly AiAnalysisService aiService;

    public AiAnalysisController(AiAnalysisService aiService)
    {
        this.aiService = aiService;
    }

    [HttpGet("statistics/{userId}")]
    public async Task<IActionResult> Analyze(int userId)
    {
        try
        {
            var result = await aiService.AnalyzeUserFinanceAsync(userId);
            return Ok(new { advice = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка анализа: {ex.Message}");
        }
    }

    [HttpGet("investments/{userId}")]
    public async Task<IActionResult> AnalyzeInvest(int userId)
    {
        try
        {
            var result = await aiService.AnalyzeUserFinanceAsyncInvest(userId);
            return Ok(new { advice = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка анализа: {ex.Message}");
        }
    }

    [HttpGet("bestPrice/{userId}")]
    public async Task<IActionResult> AnalyzeBestPrice(int userId)
    {
        try
        {
            var result = await aiService.AnalyzeUserFinanceAsyncBestChoice(userId);
            return Ok(new { advice = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка анализа: {ex.Message}");
        }
    }
}

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

    [HttpGet("{userId}")]
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
}

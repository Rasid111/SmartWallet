using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class IncomeController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task GetIncomeByUserId(int userId)
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartWalletWebApi.Service;

namespace SmartWalletWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OpenRouterController : ControllerBase
{
    private OpenRouterService openRouterService;

    public OpenRouterController(OpenRouterService openRouterService)
    {
        this.openRouterService = openRouterService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string message)
    {
        var response = await openRouterService.SendMessage(message);
        return Ok(response);
    }
}

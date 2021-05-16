using System;
using Microsoft.AspNetCore.Mvc;

namespace NodeService
{
    [ApiController]
    [Route("block")]
    public sealed class BlockController : Controller
    {
        [HttpGet("t")]
        public IActionResult T()
        {
            return Ok("ok");
        }
    }
}
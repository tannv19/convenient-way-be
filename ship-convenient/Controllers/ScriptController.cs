using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ship_convenient.Services.ScriptService;

namespace ship_convenient.Controllers
{
    public class ScriptController : BaseApiController
    {
        private readonly IScriptService _scriptService;

        public ScriptController(IScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        [HttpGet("create-active-account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateActiveAccount()
        {
            var response = await _scriptService.CreateActiveAccount();
            return Ok(response);
        }

        [HttpGet("create-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePackages()
        {
            var response = await _scriptService.CreatePackages();
            return Ok(response);
        }

        [HttpGet("approved-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApprovedPackages()
        {
            var response = await _scriptService.ApprovedPackages();
            return Ok(response);
        }

        [HttpGet("selected-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SelectedPackages()
        {
            var response = await _scriptService.SelectedPackages();
            return Ok(response);
        }

        [HttpGet("pickup-success-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PickupSuccessPackages()
        {
            var response = await _scriptService.PickupSuccessPackages();
            return Ok(response);
        }

        [HttpGet("delivered-success-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeliveredSuccessPackages()
        {
            var response = await _scriptService.DeliveredSuccessPackages();
            return Ok(response);
        }

        [HttpGet("remove-script-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveScriptData()
        {
            var response = await _scriptService.RemoveScriptData();
            return Ok(response);
        }
    }
}

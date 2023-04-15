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
        public async Task<IActionResult> CreateActiveAccount(int deliverCount = 20, int senderCount = 10)
        {
            var response = await _scriptService.CreateActiveAccount(deliverCount, senderCount);
            return Ok(response);
        }

        [HttpGet("create-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePackages(int packageCount = 100)
        {
            var response = await _scriptService.CreatePackages(packageCount);
            return Ok(response);
        }

        [HttpGet("approved-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApprovedPackages(int packageCount = 80)
        {
            var response = await _scriptService.ApprovedPackages(packageCount);
            return Ok(response);
        }

        [HttpGet("selected-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SelectedPackages(int packageCount = 60)
        {
            var response = await _scriptService.SelectedPackages(packageCount);
            return Ok(response);
        }

        [HttpGet("pickup-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PickupSuccessPackages(int pickupSuccess = 50, int pickupFailed = 10)
        {
            var response = await _scriptService.PickupPackages(pickupSuccess, pickupFailed);
            return Ok(response);
        }

        [HttpGet("delivered-packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeliveredSuccessPackages(int deliveredSuccess = 40, int deliveredFailed = 10)
        {
            var response = await _scriptService.DeliveredPackages(deliveredSuccess, deliveredFailed);
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

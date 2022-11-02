using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.Common.AuxiliaryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inforce_SlotMachine.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpinController : ControllerBase
    {
        private readonly ISpinService _spinService;

        public SpinController(ISpinService spinService)
        {
            _spinService = spinService;
        }

        [HttpPut]
        public async Task<IActionResult> Spin(SpinBet model)
        {
            return Ok(await _spinService.Spin(model));
        }
    }
}

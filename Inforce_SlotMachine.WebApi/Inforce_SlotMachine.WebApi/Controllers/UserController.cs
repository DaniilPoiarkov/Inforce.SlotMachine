using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.Common.AuxiliaryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inforce_SlotMachine.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpPut("balance")]
        public async Task<IActionResult> UpdateBalance([FromBody] UpdateBalance model)
        {
            return Ok(await _userService.UpdateBalance(model));
        }

        [HttpPut("slots")]
        public async Task<IActionResult> UpdateSlotsCount([FromBody] UpdateSlotMachine model)
        {
            return Ok(await _userService.UpdateSlotMachineFields(model));
        }
    }
}

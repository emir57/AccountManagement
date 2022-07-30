using AccountManager.Business.Abstract;
using AccountManager.Dto.Concrete;
using AccountManager.Entity.Concrete;
using AutoMapper;
using Core.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace AccountManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BaseController<AccountDto, Account>
    {
        private readonly IAuthService _authService;
        public AccountsController(IAccountService baseService, IMapper mapper, IAuthService authService) : base(baseService, mapper)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return await base.GetListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            return await base.GetByIdAsync(id);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterAsync(registerDto);
            if (registerResult.Success == false)
                return BadRequest(registerResult);
            return Ok(registerResult);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] AccountDto personDto)
        {
            return await base.UpdateAsync(id, personDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            return await base.DeleteAsync(id);
        }
    }
}

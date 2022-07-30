using AccountManager.Business.Abstract;
using AccountManager.Dto.Concrete;
using AccountManager.Entity.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : BaseController<PersonDto, Person>
    {
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService, IMapper mapper) : base(personService, mapper)
        {
            _personService = personService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _personService.GetPersonsAsync();
            if (result.Success == false)
                return BadRequest(result);
            return Ok(result);
        }
    }
}

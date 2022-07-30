using AccountManager.Business.Abstract;
using AccountManager.Business.ValidationRules.FluentValidation;
using AccountManager.Data.Abstract;
using AccountManager.Dto.Concrete;
using AccountManager.Entity.Concrete;
using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions.Jwt;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace AccountManager.Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class PersonManager : AsyncBaseManager<PersonDto, Person>, IPersonService
    {
        private readonly IPersonDal _personDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PersonManager(IPersonDal repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper)
        {
            _personDal = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        [ValidationAspect(typeof(PersonValidator))]
        public override Task<IResult> AddAsync(PersonDto entity)
        {
            return base.AddAsync(entity);
        }

        public async Task<IDataResult<List<Person>>> GetPersonsAsync()
        {
            string loginedAccountId = _httpContextAccessor.HttpContext.User.ClaimId();
            var persons = await _personDal.GetAllAsync(x => x.AccountId == Convert.ToInt32(loginedAccountId));
            return new SuccessDataResult<List<Person>>(persons.ToList());
        }
        [ValidationAspect(typeof(PersonValidator))]
        public override Task<IResult> UpdateAsync(int id, PersonDto entity)
        {
            return base.UpdateAsync(id, entity);
        }
    }
}

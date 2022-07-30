using AccountManager.Dto.Concrete;
using AccountManager.Entity.Concrete;
using AutoMapper;
using Core.Entity.Concrete;

namespace AccountManager.Business.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<AccountDto, RegisterDto>().ReverseMap();

            CreateMap<PersonDto, Person>().ReverseMap();
        }
    }
}

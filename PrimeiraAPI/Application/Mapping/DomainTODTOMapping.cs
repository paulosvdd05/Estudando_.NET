using AutoMapper;
using PrimeiraAPI.Domain.Model;
using WebApi.Domain.DTOs;


namespace WebApi.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.NameEmployee, m => m.MapFrom(orig => orig.name));

        }
    }
}
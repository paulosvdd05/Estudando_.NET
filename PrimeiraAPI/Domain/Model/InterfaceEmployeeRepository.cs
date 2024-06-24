
using WebApi.Domain.DTOs;

namespace PrimeiraAPI.Domain.Model
{
    public interface InterfaceEmployeeRepository
    {
        void add(Employee employee);

        List<EmployeeDTO> Get(int pageNumber, int pageQuantity);

        Employee? Get(int id);
    }
}

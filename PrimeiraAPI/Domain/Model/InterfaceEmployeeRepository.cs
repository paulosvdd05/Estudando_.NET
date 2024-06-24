namespace PrimeiraAPI.Domain.Model
{
    public interface InterfaceEmployeeRepository
    {
        void add(Employee employee);

        List<Employee> Get(int pageNumber, int pageQuantity);

        Employee? Get(int id);
    }
}

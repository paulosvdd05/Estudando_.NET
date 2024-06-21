namespace PrimeiraAPI.Model
{
    public interface InterfaceEmployeeRepository
    {
        void add(Employee employee);

        List<Employee> Get();

        Employee? Get(int id);
    }
}

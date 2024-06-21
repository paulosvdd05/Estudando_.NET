using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Model;
using PrimeiraAPI.ViewModel;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {

        private readonly InterfaceEmployeeRepository _employeeRepository;

        public EmployeeController(InterfaceEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            using Stream fileStram = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStram);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);
            _employeeRepository.add(employee);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/dowlaod")]
        public IActionResult DowloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(dataBytes, "image/png");

        }

        [HttpGet]
        public IActionResult Get()
        {
            var employess = _employeeRepository.Get();

            return Ok(employess);
        }
    }
}

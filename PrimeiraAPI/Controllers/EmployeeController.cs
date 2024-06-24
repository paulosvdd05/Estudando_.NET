using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeiraAPI.Model;
using PrimeiraAPI.ViewModel;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {

        private readonly InterfaceEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(InterfaceEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            
            var employess = _employeeRepository.Get(pageNumber, pageQuantity);
            throw new Exception("erro teste");
            _logger.Log(LogLevel.Error, "erro teste");
            return Ok(employess);
        }
    }
}

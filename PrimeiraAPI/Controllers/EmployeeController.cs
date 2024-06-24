using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeiraAPI.Application.ViewModel;
using PrimeiraAPI.Domain.Model;
using PrimeiraAPI.Infraestrutura.Repositories;
using WebApi.Domain.DTOs;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {

        private readonly InterfaceEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(InterfaceEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            _logger.Log(LogLevel.Error, "erro teste");
            return Ok(employess);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {

            var employess = _employeeRepository.Get(id);
            var employeeDTOS = _mapper.Map<EmployeeDTO>(employess);
            _logger.Log(LogLevel.Error, "erro teste");
            return Ok(employeeDTOS);
        }
    }
}

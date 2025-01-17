﻿using Microsoft.AspNetCore.Connections;
using PrimeiraAPI.Domain.Model;
using WebApi.Domain.DTOs;

namespace PrimeiraAPI.Infraestrutura.Repositories
{
    public class EmployeeRepository : InterfaceEmployeeRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public void add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public List<EmployeeDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.Employees.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(b =>
                new EmployeeDTO()
                {
                    Id = b.id,
                    NameEmployee = b.name,
                    Photo = b.photo
                }
                )
                .ToList();
        }

        public Employee? Get(int id)
        {
            return _context.Employees.Find(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cars.core.Dto;
using Cars.core.ServiceInterface;
using Cars.Data;
using Cars.core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cars.ApplicationServices.Services
{
    public class CarServices : ICarsSerivce
    {
        private readonly CarsDbContext _context;

        public CarServices(CarsDbContext context)
        {
            _context = context;
        }

        public async Task<Car> Create(CarsDto dto)
        {
            Car car = new Car();
            car.Id = Guid.NewGuid();
            car.Model = dto.Model;
            car.Color = dto.Color;
            car.Plate = dto.Plate;
            car.Mileage = dto.Mileage;
            car.Added = DateTime.Now;
            car.Made = dto.Made;

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> Edit(CarsDto dto)
        {
            Car domain = new ();

            domain.Id = dto.Id;
            domain.Model = dto.Model;
            domain.Color = dto.Color;
            domain.Plate = dto.Plate;
            domain.Mileage = dto.Mileage;

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();

            return domain;

        }
    }
}
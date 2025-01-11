using Cars.core.Domain;
using Cars.core.Dto;
using Cars.core.ServiceInterface;
using Cars.Data;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsDbContext _context;
        private readonly ICarsSerivce _carServices;
        public CarsController(CarsDbContext context,
            ICarsSerivce carServices)
        {
            _context = context;
            _carServices = carServices;
        }
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.Select(car => new CarsViewModel
            {
                Id = car.Id,
                Model = car.Model,
                Color = car.Color,
                Plate = car.Plate,
                Mileage = car.Mileage,
                Made = car.Made
            }).ToListAsync();

            return View(cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var cars = new AddCarViewModel();
            return View("Create",cars);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCarViewModel vm)
        {
            var car = new CarsDto
            {
                Id = vm.Id,
                Model = vm.Model,
                Color = vm.Color,
                Plate = vm.Plate,
                Mileage = vm.Mileage,
                Made = vm.Made,
                Added = DateTime.Now,
            };
            var result = await _carServices.Create(car);
            return RedirectToAction(nameof(Index), vm);

           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Console.WriteLine($"Received Id: {id}");
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            Console.WriteLine($"Received Id: {id}");

            if (car == null)
            {
                Console.WriteLine("Car not found!");
                return RedirectToAction(nameof(Index));
            }

            var result = new EditCarViewModel
            {
                Id = car.Id,
                Model = car.Model,
                Color = car.Color,
                Plate = car.Plate,
                Mileage = car.Mileage,
                Made = car.Made
            };

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditCarViewModel vm)
        {
            var dto = new CarsDto()
            {
                Id= vm.Id,
                Model = vm.Model,
                Color = vm.Color,
                Plate = vm.Plate,
                Mileage = vm.Mileage,
                Made = vm.Made,
            };

            var result = await _carServices.Edit(dto);
            return RedirectToAction(nameof(Index), vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _carServices.Delete(id);

            return RedirectToAction(nameof(Index));
        }


    }
}

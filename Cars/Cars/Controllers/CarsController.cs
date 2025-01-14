using Cars.core.Domain;
using Cars.core.Dto;
using Cars.core.ServiceInterface;
using Cars.Data;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<IActionResult> Index(string searchModel, string searchColor, string searchPlate, int? searchMade, string searchMileage)
        {
            var userSearch = _context.Cars.AsQueryable();

            // Model filter
            if (!string.IsNullOrEmpty(searchModel))
                userSearch = userSearch.Where(car => car.Model.Contains(searchModel));

            // Color filter
            if (!string.IsNullOrEmpty(searchColor))
                userSearch = userSearch.Where(car => car.Color.Contains(searchColor));

            // Plate filter
            if (!string.IsNullOrEmpty(searchPlate))
                userSearch = userSearch.Where(car => car.Plate.Contains(searchPlate));

            // Made filter (year only)
            if (searchMade.HasValue)
                userSearch = userSearch.Where(car => car.Made.Year == searchMade.Value);

            // Mileage filter (as discussed earlier)
            if (!string.IsNullOrEmpty(searchMileage))
            {
                userSearch = searchMileage switch
                {
                    "0-5000" => userSearch.Where(car => car.Mileage >= 0 && car.Mileage <= 5000),
                    "5001-10000" => userSearch.Where(car => car.Mileage >= 5001 && car.Mileage <= 10000),
                    "10001-20000" => userSearch.Where(car => car.Mileage >= 10001 && car.Mileage <= 20000),
                    "20001-50000" => userSearch.Where(car => car.Mileage >= 20001 && car.Mileage <= 50000),
                    "50001-100000" => userSearch.Where(car => car.Mileage >= 50001 && car.Mileage <= 100000),
                    "100001+" => userSearch.Where(car => car.Mileage > 100000),
                    _ => userSearch
                };
            }

            var cars = await userSearch.Select(car => new CarsViewModel
            {
                Id = car.Id,
                Model = car.Model,
                Color = car.Color,
                Plate = car.Plate,
                Mileage = car.Mileage,
                Made = car.Made,
            }).ToListAsync();

            ViewData["searchMade"] = searchMade;
            return View(cars);
        }



        [HttpGet]
        public IActionResult Create()
        {
            var cars = new AddCarViewModel();
            return View("Create", cars);
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
                Id = vm.Id,
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                Console.WriteLine("Car not found!");
                return RedirectToAction(nameof(Index));
            }

            var carDetails = new CarDetailsViewModel
            {
                Id = car.Id,
                Model = car.Model,
                Color = car.Color,
                Plate = car.Plate,
                Mileage = car.Mileage,
                Made = car.Made,
                Added = car.Added
            };

            return View(carDetails);
        }
    }
}
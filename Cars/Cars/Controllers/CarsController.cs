using Cars.core.Domain;
using Cars.Data;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsDbContext _context;
        public CarsController(CarsDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // Fetch cars from database and map to CarsViewModel
            var cars = await _context.Cars.Select(car => new CarsViewModel
            {
                Model = car.Model,
                Color = car.Color,
                Plate = car.Plate,
                Mileage = car.Mileage,
                Made = car.Made
            }).ToListAsync();

            // Pass the list of cars to the Index view
            return View(cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCarViewModel vm)
        {
            var car = new Car
            {
                Model = vm.Model,
                Color = vm.Color,
                Plate = vm.Plate,
                Mileage = vm.Mileage,
                Made = vm.Made,
                Added = DateTime.Now,
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return View();
        }

        /*[HttpGet]
        public async Task<IActionResult> List()
        {
            var cars = await _context.Cars.Select(car => new CarsViewModel
            {
                Model = car.Model,
                Color = car.Color,
                Plate = car.Plate,
                Mileage = car.Mileage,
                Made = car.Made // This should be a DateTime
            }).ToListAsync();

            // Ensure cars is not null before passing it to the view
            if (cars == null)
            {
                return View(new List<CarsViewModel>());
            }

            return View(cars);
        }*/
    }
}

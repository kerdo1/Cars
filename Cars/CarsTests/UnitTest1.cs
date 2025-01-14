using Microsoft.EntityFrameworkCore.Diagnostics;
using Cars.core.Domain;
using Cars.ApplicationServices.Services;
using Xunit;
using Cars.core.Dto;
using Cars.core.ServiceInterface;
using System.Xml;

namespace CarsTests
{
    public class UnitTest1 : TestBase
    {
        [Fact]
        public async void Should_Create_Car_When_Valid_Data()
        {
            CarsDto dto = new()
            {
                Model = "Audi",
                Color = "Red",
                Plate = " ABCD1232",
                Mileage = 3600,
                Made = DateTime.Now,
                Added = DateTime.Now,
            };
            var result = await Svc<ICarsSerivce>().Create(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Model, result.Model);
            Assert.Equal(dto.Mileage, result.Mileage);


    }
        [Fact]
        public async Task Should_Delete_Car_By_Id()
        {
            CarsDto car = Mock();

            var addcar = await Svc<ICarsSerivce>().Create(car);
            await Svc<ICarsSerivce>().Delete((Guid)addcar.Id);
            
            Assert.NotNull(addcar);
        }


        [Fact]
        public async void Shouldnt_Create_Car_With_Missing_Field()
        {
            CarsDto car = new()
            {
                Model = "Audi",
                //Color = 12,
                Plate = "ABCD1232",
                Mileage = 3600,
                Made = DateTime.Now,
                Added = DateTime.Now,
            };
            var result = await Svc<ICarsSerivce>().Create(car);
        }


        [Fact]
        public async Task Shouldnt_Delete_Car_By_Id_When_Did_Not_Press_Delete()
        {
            CarsDto car = Mock();

            var car1 = await Svc<ICarsSerivce>().Create(car);
            var car2 = await Svc<ICarsSerivce>().Create(car);

            await Svc<ICarsSerivce>().Delete(car2.Id);

            var remainingCar = await Svc<ICarsSerivce>().GetById(car1.Id); 
            Assert.NotNull(remainingCar);
            Assert.Equal(car1.Id, remainingCar.Id);
        }


        private CarsDto Mock()
        {
            CarsDto car = new()
            {
                Model = "BMW",
                Color = "Black",
                Plate = " CCDEE112",
                Mileage = 86000,
                Made = DateTime.Now,
                Added = DateTime.Now,
            };

            return car;
        }
    }
}

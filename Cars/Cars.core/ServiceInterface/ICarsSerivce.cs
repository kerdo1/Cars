using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cars.core.Domain;
using Cars.core.Dto;

namespace Cars.core.ServiceInterface
{
    public interface ICarsSerivce
    {
        Task<Car> Create(CarsDto vm);
        Task<Car> Edit(CarsDto vm);
        Task Delete(Guid id);
    }
}

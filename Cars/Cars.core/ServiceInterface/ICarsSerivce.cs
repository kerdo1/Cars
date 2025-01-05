using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cars.core.Domain;
namespace Cars.core.ServiceInterface
{
    public interface ICarsSerivce
    {
        Task<Car> List();
    }
}

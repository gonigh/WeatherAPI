using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.BO;

namespace WeatherAPI.Service.Interface
{
    public interface IDangerours
    {
        bool IsDangerours(HttpBO bo);
    }
}

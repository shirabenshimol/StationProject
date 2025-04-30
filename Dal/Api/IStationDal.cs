using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stationProject.Dal.Models;

namespace Dal.Api
{
    public interface IStationDal : ICrudDal<Station>
    {
          Task<List<Station>> Get();
 
    }
}

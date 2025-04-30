using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bl.Service;

namespace Bl.Api
{
    public interface IBl
    {
        public IMeasureBl  measure{ get; }
        public IStationBl station { get; }
    }
}

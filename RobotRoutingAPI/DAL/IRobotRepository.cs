using PayloadRobotSelectorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayloadRobotSelectorAPI.DAL
{
    public interface IRobotRepository
    {
        Task<IEnumerable<Robot>> GetRobotsAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayloadRobotSelectorAPI.Models
{
    public class RobotSelection
    {
        public string robotId { get; set; }
        public double distanceToGoal { get; set; }
        public int batteryLevel { get; set; }
    }
}

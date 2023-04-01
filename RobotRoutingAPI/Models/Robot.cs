using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayloadRobotSelectorAPI.Models
{
    public class Robot
    {
        public string robotId { get; set; }
        public double distanceToGoal { get; set; }
        public int batteryLevel { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}

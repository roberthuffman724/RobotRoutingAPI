using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayloadRobotSelectorAPI.DAL;
using PayloadRobotSelectorAPI.Models;

namespace PayloadRobotSelectorAPI.Controllers
{
    [ApiController]
    public class RobotSelectionController : ControllerBase
    {
        private readonly IRobotRepository _robotRepository;
        private readonly ILogger<RobotSelectionController> _logger;
        private IEnumerable<Robot> _robots;

        public RobotSelectionController(IRobotRepository robotRepository, ILogger<RobotSelectionController> logger)
        {
            _robotRepository = robotRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/robots/closest")]
        public async Task<RobotSelection> GetRobotSelectionAsync([FromBody]Payload loadToBeMoved)
        {
            try
            {
                //get list of available robots
                _robots = await _robotRepository.GetRobotsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve list of robots");
                throw;
            }

            try
            {
                //determine robot selection based on distance to load and battery level
                RobotSelection robotSelection = CalculateRobotSelection(loadToBeMoved);
                return robotSelection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to evaluate robot selection");
                throw;
            }
        }

        private RobotSelection CalculateRobotSelection(Payload loadToBeMoved)
        {
            Robot closestRobot = null;

            //calculate distance of each robot from payload
            foreach (var r in _robots)
            {
                r.distanceToGoal = Math.Sqrt(Math.Pow(r.x - loadToBeMoved.x, 2) + Math.Pow(r.y - loadToBeMoved.y, 2));
            }

            if (_robots.Count(r => r.distanceToGoal <= 10) > 1)
            {
                //if multiple robots are within a range of 10, select the one with the most battery
                closestRobot = _robots.Where(r => r.distanceToGoal <= 10).OrderByDescending(r => r.batteryLevel).First();
            }
            else
            {
                //otherwise, select closest robot
                closestRobot = _robots.OrderBy(r => r.distanceToGoal).First();
            }

            //Prepare response object
            RobotSelection robotSelection = new RobotSelection()
            {
                distanceToGoal = closestRobot.distanceToGoal,
                batteryLevel = closestRobot.batteryLevel,
                robotId = closestRobot.robotId
            };

            return robotSelection;
        }
    }
}

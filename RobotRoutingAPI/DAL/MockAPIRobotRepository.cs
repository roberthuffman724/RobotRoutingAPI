using PayloadRobotSelectorAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PayloadRobotSelectorAPI.DAL
{
    public class MockAPIRobotRepository : IRobotRepository
    {
        private HttpClient _httpClient;

        public MockAPIRobotRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Robot>> GetRobotsAsync()
        {
            IEnumerable<Robot> robots = null;

            //retrieve list of robots
            HttpResponseMessage response = await _httpClient.GetAsync("robots");
            //throw exception if request is not successfull
            response.EnsureSuccessStatusCode();

            //read response into list of robots
            Stream contentStream = await response.Content.ReadAsStreamAsync();
            robots = await JsonSerializer.DeserializeAsync<IEnumerable<Robot>>(contentStream);

            return robots;
        }
    }
}

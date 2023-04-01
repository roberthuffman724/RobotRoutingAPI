# RobotRoutingAPI
## Robot Routing API for SVT Robotics
Hello! My name is Bobby Huffman and this solution demonstrates a simple API for selecting a robot to move a payload based on proximity and battery level.

## Running the API
Make sure that IIS and required components are installed and enabled. To run the API for testing: Open the solution in Visual Studio, then build and run.

## Testing the API
To test the API with a JSON payload: 
1. Run the API. 
2. Using Postman or another similar utility, make a POST request to the following endpoint: http://localhost:5000/api/robots/closest.
    - The request body should contain a raw JSON payload with the following values:
      - loadId: string
      - x: integer
      - y: integer
  - Example payload:  { "loadId": "231", "x": 41, "y": 60 }
3. The Reponse body will contain a result with the following values:
    - robotId: string
    - distanceToGoal: double
    - batteryLevel: integer
  - Example response: { "robotId": "56", "distanceToGoal": 7.615773105863909, "batteryLevel": 100}
  
## Plans for Improvement
1. Implement better logging
    - As this project grows larger, it would be beneficial to have logging for events that would aid in troubleshooting. I would add logging events especially for calls made to outside resources, such as the call to retrieve a list of robots.
2. More exception handling
    - Some top-level exception handlers were added to log at major potential breaking points, more specific exception handlers could be added.
3. Add Authentication
    - Internal services should generally be protected from unauthorized access. Unless this service were hosted and utilized internally on a protected network, Implementation of an authentication method such as OAuth would prevent unauthorized access.
    
## Feature Development Plans
If this API were intended to handle the routing of robots to deliver a payload, future feature development may involve adding routes to issue commands to the selected robot to pick up and deliver the payload. This API call may include the loadId of the payload, which the robot could use to retrieve more detailed information about the payload such as its position and dimensions to determine necessary pathing, or the full details of the payload depending on the amount of information required and how frequently it would need updates. Delivery of the payload would require different data such as the coordinates of the dropoff or, if delivery was to a pre-determined location such as a delivery bay, an identifier for that location.

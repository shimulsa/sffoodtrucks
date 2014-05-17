May 16, 2014

Hosted Application (Azure) - http://sffoodtrucks.azurewebsites.net
Hosted Repository (GitHub) - https://github.com/shimulsa/sffoodtrucks

Project: SF Food Trucks
Technical Track: Full Stack
--------------------------------
Reasoning:
I really enjoy working on services - it is gratifying to see millions of users hit a URL and have it power their
core experiences. That said, a consumer experience can not be complete without a delightful front end. Keeping
this in mind, I chose to opt for full stack in order to go through an exercise to deliver a good E2E user experience.
I also used this as a learning opportunity to play around with technologies.

Previous Experience:
While at Microsoft, I have gained experience in development of front end as well as back end components. 
Front end experience - I worked on two internal websites during my work at Microsoft, getting exposure to advanced MVC/HTML/JS/CSS concepts.
Back end experience - I have experience standing up REST services in Azure as part of the Windows Phone Store Services team.

--------------------------------
Tools and Technologies - What and Why?
- Languages: C#, JS, HTML, CSS, JSON
- IDE: Visual Studio
- Web Framework: ASP.NET MVC5
- REST API Framework: WebAPI 
- Host: Azure
- Platform: Windows 8.1, Microsoft .NET
- Integration: Bing Maps, DataSF (open data)
- Source code: Git (Github)

Reasoning for Choice: 
ASP.NET is a complete web framework to create websites quickly and publish them directly to Azure. The E2E integration is quick to set up. Given the time
constraints of the project, using ASP.NET was the most efficient way to have the pipeline running quickly. Since I am very familiar with .NET and ASP.NET, it was a 
natural choice for me to start with something I am comfortable in.
Cons: .NET MVC5 is heavy-weight with a lot of boilerplate code that comes with the project templates.

--------------------------------
What code did I write?
Here is a list of the files I authored and what they do:

Backend
- FoodTruckController.cs - API definition for fetching "nearby" food trucks, given a location
- FoodTruckRespository.cs - Data repository that exposes methods to fetch, cache and filter data
- RestClient.cs - Actual Rest client that the data repository uses to make a REST call to DataSF APIs and does
JSON de/serialization
- FoodTruck.cs - Model object to transform a JSON object from a REST response object

Frontend
- Index.html - Renders the landing page with search functionality and Bing Maps control
- BingMaps.js - Javascript functions that the map uses to handle search and map-interaction events along with
displays/views.

Unit Tests
- FoodTruckControllerTest.cs - Unit tests to test the Search API 
- FoodTruckRepositoryTest.cs - Unit tests to test caching, fetching data and calculating distances between 2 points on a map

--------------------------------
API Docs

Host: sffoodtrucks.azurewebsites.net
GET /v1/foodtruck?latitude=37.7833&longitude=-122.4167&limit=10 HTTP/1.1

Try in browser: http://sffoodtrucks.azurewebsites.net/v1/foodtruck?latitude=37.7833&longitude=-122.4167&limit=1

Request Parameters:
Latitude: Latitude of location around which you are searching for food trucks
Longitude: Longitude of location around which you are searching for food trucks
[OPTIONAL] Limit: Top N closest results. Default value is 50, if not passed


Response: Array of Food Trucks closest to the entered location. # results = limit (or 50 if limit is not specified in the request). JSON for a SINGLE food truck:
[{"applicant":"John's Catering #5","schedule":"http://bsm.sfdpw.org/PermitsTracker/reports/report.aspx?title=schedule&report=rptSchedule&params=permit=14MFF-0023&ExportPDF=1&Filename=14MFF-0023_schedule.pdf","address":"180 REDWOOD ST","lot":"013","block":"0766","fooditems":"Cold Truck: Soda:Chips:Candy: Cold/Hot Sandwiches: Donuts.  (Pitco Wholesale)","latitude":37.7808315769119,"longitude":-122.419942864682}]


Trade-offs:
Given more time, I would want to enable the following:

- Add a "DRAG" event on the orange pin to enable the "repositioning" user-interaction on the map itself. The drag event
would trigger a re-rendering of the map with the fresh set of results.
- Show "Progress". Sometimes when a search is fired, it takes a few seconds to load the data. It would be nice to
show some indication of progress on the webpage.
- Allow more filtering on the data (e.g., times)
- Show more information in the infobox in a meaningful way. For instance, if I tried to put in "fooditems" in the
infoboxes, it was making the information look cluttered. I would try to do meaningful extraction of data from
food items to give the user an idea of what "type" of food to expect. The reason I didn't prioritize this is because
generally the name of the applicant is enough to get an idea.
- Better Error Handling and Messaging. For instance, I would like to address errors that occur when the address is not accurate.
I would also like to render errors in a more user-friendly way on the website.
- Trigger search on load of the website based on the user's browser location
- Improving UI. The Infobox UI can use better CSS styling and the ability to handle overflowing text meaningfully.
- Consider open source stack instead of the .NET/Azure stack

Other project (protoype/non-functional) - cheffort.herokuapp.com
Profiles: https://cal.berkeley.edu/shimul, http://www.linkedin.com/pub/shimul-sachdeva/13/7/100

# HackerNewsAPI_JorgeLozada
This project implements a RESTful API using ASP.NET Core to fetch the top `n` stories from Hacker News, ordered by score.

## Setup
1. Clone the repository: `git clone https://github.com/Steelclio/HackerNewsAPI_JorgeLozada.git`.
2. Navigate to the Project Directory: `cd HackerNewsAPI_JorgeLozada`.
3. Restore Dependencies: `dotnet restore`.
4. Build the application: `dotnet build`.
3. Run the application: `dotnet run` to start the API.

## Endpoints
- `GET /api/hackernews/beststories?n=10`: Retrieves the top 10 stories.

## Assumptions
- The application uses in-memory caching to reduce the load on the Hacker News API, with a cache duration of 5 minutes.
- The application is intended to be run in a development environment; if you intend to deploy it in production, consider using a distributed caching system like Redis.
- The Swagger UI is enabled by default in development mode to facilitate API exploration and testing.

## Enhancements
- Caching: Implement distributed caching (e.g., Redis) for better performance in a production environment.
- Rate Limiting: Implement rate limiting to protect both your API and the Hacker News API from being overwhelmed by too many requests.
- Error Handling: Improve error handling to account for potential issues such as API timeouts, failures, and rate limiting from the Hacker News API.
- Authentication and Authorization: Add authentication and authorization mechanisms to secure the API endpoints.
- Logging: Integrate a logging framework to monitor API usage and track issues in real-time.

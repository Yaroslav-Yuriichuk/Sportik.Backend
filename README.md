## Overview

Sportik Backend is a backend application to create reminders for exercises and track the progress.

## Build

To build the application, you need to have the following tools installed:
- .NET 8+ SDK

To build the application, follow these steps:
- Clone the repository:
  ```bash
  https://github.com/Yaroslav-Yuriichuk/Sportik.Backend.git
- Open the solution file in IDE.
- Set environment variables:
  - `ASPNETCORE_ENVIRONMENT` to `Development` or `Production`.
  - `ASPNETCORE_URLS` to the desired URL (e.g., `http://localhost:5000`).
  - `ConnectionStrings__DefaultConnection` to your database connection string.
  - `Jwt__Secret` to your JWT secret key.
- Select the configuration you want to build.
- Click "Build" -> "Build Solution" to build the application or run directly in IDE.

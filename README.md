# Recipe Finder

## Overview

The Recipe Finder app allows users to find recipes based on ingredients they have. It utilizes the Spoonacular API to fetch recipes and provides filtering options based on cuisine and diet.

## Features

- Search for recipes by ingredient
- Filter recipes by cuisine and diet
- Securely handle API keys using environment variables
- Resilient HTTP requests with Polly for retries
- Timeout and cancellation support for HTTP requests

## Technologies Used

- .NET 6
- ASP.NET Core Web API
- HttpClient
- Newtonsoft.Json
- Polly
- xUnit (for unit testing)
- Moq (for mocking in tests)
- FluentAssertions (for assertions in tests)

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Spoonacular API Key](https://spoonacular.com/food-api)

## Getting Started

### Clone the Repository

```sh
git clone https://github.com/your-username/recipe-finder.git
cd recipe-finder

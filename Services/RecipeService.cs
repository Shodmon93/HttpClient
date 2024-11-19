using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using RecipeFinder.Models;
using RecipeFinder.ViewModels;

namespace RecipeFinder.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RecipeService> _logger;

        public RecipeService(IHttpClientFactory httpClientFactory, ILogger<RecipeService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("SpoonacularClient");
            _logger = logger;
        }

        public async Task<List<RecipeViewModel>> GetRecipesAsync(string ingredient, string cuisine = null, string diet = null)
        {
            var url = $"recipes/findByIngredients?ingredients={ingredient}";
            if (!string.IsNullOrEmpty(cuisine))
            {
                url += $"&cuisine={cuisine}";
            }
            if (!string.IsNullOrEmpty(diet))
            {
                url += $"&diet={diet}";
            }
            _logger.LogInformation("Fetching recipes fro ingredient {ingredient}", ingredient);
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var recipes = JsonConvert.DeserializeObject<List<Recipe>>(responseString);
                _logger.LogInformation("Found {recipes.Count} recipes", recipes.Count);
                var recipeViewModel = recipes.Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.Image,
                    Ingredients = string.Join(", ", r.Ingredients.Select(i => i.Name))
                }).ToList();
                return recipeViewModel;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Request Error {ex.Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected Error: {ex.Message}", ex.Message);
                throw;
            }

        }

    }
}
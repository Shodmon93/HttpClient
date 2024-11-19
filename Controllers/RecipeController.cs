using Microsoft.AspNetCore.Mvc;
using RecipeFinder.Models;
using RecipeFinder.Services;
using RecipeFinder.ViewModels;

namespace RecipeFinder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly RecipeService _recipeService;
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(RecipeService recipeService, ILogger<RecipeController> logger)
    {
        _recipeService = recipeService;
        _logger = logger;
    }

    [HttpGet("GetRecipes")]
    public async Task<ActionResult<List<RecipeViewModel>>> GetRecipes(string ingredient, [FromQuery] string cuisine = null,
        [FromQuery] string diet = null)
    {
        if (string.IsNullOrWhiteSpace(ingredient))
        {
            return BadRequest("Ingredient must be provided.");
        }
        try
        {
            var recipe = await _recipeService.GetRecipesAsync(ingredient, cuisine, diet);
            if (recipe == null || recipe.Count == 0)
            {
                return NotFound("No recipe found for the given ingredient.");
            }

            return Ok(recipe);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("HTTP request error {ex.Message}", ex.Message);
            return StatusCode(503, $"Error communicating with the API. Please try again later: {ex}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected error {ex.Message}", ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
using Newtonsoft.Json;

namespace RecipeFinder.Models;

public class Recipe
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("title")]
    public string  Title  { get; set; }
    
    [JsonProperty("image")]
    public string Image { get; set; }
    [JsonProperty("missedIngredients")] 
    public List<Ingredient> Ingredients { get; set; }
    
}
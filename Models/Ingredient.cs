using Newtonsoft.Json;

namespace RecipeFinder.Models;

public class Ingredient
{
    [JsonProperty("id")] 
    public int Id { get; set; }
    [JsonProperty("amount")] 
    public string Amount { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("originalName")]
    public string OriginalName { get; set; }
}
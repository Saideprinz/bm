 In the C# file, write a program to perform a GET request on the route https://coderbyte.com/api/challenges/json/json-cleaning and then clean the object according to the following rules: Remove all keys that have values of N/A, -, or empty strings. If one of these values appear in an array, remove that single item from the array. Then console log the modified object as a string.

Example Input
{"name":{"first":"Daniel","middle":"N/A","last":"Smith"},"age":45}

Example Output
{"name":{"first":"Daniel","last":"Smith"},"age":45}





Here's a C# program that performs a GET request on the provided route, cleans the JSON object according to the specified rules, and then prints the modified object as a string:

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = "https://coderbyte.com/api/challenges/json/json-cleaning";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject cleanedObject = CleanJsonObject(json);
                    Console.WriteLine(cleanedObject.ToString());
                }
                else
                {
                    Console.WriteLine("Failed to retrieve data. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    static JObject CleanJsonObject(string json)
    {
        JObject jsonObject = JObject.Parse(json);
        CleanObject(jsonObject);
        return jsonObject;
    }

    static void CleanObject(JObject obj)
    {
        foreach (var property in obj.Properties().ToList())
        {
            if (property.Value.Type == JTokenType.Object)
            {
                CleanObject((JObject)property.Value);
            }
            else if (property.Value.Type == JTokenType.Array)
            {
                CleanArray((JArray)property.Value);
            }
            else
            {
                if (property.Value.ToString() == "N/A" || property.Value.ToString() == "-" || property.Value.ToString() == "")
                {
                    property.Remove();
                }
            }
        }
    }

    static void CleanArray(JArray array)
    {
        for (int i = array.Count - 1; i >= 0; i--)
        {
            JToken item = array[i];
            if (item.Type == JTokenType.Object)
            {
                CleanObject((JObject)item);
            }
            else if (item.Type == JTokenType.Array)
            {
                CleanArray((JArray)item);
            }
            else
            {
                if (item.ToString() == "N/A" || item.ToString() == "-" || item.ToString() == "")
                {
                    array.RemoveAt(i);
                }
            }
        }
    }
}
```

This program sends a GET request to the provided URL, retrieves the JSON response, cleans the JSON object according to the specified rules, and then prints the modified object as a string. Make sure to include the Newtonsoft.Json package in your project for this code to work.

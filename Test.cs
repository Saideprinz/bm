using System;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        string json = @"{
            ""name"": ""John"",
            ""age"": ""N/A"",
            ""city"": ""New York"",
            ""country"": ""N/A""
        }";

        JObject jsonObject = JObject.Parse(json);

        RemoveNAValues(jsonObject);

        string result = jsonObject.ToString();
        Console.WriteLine(result);
    }

    static void RemoveNAValues(JObject obj)
    {
        foreach (var property in obj.Properties().ToList())
        {
            if (property.Value.ToString() == "N/A")
            {
                property.Remove();
            }
            else if (property.Value.Type == JTokenType.Object)
            {
                RemoveNAValues((JObject)property.Value);
            }
        }
    }
}

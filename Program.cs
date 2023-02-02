using Configurator;
using ConsoleApp1;
using System.Text.Json;

Console.WriteLine("Welcome to my shop! Here you can try out various PC configurations!");

Data? allData = JsonSerializer.Deserialize<Data?>(File.ReadAllText("pc-store-inventory.json"));

while (true)
{
    string? input;

    Console.WriteLine("Please enter a CPU, Motherboard and Memory:");
    input = Console.ReadLine();

    if (input == null)
    {
        Console.WriteLine("ERROR: Please enter valid input!");
        break;
    }

    var components = Utils.GetEnteredComponents(input, allData);
    if (components.Count == 0)
    {
        Console.WriteLine("ERROR!");
        break;
    }

    Utils.ValidateAndGenerateConfigurations(components, allData);
    Console.WriteLine("Would you like to try a new configuration? Y/N ");
    input = Console.ReadLine();

    if (input != null && (input == "n" || input == "N")) break;
}

Console.WriteLine("Thank you for using my shop!");

return;

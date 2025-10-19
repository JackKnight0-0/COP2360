using System;
using System.Collections.Generic;
using System.Linq;

class Program
{

    public static (string type, int health, string region, string attributes) GetStats()
    {
        /// Methods to get stats about Pokemons
        int health;
        Console.WriteLine("Enter the type of Pokemon: ");
        string? type = Console.ReadLine();
        Console.WriteLine("Enter the health of the Pokemon: ");
        string? healthRaw = Console.ReadLine();
        Console.WriteLine("Enter the region of the Pokemon: ");
        string? region = Console.ReadLine();
        Console.WriteLine("Enter attributes of Pokemon: ");
        string? attributes = Console.ReadLine();

        if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(healthRaw) || string.IsNullOrEmpty(region) || string.IsNullOrEmpty(attributes)) /// Checking if any input is empty
        {
            Console.WriteLine("Type, health, region, and attributes cannot be empty.");
            return ("error", 0, "unknown", "none");
        }
 

        if (!int.TryParse(healthRaw, out health)) /// Checking if health is a number
        {
            Console.WriteLine("Please, enter the number for the health.");
            return ("error", 0);
        }



        return (type, health, region, attributes);
    }

    public static string GetName()
    {
        /// Method to get the name of Pokemon.
        Console.WriteLine("Enter the name of the Pokemon: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Invalid name.");
            return "unknown";
        }
        return name;
    }

    public static void PrintMenu()
    {
        /// Menu of available commands
        Console.WriteLine("\tMenu");
        Console.WriteLine("a - Add a new Pokemon to the dictionary.");
        Console.WriteLine("p - Show the dictionary's contents.");
        Console.WriteLine("r - Remove the Pokemon.");
        Console.WriteLine("e - Add a new stats to an existing Pokemon.");
        Console.WriteLine("s - Sort the dictionary's contents.");
        Console.WriteLine("q - Stop operations.");
    }

    static void Main(string[] args)
    {
        Dictionary<string, Dictionary<string, object>> pokemons = new Dictionary<string, Dictionary<string, object>>();
        Console.WriteLine("Welcome to the Pokemon Dictionary! Here are the following operations: ");
        char op = '\0';
        while (op != 'q')
        {
            PrintMenu();
            op = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (op)
            {
                case 'a':
                    var name = GetName();
                    var (type, health, region, attributes) = GetStats();
                    if (type == "error")
                    {
                        break;
                    }
                    if (pokemons.ContainsKey(name))
                    {
                        Console.WriteLine("This Pokemon already exists!");
                        break;
                    }
                    pokemons.Add(
                        name, new Dictionary<string, object>
                            {
                                {"type", type},
                                {"health", health},
                                {"region", region},
                                {"attributes", attributes}
                              }
                    );
                    break;
                case 'p':
                    IEnumerator<KeyValuePair<string, Dictionary<string, object>>> enumerator = pokemons.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        var currPokemon = enumerator.Current;
                        Console.WriteLine($"{currPokemon.Key}: ");
                        IEnumerator<KeyValuePair<string, object>> stats = currPokemon.Value.GetEnumerator();
                        while (stats.MoveNext())
                        {
                            var currStat = stats.Current;
                            if (currStat.Value is int)
                            {
                                Console.WriteLine($"\tHealth: {currStat.Value} ");
                            }
                            else if (currStat.Value is string)
                            {
                                if (currStat.Key == "type")
                                    Console.WriteLine($"\tType: {currStat.Value}");
                                else if (currStat.Key == "region")
                                    Console.WriteLine($"\tRegion: {currStat.Value}");
                                else if (currStat.Key == "attributes")
                                    Console.WriteLine($"\tAttributes: {currStat.Value}");
                            }
                        }
                    }
                    break;
                case 'r':
                    name = GetName();
                    bool isRemoved = pokemons.Remove(name);
                    if (!isRemoved)
                    {
                        Console.WriteLine("The name of the Pokemon does not exist in the dictionary.");
                    }
                    else
                    {
                        Console.WriteLine("Success!");
                    }
                    break;
                case 'e':
                    Console.WriteLine("Enter the name of the existing Pokemon in the dictionary: ");
                    name = GetName();
                    var (newType, newHealth, newRegion, newAttributes) = GetStats();
                    if (newType == "error")
                    {
                        break;
                    }
                    if (pokemons.ContainsKey(name))
                    {
                        pokemons[name]["type"] = newType;
                        pokemons[name]["health"] = newHealth;
                        pokemons[name]["region"] = newRegion;
                        pokemons[name]["attributes"] = newAttributes;
                    }
                    break;
                case 's':
                    if (pokemons.Count == 0)
                    {
                        Console.WriteLine("The dictionary is empty.");
                        break;
                    }
                    pokemons = pokemons.OrderBy(pokemon => pokemon.Key).ToDictionary(pokemon => pokemon.Key, pokemon => pokemon.Value);
                    Console.WriteLine("The dictionary is now sorted.");
                    break;
            }
        }
    }
}

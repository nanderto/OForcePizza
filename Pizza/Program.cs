namespace Pizza
{
    using System.IO;
    using System;
    using System.Collections.Generic;
    using NDesk.Options;
    using Pizza.PizzaServer1;
    using Microsoft.Rest;
    using System.Net.Http;
    using PizzaServer.Models;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using PizzaServer;

    public class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            //args = new string[] {"--getPizzas"};
            //args = new string[] { "--getpizza", "-pi:2" };
            //args = new string[] { "--GetToppings" };
            //args = new string[] { "--AddTopping", "-t:Avocado" };
            //args = new string[] { "--AddTopping", "" }; //invalid
            //args = new string[] { "--DeleteTopping", "-ti:9" };
            //args = new string[] { "--DeletePizza", "8" };
            //args = new string[] { "--addPizza", "-n:peperoni", "-d:Pizza with Pepperoni" };
            //args = new string[] { "--addPizza", "-n:Cheese", "-d:Pizza with ust Cheese" };
            //args = new string[] { "--addToppingToPizza", "-pi:1", "-ti:10" };
            //args = new string[] { "--addToppingToPizza", "14", "5" };
            //args = new string[] { "--gettoppingsforpizza", "-pi:9" };
            //args = new string[] { "-h" };

            //bool show_help = false;

            var result = ParseArguments(args);

            OptionSet p = result.p;
            var operation = result.operation;
            List<string> extra = result.extra;
            List<string> toppings = result.toppings;
            string topping = result.topping;
            string toppingId = result.toppingId;
            string pizzaId = result.pizzaId;
            string name = result.name;
            string description = result.description;
            
            if (result.show_help)
            {
                ShowHelp(p);
                return;
            }

            if (result.extra.Count == 0)
            {
                Console.WriteLine("You must provide a operation --<operation> getPizzas, addPizza, addTopping, GetToppings, AddToppingToPizza");
                return;
            }

            string opp = result.extra[0].ToLower();

            switch (opp)
            {
                case "--getpizzas":
                    await GetPizzasAsync();
                    break;
                case "--getpizza":
                    pizzaId = checkId(extra, pizzaId);
                    await GetPizzaAsync(pizzaId);
                    break;
                case "--deletepizza":
                    pizzaId = checkId(extra, pizzaId);
                    await DeletePizzasAsync(pizzaId);
                    break;
                case "--addpizza":
                    await AddPizzaAsync(new Pizza { Name = name, Description = description });
                    break;
                case "--addtopping":
                    await ValidateToppingAndRunAdd(extra, topping);
                    break;
                case "--gettoppings":
                    await GetToppingsAsync();
                    break;
                case "--deletetopping":
                    toppingId = checkId(extra, toppingId);
                    await DeleteToppingAsync(toppingId);
                    break;
                case "--addtoppingtopizza":
                    toppingId = checkId(extra, toppingId, 2);
                    pizzaId = checkId(extra, pizzaId, 1);
                    await AddToppingToPizzaAsync(pizzaId, toppingId);
                    break;
                case "--gettoppingsforpizza":
                    pizzaId = checkId(extra, pizzaId, 1);
                    await GetToppingsForPizzaAsync(pizzaId);
                    break;
                default:
                    DisplayErrorMessage("You did not specify an operation to carry out try using -h for help");
                    break;
            }
        }

        private static async Task ValidateToppingAndRunAdd(List<string> extra, string topping)
        {
            topping = CheckTopping(extra, topping);
            if (string.IsNullOrEmpty(topping))
            {
                DisplayErrorMessage("You did not specify a topping corrrectly or at all");
            }
            else
            {
                await AddToppingAsync(topping);
            }
        }

        private async static Task GetPizzaAsync(string pizzaId)
        {
            DelegatingHandler[] start = null;
            using (var pizzaServer = new PizzaServer.PizzaServerRESTAPI(new AnonymousCredential(), start))
            {
                try
                {
                    var result = await pizzaServer.GetPizzaUsingGETAsync(long.Parse(pizzaId));

                    Console.WriteLine($"{result.Name} {result.Description}");

                    var toppings = await pizzaServer.GetToppingsUsingGETAsync(long.Parse(pizzaId));

                    Console.WriteLine("Includes the following toppings: ");
                    int i = 0;
                    foreach (var item in toppings)
                    {
                        i++;
                        Console.Write(item.Name);
                        if (i < toppings.Count) Console.Write(", ");
                    }
                }
                catch (HttpOperationException ex)
                {
                    DisplayErrorMessage($"Failed to get Pizza with PizzaId {pizzaId}\n {ex.Message}");
                }
                catch (Exception ex)
                {
                    DisplayErrorMessage($"Failed to get Pizza with PizzaId {pizzaId}\n {ex.Message}");
                }
            }
        }

        private static string checkId(List<string> extra, string inputId, int position = 1)
        {
            if (string.IsNullOrEmpty(inputId))
            {
                if (extra.Count > position)
                {
                    int id = 0;
                    if (int.TryParse(extra[position], out id))
                    {
                        inputId = extra[position];
                    }
                }
            }

            return inputId;
        }

        private static string CheckTopping(List<string> extra, string input, int position = 1)
        {
            if (string.IsNullOrEmpty(input))
            {
                if (extra.Count > position)
                {
                    int id = 0;
                    input = extra[position];     
                }
            }

            return input;
        }

        private async static Task DeletePizzasAsync(string pizzaId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"http://pizza-server.driver-ready.com/pizzas/{pizzaId}");
            }

            await GetPizzasAsync();
        }

        private async static Task DeleteToppingAsync(string id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"http://pizza-server.driver-ready.com/toppings/{id}");
            }

            var ret =  await GetToppingsAsync();
        }

        static async Task<string> GetToppingsAsync()
        {
            using (var client = new HttpClient())
            {

                string result = string.Empty;
   
                var response = await client.GetAsync("http://pizza-server.driver-ready.com/toppings/").ConfigureAwait(false);
                
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var body = SafeJsonConvert.DeserializeObject<IList<Topping>>(result);
                        Console.WriteLine("Toppings");
                        foreach (var item in body)
                        {
                            Console.WriteLine($"{item.Name} ({item.Id})");
                        }
                    }
                    catch (JsonException ex)
                    {
                    }

                }

                return result;
            }
        }

        private async static Task GetToppingsForPizzaAsync(string pizzaId)
        {
            DelegatingHandler[] start = null;
            using (var pizzaServer = new PizzaServer.PizzaServerRESTAPI(new AnonymousCredential(), start))
            {
                try
                {
                    var toppings = await pizzaServer.GetToppingsUsingGETAsync(long.Parse(pizzaId));
                    Console.WriteLine("Now includes the following toppings: ");
                    int i = 0;
                    foreach (var item in toppings)
                    {
                        i++;
                        Console.Write(item.Name);
                        if (i < toppings.Count) Console.Write(", ");
                    }
                }
                catch (HttpOperationException ex)
                {
                    DisplayErrorMessage($"Failed to get Toppings for Pizza with PizzaId {pizzaId}\n {ex.Message}");
                    DisplayErrorMessage($"Try running 'pizza --getPizzza {pizzaId}' to ensure it exists");
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Operation returned an invalid status code 'Created'")
                    {
                        //eat it
                    }
                    else
                    {
                        DisplayErrorMessage($"Failed to get Toppings for Pizza with PizzaId {pizzaId}\n {ex.Message}");
                    }
                }
            }
        }

        private async static Task AddToppingToPizzaAsync(string pizzaId, string toppingId)
        {
            DelegatingHandler[] start = null;
            using (var pizzaServer = new PizzaServer.PizzaServerRESTAPI(new AnonymousCredential(), start))
            {
                try
                {
                    await pizzaServer.AddToppingUsingPOSTAsync(long.Parse(pizzaId), long.Parse(toppingId));
                }
                catch (HttpOperationException ex)
                {
                    if (ex.Message == "Operation returned an invalid status code 'Created'")
                    {
                        //eat it
                        await GetToppingsForPizzaAsync(pizzaId);
                    }
                    else
                    {
                        DisplayErrorMessage($"Failed to add Topping to Pizza \n {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Operation returned an invalid status code 'Created'")
                    {
                        //eat it
                        await GetToppingsForPizzaAsync(pizzaId);
                    }
                    else
                    {
                        DisplayErrorMessage($"Failed to add Topping to Pizza \n {ex.Message}");
                    }
                }
            }
        }

        private static async Task AddToppingAsync(string name)
        {
            DelegatingHandler[] start = null;
            using (var pizzaServer = new PizzaServer.PizzaServerRESTAPI(new AnonymousCredential(), start))
            {
                try
                {
                    var result = await pizzaServer.CreateToppingUsingPOSTAsync(new Topping { Name = name });
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Operation returned an invalid status code 'Created'")
                    {
                        //eat it
                        Console.WriteLine($"Added Topping {name}");
                        await GetToppingsAsync();
                    }
                    else
                    {
                        DisplayErrorMessage("Failed to add Topping {name}");
                    }
                }
            }
        }

        private async static Task AddPizzaAsync(Pizza pizza)
        {
            DelegatingHandler[] start = null;
            using (var pizzaServer = new PizzaServer.PizzaServerRESTAPI(new AnonymousCredential(), start))
            {
                try
                {
                    var pizzas = await pizzaServer.CreatePizzaUsingPOSTAsync(pizza);
                    
                }
                catch (HttpOperationException ex)
                {
                    if (ex.Message == "Operation returned an invalid status code 'Created'")
                    {
                        //eat it
                        Console.WriteLine($"Added new Pizza {pizza.Name}");
                    }
                    else
                    {
                        DisplayErrorMessage("Failed to add new Pizzag {name}");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Operation returned an invalid status code 'Created'")
                    {
                    }
                    else
                    {
                        DisplayErrorMessage("Failed to add new Pizzag {name}");
                    }
                }
                finally
                {
                    await GetPizzasAsync();
                }
            }
        }

        private static void DisplayErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private async static Task GetPizzasAsync()
        {
            DelegatingHandler[] start = null;
            var pizzaServer = new PizzaServer.PizzaServerRESTAPI(new AnonymousCredential(), start);
            var pizzas = await pizzaServer.GetPizzasUsingGETWithHttpMessagesAsync();
            //var result = pizzas.Body;
            await DisplayResultAsync(pizzas.Body as IList<Pizza>);
        }

        private async static Task DisplayResultAsync(IList<Pizza> result)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ID  Name\t\t\t Description");
            Console.ResetColor();

            foreach (var item in result)
            {
                Console.WriteLine($"({item.Id,-1}) {item.Name,-25}{item.Description,-10}");
            }
            
        }

        public static (List<string> extra, List<string> operation, string topping, string pizzaId, string toppingId, string name, string description, bool show_help, OptionSet p, List<string> toppings) ParseArguments(string[] args)
        {
            List<string> extra1 = new List<string>();
            List<string> toppings1 = new List<string>();
            string topping1 = "";
            string pizzaId1 = "";
            string toppingId1 = "";
            string name1 = "";
            string description1= "";
            bool show_help = false;

            var p = new OptionSet()
                    {
                        {
                            "ts|toppings=", "toppings you want on your pizza.  Seperate by ,",
                            v => toppings1.Add(v)
                        },
                        { "t|topping=", "New Topping", v => topping1 = v },
                        //{ "o|operation=", "operation to run: getPizzas, addPizza, addTopping, GetToppings, AddTopping", v => operation1 = v },
                        { "pi|pizzaid=", "Pizza to change or delete", v => pizzaId1 = v },
                        { "n|name=", "Name of new Pizza or Topping", v => name1 = v },
                        { "d|description=", "Description of new Pizza or Topping", v => description1 = v },
                        { "ti|toppingid=", "Topping Id to add or delete from topping list or to a pizza", v => toppingId1 = v },
                        { "h|help", "show this message and exit", v => show_help = v != null }
                    };

            try
            {
                extra1 = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("Pizza error: Could not parse arguments: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `Pizza -h' for more information.");
                return (extra1, toppings1, topping1, pizzaId1, toppingId1, name1, description1, show_help, p, toppings1);
            }

            return (extra1, toppings1, topping1, pizzaId1, toppingId1, name1, description1, show_help, p, toppings1);
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: pizza --<operation> <parameterlist>");
            Console.WriteLine("oprations include: getPizzas, getpizza, addPizza, deletepizza, addTopping, GetToppings, AddTopping, deletetopping, addtoppingtopizza, gettoppingsforpizza");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}

namespace ReplaceToken
{
    using System.IO;
    using System;
    using System.Collections.Generic;
    using NDesk.Options;

    public class Program
    {
        static void Main(string[] args)
        {
            //args = new[] { "-t:something", "-tr:__XX__:something", "-tr:__YY__:_ZZ_", "-file:C:\\t\\test.txt", "-r", "XXXX" };
            //args = new string[] {"-h"};
            //args = new string[] { "-file:C:\\t\\test.txt", "-tr:__RelyingPartyConfiguration__:'Data Source=DALDBQV105.us.btswyn.com\\SQL2008;Initial Catalog=RelyingPartyConfiguration;User ID=rpConfigU'" };
            //args = new string[] { "-file:C:\\t\\test.txt", @"-tr:__ADAPI__%%%http://services.bgrsdev2.local/AD/", "-s:%%%" };
            // args = new string[] { "-file:C:\\t\\test.txt", @"-t:__ADAPI__", @"-r:http://services.bgrsdev2.local/AD/" };

            bool show_help = false;

            Console.WriteLine($"Pizza is about to parse the command line arguments");

            var result = ParseArguments(args);
            //, tokenReplacements, out p, token, replacement, file, show_help);
            OptionSet p = result.p;
            string operation = result.operation;
            List<string> toppings = result.toppings;


            if (result.show_help)
            {
                ShowHelp(p);
                return;
            }

            Console.WriteLine($"operation: {result.operation}");
            Console.WriteLine($"toppings: {result.toppings}");
            Console.WriteLine($"PizzaId: {result.pizzaId}");

            if (string.IsNullOrEmpty(result.operation))
            {
                Console.WriteLine("You must provide a operation -o:<operation>");
                return;
            }

            foreach (var topping in toppings)
            {
                //Console.WriteLine($"Replacing token: {toppings}");

                //var tokenAndReplacement = SplitToken(toppings, ":");

                //Console.WriteLine($"Token: {tokenAndReplacement[0]}");
                //Console.WriteLine($"Replacement: {tokenAndReplacement[1]}");
                //text = ReplaceToken(text, tokenAndReplacement[0], tokenAndReplacement[1]);
            }

            //if (info.Exists)
            //{
            //    Console.WriteLine($"File found: {info.FullName}");

            //    string text = string.Empty;
            //    try
            //    {
            //        text = File.ReadAllText(file);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine($"Could not read the file: {info.FullName}");
            //        Console.WriteLine();
            //        Console.WriteLine(e.Message);
            //        Console.WriteLine();
            //        Console.WriteLine(e.StackTrace);
            //        throw;
            //    }

            //    if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(replacement))
            //    {
            //        text = text.Replace(token, replacement);
            //    }
                
            //    string stringSepartor = ":";

            //    if (!string.IsNullOrEmpty(separator))
            //    {
            //        stringSepartor = separator;
            //    }

               

            //    try
            //    {
            //        File.WriteAllText(file, text);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine($"Could not write the file: {info.FullName}");
            //        Console.WriteLine();
            //        Console.WriteLine(e.Message);
            //        Console.WriteLine();
            //        Console.WriteLine(e.StackTrace);
            //        throw;
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"File Not found: {info.FullName}");
            //    throw new FileNotFoundException(info.FullName);
            //}
        }

        public static string ReplaceToken(string text, string token, string replacement)
        {
            if (text.Contains(token))
            {
                text = text.Replace(token, replacement);
            }
            else
            {
                Console.WriteLine($"Could not find the token: {token}");
            }

            return text;
        }

        public static string[] SplitToken(string tokenReplacement, string separator)
        {
            return tokenReplacement.Split(new string[1] { separator }, StringSplitOptions.None);
        }

        public static (string operation, string pizzaId, bool show_help, OptionSet p, List<string> toppings) ParseArguments(string[] args)
        {
            List<string> toppings = new List<string>();
            string operation = "";
            string pizzaId = "";
            bool show_help = false;

            var p = new OptionSet()
                    {
                        {
                            "t|topping=", "toppings ou want on your pizza. Seperate by :",
                            v => toppings.Add(v)
                        },
                        { "o|operation=", "operation to run: getPizzas, addPizza, addTopping, GetToppings, AddTopping", o => operation = o },
                        { "i|pizzaid=", "Pizza to add topping too", i => pizzaId = i },
                        { "h|help", "show this message and exit", v => show_help = v != null }
                    };

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("Pizza error: Could not parse arguments: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `Pizza --help' for more information.");
                return (operation, pizzaId, show_help, p, toppings);
            }

            return (operation, pizzaId, show_help, p, toppings);
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: pizza -o:operation: getPizzas, addPizza, addTopping, GetToppings, AddTopping");
            Console.WriteLine("You can add multiple toppings seperat with ':'");
            Console.WriteLine();
            //Console.WriteLine("Options:");
            //p.WriteOptionDescriptions(Console.Out);
        }

        private static string GetArg(string[] args, out string[] leftOver)
        {
            leftOver = new string[0];
            if (args.Length > 1)
            {
                leftOver = new string[args.Length - 1];
                for (int i = 1; i < args.Length; ++i)
                {
                    leftOver[i - 1] = args[i];
                } 
            }

            if (args.Length == 0)
            {
                return null;
            }

            return args[0];
        }
    }
}

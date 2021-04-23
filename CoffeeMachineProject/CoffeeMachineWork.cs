using CoffeeMachineProject.Models;
using CoffeeMachineProject.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoffeeMachineProject
{
    public class CoffeeMachineWork : IDisposable
    {
        private readonly CoffeeMachineService menuService;
        private readonly ApplicationDBContext context;
        private decimal userBalance;
        private int coffeeType;
        private bool firstLoop = true;
        private bool disposed = false;

        public CoffeeMachineWork()
        {
            context = new ApplicationDBContext();
            menuService = new CoffeeMachineService(context);
        }

        public void Start()
        {
            if (firstLoop)
            {
                Greeting();
                AddCoins();
            }
            else
            {
                UpdateBalance();
            }     

            ShowMenu();
            string coffeInput = Console.ReadLine();

            GetCoffeeInput(coffeInput);
            CheckStore();

            bool enoughMoney = CheckBalance();
            if (enoughMoney)
            {
                GiveCoffee();
                bool again = ChangeMassage();
                if (again)
                {
                    Console.Clear();
                    firstLoop = false;
                    Start();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your balance is not enough!!!");
                Console.ResetColor();
                GiveChange();
            }
        }

        bool ChangeMassage()
        {
            if (userBalance > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Your change is {userBalance}");
                Console.WriteLine("If you want to take it input - 0 -.");
                Console.WriteLine("If you want to choose another coffee input - 1 -.");
                Console.ResetColor();

                string input = Console.ReadLine();

                if (input == "0")
                {
                    GiveChange();
                    return false;
                }
                else if (input == "1")
                {
                    return true;
                }
                else
                {
                    GiveChange();
                    WrongInput();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You have no change.");
                Console.WriteLine("Thank you for using our services.");
                Console.ResetColor();
            }
            return false;
        }

        void GiveChange()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Take your change, thank you for using our services.");
            Console.ResetColor();

            userBalance = 0;
            UpdateBalance();
        }

        bool CheckBalance()
        {
            var price = menuService.GetCoffee(coffeeType).Price;
            return price <= userBalance;
        }

        void GiveCoffee()
        {
            menuService.MakeCoffee(coffeeType);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Your coffee is ready, enjoy it...");
            Console.ResetColor();

            userBalance -= menuService.GetCoffee(coffeeType).Price;
            UpdateBalance();
        }

        bool CheckStore()
        {
            var stores = menuService.GetStores();
            var portion = menuService.GetPortion(coffeeType);

            if (stores[0].IngredientTrack >= portion.WaterGram
                && stores[1].IngredientTrack >= portion.CoffeeBeansGram
                && stores[2].IngredientTrack >= portion.SugarGram)
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, trak is over, please, come later!!!");
                Console.ResetColor();
                if (userBalance > 0)
                {
                    GiveChange();
                }

                Environment.Exit(0);
                return false;
            }
        }

        void AddCoins()
        {
            var coins = menuService.GetCoins();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Your balance is {userBalance}, input coin and push enter (50,100,200,500).");
            Console.ResetColor();

            bool isValid = decimal.TryParse(Console.ReadLine(), out decimal result);
            if (!isValid)
            {
                WrongInput();
            }
            else
            {
                foreach (var item in coins)
                {
                    if (item.Value == result)
                    {
                        userBalance += result;
                        UpdateBalance();
                        return;
                    }
                }
                WrongInput();
            }
        }

        void GetCoffeeInput(string input)
        {
            bool isValid = int.TryParse(input, out int result);
            if (!isValid || result == 0)
            {
                WrongInput();
            }
            else
            {
                var coffee = menuService.GetCoffee(result);

                if (coffee.Price != 0)
                {
                    coffeeType = result;
                    return;
                }

                WrongInput();
            }
        }

        void Greeting()
        {
            UpdateBalance();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome to Coffee Machine!");
            Console.ResetColor();
        }

        void ShowMenu()
        {
            Console.WriteLine("Here are our coffee types, please, input the number of your chosen coffee and push enter.");
            Console.ResetColor();

            var coffees = menuService.GetCoffees();
            foreach (var item in coffees)
            {
                Console.WriteLine($"{item.Id}\t{item.Name,10}{item.Price,10}coin\t{item.Portion.WaterGram}g. water\t{item.Portion.CoffeeBeansGram}g. coffee\t{item.Portion.SugarGram}g. sugar");
            }
        }

        void WrongInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Your input is wrong, please start again!!!");
            Console.ResetColor();

            Console.Beep(600, 600);
            Console.Beep(600, 600);
            Console.Beep(600, 600);

            Thread.Sleep(500);
            Environment.Exit(0);
        }

        void UpdateBalance()
        {
            int oldX = Console.CursorLeft;
            int oldY = Console.CursorTop;
            int x = 0;
            int y = 0;

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(new string('-', 15));
            Console.WriteLine($"Balance : {userBalance}    ");
            Console.WriteLine(new string('-', 15));
            Console.SetCursorPosition(oldX, oldY + 3);
            Console.ResetColor();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }
    }
}

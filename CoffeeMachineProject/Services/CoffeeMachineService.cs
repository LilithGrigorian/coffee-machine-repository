using CoffeeMachineProject.Data;
using CoffeeMachineProject.Entites;
using CoffeeMachineProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoffeeMachineProject.Services
{
    public class CoffeeMachineService
    {
        private readonly IGenericRepository<Coffee> coffeeRepository;
        private readonly IGenericRepository<Portion> portionRepository;
        private readonly IGenericRepository<Coin> coinRepository;
        private readonly IGenericRepository<Store> storeRepository;

        public CoffeeMachineService(ApplicationDBContext context)
        {
            coffeeRepository = new GenericRepository<Coffee>(context);
            portionRepository = new GenericRepository<Portion>(context);
            coinRepository = new GenericRepository<Coin>(context);
            storeRepository = new GenericRepository<Store>(context);
        }

        public List<CoinModel> GetCoins()
        {
            List<CoinModel> coinModels = new List<CoinModel>();
            var coins = coinRepository.GetAll();

            if (coins != null)
            {
                foreach (var item in coins)
                {
                    coinModels.Add(new CoinModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Value = item.Value
                    });
                }
            }
            return coinModels;
        }

        public List<CoffeeModel> GetCoffees()
        {
            List<CoffeeModel> coffeeModels = new List<CoffeeModel>();
            var coffees = coffeeRepository.GetAll();

            if (coffees != null)
            {
                foreach (var item in coffees)
                {
                    coffeeModels.Add(new CoffeeModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        PortionId = item.PortionId,
                        Portion = GetPortion(item.Id)
                    }); ;
                }
            }
            return coffeeModels;
        }

        public CoffeeModel GetCoffee(int id)
        {
            CoffeeModel coffeeModel = new CoffeeModel();
            var coffee = coffeeRepository.GetById(id);

            if (coffee != null)
            {
                coffeeModel.Id = coffee.Id;
                coffeeModel.Name = coffee.Name;
                coffeeModel.PortionId = coffee.PortionId;
                coffeeModel.Price = coffee.Price;
            }
            return coffeeModel;
        }

        public PortionModel GetPortion(int id)
        {
            PortionModel portionModel = new PortionModel();
            var portion = portionRepository.GetById(id);

            if (portion != null)
            {
                portionModel.Id = portion.Id;
                portionModel.WaterGram = portion.WaterGram;
                portionModel.CoffeeBeansGram = portion.CoffeeBeansGram;
                portionModel.SugarGram = portion.SugarGram;
            }
            return portionModel;
        }

        public List<StoreModel> GetStores()
        {
            List<StoreModel> storeModels = new List<StoreModel>();
            var stores = storeRepository.GetAll();

            if (stores != null)
            {
                foreach (var item in stores)
                {
                    storeModels.Add(new StoreModel
                    {
                        Id = item.Id,
                        IngredientName = item.IngredientName,
                        IngredientTrack = item.IngredientTrack
                    }); ;
                }
            }
            return storeModels;
        }

        public void MakeCoffee(int id)
        {
            var portion = portionRepository.GetById(id);

            var waterStore = storeRepository.GetById(1);
            waterStore.IngredientTrack -= portion.WaterGram;
            waterStore.ModifiedDate = DateTime.Now;
            storeRepository.Update(waterStore);

            var coffeeBeansStore = storeRepository.GetById(2);
            coffeeBeansStore.IngredientTrack -= portion.CoffeeBeansGram;
            coffeeBeansStore.ModifiedDate = DateTime.Now;
            storeRepository.Update(coffeeBeansStore);

            var sugarStore = storeRepository.GetById(3);
            sugarStore.IngredientTrack -= portion.SugarGram;
            sugarStore.ModifiedDate = DateTime.Now;
            storeRepository.Update(sugarStore);

            storeRepository.Save();
        }
    }
}

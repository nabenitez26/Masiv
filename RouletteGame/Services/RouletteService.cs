using RouletteGame.DTO;
using RouletteGame.Models;
using RouletteGame.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteGame.Services
{
    public class RouletteService: IRouletteService
    {
        private readonly IRouletteRepository _rouletteRepository;
        public RouletteService(IRouletteRepository rouletteRepository)
        {
            _rouletteRepository = rouletteRepository;
        }
        public async Task<Roulette> AddNewRoulette() 
        {
            Roulette roulette = new();
            roulette.Id = 0;
            roulette.IsOpen = false;
            await _rouletteRepository.Save(roulette :roulette);
            return roulette;
        }
        public async Task<bool> OpenRoulette(int Id)
        { 
            Roulette roulette = await _rouletteRepository.GetById(Id:Id);
            if (roulette == null)
            {
                return false;
            }
            if (roulette.IsOpen)
            {
                return false; 
            }
            roulette.DateOpened = DateTime.Now;
            roulette.DateClosed = null;
            roulette.IsOpen = true;
            roulette.Bets = null;
            return await _rouletteRepository.Save(roulette: roulette);
        }
        public async Task<bool> Bet(int Id, BetCustomer betCustomer)
        {
            Roulette roulette = await _rouletteRepository.GetById(Id:Id);
            if (roulette == null)
            {
                return false;
            }
            if (!roulette.IsOpen)
            {
                return false;
            }
            List<BetCustomer> bets;
            if (roulette.Bets == null)
            {
                bets = new(); 
            }
            else 
            {
                bets = roulette.Bets;
            }
            bets.Add(betCustomer);
            roulette.Bets = bets;
            return await _rouletteRepository.Save(roulette: roulette);
        }
        public async Task<BetResponse> CloseRoulette(int Id)
        {
            BetResponse betResponse = new();
            Roulette roulette = await _rouletteRepository.GetById(Id: Id);
            if (roulette == null)
            {
                return null;
            }
            if (!roulette.IsOpen)
            {
                return null;
            }
            roulette.DateClosed = DateTime.Now;
            roulette.IsOpen = false;
            await _rouletteRepository.Save(roulette: roulette);
            short numberSelect = playRoulette();
            betResponse.NumberRoulette = numberSelect;
            betResponse.betCustomers = await ValidateWinners(numberSelect: numberSelect, betCustomers: roulette.Bets);
            return betResponse;
        }
        private short playRoulette()
        {
            var rand = new Random();
            return (short)rand.Next(1, 36);
        }
        private async Task<List<BetCustomer>> ValidateWinners(short numberSelect, List<BetCustomer> betCustomers)
        {
            List<BetCustomer> betCustomersResult = new();
            await Task.Run(() =>
            {
                foreach (var item in betCustomers)
                {
                    BetCustomer bet = new();
                    bet.Number = item.Number;
                    bet.Money = item.Money;
                    bet.UserId = item.UserId;
                    if (item.Number == numberSelect)
                    {
                        bet.IsWinner = true;
                        bet.TotalMoney = item.Money * 5;
                    }
                    else if (ValidateIsColorWinner(numberSelect: numberSelect, numberBet: item.Number))
                    {
                        bet.IsWinner = true;
                        bet.TotalMoney = item.Money * 1.8;
                    }
                    else
                    {
                        bet.IsWinner = false;
                        bet.TotalMoney = 0;
                    }
                    betCustomersResult.Add(bet);
                }
            });
            return betCustomersResult;
        }
        private bool ValidateIsColorWinner(int numberSelect, int numberBet)
        {
            bool IsEvenNumber = ValidateIsEvenNumber(number :numberSelect);
            if (IsEvenNumber && numberBet == 38)
                return true;
            if (!IsEvenNumber && numberBet == 37)
                return true;

            return false;
        }
        private bool ValidateIsEvenNumber(int number)
        {
            if ((number % 2) == 0)
                return true;
            else
                return false;
        }
        public async Task<List<Roulette>> GetAll() 
        {
            return await _rouletteRepository.GetAll();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouletteGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouletteGame.DTO;
using RouletteGame.Models;

namespace RouletteGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : Controller
    {
        private readonly IRouletteService _rouletteService;
        public RouletteController(IRouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }
        [HttpPost("AddNewRoulette")]
        public async Task<IActionResult> AddNewRoulette()
        {
            var newRoulette = await _rouletteService.AddNewRoulette();
            return Ok(new { newRoulette.Id });
        }
        [HttpPut("OpenRoulette/{id}")]
        public async Task<IActionResult> OpenRoulette([FromRoute(Name = "id")] int Id)
        {
            return Ok(await _rouletteService.OpenRoulette(Id:Id));
        }
        [HttpPost("Bet/{idRoulette}")]
        public async Task<IActionResult> BetAsync([FromHeader(Name = "userId")] int UserId, [FromRoute(Name = "idRoulette")] int Id,
           [FromBody] BetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Bad Request"
                });
            }
            BetCustomer betCustomer = new();
            betCustomer.UserId = UserId;
            betCustomer.Number = request.Number;
            betCustomer.Money = request.Money;
            if(await _rouletteService.Bet(Id: Id, betCustomer:betCustomer))
                return Ok(new { Message = "Se realizo su apuesta con exito" });
            else
                return BadRequest(new { Message = "Su apuesta no puede ser realizada, intente mas tarde" });

        }
        [HttpPut("CloseRoulette/{id}")]
        public async Task<IActionResult> CloseRouletteAsync([FromRoute(Name = "id")] int id)
        {
            var rouletteBets = await _rouletteService.CloseRoulette(id);
            if (rouletteBets != null)
                return Ok(rouletteBets);
            else
                return BadRequest(new { Message = "No es posible cerrar Apustas para la ruleta indicada" });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var listroulette = await _rouletteService.GetAll();
            return Ok(listroulette);
        }       
    }
}

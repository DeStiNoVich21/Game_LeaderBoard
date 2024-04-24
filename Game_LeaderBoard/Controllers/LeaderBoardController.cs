using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Game_LeaderBoard.MongoRepository.GenericRepository;
using Game_LeaderBoard.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
namespace Game_LeaderBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin")] 
    [EnableCors("_myAllowSpecificOrigins")]

    public class LeaderBoardController : Controller
    {
        private readonly IMongoRepository<Users> _mongoRepository;

        public LeaderBoardController(IMongoRepository<Users> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        [HttpGet("GetPlayer")]
        public async Task<ActionResult> GetPlayers(string id)
        {
            try
            {
                var user = await _mongoRepository.FindById(id);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("No player with such id");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBestPlayers")]
        public async Task<ActionResult> GetBestPlayers()
        {
            try
            {
                // Получаем всех игроков
                var allPlayers = await _mongoRepository.GetAllAsync();

                // Сортируем игроков по убыванию количества очков и берем первые 10
                var topPlayers = allPlayers.OrderByDescending(p => p.score).Take(10);

                return Ok(topPlayers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddPlayer")]
        public async Task<ActionResult> AddPlayer(Users user)
        {
            try
            {
                
                _mongoRepository.InsertOne(user);
                return Ok(user);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddPlayers")]
        
        public async Task<ActionResult> AddPlayers()
        {
            try
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

                StringBuilder codeBuilder = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    int index = random.Next(chars.Length);
                    codeBuilder.Append(chars[index]);
                }

                string generatedCode = codeBuilder.ToString();
                Users us = new Users 
                {
                    Id = "",
                    username = "Player"+generatedCode,
                    score = 50,
                    DeviceName = "Unknown"
                };
                _mongoRepository.InsertOne(us);
                return Ok(us);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateScore")]
        public async Task<ActionResult> UpdateScore(ScoreUpdateDto scores)
        {
            try
            {
                var devicecheck = await _mongoRepository.FindById(scores.Id);
                if (devicecheck == null)
                {
                    return BadRequest("Such player doesnt exist");
                }
                else
                {
                    Users user = new Users
                    {
                        Id = scores.Id,
                        username = devicecheck.username,
                        score = scores.score,
                        DeviceName = devicecheck.DeviceName

                    };
                    _mongoRepository.ReplaceOne(user);
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdatePlayer")]
        public async Task<ActionResult> UpdatePlayer(string id)
        {
            try
            {
                var devicecheck = await _mongoRepository.FindById(id);
                if (devicecheck == null)
                {
                    return BadRequest("Such player doesnt exist");
                }
                else
                {

                    _mongoRepository.ReplaceOne(devicecheck);
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Api.Attributes;
using Ipstset.Gamebase.Core.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GamebaseApi.Games
{

    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpException]
    [EnableCors("CorsPolicy")]
    public class GamesController : BaseController
    {
        private IGameService _gameService;

        #region Route Constants

        public const string GET_ALL_GAMES = "GetGames";
        public const string GET_GAMES_BY_ID = "GetGameById";
        public const string CREATE_GAME = "CreateGame";
        public const string UPDATE_GAME = "UpdateGame";
        public const string DELETE_GAME = "DeleteGame";

        #endregion

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet(Name = GET_ALL_GAMES)]
        public IActionResult Get([FromQuery]GetGamesModel parameters)
        {
            var result = _gameService.Get(GetGamesModel.Map(parameters));
            var model = result.Items.Select(GameModel.Map).ToList();
            return Ok(model);
        }

        [HttpGet("{id:int}", Name = GET_GAMES_BY_ID)]
        public IActionResult Get(int id)
        {
            var game = _gameService.Get(id);
            return Ok(GameModel.Map(game));
        }

        [HttpPost(Name = CREATE_GAME)]
        public IActionResult Post([FromBody] CreateGameModel createModel)
        {
            var game = _gameService.Create(CreateGameModel.Map(createModel));
            return CreatedAtRoute(GET_GAMES_BY_ID, new {id = game.Id}, GameModel.Map(game));
        }

        [HttpPut("{id:int}", Name = UPDATE_GAME)]
        public IActionResult Put(int id, [FromBody] UpdateGameModel model)
        {
            var command = UpdateGameModel.Map(model);
            command.Id = id;
            var game = _gameService.Update(command);
            return Ok(GameModel.Map(game));
        }

        [HttpDelete("{id:int}", Name = DELETE_GAME)]
        public IActionResult Delete(int id)
        {
            _gameService.Delete(id);
            return NoContent();
        }
    }

}
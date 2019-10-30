using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;
using Ipstset.Core.Exceptions;

namespace Ipstset.Gamebase.Core.Games
{
    public class GameService: IGameService
    {
        private AppUser _user;
        private IGameRepository _gameRepository;
        public GameService(AppUser user, IGameRepository gameRepository)
        {
            _user = user;
            _gameRepository = gameRepository;
        }

        public Game Get(int id)
        {
            var game = _gameRepository.Get(id);

            if (game == null)
                throw new NotFoundException($"Game not found with id: {id}");

            return game;
        }

        public IQueryResult<Game> Get(IQuery query)
        {
            //TODO apply filters for real
            var games = _gameRepository.Get(query);
            return new QueryResult<Game>
            {
                Items = games,
                TotalCount = games.Count
            };
        }

        public Game Create(IDomainCommand command)
        {
            if (!(command is CreateGameCommand createCommand))
                throw new ApplicationException("Invalid command type.");

            //validate
            var validator = new CreateGameValidator();
            if (validator.IsInvalid(createCommand))
                throw new ValidationException("Object not valid", validator.Errors);

            var game = new Game
            {
                Title = createCommand.Title,
                PlatformId = createCommand.PlatformId,
                Publisher = createCommand.Publisher,
                Developer = createCommand.Developer,
                DateReleased = createCommand.DateReleased
            };

            //persist
            _gameRepository.Add(game);

            //return new instance of game
            return Get(game.Id);
        }

        public Game Update(IDomainCommand command)
        {
            if (!(command is UpdateGameCommand updateCommand))
                throw new ApplicationException("Invalid command type.");

            var existing = _gameRepository.Get(updateCommand.Id);

            if (existing == null)
                throw new NotFoundException($"Game not found with id: {updateCommand.Id}");

            if (!_user.HasRole("admin"))
                throw new NotAuthorizedException();

            //validate
            var validator = new UpdateGameValidator();
            if (validator.IsInvalid(updateCommand))
                throw new ValidationException("Object not valid", validator.Errors);

            //update fields
            existing.Title = updateCommand.Title;
            existing.Publisher = updateCommand.Publisher;
            existing.Developer = updateCommand.Developer;
            existing.PlatformId = updateCommand.PlatformId;
            existing.DateReleased = updateCommand.DateReleased;

            //persist
            _gameRepository.Update(existing);

            //return new instance of game
            return Get(existing.Id);
        }

        public void Delete(int id)
        {
            var existing = _gameRepository.Get(id);
            if (existing == null) return;
            if (!_user.HasRole("admin"))
                throw new NotAuthorizedException();
            //persist
            _gameRepository.Delete(existing);
        }
    }
}

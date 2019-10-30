using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Games;

namespace Ipstset.Gamebase.Core.Platforms
{
    public class PlatformService : IPlatformService
    {
        private AppUser _user;
        private IPlatformRepository _platformRepository;
        public PlatformService(AppUser user, IPlatformRepository platformRepository)
        {
            _user = user;
            _platformRepository = platformRepository;
        }

        public Platform Get(int id)
        {
            var platform = _platformRepository.Get(id);

            if (platform == null)
                throw new NotFoundException($"Platform not found with id: {id}");

            return platform;
        }

        public IQueryResult<Platform> Get(IQuery query)
        {
            //TODO apply filters for real
            var platforms = _platformRepository.Get(query);
            return new QueryResult<Platform>
            {
                Items = platforms,
                TotalCount = platforms.Count
            };
        }

        public Platform Create(IDomainCommand command)
        {
            if (!(command is CreatePlatformCommand createCommand))
                throw new ApplicationException("Invalid command type.");

            //validate
            var validator = new CreatePlatformValidator();
            if (validator.IsInvalid(createCommand))
                throw new ValidationException("Object not valid", validator.Errors);

            var platform = new Platform
            {
                Name = createCommand.Name
            };

            //persist
            _platformRepository.Add(platform);

            //return new instance of platform
            return Get(platform.Id);
        }

        public Platform Update(IDomainCommand command)
        {
            if (!(command is UpdatePlatformCommand updateCommand))
                throw new ApplicationException("Invalid command type.");

            var existing = _platformRepository.Get(updateCommand.Id);

            if (existing == null)
                throw new NotFoundException($"Platform not found with id: {updateCommand.Id}");

            if (!_user.HasRole("admin"))
                throw new NotAuthorizedException();

            //validate
            var validator = new UpdatePlatformValidator();
            if (validator.IsInvalid(updateCommand))
                throw new ValidationException("Object not valid", validator.Errors);

            //update fields
            existing.Name = updateCommand.Name;

            //persist
            _platformRepository.Update(existing);

            //return new instance of platform
            return Get(existing.Id);
        }

        public void Delete(int id)
        {
            var existing = _platformRepository.Get(id);
            if (existing == null) return;
            if (!_user.HasRole("admin"))
                throw new NotAuthorizedException();
            //persist
            _platformRepository.Delete(existing);
        }
    }
}

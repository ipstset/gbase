using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core;
using Ipstset.Core.Exceptions;
using Ipstset.Core.Security;
using Ipstset.Gamebase.Core.Platforms;

namespace Ipstset.Gamebase.Core.Users
{
    public class UserService: IUserService
    {
        private AppUser _user;
        private IUserRepository _userRepository;
        public UserService(AppUser user, IUserRepository userRepository)
        {
            _user = user;
            _userRepository = userRepository;
        }

        public User Get(int id)
        {
            var user = _userRepository.Get(id);

            if (user == null)
                throw new NotFoundException($"User not found with id: {id}");

            return user;
        }

        public IQueryResult<User> Get(IQuery query)
        {
            //TODO apply filters for real
            var users = _userRepository.Get(query);
            return new QueryResult<User>
            {
                Items = users,
                TotalCount = users.Count
            };
        }

        public User Create(IDomainCommand command)
        {
            if (!(command is CreateUserCommand createCommand))
                throw new ApplicationException("Invalid command type.");

            //validate
            var validator = new CreateUserValidator();
            if (validator.IsInvalid(createCommand))
                throw new ValidationException("Object not valid", validator.Errors);

            //validate password before saving user
            var passwordValidator = new PasswordValidator();
            if (passwordValidator.IsInvalid(createCommand.Password))
                throw new ValidationException("Object not valid", passwordValidator.Errors);

            var saltedHash = SaltedHash.Create(createCommand.Password, new Salt().Value, Constants.HashKey);
            var user = new User
            {
                FirstName = createCommand.FirstName,
                LastName = createCommand.LastName,
                UserName = createCommand.UserName,
                Email = createCommand.Email,
                DateCreated = DateTime.Now,
                Roles = createCommand.Roles,
                Password = saltedHash.Hash,
                Salt = saltedHash.Salt
            };

            //persist
            _userRepository.Add(user);

            //return new instance of user
            return Get(user.Id);
        }

        public User Update(IDomainCommand command)
        {
            if (!(command is UpdateUserCommand updateCommand))
                throw new ApplicationException("Invalid command type.");

            var existing = _userRepository.Get(updateCommand.Id);

            if (existing == null)
                throw new NotFoundException($"User not found with id: {updateCommand.Id}");

            if (!_user.HasRole("admin") && _user.UserId != existing.Id)
                throw new NotAuthorizedException();

            //validate
            var validator = new UpdateUserValidator();
            if (validator.IsInvalid(updateCommand))
                throw new ValidationException("Object not valid", validator.Errors);

            //update password if present
            if (!string.IsNullOrWhiteSpace(updateCommand.Password))
            {
                //validate password 
                var passwordValidator = new PasswordValidator();
                if (passwordValidator.IsInvalid(updateCommand.Password))
                    throw new ValidationException("Object not valid", passwordValidator.Errors);

                var saltedHash = SaltedHash.Create(updateCommand.Password, new Salt().Value, Constants.HashKey);
                existing.Password = saltedHash.Hash;
                existing.Salt = saltedHash.Salt;               
            }

            existing.FirstName = updateCommand.FirstName;
            existing.LastName = updateCommand.LastName;
            existing.Email = updateCommand.Email;

            //update roles if present, and admin
            if (updateCommand.Roles.Any() && _user.HasRole("admin"))
            {
                existing.Roles = updateCommand.Roles;
            }

            //persist
            _userRepository.Update(existing);

            //return new instance of user
            return Get(existing.Id);
        }

        public void Delete(int id)
        {
            var existing = _userRepository.Get(id);
            if (existing == null) return;
            if (!_user.HasRole("admin") && _user.UserId != existing.Id)
                throw new NotAuthorizedException();
            //persist
            _userRepository.Delete(existing);
        }

        public User GetByUserName(string userName)
        {
            var user = _userRepository.GetByUserName(userName);

            if (user == null)
                throw new NotFoundException($"User not found with username: {userName}");

            return user;
        }
    }
}

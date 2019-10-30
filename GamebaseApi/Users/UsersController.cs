using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Api.Attributes;
using Ipstset.Gamebase.Core.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GamebaseApi.Users
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpException]
    [EnableCors("CorsPolicy")]
    public class UsersController : BaseController
    {
        private IUserService _userService;

        #region Route Constants

        public const string GET_ALL_USERS = "GetUsers";
        public const string GET_USERS_BY_ID = "GetUserById";
        public const string CREATE_USER = "CreateUser";
        public const string UPDATE_USER = "UpdateUser";
        public const string DELETE_USER = "DeleteUser";

        #endregion

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = GET_ALL_USERS)]
        public IActionResult Get([FromQuery]GetUsersModel parameters)
        {
            var result = _userService.Get(GetUsersModel.Map(parameters));
            var model = result.Items.Select(UserModel.Map).ToList();
            return Ok(model);
        }

        [HttpGet("{id:int}", Name = GET_USERS_BY_ID)]
        public IActionResult Get(int id)
        {
            var user = _userService.Get(id);
            return Ok(UserModel.Map(user));
        }

        [HttpPost(Name = CREATE_USER)]
        public IActionResult Post([FromBody] CreateUserModel createModel)
        {
            var user = _userService.Create(CreateUserModel.Map(createModel));
            return CreatedAtRoute(GET_USERS_BY_ID, new { id = user.Id }, UserModel.Map(user));
        }

        [HttpPut("{id:int}", Name = UPDATE_USER)]
        public IActionResult Put(int id, [FromBody] UpdateUserModel model)
        {
            var command = UpdateUserModel.Map(model);
            command.Id = id;
            var user = _userService.Update(command);
            return Ok(UserModel.Map(user));
        }

        [HttpDelete("{id:int}", Name = DELETE_USER)]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}

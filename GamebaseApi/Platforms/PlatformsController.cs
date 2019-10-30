using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Api.Attributes;
using Ipstset.Gamebase.Core.Platforms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GamebaseApi.Platforms
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpException]
    [EnableCors("CorsPolicy")]
    public class PlatformsController : BaseController
    {
        private IPlatformService _platformService;

        #region Route Constants

        public const string GET_ALL_PLATFORMS = "GetPlatforms";
        public const string GET_PLATFORMS_BY_ID = "GetPlatformById";
        public const string CREATE_PLATFORM = "CreatePlatform";
        public const string UPDATE_PLATFORM = "UpdatePlatform";
        public const string DELETE_PLATFORM = "DeletePlatform";

        #endregion

        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet(Name = GET_ALL_PLATFORMS)]
        public IActionResult Get([FromQuery]GetPlatformsModel parameters)
        {
            var result = _platformService.Get(GetPlatformsModel.Map(parameters));
            var model = result.Items.Select(PlatformModel.Map).ToList();
            return Ok(model);
        }

        [HttpGet("{id:int}", Name = GET_PLATFORMS_BY_ID)]
        public IActionResult Get(int id)
        {
            var platform = _platformService.Get(id);
            return Ok(PlatformModel.Map(platform));
        }

        [HttpPost(Name = CREATE_PLATFORM)]
        public IActionResult Post([FromBody] CreatePlatformModel createModel)
        {
            var platform = _platformService.Create(CreatePlatformModel.Map(createModel));
            return CreatedAtRoute(GET_PLATFORMS_BY_ID, new { id = platform.Id }, PlatformModel.Map(platform));
        }

        [HttpPut("{id:int}", Name = UPDATE_PLATFORM)]
        public IActionResult Put(int id, [FromBody] UpdatePlatformModel model)
        {
            var command = UpdatePlatformModel.Map(model);
            command.Id = id;
            var platform = _platformService.Update(command);
            return Ok(PlatformModel.Map(platform));
        }

        [HttpDelete("{id:int}", Name = DELETE_PLATFORM)]
        public IActionResult Delete(int id)
        {
            _platformService.Delete(id);
            return NoContent();
        }
    }
}

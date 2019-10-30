using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Api.Models;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Users;

namespace GamebaseApi.Users
{
    public class GetUsersModel : IQueryModel
    {
        public string Roles { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string[] Fields { get; set; }

        public static GetUsersQuery Map(GetUsersModel model)
        {
            try
            {
                return new GetUsersQuery
                {
                    //Roles = model.Roles != null ? Array.ConvertAll(model.Roles.Split(","), int.Parse) : null,
                    Roles = model.Roles?.Split(","),
                    Limit = model.Limit,
                    Offset = model.Offset,
                    Fields = model.Fields
                };
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }
        }
    }
}

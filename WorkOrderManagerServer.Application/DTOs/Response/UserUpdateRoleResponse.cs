using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManagerServer.Application.DTOs.Response
{
    public class UserUpdateRoleResponse
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public UserUpdateRoleResponse() => Errors = new List<string>();

        public UserUpdateRoleResponse(bool success = true) : this() => Success = success;
    }
}

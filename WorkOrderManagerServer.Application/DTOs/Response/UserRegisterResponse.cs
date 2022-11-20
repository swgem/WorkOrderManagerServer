using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrderManagerServer.Application.DTOs.Request;

namespace WorkOrderManagerServer.Application.DTOs.Response
{
    public class UserRegisterResponse
    {
        public bool Success { get; private set; }

        public List<string> Errors { get; set; }

        public UserRegisterResponse() => Errors = new List<string>();

        public UserRegisterResponse(bool success = true) : this() => Success = success;

        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}

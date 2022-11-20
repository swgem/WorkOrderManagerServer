using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManagerServer.Application.DTOs.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }

        public User(string id, string name, string roles)
        {
            Id = id;
            Name = name;
            Roles = roles;
        }
    }
}

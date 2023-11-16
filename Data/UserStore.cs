using APImod9kel6.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APImod9kel6.Data
{
    public class UserStore
    {
        public static List<UserDTO> userList = new List<UserDTO>
        {
             new UserDTO{Id=1, Username="fajar", Password="fajar"},
             new UserDTO{Id=2, Username="hana", Password="hana"},
             new UserDTO{Id=3, Username="rosyad", Password="rosyad"},
             new UserDTO{Id=4, Username="febian", Password="febian"}
        };
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmlibrary.Models
{
    
    
        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; } = "";
            public string Email { get; set; } = "";
            public string Role { get; set; } = "user"; 
        }
    

}

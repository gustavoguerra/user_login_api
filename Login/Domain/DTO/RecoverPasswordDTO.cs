﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Domain.DTO
{
    public class RecoverPasswordDTO
    {
        public string Email { get; set; }
        public int  SystemId { get; set; }
    }
}

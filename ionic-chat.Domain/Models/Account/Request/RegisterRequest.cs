﻿using ionic_chat.Domain.Models.Default.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ionic_chat.Domain.Models.Account.Request
{
    public class RegisterRequest: UserRequest
    {
        public string ConfirmPassword { get; set; }
    }
}

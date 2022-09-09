﻿using System;
using System.Collections.Generic;

namespace AuthenticationAPI.DataEntity
{
    public partial class UserRegistration
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? UserRole { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Auth
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int TokenLifetimeMinutes { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}

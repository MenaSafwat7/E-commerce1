﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record UsserRegesterDTO
    {
        [Required(ErrorMessage = "Display Name is required")]
        public string DisplayName { get; init; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; init; }
        [Required(ErrorMessage = "Display Name is required")]
        public string Password { get; init; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; init; }
        public string? PhoneNumber { get; init; }

    }
}

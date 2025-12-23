using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO;
    public record LoginRequest(
        string? Email,
        string? Password);
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackton.Domain.DTO
{
    public class ValidateBody
    {
        public Guid Id { get; set; }

        public string? Action { get; set; }

        public string? Note { get; set; }



    }
}

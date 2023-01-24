using System;
using System.Collections.Generic;
using System.Text;

namespace DB.DTO
{
    public class RulesDto
    {
        public int Cards { get; set; }
        public bool IsTransfer { get; set; }
        public bool IsThrowaway { get; set; }
        public bool IsOpen { get; set; }
    }
}

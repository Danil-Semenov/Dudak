using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    public class Result
    {
        [JsonPropertyName("result")]
        public object Value { get; set; }
    }
}

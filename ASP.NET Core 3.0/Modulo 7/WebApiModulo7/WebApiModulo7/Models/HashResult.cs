using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModulo7.Models
{
    public class HashResult
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}

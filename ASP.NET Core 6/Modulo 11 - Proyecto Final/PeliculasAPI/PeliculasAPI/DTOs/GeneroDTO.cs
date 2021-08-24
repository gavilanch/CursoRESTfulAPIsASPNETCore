using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs
{
    public class GeneroDTO: GeneroCreacionDTO
    {
        public int Id { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiWeb.Modelos
{
    public class Cliente
    {
        [Key]
        public String cedula {  get; set; }
        public String name { get; set; }
        public String mail { get; set;}
        public String phone { get; set;}
        public int age { get; set;}
        public String image { get; set;}
    }
}

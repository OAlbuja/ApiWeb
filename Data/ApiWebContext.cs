using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Modelos;

namespace ApiWeb.Data
{
    public class ApiWebContext : DbContext
    {
        public ApiWebContext (DbContextOptions<ApiWebContext> options)
            : base(options)
        {
        }

        public DbSet<ApiWeb.Modelos.Cliente> Cliente { get; set; } = default!;

        public DbSet<ApiWeb.Modelos.Producto>? Producto { get; set; }

        public DbSet<ApiWeb.Modelos.Factura>? Factura { get; set; }

        public DbSet<ApiWeb.Modelos.DetallesFactura>? DetallesFactura { get; set; }
    }
}

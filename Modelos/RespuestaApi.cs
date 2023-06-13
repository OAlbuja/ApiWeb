using ApiWeb.Modelos;
using System.Collections;

namespace ApiWeb.Models
{
    public class RespuestaApi
    {
        public string httpStatusCode { get; set; }
        public Producto Nproducto { get; set; }
        public List<Producto> Lproductos { get; set; }
        public Cliente Ncliente { get; set; }
        public List<Cliente> Lclientes { get; set;}

        public Factura Nfactura { get; set; }
        public List<Factura> Lfacturas { get; set; }
        public DetallesFactura Ndetallefact { get; set; }
        public List<DetallesFactura> Ldetallefact { get; set; }
    }
}

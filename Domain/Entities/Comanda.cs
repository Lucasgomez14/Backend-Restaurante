namespace Domain.Entities
{
    public class Comanda
    {
        public Guid ComandaId { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
        public int FormaEntregaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<ComandaMercaderia> ComandasMercaderia { get; set; }
    }
}

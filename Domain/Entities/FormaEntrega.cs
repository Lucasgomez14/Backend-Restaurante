namespace Domain.Entities
{
    public class FormaEntrega
    {
        public int FormaEntregaId { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Comanda> Comandas { get; set; }
    }
}

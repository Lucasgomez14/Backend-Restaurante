namespace Application.Response
{
    public class ComandaGetResponse
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public List<MercaderiaGetResponse> mercaderias { get; set; }
        public FormaEntregaResponse formaEntrega { get; set; }

        public int PrecioTotal { get; set; }
        public string Fecha { get; set; }
    }
}

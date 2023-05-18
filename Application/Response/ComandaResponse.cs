namespace Application.Response
{
    public class ComandaResponse
    {
        public Guid ComandaId { get; set; }
        public List<ComandaMercaderiaResponse> ListaComandaMercaderiaResponse { get; set; }
        public FormaEntregaResponse FormaEntregaResponse { get; internal set; }
        public int PrecioTotal { get; set; }
        public string Fecha { get; set; }

    }
}

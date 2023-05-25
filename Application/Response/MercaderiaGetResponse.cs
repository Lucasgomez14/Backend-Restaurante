namespace Application.Response
{
    public class MercaderiaGetResponse
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double Precio { get; set; }
        public TipoMercaderiaResponse tipo { get; set; }
        public string imagen { get; set; }
    }
}

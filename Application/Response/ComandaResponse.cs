namespace Application.Response
{
    public class ComandaResponse
    {
        public string id { get; set; }
        public List<MercaderiaComandaResponse> mercaderias { get; set; }
        public FormaEntrega formaEntrega { get; internal set; }
        public double total { get; set; }
        public string fecha { get; set; }

    }
}

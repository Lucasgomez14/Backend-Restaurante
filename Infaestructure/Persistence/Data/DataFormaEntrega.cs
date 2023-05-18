using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infaestructure.Persistence.Data
{
    public class DataFormaEntrega : IEntityTypeConfiguration<FormaEntrega>
    {
        public void Configure(EntityTypeBuilder<FormaEntrega> builder)
        {
            builder.HasData(
            new FormaEntrega { FormaEntregaId = 1, Descripcion = "Salón" },
            new FormaEntrega { FormaEntregaId = 2, Descripcion = "PedidosYa" },
            new FormaEntrega { FormaEntregaId = 3, Descripcion = "Delivery" }
            );
        }
    }
}

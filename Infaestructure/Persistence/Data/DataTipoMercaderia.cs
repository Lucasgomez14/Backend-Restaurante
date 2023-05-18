using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infaestructure.Persistence.Data
{
    public class DataTipoMercaderia : IEntityTypeConfiguration<TipoMercaderia>
    {
        public void Configure(EntityTypeBuilder<TipoMercaderia> builder)
        {
            builder.HasData(
            new TipoMercaderia { TipoMercaderiaId = 1, Descripcion = "Pastas" },
            new TipoMercaderia { TipoMercaderiaId = 2, Descripcion = "Minutas" },
            new TipoMercaderia { TipoMercaderiaId = 3, Descripcion = "Entradas" },
            new TipoMercaderia { TipoMercaderiaId = 4, Descripcion = "Parrilla" },
            new TipoMercaderia { TipoMercaderiaId = 5, Descripcion = "Pizzas" },
            new TipoMercaderia { TipoMercaderiaId = 6, Descripcion = "Sandwich" },
            new TipoMercaderia { TipoMercaderiaId = 7, Descripcion = "Ensalada" },
            new TipoMercaderia { TipoMercaderiaId = 8, Descripcion = "Bebidas" },
            new TipoMercaderia { TipoMercaderiaId = 9, Descripcion = "Cerveza artesanal" },
            new TipoMercaderia { TipoMercaderiaId = 10, Descripcion = "Postres" }
            );
        }
    }
}

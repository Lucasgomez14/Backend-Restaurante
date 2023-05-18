﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infaestructure.Persistence.Data
{
    public class DataMercaderia : IEntityTypeConfiguration<Mercaderia>
    {
        public void Configure(EntityTypeBuilder<Mercaderia> builder)
        {
            builder.HasData(
                new Mercaderia { MercaderiaID = 1, Nombre = "Spaghettis", TipoMercaderiaId = 1, Precio = 1000, Ingredientes = "Spaghettis de harina,salsa fileto, carne", Preparacion = "Se ponen los fideos a hervir en una olla y luego se cubre con la salsa bolognesa", Imagen = "https://imagesvc.meredithcorp.io/v3/mm/image?q=60&c=sc&poi=face&w=2000&h=1000&url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F21%2F2018%2F02%2F14%2Frecetas-4115-spaghetti-boloesa-facil-2000.jpg" },
                        new Mercaderia { MercaderiaID = 2, Nombre = "Ñoquis", TipoMercaderiaId = 1, Precio = 1000, Ingredientes = "Ñoquis de papa,salsa fileto, carne", Preparacion = "Se ponen los ñoquis a hervir en una olla y luego se cubre con la salsa bolognesa", Imagen = "https://www.laitalianapastas.com/wp-content/uploads/2017/05/noquispapa.jpg" },
                        new Mercaderia { MercaderiaID = 3, Nombre = "Milanesa de carne", TipoMercaderiaId = 2, Precio = 1200, Ingredientes = "Milanesa de carne, papas", Preparacion = "Se ponen en el horno las milanesas y luego las papas para salir bien doraditas", Imagen = "https://www.clarin.com/img/2021/12/15/la-milanesa-con-papas-fritas___OFsTMm3-P_2000x1500__1.jpg" },
                        new Mercaderia { MercaderiaID = 4, Nombre = "Milanesa de pollo", TipoMercaderiaId = 2, Precio = 1200, Ingredientes = "Milanesa de pollo, papas", Preparacion = "Se ponen en el horno las milanesas y luego las papas para salir bien doraditas", Imagen = "https://i.ytimg.com/vi/OP74S9ffEyo/maxresdefault.jpg" },
                        new Mercaderia { MercaderiaID = 5, Nombre = "Bastones de muzzarella", TipoMercaderiaId = 3, Precio = 700, Ingredientes = "muzarella,rebozado de pan rallado", Preparacion = "Se rebozan muzarella en pan rallado y se ponen en la freidora para que salgan crocantes", Imagen = "https://www.clarin.com/img/2020/07/21/EVsX1RphU_1200x630__1.jpg" },
                        new Mercaderia { MercaderiaID = 6, Nombre = "Papas fritas", TipoMercaderiaId = 3, Precio = 600, Ingredientes = "Papas fritas", Preparacion = "Se ponen las papas fritas en la freidora para que salgan lo más doradas posibles", Imagen = "https://www.petitchef.es/imgupl/recipe-step/460076s2122991.jpg" },
                        new Mercaderia { MercaderiaID = 7, Nombre = "Lomo", TipoMercaderiaId = 4, Precio = 2500, Ingredientes = "Lomo", Preparacion = "Se pone el lomo en la parrilla hasta que quedé lo más jugoso posible", Imagen = "https://habemusasado.com.ar/wp-content/uploads/2021/03/lomo-a-la-parrilla-con-papas-a-la-provenzal.jpg" },
                        new Mercaderia { MercaderiaID = 8, Nombre = "Vacío", TipoMercaderiaId = 4, Precio = 2000, Ingredientes = "Vacío", Preparacion = "Se pone el lomo en la parrilla hasta que quedé lo más jugoso posible del chef", Imagen = "https://hacerasado.com.ar/wp-content/uploads/2020/05/vacio-asado.jpg" },
                        new Mercaderia { MercaderiaID = 9, Nombre = "Pizza de muzarella", TipoMercaderiaId = 5, Precio = 1700, Ingredientes = "Harina, Salsa de tomate, tomate, queso, aceitunas", Preparacion = "Se amaza la masa, se le coloca muzzarela, el tomate y luego se manda al horno hasta que esté cocida", Imagen = "https://irecetasfaciles.com/wp-content/uploads/2019/08/pizza-de-jamon-queso-y-tocino.jpg" },
                        new Mercaderia { MercaderiaID = 10, Nombre = "Pizza napolitana", TipoMercaderiaId = 5, Precio = 1900, Ingredientes = "Harina, Salsa de tomate, tomate, queso, jamón, aceitunas", Preparacion = "Se amaza la masa, se le coloca muzzarela, el tomate y luego se manda al horno hasta que esté cocida", Imagen = "https://www.johaprato.com/files/styles/flexslider_full/public/pizza-napolitana.jpg?itok=N0LPyO1C" },
                        new Mercaderia { MercaderiaID = 11, Nombre = "Sandwich de milanesa", TipoMercaderiaId = 6, Precio = 1500, Ingredientes = "Pan, milanesa de pollo/carne, lechuga, tomate, queso, jamón", Preparacion = "Se coloca la milanesa entre dos panes y luego se coloca tomate, lechuga, jamon y queso", Imagen = "https://deliveryolavarria.com/wp-content/uploads/classified-listing/2021/06/Sandwich-de-Milanesa-Completo-Rotiseria-Lanueva-Delivery-Olavarria.jpg" },
                        new Mercaderia { MercaderiaID = 12, Nombre = "Sandwich de lomo", TipoMercaderiaId = 6, Precio = 2500, Ingredientes = "Pan, lomo, lechuga, tomate, queso, jamón", Preparacion = "Se coloca el lomo entre dos panes y luego se coloca tomate, lechuga, jamon y queso", Imagen = "https://laverdadonline.com/wp-content/uploads/2021/06/lomito-1280x720.jpg" },
                        new Mercaderia { MercaderiaID = 13, Nombre = "Ensalada jardinera", TipoMercaderiaId = 7, Precio = 1000, Ingredientes = "papa, zanahoria, arvejas, zapallo", Preparacion = "Se coloca papa, zanahoria, arvejas y el zapallo y se mezclan", Imagen = "https://media.minutouno.com/p/47b326b7fd5e666efcda98c58264ef19/adjuntos/150/imagenes/037/428/0037428131/610x0/smart/jardinera-retirada-anmat.jpg" },
                        new Mercaderia { MercaderiaID = 14, Nombre = "Ensalada rusa", TipoMercaderiaId = 7, Precio = 1000, Ingredientes = "papa, mayonesa, arvejas", Preparacion = "Se coloca papa, mayonesa, arvejas y se mezcla", Imagen = "https://recetas123.net/wp-content/uploads/Ensalada-rusa.jpg" },
                        new Mercaderia { MercaderiaID = 15, Nombre = "Coca Cola", TipoMercaderiaId = 8, Precio = 350, Ingredientes = "Coca cola", Preparacion = "Se sirve en un vaso coca cola", Imagen = "https://http2.mlstatic.com/D_NQ_NP_940822-MLA31035814287_062019-O.webp" },
                        new Mercaderia { MercaderiaID = 16, Nombre = "Agua", TipoMercaderiaId = 8, Precio = 350, Ingredientes = "Agua", Preparacion = "Se sirve en un vaso agua", Imagen = "https://images.ecestaticos.com/hGWahS40nsMxN3rj5xvDbv2689I=/118x0:2004x1413/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F2d3%2F829%2Fca8%2F2d3829ca834316ffe100eb7277eb0d29.jpg" },
                        new Mercaderia { MercaderiaID = 17, Nombre = "Cerveza negra", TipoMercaderiaId = 9, Precio = 350, Ingredientes = "Cerveza negra", Preparacion = "Se sirve en un vaso cerveza negra", Imagen = "https://thumbs.dreamstime.com/b/vaso-de-cerveza-negra-aislado-sobre-fondo-blanco-contiene-trazado-recorte-marr%C3%B3n-212794983.jpg" },
                        new Mercaderia { MercaderiaID = 18, Nombre = "Cerveza rubia", TipoMercaderiaId = 9, Precio = 350, Ingredientes = "Cerveza rubia", Preparacion = "Se sirve en un vaso cerveza rubia", Imagen = "https://www.seekpng.com/png/detail/239-2397428_descripcin-vaso-de-cerveza-rubia.png" },
                        new Mercaderia { MercaderiaID = 19, Nombre = "Flan", TipoMercaderiaId = 10, Precio = 800, Ingredientes = "Huevo, leche, azúcar", Preparacion = "mezcla el huevo con caramelo y se manda al horno en una flanera", Imagen = "https://www.lactaidenespanol.com/sites/lactaid_us/files/recipe-images/easy_flan2.jpg" },
                        new Mercaderia { MercaderiaID = 20, Nombre = "Helado", TipoMercaderiaId = 10, Precio = 800, Ingredientes = "Helado", Preparacion = "Se sirven bochas de helado comprado en un boul", Imagen = "https://d3ugyf2ht6aenh.cloudfront.net/stores/001/301/446/products/129-tamano-web1-2b57be5f700bad859115982923788478-640-0.jpg" }
                );
        }
    }
}

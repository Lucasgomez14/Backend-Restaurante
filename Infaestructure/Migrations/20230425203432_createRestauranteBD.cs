using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infaestructure.Migrations
{
    /// <inheritdoc />
    public partial class createRestauranteBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormasEntrega",
                columns: table => new
                {
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasEntrega", x => x.FormaEntregaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderia",
                columns: table => new
                {
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderia", x => x.TipoMercaderiaId);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComandaId);
                    table.ForeignKey(
                        name: "FK_Comanda_FormasEntrega_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormasEntrega",
                        principalColumn: "FormaEntregaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderia",
                columns: table => new
                {
                    MercaderiaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Ingredientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preparacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderia", x => x.MercaderiaID);
                    table.ForeignKey(
                        name: "FK_Mercaderia_TipoMercaderia_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderia",
                        principalColumn: "TipoMercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderia",
                columns: table => new
                {
                    ComandaMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MercaderiaId = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderia", x => x.ComandaMercaderiaId);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "ComandaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Mercaderia_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderia",
                        principalColumn: "MercaderiaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormasEntrega",
                columns: new[] { "FormaEntregaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Salón" },
                    { 2, "PedidosYa" },
                    { 3, "Delivery" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderia",
                columns: new[] { "TipoMercaderiaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Pastas" },
                    { 2, "Minutas" },
                    { 3, "Entradas" },
                    { 4, "Parrilla" },
                    { 5, "Pizzas" },
                    { 6, "Sandwich" },
                    { 7, "Ensalada" },
                    { 8, "Bebidas" },
                    { 9, "Cerveza artesanal" },
                    { 10, "Postres" }
                });

            migrationBuilder.InsertData(
                table: "Mercaderia",
                columns: new[] { "MercaderiaID", "Imagen", "Ingredientes", "Nombre", "Precio", "Preparacion", "TipoMercaderiaId" },
                values: new object[,]
                {
                    { 1, "https://imagesvc.meredithcorp.io/v3/mm/image?q=60&c=sc&poi=face&w=2000&h=1000&url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F21%2F2018%2F02%2F14%2Frecetas-4115-spaghetti-boloesa-facil-2000.jpg", "Spaghettis de harina,salsa fileto, carne", "Spaghettis", 1000, "Se ponen los fideos a hervir en una olla y luego se cubre con la salsa bolognesa", 1 },
                    { 2, "https://www.laitalianapastas.com/wp-content/uploads/2017/05/noquispapa.jpg", "Ñoquis de papa,salsa fileto, carne", "Ñoquis", 1000, "Se ponen los ñoquis a hervir en una olla y luego se cubre con la salsa bolognesa", 1 },
                    { 3, "https://www.clarin.com/img/2021/12/15/la-milanesa-con-papas-fritas___OFsTMm3-P_2000x1500__1.jpg", "Milanesa de carne, papas", "Milanesa de carne", 1200, "Se ponen en el horno las milanesas y luego las papas para salir bien doraditas", 2 },
                    { 4, "https://i.ytimg.com/vi/OP74S9ffEyo/maxresdefault.jpg", "Milanesa de pollo, papas", "Milanesa de pollo", 1200, "Se ponen en el horno las milanesas y luego las papas para salir bien doraditas", 2 },
                    { 5, "https://www.clarin.com/img/2020/07/21/EVsX1RphU_1200x630__1.jpg", "muzarella,rebozado de pan rallado", "Bastones de muzzarella", 700, "Se rebozan muzarella en pan rallado y se ponen en la freidora para que salgan crocantes", 3 },
                    { 6, "https://www.petitchef.es/imgupl/recipe-step/460076s2122991.jpg", "Papas fritas", "Papas fritas", 600, "Se ponen las papas fritas en la freidora para que salgan lo más doradas posibles", 3 },
                    { 7, "https://habemusasado.com.ar/wp-content/uploads/2021/03/lomo-a-la-parrilla-con-papas-a-la-provenzal.jpg", "Lomo", "Lomo", 2500, "Se pone el lomo en la parrilla hasta que quedé lo más jugoso posible", 4 },
                    { 8, "https://hacerasado.com.ar/wp-content/uploads/2020/05/vacio-asado.jpg", "Vacío", "Vacío", 2000, "Se pone el lomo en la parrilla hasta que quedé lo más jugoso posible del chef", 4 },
                    { 9, "https://irecetasfaciles.com/wp-content/uploads/2019/08/pizza-de-jamon-queso-y-tocino.jpg", "Harina, Salsa de tomate, tomate, queso, aceitunas", "Pizza de muzarella", 1700, "Se amaza la masa, se le coloca muzzarela, el tomate y luego se manda al horno hasta que esté cocida", 5 },
                    { 10, "https://www.johaprato.com/files/styles/flexslider_full/public/pizza-napolitana.jpg?itok=N0LPyO1C", "Harina, Salsa de tomate, tomate, queso, jamón, aceitunas", "Pizza napolitana", 1900, "Se amaza la masa, se le coloca muzzarela, el tomate y luego se manda al horno hasta que esté cocida", 5 },
                    { 11, "https://deliveryolavarria.com/wp-content/uploads/classified-listing/2021/06/Sandwich-de-Milanesa-Completo-Rotiseria-Lanueva-Delivery-Olavarria.jpg", "Pan, milanesa de pollo/carne, lechuga, tomate, queso, jamón", "Sandwich de milanesa", 1500, "Se coloca la milanesa entre dos panes y luego se coloca tomate, lechuga, jamon y queso", 6 },
                    { 12, "https://laverdadonline.com/wp-content/uploads/2021/06/lomito-1280x720.jpg", "Pan, lomo, lechuga, tomate, queso, jamón", "Sandwich de lomo", 2500, "Se coloca el lomo entre dos panes y luego se coloca tomate, lechuga, jamon y queso", 6 },
                    { 13, "https://media.minutouno.com/p/47b326b7fd5e666efcda98c58264ef19/adjuntos/150/imagenes/037/428/0037428131/610x0/smart/jardinera-retirada-anmat.jpg", "papa, zanahoria, arvejas, zapallo", "Ensalada jardinera", 1000, "Se coloca papa, zanahoria, arvejas y el zapallo y se mezclan", 7 },
                    { 14, "https://recetas123.net/wp-content/uploads/Ensalada-rusa.jpg", "papa, mayonesa, arvejas", "Ensalada rusa", 1000, "Se coloca papa, mayonesa, arvejas y se mezcla", 7 },
                    { 15, "https://http2.mlstatic.com/D_NQ_NP_940822-MLA31035814287_062019-O.webp", "Coca cola", "Coca Cola", 350, "Se sirve en un vaso coca cola", 8 },
                    { 16, "https://images.ecestaticos.com/hGWahS40nsMxN3rj5xvDbv2689I=/118x0:2004x1413/1200x900/filters:fill(white):format(jpg)/f.elconfidencial.com%2Foriginal%2F2d3%2F829%2Fca8%2F2d3829ca834316ffe100eb7277eb0d29.jpg", "Agua", "Agua", 350, "Se sirve en un vaso agua", 8 },
                    { 17, "https://thumbs.dreamstime.com/b/vaso-de-cerveza-negra-aislado-sobre-fondo-blanco-contiene-trazado-recorte-marr%C3%B3n-212794983.jpg", "Cerveza negra", "Cerveza negra", 350, "Se sirve en un vaso cerveza negra", 9 },
                    { 18, "https://www.seekpng.com/png/detail/239-2397428_descripcin-vaso-de-cerveza-rubia.png", "Cerveza rubia", "Cerveza rubia", 350, "Se sirve en un vaso cerveza rubia", 9 },
                    { 19, "https://www.lactaidenespanol.com/sites/lactaid_us/files/recipe-images/easy_flan2.jpg", "Huevo, leche, azúcar", "Flan", 800, "mezcla el huevo con caramelo y se manda al horno en una flanera", 10 },
                    { 20, "https://d3ugyf2ht6aenh.cloudfront.net/stores/001/301/446/products/129-tamano-web1-2b57be5f700bad859115982923788478-640-0.jpg", "Helado", "Helado", 800, "Se sirven bochas de helado comprado en un boul", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_FormaEntregaId",
                table: "Comanda",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_ComandaId",
                table: "ComandaMercaderia",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_MercaderiaId",
                table: "ComandaMercaderia",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderia_TipoMercaderiaId",
                table: "Mercaderia",
                column: "TipoMercaderiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderia");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Mercaderia");

            migrationBuilder.DropTable(
                name: "FormasEntrega");

            migrationBuilder.DropTable(
                name: "TipoMercaderia");
        }
    }
}

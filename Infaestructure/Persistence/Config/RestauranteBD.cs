using Domain.Entities;
using Infaestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infaestructure.Persistence.Config
{
    public class RestauranteBD :DbContext
    {

        public DbSet<Mercaderia> Mercaderia { get; set; }
        public DbSet<TipoMercaderia> TipoMercaderia { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderia { get; set; }
        public DbSet<FormaEntrega> FormasEntrega { get; set; }
        public DbSet<Comanda> Comanda { get; set; }


        public RestauranteBD(DbContextOptions<RestauranteBD> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Comanda
            modelBuilder.Entity<Comanda>()
                .HasMany<ComandaMercaderia>(com => com.ComandasMercaderia)
                .WithOne(commer => commer.Comanda)
                .HasForeignKey(commer => commer.ComandaId)
                .IsRequired();

            //FormaEntrega
            modelBuilder.Entity<FormaEntrega>()
                .HasMany<Comanda>(foren => foren.Comandas)
                .WithOne(com => com.FormaEntrega)
                .HasForeignKey(com => com.FormaEntregaId)
                .IsRequired();
            modelBuilder.Entity<FormaEntrega>().Property(foren => foren.Descripcion).HasMaxLength(50).IsRequired();
            modelBuilder.ApplyConfiguration(new DataFormaEntrega());

            //Mercaderia
            modelBuilder.Entity<Mercaderia>()
                .HasMany<ComandaMercaderia>(mer => mer.ComandasMercaderia)
                .WithOne(commer => commer.Mercaderia)
                .HasForeignKey(commer => commer.MercaderiaId)
                .IsRequired();
            modelBuilder.ApplyConfiguration(new DataMercaderia());

            modelBuilder.Entity<Mercaderia>(entity =>
            {
                entity.Property(mer => mer.Nombre)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(mer => mer.Ingredientes)
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(mer => mer.Preparacion)
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(mer => mer.Imagen)
                .HasMaxLength(255)
                .IsRequired();

            });

            //TipoMercaderia
            modelBuilder.Entity<TipoMercaderia>()
                .HasMany<Mercaderia>(tipmer => tipmer.Mercaderias)
                .WithOne(mer => mer.TipoMercaderia)
                .HasForeignKey(mer => mer.TipoMercaderiaId);
            modelBuilder.Entity<TipoMercaderia>().Property(tipmer => tipmer.Descripcion).HasMaxLength(50).IsRequired();
            modelBuilder.ApplyConfiguration(new DataTipoMercaderia());


        }
    }
}

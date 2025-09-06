using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prueba_ApiBTW.Models;

public partial class appcontext : DbContext
{
    public appcontext()
    {
    }

    public appcontext(DbContextOptions<appcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Prodcuto> Prodcutos { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventoryStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK_CATEGORIA");

            entity.ToTable("CATEGORIAS");

            entity.Property(e => e.IdCategoria)
                .ValueGeneratedNever()
                .HasColumnName("ID_CATEGORIA");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CATEGORIA");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimientos);

            entity.ToTable("MOVIMIENTOS");

            entity.Property(e => e.IdMovimientos)
                .ValueGeneratedNever()
                .HasColumnName("ID_MOVIMIENTOS");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.Comentario)
                .HasMaxLength(255)
                .HasColumnName("COMENTARIO");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.FechaMovimiento).HasColumnName("FECHA_MOVIMIENTO");
            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.IdTipoMovimiento).HasColumnName("ID_TIPO_MOVIMIENTO");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MOVIMIENTOS_PRODUCTOS");

            entity.HasOne(d => d.IdTipoMovimientoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MOVIMIENTOS_TIPO_MOVIMIENTO");
        });

        modelBuilder.Entity<Prodcuto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_PRODUCTO");

            entity.ToTable("PRODCUTOS");

            entity.Property(e => e.IdProducto)
                .ValueGeneratedNever()
                .HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.Catidad).HasColumnName("CATIDAD");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PRODUCTO");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .HasColumnName("PRECIO_UNITARIO");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Prodcutos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_PRODUCTO_PRODUCTO");
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimiento);

            entity.ToTable("TIPO_MOVIMIENTO");

            entity.Property(e => e.IdTipoMovimiento)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_MOVIMIENTO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("DESCRIPCION");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

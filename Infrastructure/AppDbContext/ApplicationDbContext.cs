using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AppDbContext;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Movimientos> Movimientos { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK_CATEGORIA");

            entity.ToTable("CATEGORIAS");

            entity.Property(e => e.IdCategoria)
                .HasColumnName("ID_CATEGORIA");

            entity.Property(e => e.Estado).HasColumnType("ESTADO");

            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CATEGORIA");
        });

        modelBuilder.Entity<Movimientos>(entity =>
        {
            entity.HasKey(e => e.IdMovimientos);

            entity.ToTable("MOVIMIENTOS");

            entity.Property(e => e.IdMovimientos)
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

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_PRODUCTO");

            entity.ToTable("PRODUCTOS");

            entity.Property(e => e.IdProducto)
                .HasColumnName("ID_PRODUCTO");

            entity.Property(e => e.Cantidad).HasColumnName("CATIDAD");

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

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_PRODUCTO_PRODUCTO");
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimiento);

            entity.ToTable("TIPO_MOVIMIENTO");

            entity.Property(e => e.IdTipoMovimiento)
                .HasColumnName("ID_TIPO_MOVIMIENTO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("DESCRIPCION");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
    }
}

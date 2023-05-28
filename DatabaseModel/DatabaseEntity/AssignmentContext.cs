using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModel.DatabaseEntity;

public partial class AssignmentContext : DbContext
{
    public AssignmentContext()
    {
    }

    public AssignmentContext(DbContextOptions<AssignmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SalesTransaction> SalesTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-ADSROIO;Initial Catalog=Assignment;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC075BCC0E4A");

            entity.HasIndex(e => e.Phone, "UQ__Customer__B43B145FC8842B1B").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoice__3214EC07AAACC02D");

            entity.ToTable("Invoice");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("money");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoice_customer");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07FDC458FA");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
        });

        modelBuilder.Entity<SalesTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalesTra__3214EC07632A8DDF");

            entity.ToTable("SalesTransaction");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsInvoiceGenerated).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalAmount).HasColumnType("money");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesTransactions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sales_transaction_customer");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesTransactions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sales_transaction_product");

            entity.HasMany(d => d.Invoices).WithMany(p => p.SalesTransactions)
                .UsingEntity<Dictionary<string, object>>(
                    "SalesTransactionInvoice",
                    r => r.HasOne<Invoice>().WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_sales_transaction_invoice_invoice"),
                    l => l.HasOne<SalesTransaction>().WithMany()
                        .HasForeignKey("SalesTransactionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_sales_transaction_invoice_sales_transaction"),
                    j =>
                    {
                        j.HasKey("SalesTransactionId", "InvoiceId").HasName("PK_sales_transaction_invoice");
                        j.ToTable("SalesTransactionInvoice");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

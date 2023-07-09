using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace B2CWebApp.Models;

public partial class B2cContext : DbContext
{
    public B2cContext()
    {
    }

    public B2cContext(DbContextOptions<B2cContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderInvoice> OrderInvoices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCapacity> ProductCapacities { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductStore> ProductStores { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=b2c;Uid=root;Pwd=123456;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cart");

            entity.HasIndex(e => e.UserId, "FK6a04kl6i8evrcahsbwvrqlyp0");

            entity.HasIndex(e => e.ProductId, "FK6kstp2fp3xqud2uf67dmr87wg");

            entity.HasIndex(e => e.OrderId, "FKe55k9wna1yr9ipa03kk7v27rb");

            entity.HasIndex(e => e.StoreId, "FKfobm82gm795x3foq6tvupdrqk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Carts)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FKe55k9wna1yr9ipa03kk7v27rb");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK6kstp2fp3xqud2uf67dmr87wg");

            entity.HasOne(d => d.Store).WithMany(p => p.Carts)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FKfobm82gm795x3foq6tvupdrqk");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK6a04kl6i8evrcahsbwvrqlyp0");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("feedback");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Rating).HasColumnName("rating");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.ShipperId, "FK5yxssoj67o66ear379ffbw59n");

            entity.HasIndex(e => e.User, "FK7ypwt90ivk5ihxv6kayioi8rv");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.OrderStatus)
                .HasColumnType("enum('CANCELLED','DELIVERY','DONE','IN_PROCESS','UNCONFIRMED')")
                .HasColumnName("order_status");
            entity.Property(e => e.OrderTime)
                .HasMaxLength(6)
                .HasColumnName("order_time");
            entity.Property(e => e.ShipperId).HasColumnName("shipper_id");
            entity.Property(e => e.ShippingStatus)
                .HasColumnType("enum('CANCELLED','DELIVERED','DELIVERING','PROCESSING')")
                .HasColumnName("shipping_status");
            entity.Property(e => e.Tel)
                .HasMaxLength(255)
                .HasColumnName("tel");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.Shipper).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipperId)
                .HasConstraintName("FK5yxssoj67o66ear379ffbw59n");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK7ypwt90ivk5ihxv6kayioi8rv");
        });

        modelBuilder.Entity<OrderInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_invoice");

            entity.HasIndex(e => e.OrderId, "UK_7re0xirh8lq2rijvwe66061mk").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(255)
                .HasColumnName("invoice_number");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentMethod)
                .HasColumnType("enum('COD','INTERNET_BANKING','VISA')")
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasColumnType("enum('PAID','UNPAID')")
                .HasColumnName("payment_status");

            entity.HasOne(d => d.Order).WithOne(p => p.OrderInvoice)
                .HasForeignKey<OrderInvoice>(d => d.OrderId)
                .HasConstraintName("FKaqipu42uuecp4ch9k3prp7jdw");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.ProductTypeId, "FKlabq3c2e90ybbxk58rc48byqo");

            entity.HasIndex(e => e.ProductCapacityId, "capacity_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Available)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("available");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductCapacityId).HasColumnName("product_capacity_id");
            entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");

            entity.HasOne(d => d.ProductCapacity).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCapacityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("capacity");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKlabq3c2e90ybbxk58rc48byqo");
        });

        modelBuilder.Entity<ProductCapacity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_capacity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("capacity");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_image");

            entity.HasIndex(e => e.ProductId, "FK6oo0cvcdtb6qmwsga468uuukk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImgPath)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("img_path");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK6oo0cvcdtb6qmwsga468uuukk");
        });

        modelBuilder.Entity<ProductStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_store");

            entity.HasIndex(e => e.ProductId, "FK92v519we6ha4h8lq8a0lctlb3");

            entity.HasIndex(e => e.StoreId, "FKpgqd352fxa0lqtu3s79w2rqe4");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductStores)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK92v519we6ha4h8lq8a0lctlb3");

            entity.HasOne(d => d.Store).WithMany(p => p.ProductStores)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKpgqd352fxa0lqtu3s79w2rqe4");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ResetPasswordToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reset_password_token");

            entity.HasIndex(e => e.UserId, "UK_48tx38u7rrhldrpgxswjwyfhg").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpiryDate)
                .HasMaxLength(6)
                .HasColumnName("expiry_date");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.ResetPasswordToken)
                .HasForeignKey<ResetPasswordToken>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKf2tlmidtga0ohscum2abphe9o");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "UK_8sewwnpamngi6b1dwaa88askk").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("shipper");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("store");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Tel)
                .HasMaxLength(255)
                .HasColumnName("tel");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_role");

            entity.HasIndex(e => e.UserId, "FK859n2jvi8ivhui0rl0esws6o");

            entity.HasIndex(e => e.RoleId, "FKa68196081fvovjhkek5m97n3y");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKa68196081fvovjhkek5m97n3y");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK859n2jvi8ivhui0rl0esws6o");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

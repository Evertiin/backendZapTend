using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Models;

public partial class DbzapContext : DbContext
{
    public DbzapContext()
    {
    }

    public DbzapContext(DbContextOptions<DbzapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Instance> Instances { get; set; }

    public virtual DbSet<Nicho> Nichos { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Prompt> Prompts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserNicho> UserNichos { get; set; }

    public virtual DbSet<ValoresVariavei> ValoresVariaveis { get; set; }

    public virtual DbSet<Variavei> Variaveis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=dbzap;Username=postgres;Password=1010");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("Category_pkey");

            entity.ToTable("Category", "mydb");

            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
        });

        modelBuilder.Entity<Instance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Instance_pkey");

            entity.ToTable("Instance", "mydb");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.UserIduser)
                .HasMaxLength(100)
                .HasColumnName("user_iduser");

            entity.HasOne(d => d.UserIduserNavigation).WithMany(p => p.Instances)
                .HasForeignKey(d => d.UserIduser)
                .HasConstraintName("fk_instance_user");
        });

        modelBuilder.Entity<Nicho>(entity =>
        {
            entity.HasKey(e => e.IdNichos).HasName("Nichos_pkey");

            entity.ToTable("Nichos", "mydb");

            entity.Property(e => e.IdNichos).HasColumnName("idNichos");
            entity.Property(e => e.CategoryIdCategory).HasColumnName("Category_idCategory");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);

            entity.HasOne(d => d.CategoryIdCategoryNavigation).WithMany(p => p.Nichos)
                .HasForeignKey(d => d.CategoryIdCategory)
                .HasConstraintName("fk_Nichos_Category1");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.IdPayments).HasName("Payments_pkey");

            entity.ToTable("Payments", "mydb");

            entity.Property(e => e.IdPayments).HasColumnName("idPayments");
            entity.Property(e => e.Cycle)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Description).HasMaxLength(45);
            entity.Property(e => e.DueDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.PlanId).HasColumnName("Plan_Id");
            entity.Property(e => e.TypePayment)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .HasColumnName("User_Id");
            entity.Property(e => e.ValuePayment).HasPrecision(10, 2);

            entity.HasOne(d => d.Plan).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("fk_Payments_Plan1");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_Payments_User1");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Plan_pkey");

            entity.ToTable("Plan", "mydb");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.SubscribeAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("subscribeAt");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .HasColumnName("user_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Plans)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_Plan_user1");
        });

        modelBuilder.Entity<Prompt>(entity =>
        {
            entity.HasKey(e => e.IdPrompts).HasName("Prompts_pkey");

            entity.ToTable("Prompts", "mydb");

            entity.Property(e => e.IdPrompts).HasColumnName("idPrompts");
            entity.Property(e => e.Conteudo).IsRequired();
            entity.Property(e => e.NichosIdNichos).HasColumnName("Nichos_idNichos");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(45);

            entity.HasOne(d => d.NichosIdNichosNavigation).WithMany(p => p.Prompts)
                .HasForeignKey(d => d.NichosIdNichos)
                .HasConstraintName("fk_Prompts_Nichos1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdAutentication).HasName("User_IdAutentication_pkey");

            entity.ToTable("User", "mydb");

            entity.HasIndex(e => e.IdAutentication, "User_IdAutentication_unique").IsUnique();

            entity.Property(e => e.IdAutentication).HasMaxLength(100);
            entity.Property(e => e.Adress)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Sobrenome)
                .HasMaxLength(255)
                .HasColumnName("sobrenome");
        });

        modelBuilder.Entity<UserNicho>(entity =>
        {
            entity.HasKey(e => e.IdUserNichos).HasName("User_Nichos_pkey");

            entity.ToTable("User_Nichos", "mydb");

            entity.Property(e => e.IdUserNichos).HasColumnName("idUser_Nichos");
            entity.Property(e => e.NichosIdNichos).HasColumnName("Nichos_idNichos");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .HasColumnName("User_Id");

            entity.HasOne(d => d.NichosIdNichosNavigation).WithMany(p => p.UserNichos)
                .HasForeignKey(d => d.NichosIdNichos)
                .HasConstraintName("fk_User_Nichos_Nichos1");

            entity.HasOne(d => d.User).WithMany(p => p.UserNichos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_User_Nichos_User1");
        });

        modelBuilder.Entity<ValoresVariavei>(entity =>
        {
            entity.HasKey(e => e.IdValoresVariaveis).HasName("Valores_Variaveis_pkey");

            entity.ToTable("Valores_Variaveis", "mydb");

            entity.Property(e => e.IdValoresVariaveis).HasColumnName("idValores_Variaveis");
            entity.Property(e => e.PromptsIdPrompts).HasColumnName("Prompts_idPrompts");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .HasColumnName("User_Id");
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.VariaveisIdVariaveis).HasColumnName("Variaveis_idVariaveis");

            entity.HasOne(d => d.PromptsIdPromptsNavigation).WithMany(p => p.ValoresVariaveis)
                .HasForeignKey(d => d.PromptsIdPrompts)
                .HasConstraintName("fk_Valores_Variaveis_Prompts1");

            entity.HasOne(d => d.User).WithMany(p => p.ValoresVariaveis)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_Valores_Variaveis_User1");

            entity.HasOne(d => d.VariaveisIdVariaveisNavigation).WithMany(p => p.ValoresVariaveis)
                .HasForeignKey(d => d.VariaveisIdVariaveis)
                .HasConstraintName("fk_Valores_Variaveis_Variaveis1");
        });

        modelBuilder.Entity<Variavei>(entity =>
        {
            entity.HasKey(e => e.IdVariaveis).HasName("Variaveis_pkey");

            entity.ToTable("Variaveis", "mydb");

            entity.Property(e => e.IdVariaveis).HasColumnName("idVariaveis");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.PromptsIdPrompts).HasColumnName("Prompts_idPrompts");

            entity.HasOne(d => d.PromptsIdPromptsNavigation).WithMany(p => p.Variaveis)
                .HasForeignKey(d => d.PromptsIdPrompts)
                .HasConstraintName("fk_Variaveis_Prompts1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

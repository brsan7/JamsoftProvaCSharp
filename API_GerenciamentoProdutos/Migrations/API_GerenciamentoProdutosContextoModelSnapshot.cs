﻿// <auto-generated />
using System;
using API_GerenciamentoProdutos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_GerenciamentoProdutos.Migrations
{
    [DbContext(typeof(API_GerenciamentoProdutosContexto))]
    partial class API_GerenciamentoProdutosContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API_GerenciamentoProdutos.Entidades.Cartao", b =>
                {
                    b.Property<string>("numero_cartao")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<string>("bandeira")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("cvv")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("data_expiracao")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("titular")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("numero_cartao");

                    b.ToTable("Cartoes");
                });

            modelBuilder.Entity("API_GerenciamentoProdutos.Entidades.Compra", b =>
                {
                    b.Property<int>("compra_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("compra_id"), 1L, 1);

                    b.Property<DateTime>("data_compra")
                        .HasColumnType("datetime2");

                    b.Property<string>("numero_cartao")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<int>("produto_id")
                        .HasColumnType("int");

                    b.Property<int>("qtde_comprada")
                        .HasColumnType("int");

                    b.HasKey("compra_id");

                    b.HasIndex("numero_cartao");

                    b.HasIndex("produto_id");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("API_GerenciamentoProdutos.Entidades.Pagamento", b =>
                {
                    b.Property<int>("pagamento_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("pagamento_id"), 1L, 1);

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("numero_cartao")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<decimal>("valor")
                        .HasColumnType("money")
                        .HasColumnName("valor");

                    b.HasKey("pagamento_id");

                    b.HasIndex("numero_cartao");

                    b.ToTable("Pagamentos");
                });

            modelBuilder.Entity("API_GerenciamentoProdutos.Entidades.Produto", b =>
                {
                    b.Property<int>("produto_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("produto_id"), 1L, 1);

                    b.Property<DateTime?>("data_ultima_compra")
                        .HasColumnType("datetime2");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("qtde_estoque")
                        .HasColumnType("int");

                    b.Property<decimal?>("valor_ultima_compra")
                        .HasColumnType("money")
                        .HasColumnName("valor_ultima_compra");

                    b.Property<decimal>("valor_unitario")
                        .HasColumnType("smallmoney")
                        .HasColumnName("valor_unitario");

                    b.HasKey("produto_id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("API_GerenciamentoProdutos.Entidades.Compra", b =>
                {
                    b.HasOne("API_GerenciamentoProdutos.Entidades.Cartao", "Cartao")
                        .WithMany()
                        .HasForeignKey("numero_cartao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_GerenciamentoProdutos.Entidades.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("produto_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cartao");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("API_GerenciamentoProdutos.Entidades.Pagamento", b =>
                {
                    b.HasOne("API_GerenciamentoProdutos.Entidades.Cartao", "Cartao")
                        .WithMany()
                        .HasForeignKey("numero_cartao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cartao");
                });
#pragma warning restore 612, 618
        }
    }
}

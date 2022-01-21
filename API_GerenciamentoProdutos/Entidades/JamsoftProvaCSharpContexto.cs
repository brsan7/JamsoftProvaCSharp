﻿using Microsoft.EntityFrameworkCore;

namespace JamsoftProvaCSharp.Entidades
{
    public class JamsoftProvaCSharpContexto : DbContext
    {

        public JamsoftProvaCSharpContexto(DbContextOptions<JamsoftProvaCSharpContexto> options)
            : base(options)
        {
            //
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CursoASPdotNET;Integrated Security=true");
        }

        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Cartao> Cartoes { get; set; } = null!;
        public DbSet<Compra> Compras { get; set; } = null!;
    }
}

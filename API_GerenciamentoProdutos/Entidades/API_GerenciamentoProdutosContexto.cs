using Microsoft.EntityFrameworkCore;

namespace API_GerenciamentoProdutos.Entidades
{
    public class API_GerenciamentoProdutosContexto : DbContext
    {

        public API_GerenciamentoProdutosContexto(DbContextOptions<API_GerenciamentoProdutosContexto> options)
            : base(options)
        {
            //
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"
                                    Server=(localdb)\mssqllocaldb;
                                    Database=JamsoftProvaCSharp;
                                    Integrated Security=true");
        }

        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Compra> Compras { get; set; } = null!;
        public DbSet<Cartao> Cartoes { get; set; } = null!;
        public DbSet<Pagamento> Pagamentos { get; set; } = null!;
    }
}

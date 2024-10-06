using Microsoft.EntityFrameworkCore;

namespace mf_dev_beckend_2023.Models
{
    /*classe que gerencia o contexto do banco de dados
     responsavel por fazer a confi do entity framework para cração do banco e das tabelas...*/

    public class AppDbContext : DbContext
    {
        //contrutor da classe
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }


    }
}

using Microsoft.EntityFrameworkCore;
namespace  ListaZakupowApi.Models
{
    public class ListaZakupowContext : DbContext{
        public ListaZakupowContext(DbContextOptions<ListaZakupowContext> options):base(options)
        {

        }
        public DbSet<Zakup> Zakupy {get;set;}
    }
}
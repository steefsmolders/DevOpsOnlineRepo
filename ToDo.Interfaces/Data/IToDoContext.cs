using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Interfaces.Data
{
    public interface IToDoContext
    {
        DbSet<Item> Items { get; set; }

        DbEntityEntry Entry(object entity);

        Task<int> SaveChangesAsync();
    }
}
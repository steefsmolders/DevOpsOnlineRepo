using System.Data.Entity;
using ToDo.Interfaces.Data;
using ToDo.Models;

namespace ToDo.Data
{
    public class ToDoContext : DbContext, IToDoContext
    {
        public DbSet<Item> Items { get; set; }
    }
}
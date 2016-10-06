using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Interfaces.Data;
using ToDo.Models;

namespace ToDo.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly IToDoContext context;

        public ItemRepository(IToDoContext context)
        {
            this.context = context;
        }

        public void DeleteItem(Item item)
        {
            context.Items.Remove(item);
        }

        public async Task<Item> GetItemByID(int itemId)
        {
            return await context.Items.FindAsync(itemId);
        }

        public IQueryable<Item> GetItems()
        {
            return context.Items;
        }

        public void InsertItem(Item item)
        {
            context.Items.Add(item);
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }

        public void UpdateItem(Item item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
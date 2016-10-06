using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Interfaces.Data
{
    public interface IItemRepository
    {
        IQueryable<Item> GetItems();

        Task<Item> GetItemByID(int itemId);

        void InsertItem(Item item);

        void DeleteItem(Item item);

        void UpdateItem(Item item);

        Task<int> Save();
    }
}
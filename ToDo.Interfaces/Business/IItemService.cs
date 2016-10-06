using PagedList;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Interfaces.Business
{
    public interface IItemService
    {
        PagedList<Item> GetItems(int page = 1);

        PagedList<Item> SearchItem(string query, int page = 1);

        Task<int> AddItem(Item newItem);

        Task<int> UpdateItem(Item item);

        Task<int> DeleteItem(int itemId);

        Statistics GetStatistics();
    }
}
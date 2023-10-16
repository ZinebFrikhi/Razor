using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RazorProject.Models
{
    public class GlobalListService
    {
        private List<Product> globalList = new List<Product>();

        public List<Product> GetGlobalList()
        {
            return globalList;
        }

        public void AddToGlobalList(Product item)
        {
            globalList.Add(item);
        }
        public void DeleteFromGlobalList(int? Id)
        {
            // Find and remove the item from the global list
            var itemToRemove = globalList.FirstOrDefault(item => item.Id == Id); // Replace 'Id' with the appropriate identifier property
            if (itemToRemove != null)
            {
                globalList.Remove(itemToRemove);
            }
        }
  
    }

}

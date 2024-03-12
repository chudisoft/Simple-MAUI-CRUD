using SimpleMAUICRUD.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMAUICRUD.factories
{
    // Define a factory interface
    public interface IItemPageFactory
    {
        CreateItemPage CreateItemPage(int itemId = 0);
        ItemDetailPage ItemDetailsPage(int itemId);
    }

    // Implement the factory
    public class ItemPageFactory : IItemPageFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ItemPageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CreateItemPage CreateItemPage(int itemId = 0)
        {
            var page = _serviceProvider.GetService<CreateItemPage>();
            page.ItemId = itemId;
            return page;
        }
        public ItemDetailPage ItemDetailsPage(int itemId)
        {
            var page = _serviceProvider.GetService<ItemDetailPage>();
            page.ItemId = itemId;
            return page;
        }
    }

}

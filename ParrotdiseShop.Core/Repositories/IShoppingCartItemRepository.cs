﻿using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.Repositories
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem>
    {
        IEnumerable<ShoppingCartItem> GetAllShoppingCartItemsWithProductsBy(string userId);

    }
}

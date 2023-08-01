﻿using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartItemRepository(ApplicationDbContext context) : base(context) 
        { 
            _context = context;
        }

        public IEnumerable<ShoppingCartItem> GetAllShoppingCartItemsBy(string userId)
        {
            return _context.ShoppingCartItems.Where(sc => sc.UserId == userId);
        }
    }
}

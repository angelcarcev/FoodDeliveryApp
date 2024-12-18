using FoodApp.Domain.Domain;
using FoodApp.Domain.DTO;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<FoodItem> _fooditemRepository;

        private readonly IRepository<FoodItemInOrder> _foodItemInOrderRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<FoodItem> fooditemrepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<FoodItemInOrder> fooditeminorderrepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _foodItemInOrderRepository = fooditeminorderrepository;
            _fooditemRepository = fooditemrepository;
        }
        public bool deleteFoodFromShoppingCart(string userId, Guid productId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);
                var product_to_delete = loggedInUser?.UserCart?.FoodItemInShoppingCarts.FirstOrDefault(z => z.FoodItemId == productId);
                if (product_to_delete == null)
                {
                    throw new InvalidOperationException("Product not found in the shopping cart.");
                }
                loggedInUser?.UserCart?.FoodItemInShoppingCarts?.Remove(product_to_delete);

                _shoppingCartRepository.Update(loggedInUser.UserCart);

                return true;

            }

            return false;

        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser?.UserCart;
            var allProduct = userShoppingCart?.FoodItemInShoppingCarts?.ToList();

            var totalPrice = allProduct.Select(x => (x.FoodItem.Price * x.Quantity)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                FoodItems = allProduct,
                TotalPrice = totalPrice
            };
            return dto;
        }

        public bool order(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(order);

                List<FoodItemInOrder> foodItemInOrder = new List<FoodItemInOrder>();

                var lista = userShoppingCart.FoodItemInShoppingCarts.Select(
                    x => new FoodItemInOrder
                    {
                        Id = Guid.NewGuid(),
                        FoodId = x.FoodItem.Id,
                        FoodItem = x.FoodItem,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();

                foodItemInOrder.AddRange(lista);

                foreach (var product in foodItemInOrder)
                {
                    _foodItemInOrderRepository.Insert(product);
                }

                loggedInUser.UserCart.FoodItemInShoppingCarts.Clear();
                _userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }

        
        public AddToCartDto getProductInfo(Guid Id)
        {
            var selectedProduct = _fooditemRepository.Get(Id);
            if (selectedProduct != null)
            {
                var model = new AddToCartDto
                {
                    FoodName = selectedProduct.FoodItemName,
                    FoodId = selectedProduct.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public bool AddToShoppingConfirmed(FoodItemInShoppingCart model, string userId)
        {
            throw new NotImplementedException();
        }

        public bool AddToShoppingConfirmed(string userId, AddToCartDto model)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var selectedProduct = _fooditemRepository.Get(model.FoodId);

                if (selectedProduct != null && userCart != null)
                {
                    userCart?.FoodItemInShoppingCarts?.Add(new FoodItemInShoppingCart
                    {
                        FoodItem = selectedProduct,
                        FoodItemId = selectedProduct.Id,
                        ShoppingCart = userCart,
                        ShoppingCartId = userCart.Id,
                        Quantity = model.Quantity
                    });

                    _shoppingCartRepository.Update(userCart);
                    return true;
                }
            }
            return false;
        }
    }
}

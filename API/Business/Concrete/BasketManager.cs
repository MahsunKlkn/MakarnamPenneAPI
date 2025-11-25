using Business.Abstract;
using DataAccessLayer.Abstarct;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }
        public void AddBasket(Basket basket)
        {
            // Zaman damgaları set edilmemişse ekle
            if (basket.DateCreated == default)
                basket.DateCreated = DateTime.UtcNow;
            basket.DateUpdated = basket.DateCreated;
            _basketDal.Insert(basket);
        }

        public void DeleteBasket(Basket basket)
        {
            _basketDal.Delete(basket);
        }

        public List<Basket> GetAllBasket()
        {
            return _basketDal.GetListAll();
        }

        public Basket GetById(int id)
        {
            return _basketDal.GetById(id);
        }

        public void UpdateBasket(Basket basket)
        {
            basket.DateUpdated = DateTime.UtcNow;
            _basketDal.Update(basket);
        }

        public List<Basket> GetBasketsByKullaniciId(int kullaniciId)
        {
            return _basketDal.GetBasketsByKullaniciId(kullaniciId);
        }

        public Basket? UpdateBasketByKullaniciId(int kullaniciId, string? productIds)
        {
            var baskets = _basketDal.GetBasketsByKullaniciId(kullaniciId);
            if (baskets == null || baskets.Count == 0)
                return null;

            // Varsayım: kullanıcı başına tek aktif sepet. Birden fazlaysa ilkini güncelliyoruz.
            var basket = baskets.First();
            basket.ProductIds = productIds;
            basket.DateUpdated = DateTime.UtcNow;
            _basketDal.Update(basket);
            return basket;
        }
    }
}

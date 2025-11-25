using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBasketService
    {
        List<Basket> GetAllBasket();
        void AddBasket(Basket basket);
        void UpdateBasket(Basket basket);
        void DeleteBasket(Basket basket);

        Basket GetById(int id);
        List<Basket> GetBasketsByKullaniciId(int kullaniciId);
        /// <summary>
        /// Kullanıcı ID'sine göre ilgili sepeti günceller. Birden fazla varsa ilkini günceller.
        /// Eğer hiç yoksa null döner.
        /// </summary>
        /// <param name="kullaniciId">Kullanıcı ID</param>
        /// <param name="productIds">Yeni ürün ID listesi (virgülle ayrılmış)</param>
        /// <returns>Güncellenen Basket ya da null</returns>
        Basket? UpdateBasketByKullaniciId(int kullaniciId, string? productIds);
    }
}

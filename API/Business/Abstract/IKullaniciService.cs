using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IKullaniciService
    {
        List<Kullanici> GetAllKullanici();
        void AddKullanici(Kullanici kullanici);
        void UpdateKullanici(Kullanici kullanici);
        void DeleteKullanici(Kullanici kullanici);

        Kullanici GetById(int id);
        DtoKullaniciToken GetKullaniciToken(string eposta, string sifre);
                /// <summary>
                /// Kullanıcı token'ını eposta bilgisine göre alır.
                /// Google ile giriş yapan kullanıcılar için kullanılır.
                /// Fonksiyon kendi içinde onay durumunu ve tenantId kontrolünü yapar.
                /// </summary>
                /// <param name="eposta"></param>
                /// <returns></returns>
        DtoKullaniciToken GetKullaniciToken(string eposta);
    }
}

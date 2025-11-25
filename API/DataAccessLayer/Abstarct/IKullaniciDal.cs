using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstarct
{
    public interface IKullaniciDal : IGenericDal<Kullanici>
    {

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

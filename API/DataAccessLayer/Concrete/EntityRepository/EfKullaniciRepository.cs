using DataAccessLayer.Abstarct;
using DataAccessLayer.Repositories;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityRepository
{
    public class EfKullaniciRepository : GenericRepository<Kullanici>, IKullaniciDal
    {
        public EfKullaniciRepository(Context.Context context) : base(context)
        {
        }

        public DtoKullaniciToken GetKullaniciToken(string eposta, string sifre)
        {
            var kullanici = _context.Kullanici
                .FirstOrDefault(k => k.Eposta == eposta && k.Sifre == sifre);

            if (kullanici == null)
                return null!;

            return new DtoKullaniciToken
            {
                Kullanici = kullanici,
                KullaniciId = kullanici.Id.ToString()
            };
        }

        public DtoKullaniciToken GetKullaniciToken(string eposta)
        {
            var kullanici = _context.Kullanici
                .FirstOrDefault(k => k.Eposta == eposta);

            if (kullanici == null)
                return null!;

            return new DtoKullaniciToken
            {
                Kullanici = kullanici,
                KullaniciId = kullanici.Id.ToString()
            };
        }
    }
}

using Business.Abstract;
using DataAccessLayer.Abstarct;
using Tool.GeneralTool;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class KullaniciManager : IKullaniciService
    {
        private readonly IKullaniciDal _kullaniciDal;

        public KullaniciManager(IKullaniciDal kullaniciDal)
        {
            _kullaniciDal = kullaniciDal;
        }
        public void AddKullanici(Kullanici kullanici)
        {
            _kullaniciDal.Insert(kullanici);
        }

        public void DeleteKullanici(Kullanici kullanici)
        {
            _kullaniciDal.Delete(kullanici);
        }

        public List<Kullanici> GetAllKullanici()
        {
            return _kullaniciDal.GetListAll();
        }

        public Kullanici GetById(int id)
        {
            return _kullaniciDal.GetById(id);
        }

        public void UpdateKullanici(Kullanici kullanici)
        {
            _kullaniciDal.Update(kullanici);
        }


        public DtoKullaniciToken GetKullaniciToken(string eposta, string sifre)
        {
            try
            {
                DtoKullaniciToken result = _kullaniciDal.GetKullaniciToken(eposta,sifre);
                //DtoKullaniciToken result = _kullaniciDal.GetKullaniciToken(eposta, GeneralTool.ComputeSha1Password(sifre));

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Manager katmaninda hata: {ex.Message}", ex);
            }
        }

        public DtoKullaniciToken GetKullaniciToken(string eposta)
        {
            try
            {

                DtoKullaniciToken result = _kullaniciDal.GetKullaniciToken(eposta);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Manager katmaninda hata: {ex.Message}", ex);
            }
        }
    }
}

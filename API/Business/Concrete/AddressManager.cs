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
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }
        public void AddAddress(Address address)
        {
            _addressDal.Insert(address);
        }

        public void DeleteAddress(Address address)
        {
            _addressDal.Delete(address);
        }

        public List<Address> GetAllAddress(Address address)
        {
            return _addressDal.GetListAll();
        }

        public Address GetById(int id)
        {
            return _addressDal.GetById(id);
        }

        public void UpdateAddress(Address address)
        {
            _addressDal.Update(address);
        }
    }
}

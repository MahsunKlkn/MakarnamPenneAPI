using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAddressService
    {
        List<Address> GetAllAddress(Address address);
        void AddAddress(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(Address address);

        Address GetById(int id);
    }
}

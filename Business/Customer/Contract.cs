using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Customer
{
    public class Contract
    {
        public Contract()
        { }

        public int Save(Entity.Customer.Contract contract)
        {
            return DataAccess.Customer.Contract.Save(contract);
        }

        public DataTable GetAll()
        {
            return DataAccess.Customer.Contract.GetAll();
        }

        public Entity.Customer.Contract GetById(int contractId)
        {
            return DataAccess.Customer.Contract.GetById(contractId);
        }
    }
}

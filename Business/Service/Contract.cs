using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Service
{
    public class Contract
    {
        public int Save(Entity.Service.Contract contract)
        {
            return DataAccess.Service.Contract.Save(contract);
        }

        public DataTable GetAll(int customerId)
        {
            return DataAccess.Service.Contract.GetAll(customerId);
        }

        public DataSet GetById(int contractId)
        {
            return DataAccess.Service.Contract.GetById(contractId);
        }

        public int Delete(int contractId)
        {
            return DataAccess.Service.Contract.Delete(contractId);
        }

        public bool Service_MachineIsInContractCheck(int customerPurchaseId)
        {
            return DataAccess.Service.Contract.Service_MachineIsInContractCheck(customerPurchaseId);
        }

        public DataTable Services_ContractStatus(int assignEngineer)
        {
            return DataAccess.Service.Contract.Services_ContractStatus(assignEngineer);
        }

        public DataSet Service_ContractStatusList(Entity.Service.Contract contract)
        {
            return DataAccess.Service.Contract.Service_ContractStatusList(contract);
        }

        public DataSet Service_ContractExpiringList(Entity.Service.Contract contract)
        {
            return DataAccess.Service.Contract.Service_ContractExpiringList(contract);
        }

        public DataSet Service_ContractExpiredList(Entity.Service.Contract contract)
        {
            return DataAccess.Service.Contract.Service_ContractExpiredList(contract);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Models;
using TPS.ViewModels;

namespace TPS.Data
{
    public class ContractManagerRepository
    {
        public List<ContractManager> GetContractManagers()
        {          
            List<ContractManager> managersToReturn = new List<ContractManager>();

            using (var context = new Context())
            {
                managersToReturn = context.ContractManagers
                    .OrderBy(s => s.ID)
                    .ToList();
            }

            return managersToReturn;
        }

        public void AddManager(AccountRegisterViewModel viewModel)
        {
            using (var context = new Context())
            {
                context.ContractManagers.Add(new ContractManager()
                {
                    Name = viewModel.Username
                });

                context.SaveChanges();
            }
        }
    }
}
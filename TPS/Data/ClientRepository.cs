using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Models;
using TPS.ViewModels;

namespace TPS.Data
{
    public class ClientRepository
    {
        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            using (var context = new Context())
            {
                clients = context.Clients
                    .OrderByDescending(s => s.ID)
                    .ToList();
            }
            return clients;
        }

        public void AddClient(AccountRegisterViewModel viewModel)
        {
            using (var context = new Context())
            {
                context.Clients.Add(new Client()
                {
                    Name = viewModel.Username                   
                });

                context.SaveChanges();
            }
        }
    }
}
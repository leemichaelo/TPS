using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Models;

namespace TPS.Data
{
    public class AccountRepository
    {
        public UserLogin GetUser(string username)
        {
            using (var context = new Context())
            {
                UserLogin userToReturn = context.Users
                    .Where(u => u.UserName == username)
                    .SingleOrDefault();

                return userToReturn;
            }
        }

        public List<UserLogin> GetUsers()
        {
            List<UserLogin> listToReturn = new List<UserLogin>();
            using (var context = new Context())
            {
                listToReturn = context.Users
                    .OrderBy(u => u.UserName)
                    .ToList();
            }
            return listToReturn;
        }

        public List<UserLogin> GetUsersByRole(string role)
        {
            List<UserLogin> listToReturn = new List<UserLogin>();

            using (var context = new Context())
            {
                listToReturn = context.Users
                    .Where(u => u.Roles.Any(r => r.RoleId == role))
                    .OrderBy(u => u.UserName)
                    .ToList();
            }
            return listToReturn;
        }

        public void UpdateUser(UserLogin updatedInformation)
        {
            using (var context = new Context())
            {
                var userToUpdate = context.Users
                    .Where(u => u.UserName == updatedInformation.UserName)
                    .SingleOrDefault();

                context.Entry(userToUpdate).CurrentValues
                    .SetValues(updatedInformation);

                context.SaveChanges();
            }
        }

        public void DeleteUser(string username)
        {
            using (var context = new Context())
            {
                UserLogin userToDelete = context.Users
                    .Where(u => u.UserName == username)
                    .SingleOrDefault();

                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }
        }

    }
}
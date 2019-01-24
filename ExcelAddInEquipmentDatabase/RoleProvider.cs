using System;
using System.Linq;
using System.Configuration;

namespace ExcelAddInEquipmentDatabase
{
    public class RoleProvider
    {

       // var connection = ConnectionFactory.GetConnection(ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString, DataBaseProvider);
        private EquiEntities db = new EquiEntities();

        public string[] GetRolesForUser(string username)
        {
            var roles = from perm in db.h_usersPermisions
                        where perm.L_users.username == username
                        select perm.Role;

            if (roles != null)
                return roles.ToArray();
            else
                return new string[] { };
        }

        public bool IsUserInRole(string username, string roleName)
        {
            var roles = from perm in db.h_usersPermisions
                        where perm.L_users.username == username
                        select perm.Role;

            if (roles != null)
                return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            else
                return false;
        }
    }
}
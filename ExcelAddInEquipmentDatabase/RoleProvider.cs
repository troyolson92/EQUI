using System;
using System.Linq;
using System.Configuration;

namespace ExcelAddInEquipmentDatabase
{
    public class RoleProvider
    {

       // var connection = ConnectionFactory.GetConnection(ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString, DataBaseProvider);

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string[] GetRolesForUser(string username)
        {
            try
            {
                EquiEntities db = new EquiEntities();
                return db.h_usersPermisions.Where(c => c.L_users.username == username).Select(c => c.Role).ToArray();
            }
            catch (Exception ex)
            {
                log.Error("Failed to get user roles", ex);
                return new string[] { };
            }        
        }

        public bool IsUserInRole(string username, string roleName)
        {
            try
            {
                EquiEntities db = new EquiEntities();
                var roles =  db.h_usersPermisions.Where(c => c.L_users.username == username).Select(c => c.Role).ToArray();
                if (roles != null)
                    return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
                else
                    return false;
            }
            catch (Exception ex)
            {
                log.Error("Failed to get user roles", ex);
                return false;
            }

        }
    }
}
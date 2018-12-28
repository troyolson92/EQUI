using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelAddInEquipmentDatabase
{
        public class RoleProvider 
        {

            public  string[] GetRolesForUser(string username)
            {
                using (GADATAUserRoleProvider db = new GADATAUserRoleProvider())
                {
                    var roles = from perm in db.h_usersPermisions
                                where perm.L_users.username == username
                                select perm.Role;

                    if (roles != null)
                        return roles.ToArray();
                    else
                        return new string[] { }; ;
                }
            }

            public  bool IsUserInRole(string username, string roleName)
            {

                using (GADATAUserRoleProvider db = new GADATAUserRoleProvider())
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
    }


using EqUiWebUi.Areas.user_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EqUiWebUi
{
    public class roleProvider : RoleProvider
    {
        public override string ApplicationName { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (GADATAEntitiesUserManagement db = new GADATAEntitiesUserManagement())
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

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {

            using (GADATAEntitiesUserManagement db = new GADATAEntitiesUserManagement())
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

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
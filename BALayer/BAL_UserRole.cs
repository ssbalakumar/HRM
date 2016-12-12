using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_UserRoles
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listUserRole = new List<Entity_HRM>();

            try
            {
                var query = (from userrole in dbcontext.mtblUserRoles
                             select new
                             {
                                 userrole.intUserRoleId,
                                 userrole.varUserRole,
                                 userrole.varMasterFile,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listUserRole.Add(new Entity_HRM { intUserRoleId = (decimal)usr.intUserRoleId, varUserRole = usr.varUserRole, varMasterFile = usr.varMasterFile });
                }
            }
            catch (Exception ex)
            {
            }
            return listUserRole;
        }

        public List<Entity_HRM> GetUserRole()
        {
            List<Entity_HRM> listUserRole = new List<Entity_HRM>();

            try
            {
                var query = (from userrole in dbcontext.mtblUserRoles
                             where userrole.varUserRole.ToUpper() != "DEVELOPMENT"
                             select new
                             {
                                 userrole.intUserRoleId,
                                 userrole.varUserRole,
                                 userrole.varMasterFile,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listUserRole.Add(new Entity_HRM { intUserRoleId = (decimal)usr.intUserRoleId, varUserRole = usr.varUserRole, varMasterFile = usr.varMasterFile });
                }
            }
            catch (Exception ex)
            {
            }
            return listUserRole;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listUserRole = new List<Entity_HRM>();
            try
            {
                var query = (from userrole in dbcontext.mtblUserRoles
                             where userrole.intUserRoleId == _HrmEntity.intUserRoleId
                             select new
                             {
                                 userrole.intUserRoleId,
                                 userrole.varUserRole,
                                 userrole.varMasterFile,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listUserRole.Add(new Entity_HRM { intUserRoleId = (decimal)usr.intUserRoleId, varUserRole = usr.varUserRole, varMasterFile = usr.varMasterFile });
                }
            }
            catch (Exception ex)
            {
            }
            return listUserRole;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblUserRoles.SingleOrDefault(i => i.varUserRole.ToUpper() == _HrmEntity.varUserRole.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblUserRole userrole = new mtblUserRole();

                    userrole.varUserRole = _HrmEntity.varUserRole;
                    userrole.varMasterFile = _HrmEntity.varMasterFile;
                    dbcontext.mtblUserRoles.InsertOnSubmit(userrole);
                    dbcontext.SubmitChanges();
                    id = 1;
                }
                else
                {
                    id = 0;
                }
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }

        public int Update(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblUserRoles.SingleOrDefault(i => i.varUserRole.ToUpper() == _HrmEntity.varUserRole.ToUpper() && i.intUserRoleId != _HrmEntity.intUserRoleId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblUserRole userrole = dbcontext.mtblUserRoles.Single(p => p.intUserRoleId == _HrmEntity.intUserRoleId);
                    userrole.varUserRole = _HrmEntity.varUserRole;
                    userrole.varMasterFile = _HrmEntity.varMasterFile;
                    dbcontext.SubmitChanges();
                    id = 1;
                }
                else
                {
                    id = 0;
                }
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }

        public int Delete(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                dbcontext.mtblUserRoles.DeleteOnSubmit(dbcontext.mtblUserRoles.Single(p => p.intUserRoleId == _HrmEntity.intUserRoleId));
                dbcontext.SubmitChanges();
                id = 1;
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }
    }
}
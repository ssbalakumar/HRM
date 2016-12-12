using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_User
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAllUSer()
        {
            List<Entity_HRM> listuser = new List<Entity_HRM>();
            try
            {
                var query = (from userrole in dbcontext.mtblUserRoles
                             join user in dbcontext.mtblUsers on userrole.intUserRoleId equals user.intUserRoleId
                             orderby user.varUserName ascending
                             select new
                             {
                                 userrole.intUserRoleId,
                                 userrole.varUserRole,
                                 user.intUserId,
                                 user.varUserName,
                                 user.varPwd,
                                 user.dtDate
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listuser.Add(new Entity_HRM { intUserRoleId = (decimal)usr.intUserRoleId, varUserRole = usr.varUserRole, intUserId = (decimal)usr.intUserId, varUserName = usr.varUserName, varPwd = usr.varPwd, dtDate = usr.dtDate.Value.Date });
                }
            }
            catch (Exception ex)
            {
            }
            return listuser;
        }

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listuser = new List<Entity_HRM>();
            try
            {
                var query = (from userrole in dbcontext.mtblUserRoles
                             join user in dbcontext.mtblUsers on userrole.intUserRoleId equals user.intUserRoleId
                             where userrole.varUserRole.ToUpper() != "DEVELOPMENT"
                             orderby user.varUserName ascending
                             select new
                             {
                                 userrole.intUserRoleId,
                                 userrole.varUserRole,
                                 user.intUserId,
                                 user.varUserName,
                                 user.varPwd,
                                 user.dtDate
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listuser.Add(new Entity_HRM { intUserRoleId = (decimal)usr.intUserRoleId, varUserRole = usr.varUserRole, intUserId = (decimal)usr.intUserId, varUserName = usr.varUserName, varPwd = usr.varPwd, dtDate = usr.dtDate.Value.Date });
                }
            }
            catch (Exception ex)
            {
            }
            return listuser;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listuser = new List<Entity_HRM>();
            try
            {
                var query = (from userrole in dbcontext.mtblUserRoles
                             join user in dbcontext.mtblUsers on userrole.intUserRoleId equals user.intUserRoleId
                             where user.intUserId == _HrmEntity.intUserId
                             select new
                             {
                                 userrole.intUserRoleId,
                                 userrole.varUserRole,
                                 user.intUserId,
                                 user.varUserName,
                                 user.varPwd,
                                 user.dtDate
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listuser.Add(new Entity_HRM { intUserRoleId = (decimal)usr.intUserRoleId, varUserRole = usr.varUserRole, intUserId = (decimal)usr.intUserId, varUserName = usr.varUserName, varPwd = usr.varPwd, dtDate = usr.dtDate.Value.Date });
                }
            }
            catch (Exception ex)
            {
            }
            return listuser;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblUsers.SingleOrDefault(i => i.varUserName.ToUpper() == _HrmEntity.varUserName.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblUser user = new mtblUser();
                    user.intUserRoleId = _HrmEntity.intUserRoleId;
                    user.varUserName = _HrmEntity.varUserName;
                    user.varPwd = _HrmEntity.varPwd;
                    user.dtDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(System.DateTime.Now, TimeZoneInfo.Local.Id, "India Standard Time");
                    dbcontext.mtblUsers.InsertOnSubmit(user);
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
                var query = dbcontext.mtblUsers.SingleOrDefault(i => i.varUserName.ToUpper() == _HrmEntity.varUserName.ToUpper() && i.intUserId != _HrmEntity.intUserId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblUser user = dbcontext.mtblUsers.Single(p => p.intUserId == _HrmEntity.intUserId);
                    user.intUserRoleId = _HrmEntity.intUserRoleId;
                    user.varUserName = _HrmEntity.varUserName;
                    user.varPwd = _HrmEntity.varPwd;
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
                dbcontext.mtblUsers.DeleteOnSubmit(dbcontext.mtblUsers.Single(p => p.intUserId == _HrmEntity.intUserId));
                dbcontext.SubmitChanges();
                id = 1;
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }

        public string GetPassword(Entity_HRM _HrmEntity)
        {
            string strPassword = "";
            var users = from u in dbcontext.mtblUsers where u.intUserId == _HrmEntity.intUserId select u;
            if (users.Any())
            {
                strPassword = users.Single().varPwd;
            }
            return strPassword;
        }

        public void UpdatePassword(Entity_HRM _HrmEntity)
        {
            mtblUser User = dbcontext.mtblUsers.Single(p => p.intUserId == _HrmEntity.intUserId);
            User.varPwd = _HrmEntity.varPwd;
            dbcontext.SubmitChanges();
        }

        public void AddLoginDetail(Entity_HRM _HrmEntity)
        {
            ctblLoginDetail login = new ctblLoginDetail();
            login.dtDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(System.DateTime.Now, TimeZoneInfo.Local.Id, "India Standard Time");
            login.varIPNo = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].Trim();
            login.intId = _HrmEntity.intUserId;
            login.varType = "A";
            dbcontext.ctblLoginDetails.InsertOnSubmit(login);
            dbcontext.SubmitChanges();
        }

        public string GetMasterPage(decimal intUserId)
        {
            string strMasterPage = string.Empty;
            var users = from userrole in dbcontext.mtblUserRoles
                        join user in dbcontext.mtblUsers on userrole.intUserRoleId equals user.intUserRoleId
                        where user.intUserId == intUserId
                        select userrole;

            if (users.Any())
            {
                strMasterPage = users.Single().varMasterFile;
            }
            return strMasterPage;
        }

        public string GetName(Entity_HRM _HrmEntity)
        {
            string strName = String.Empty;

            var results = (from userrole in dbcontext.mtblUserRoles
                           join user in dbcontext.mtblUsers on userrole.intUserRoleId equals user.intUserRoleId
                           where user.intUserId == _HrmEntity.intUserId
                           select user.varUserName);

            if (results.Any())
            {
                strName = results.First().ToString();
            }
            return strName;
        }

        public decimal GetTotalUserCount()
        {
            decimal intCount = 0;

            var qry = from u in dbcontext.mtblUsers select u;
            if (qry.Any())
            {
                intCount = qry.Count();
            }

            return intCount;
        }

    }
}
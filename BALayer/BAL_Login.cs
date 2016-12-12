using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_Login
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetLoginDetails(string strType, DateTime startDate, DateTime enddate)
        {
            List<Entity_HRM> listLogin = new List<Entity_HRM>();

            try
            {
                var qry = (from log in dbcontext.ctblLoginDetails
                           join user in dbcontext.mtblUsers on log.intId equals user.intUserId
                           join userrole in dbcontext.mtblUserRoles on user.intUserRoleId equals userrole.intUserRoleId
                           where log.varType == "A"
                           select new
                           {
                               Id = "-",
                               Name = user.varUserName,
                               UserType = userrole.varUserRole,
                               log.varIPNo,
                               log.dtDate
                           });

                if (!string.IsNullOrEmpty(strType))
                {
                    if (strType.Equals("E"))
                    {
                        qry = (from log in dbcontext.ctblLoginDetails
                               join emp in dbcontext.mtblEmployees on log.intId equals emp.intEmpId
                               where log.varType == "P"
                               select new
                               {
                                   Id = emp.varEmpId,
                                   Name = emp.varEmpName,
                                   UserType = "Employee",
                                   log.varIPNo,
                                   log.dtDate
                               });
                    }

                    var query = (from q in qry
                                 orderby Convert.ToDateTime(q.dtDate).Date descending
                                 where Convert.ToDateTime(q.dtDate).Date >= startDate && Convert.ToDateTime(q.dtDate).Date <= enddate
                                 select new
                                 {
                                     q.Id,
                                     q.Name,
                                     q.UserType,
                                     q.varIPNo,
                                     q.dtDate
                                 }).AsEnumerable();

                    foreach (var login in query)
                    {
                        listLogin.Add(new Entity_HRM
                        {
                            strUserId = login.Id,
                            strLoginName = login.Name,
                            UserType = login.UserType,
                            dtDate = Convert.ToDateTime(login.dtDate),
                            varIPNo = login.varIPNo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return listLogin;
        }
    }
}
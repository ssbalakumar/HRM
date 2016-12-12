using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_Department
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listDept = new List<Entity_HRM>();

            try
            {
                var query = (from dep in dbcontext.mtblDepartments
                             select new
                             {
                                 dep.intDepartmentId,
                                 dep.varDepartment,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listDept.Add(new Entity_HRM { intDepartmentId = (decimal)usr.intDepartmentId, varDepartment = usr.varDepartment });
                }
            }
            catch (Exception ex)
            {
            }
            return listDept;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listDept = new List<Entity_HRM>();
            try
            {
                var query = (from dep in dbcontext.mtblDepartments
                             where dep.intDepartmentId == _HrmEntity.intDepartmentId
                             select new
                             {
                                 dep.intDepartmentId,
                                 dep.varDepartment,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listDept.Add(new Entity_HRM { intDepartmentId = (decimal)usr.intDepartmentId, varDepartment = usr.varDepartment });
                }
            }
            catch (Exception ex)
            {
            }
            return listDept;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblDepartments.SingleOrDefault(i => i.varDepartment.ToUpper() == _HrmEntity.varDepartment.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblDepartment dep = new mtblDepartment();

                    dep.varDepartment = _HrmEntity.varDepartment;
                    dbcontext.mtblDepartments.InsertOnSubmit(dep);
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
                var query = dbcontext.mtblDepartments.SingleOrDefault(i => i.varDepartment.ToUpper() == _HrmEntity.varDepartment.ToUpper() && i.intDepartmentId != _HrmEntity.intDepartmentId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblDepartment dep = dbcontext.mtblDepartments.Single(p => p.intDepartmentId == _HrmEntity.intDepartmentId);
                    dep.varDepartment = _HrmEntity.varDepartment;
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
                dbcontext.mtblDepartments.DeleteOnSubmit(dbcontext.mtblDepartments.Single(p => p.intDepartmentId == _HrmEntity.intDepartmentId));
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
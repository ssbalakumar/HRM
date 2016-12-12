using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_Designation
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listDesig = new List<Entity_HRM>();

            try
            {
                var query = (from des in dbcontext.mtblDesignations
                             select new
                             {
                                 des.intDesignationId,
                                 des.varDesignation
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listDesig.Add(new Entity_HRM { intDesignationId = (decimal)usr.intDesignationId, varDesignation = usr.varDesignation });
                }
            }
            catch (Exception ex)
            {
            }
            return listDesig;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listDesig = new List<Entity_HRM>();
            try
            {
                var query = (from des in dbcontext.mtblDesignations
                             where des.intDesignationId == _HrmEntity.intDesignationId
                             select new
                             {
                                 des.intDesignationId,
                                 des.varDesignation
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listDesig.Add(new Entity_HRM { intDesignationId = (decimal)usr.intDesignationId, varDesignation = usr.varDesignation });
                }
            }
            catch (Exception ex)
            {
            }
            return listDesig;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblDesignations.SingleOrDefault(i => i.varDesignation.ToUpper() == _HrmEntity.varDesignation.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblDesignation des = new mtblDesignation();

                    des.varDesignation = _HrmEntity.varDesignation;
                    dbcontext.mtblDesignations.InsertOnSubmit(des);
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
                var query = dbcontext.mtblDesignations.SingleOrDefault(i => i.varDesignation.ToUpper() == _HrmEntity.varDesignation.ToUpper() && i.intDesignationId != _HrmEntity.intDesignationId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblDesignation des = dbcontext.mtblDesignations.Single(p => p.intDesignationId == _HrmEntity.intDesignationId);
                    des.varDesignation = _HrmEntity.varDesignation;
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
                dbcontext.mtblDesignations.DeleteOnSubmit(dbcontext.mtblDesignations.Single(p => p.intDesignationId == _HrmEntity.intDesignationId));
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
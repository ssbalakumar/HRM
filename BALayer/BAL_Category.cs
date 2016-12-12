using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_Category
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listCat = new List<Entity_HRM>();

            try
            {
                var query = (from cat in dbcontext.mtblCategories
                             select new
                             {
                                 cat.intCategoryId,
                                 cat.varCategory,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listCat.Add(new Entity_HRM { intCategoryId = (decimal)usr.intCategoryId, varCategory = usr.varCategory });
                }
            }
            catch (Exception ex)
            {
            }
            return listCat;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listCat = new List<Entity_HRM>();
            try
            {
                var query = (from cat in dbcontext.mtblCategories
                             where cat.intCategoryId == _HrmEntity.intCategoryId
                             select new
                             {
                                 cat.intCategoryId,
                                 cat.varCategory,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listCat.Add(new Entity_HRM { intCategoryId = (decimal)usr.intCategoryId, varCategory = usr.varCategory });
                }
            }
            catch (Exception ex)
            {
            }
            return listCat;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblCategories.SingleOrDefault(i => i.varCategory.ToUpper() == _HrmEntity.varCategory.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblCategory cat = new mtblCategory();

                    cat.varCategory = _HrmEntity.varCategory;
                    dbcontext.mtblCategories.InsertOnSubmit(cat);
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
                var query = dbcontext.mtblCategories.SingleOrDefault(i => i.varCategory.ToUpper() == _HrmEntity.varCategory.ToUpper() && i.intCategoryId != _HrmEntity.intCategoryId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblCategory cat = dbcontext.mtblCategories.Single(p => p.intCategoryId == _HrmEntity.intCategoryId);
                    cat.varCategory = _HrmEntity.varCategory;
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
                dbcontext.mtblCategories.DeleteOnSubmit(dbcontext.mtblCategories.Single(p => p.intCategoryId == _HrmEntity.intCategoryId));
                dbcontext.SubmitChanges();
                id = 1;
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }
        public decimal GetTotalCatCount()
        {
            decimal intCount = 0;

            var qry = from u in dbcontext.mtblCategories select u;
            if (qry.Any())
            {
                intCount = qry.Count();
            }

            return intCount;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_Country
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listCountry = new List<Entity_HRM>();

            try
            {
                var query = (from country in dbcontext.mtblCountries
                             select new
                             {
                                 country.intCountryId,
                                 country.varCountryName
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listCountry.Add(new Entity_HRM { intCountryId = (decimal)usr.intCountryId, varCountryName = usr.varCountryName });
                }
            }
            catch (Exception ex)
            {
            }
            return listCountry;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listCountry = new List<Entity_HRM>();
            try
            {
                var query = (from country in dbcontext.mtblCountries
                             where country.intCountryId == _HrmEntity.intCountryId
                             select new
                             {
                                 country.intCountryId,
                                 country.varCountryName,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listCountry.Add(new Entity_HRM { intCountryId = (decimal)usr.intCountryId, varCountryName = usr.varCountryName });
                }
            }
            catch (Exception ex)
            {
            }
            return listCountry;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblCountries.SingleOrDefault(i => i.varCountryName.ToUpper() == _HrmEntity.varCountryName.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblCountry country = new mtblCountry();

                    country.varCountryName = _HrmEntity.varCountryName;
                    dbcontext.mtblCountries.InsertOnSubmit(country);
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
                var query = dbcontext.mtblCountries.SingleOrDefault(i => i.varCountryName.ToUpper() == _HrmEntity.varCountryName.ToUpper() && i.intCountryId != _HrmEntity.intCountryId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblCountry country = dbcontext.mtblCountries.Single(p => p.intCountryId == _HrmEntity.intCountryId);
                    country.varCountryName = _HrmEntity.varCountryName;
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
                dbcontext.mtblCountries.DeleteOnSubmit(dbcontext.mtblCountries.Single(p => p.intCountryId == _HrmEntity.intCountryId));
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
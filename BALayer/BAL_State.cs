using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_State
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> liststate = new List<Entity_HRM>();
            try
            {
                var query = (from country in dbcontext.mtblCountries
                             join state in dbcontext.ctblStates on country.intCountryId equals state.intCountryId
                             orderby state.varStateName ascending
                             select new
                             {
                                 country.intCountryId,
                                 country.varCountryName,
                                 state.intStateId,
                                 state.varStateName,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    liststate.Add(new Entity_HRM { intCountryId = (decimal)usr.intCountryId, varCountryName = usr.varCountryName, intStateId = (decimal)usr.intStateId, varStateName = usr.varStateName });
                }
            }
            catch (Exception ex)
            {
            }
            return liststate;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> liststate = new List<Entity_HRM>();
            try
            {
                var query = (from country in dbcontext.mtblCountries
                             join state in dbcontext.ctblStates on country.intCountryId equals state.intCountryId
                             where state.intStateId == _HrmEntity.intStateId
                             select new
                             {
                                 country.intCountryId,
                                 country.varCountryName,
                                 state.intStateId,
                                 state.varStateName,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    liststate.Add(new Entity_HRM { intCountryId = (decimal)usr.intCountryId, varCountryName = usr.varCountryName, intStateId = (decimal)usr.intStateId, varStateName = usr.varStateName });
                }
            }
            catch (Exception ex)
            {
            }
            return liststate;
        }

        public List<Entity_HRM> GetAllDataByCountryId(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> liststate = new List<Entity_HRM>();
            try
            {
                var query = (from country in dbcontext.mtblCountries
                             join state in dbcontext.ctblStates on country.intCountryId equals state.intCountryId
                             where country.intCountryId == _HrmEntity.intCountryId
                             select new
                             {
                                 country.intCountryId,
                                 country.varCountryName,
                                 state.intStateId,
                                 state.varStateName,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    liststate.Add(new Entity_HRM { intCountryId = (decimal)usr.intCountryId, varCountryName = usr.varCountryName, intStateId = (decimal)usr.intStateId, varStateName = usr.varStateName });
                }
            }
            catch (Exception ex)
            {
            }
            return liststate;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.ctblStates.SingleOrDefault(i => i.varStateName.ToUpper() == _HrmEntity.varStateName.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    ctblState state = new ctblState();
                    state.intCountryId = _HrmEntity.intCountryId;
                    state.varStateName = _HrmEntity.varStateName;
                    dbcontext.ctblStates.InsertOnSubmit(state);
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
                var query = dbcontext.ctblStates.SingleOrDefault(i => i.varStateName.ToUpper() == _HrmEntity.varStateName.ToUpper() && i.intStateId != _HrmEntity.intStateId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    ctblState state = dbcontext.ctblStates.Single(p => p.intStateId == _HrmEntity.intStateId);
                    state.intCountryId = _HrmEntity.intCountryId;
                    state.varStateName = _HrmEntity.varStateName;
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
                dbcontext.ctblStates.DeleteOnSubmit(dbcontext.ctblStates.Single(p => p.intStateId == _HrmEntity.intStateId));
                dbcontext.SubmitChanges();
                id = 1;
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }

        public string GetStateName(Entity_HRM _HrmEntity)
        {
            string strStateName = String.Empty;

            var results = (from state in dbcontext.ctblStates
                           where state.intStateId == _HrmEntity.intStateId
                           select state.varStateName);

            if (results.Any())
            {
                strStateName = results.First().ToString();
            }
            return strStateName;
        }

        public decimal GetTotalStateCount()
        {
            decimal intCount = 0;

            var qry = from u in dbcontext.ctblStates select u;
            if (qry.Any())
            {
                intCount = qry.Count();
            }

            return intCount;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_District
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listdistrict = new List<Entity_HRM>();
            try
            {
                var query = (from dis in dbcontext.ctblDistricts
                             join st in dbcontext.ctblStates on dis.intStateId equals st.intStateId
                             join cou in dbcontext.mtblCountries on st.intCountryId equals cou.intCountryId
                             orderby dis.intDistrictId descending
                             select new
                             {
                                 cou.intCountryId,
                                 cou.varCountryName,
                                 st.intStateId,
                                 st.varStateName,
                                 dis.intDistrictId,
                                 dis.varDistrict
                             }).AsEnumerable();
                foreach (var usr in query)
                {
                    listdistrict.Add(new Entity_HRM
                    {
                        intCountryId = (decimal)usr.intCountryId,
                        varCountryName = usr.varCountryName,
                        intStateId = (decimal)usr.intStateId,
                        varStateName = usr.varStateName,
                        intDistrictId = (decimal)usr.intDistrictId,
                        varDistrict = usr.varDistrict
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listdistrict;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HRMEntity)
        {
            List<Entity_HRM> listdistrict = new List<Entity_HRM>();

            try
            {
                var query = (from dis in dbcontext.ctblDistricts
                             join st in dbcontext.ctblStates on dis.intStateId equals st.intStateId
                             join cou in dbcontext.mtblCountries on st.intCountryId equals cou.intCountryId
                             where dis.intDistrictId == _HRMEntity.intDistrictId
                             select new
                             {
                                 cou.intCountryId,
                                 cou.varCountryName,
                                 st.intStateId,
                                 st.varStateName,
                                 dis.intDistrictId,
                                 dis.varDistrict
                             }).AsEnumerable();
                foreach (var usr in query)
                {
                    listdistrict.Add(new Entity_HRM
                    {
                        intCountryId = (decimal)usr.intCountryId,
                        varCountryName = usr.varCountryName,
                        intStateId = (decimal)usr.intStateId,
                        varStateName = usr.varStateName,
                        intDistrictId = (decimal)usr.intDistrictId,
                        varDistrict = usr.varDistrict
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listdistrict;
        }

        public List<Entity_HRM> GetAllDataByStateId(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> liststate = new List<Entity_HRM>();
            try
            {
                var query = (from states in dbcontext.ctblStates
                             join district in dbcontext.ctblDistricts on states.intStateId equals district.intStateId
                             where district.intStateId == _HrmEntity.intStateId
                             select new
                             {
                                 states.intStateId,
                                 states.varStateName,
                                 district.intDistrictId,
                                 district.varDistrict,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    liststate.Add(new Entity_HRM
                    {
                        intStateId = (decimal)usr.intStateId,
                        varStateName = usr.varStateName,
                        intDistrictId = (decimal)usr.intDistrictId,
                        varDistrict = usr.varDistrict
                    });
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
                var query = dbcontext.ctblDistricts.SingleOrDefault(i => i.varDistrict.ToUpper() == _HrmEntity.varDistrict.ToUpper() && i.intStateId == _HrmEntity.intStateId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    ctblDistrict dis = new ctblDistrict();
                    dis.intDistrictId = _HrmEntity.intDistrictId;
                    dis.intStateId = _HrmEntity.intStateId;
                    dis.varDistrict = _HrmEntity.varDistrict;
                    dbcontext.ctblDistricts.InsertOnSubmit(dis);
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
                var query = dbcontext.ctblDistricts.SingleOrDefault(i => i.varDistrict.ToUpper() == _HrmEntity.varDistrict.ToUpper() && i.intStateId != _HrmEntity.intStateId && i.intDistrictId != _HrmEntity.intDistrictId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    ctblDistrict dis = dbcontext.ctblDistricts.Single(p => p.intDistrictId == _HrmEntity.intDistrictId);
                    dis.intDistrictId = _HrmEntity.intDistrictId;
                    dis.intStateId = _HrmEntity.intStateId;
                    dis.varDistrict = _HrmEntity.varDistrict;
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
                dbcontext.ctblDistricts.DeleteOnSubmit(dbcontext.ctblDistricts.Single(p => p.intDistrictId == _HrmEntity.intDistrictId));
                dbcontext.SubmitChanges();
                id = 1;
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }

        public string GetDistName(Entity_HRM _HrmEntity)
        {
            string strDistName = String.Empty;

            var results = (from dist in dbcontext.ctblDistricts
                           where dist.intDistrictId == _HrmEntity.intDistrictId
                           select dist.varDistrict);

            if (results.Any())
            {
                strDistName = results.First().ToString();
            }
            return strDistName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_Company
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listCompany = new List<Entity_HRM>();

            try
            {
                var query = (from company in dbcontext.mtblCompanies
                             join country in dbcontext.mtblCountries on company.intCountryId equals country.intCountryId
                             join state in dbcontext.ctblStates on company.intStateId equals state.intStateId
                             join district in dbcontext.ctblDistricts on company.intDistrictId equals district.intDistrictId
                             orderby company.intCompanyId
                             select new
                             {
                                 company.intCompanyId,
                                 company.varCompTitle,
                                 company.varCompanyName,
                                 company.intCountryId,
                                 country.varCountryName,
                                 company.intStateId,
                                 state.varStateName,
                                 company.intDistrictId,
                                 district.varDistrict,
                                 company.varCity,
                                 company.varAdd,
                                 company.intPincode,
                                 company.varMobile,
                                 company.varPhone,
                                 company.varEmail,
                                 company.varBankAccNo,
                                 company.varBankCode,
                                 company.varBankName,
                                 company.varBankCity,
                                 company.varPFNo,
                                 company.varESINo,
                                 company.intCompEsiPer,
                                 company.intEmpEsiPer,
                                 company.intPfLimit,
                                 company.intEsiLimit,
                                 company.intEsiDailyWage,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listCompany.Add(new Entity_HRM
                    {
                        intCompanyId = usr.intCompanyId,
                        varCompTitle = usr.varCompTitle,
                        varCompanyName = usr.varCompanyName,
                        intCountryId = (decimal)usr.intCountryId,
                        varCountryName = usr.varCountryName,
                        intStateId = (decimal)usr.intStateId,
                        varStateName = usr.varStateName,
                        intDistrictId = (decimal)usr.intDistrictId,
                        varDistrict = usr.varDistrict,
                        varCity = usr.varCity,
                        varAdd = usr.varAdd,
                        intPincode = (decimal)usr.intPincode,
                        varMobile = usr.varMobile,
                        varPhone = usr.varPhone,
                        varEmail = usr.varEmail,
                        varBankAccNo = usr.varBankAccNo,
                        varBankCode = usr.varBankCode,
                        varBankName = usr.varBankName,
                        varBankCity = usr.varBankCity,
                        varPFNo = usr.varPFNo,
                        varESINo = usr.varESINo,
                        intCompEsiPer = (decimal)usr.intCompEsiPer,
                        intEmpEsiPer = (decimal)usr.intEmpEsiPer,
                        intPfLimit = (decimal)usr.intPfLimit,
                        intEsiLimit = (decimal)usr.intEsiLimit,
                        intEsiDailyWage = (decimal)usr.intEsiDailyWage,
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listCompany;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listCompany = new List<Entity_HRM>();
            try
            {
                var query = (from company in dbcontext.mtblCompanies
                             join country in dbcontext.mtblCountries on company.intCountryId equals country.intCountryId
                             join state in dbcontext.ctblStates on company.intStateId equals state.intStateId
                             join district in dbcontext.ctblDistricts on company.intDistrictId equals district.intDistrictId
                             where company.intCompanyId == _HrmEntity.intCompanyId
                             select new
                             {
                                 company.intCompanyId,
                                 company.varCompTitle,
                                 company.varCompanyName,
                                 company.intCountryId,
                                 country.varCountryName,
                                 company.intStateId,
                                 state.varStateName,
                                 company.intDistrictId,
                                 district.varDistrict,
                                 company.varCity,
                                 company.varAdd,
                                 company.intPincode,
                                 company.varMobile,
                                 company.varPhone,
                                 company.varEmail,
                                 company.varBankAccNo,
                                 company.varBankCode,
                                 company.varBankName,
                                 company.varBankCity,
                                 company.varPFNo,
                                 company.varESINo,
                                 company.intCompEsiPer,
                                 company.intEmpEsiPer,
                                 company.intPfLimit,
                                 company.intEsiLimit,
                                 company.intEsiDailyWage,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listCompany.Add(new Entity_HRM
                    {
                        intCompanyId = usr.intCompanyId,
                        varCompTitle = usr.varCompTitle,
                        varCompanyName = usr.varCompanyName,
                        intCountryId = (decimal)usr.intCountryId,
                        varCountryName = usr.varCountryName,
                        intStateId = (decimal)usr.intStateId,
                        varStateName = usr.varStateName,
                        intDistrictId = (decimal)usr.intDistrictId,
                        varDistrict = usr.varDistrict,
                        varCity = usr.varCity,
                        varAdd = usr.varAdd,
                        intPincode = (decimal)usr.intPincode,
                        varMobile = usr.varMobile,
                        varPhone = usr.varPhone,
                        varEmail = usr.varEmail,
                        varBankAccNo = usr.varBankAccNo,
                        varBankCode = usr.varBankCode,
                        varBankName = usr.varBankName,
                        varBankCity = usr.varBankCity,
                        varPFNo = usr.varPFNo,
                        varESINo = usr.varESINo,
                        intCompEsiPer = (decimal)usr.intCompEsiPer,
                        intEmpEsiPer = (decimal)usr.intEmpEsiPer,
                        intPfLimit = (decimal)usr.intPfLimit,
                        intEsiLimit = (decimal)usr.intEsiLimit,
                        intEsiDailyWage = (decimal)usr.intEsiDailyWage,
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listCompany;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblCompanies.SingleOrDefault(i => i.varCompanyName.ToUpper() == _HrmEntity.varCompanyName.ToUpper());
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblCompany company = new mtblCompany();

                    company.varCompTitle = _HrmEntity.varCompTitle;
                    company.varCompanyName = _HrmEntity.varCompanyName;
                    company.intCountryId = _HrmEntity.intCountryId;
                    company.intStateId = _HrmEntity.intStateId;
                    company.intDistrictId = _HrmEntity.intDistrictId;
                    company.varCity = _HrmEntity.varCity;
                    company.varAdd = _HrmEntity.varAdd;
                    company.intPincode = _HrmEntity.intPincode;
                    company.varMobile = _HrmEntity.varMobile;
                    company.varPhone = _HrmEntity.varPhone;
                    company.varEmail = _HrmEntity.varEmail;
                    company.varBankAccNo = _HrmEntity.varBankAccNo;
                    company.varBankCode = _HrmEntity.varBankCode;
                    company.varBankName = _HrmEntity.varBankName;
                    company.varBankCity = _HrmEntity.varBankCity;
                    company.varPFNo = _HrmEntity.varPFNo;
                    company.varESINo = _HrmEntity.varESINo;
                    company.intCompEsiPer = _HrmEntity.intCompEsiPer;
                    company.intEmpEsiPer = _HrmEntity.intEmpEsiPer;
                    company.intPfLimit = _HrmEntity.intPfLimit;
                    company.intEsiLimit = _HrmEntity.intEsiLimit;
                    company.intEsiDailyWage = _HrmEntity.intEsiDailyWage;
                    dbcontext.mtblCompanies.InsertOnSubmit(company);
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
                var query = dbcontext.mtblCompanies.SingleOrDefault(i => i.varCompanyName.ToUpper() == _HrmEntity.varCompanyName.ToUpper() && i.intCompanyId != _HrmEntity.intCompanyId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblCompany company = dbcontext.mtblCompanies.Single(p => p.intCompanyId == _HrmEntity.intCompanyId);

                    company.varCompTitle = _HrmEntity.varCompTitle;
                    company.varCompanyName = _HrmEntity.varCompanyName;
                    company.intCountryId = _HrmEntity.intCountryId;
                    company.intStateId = _HrmEntity.intStateId;
                    company.intDistrictId = _HrmEntity.intDistrictId;
                    company.varCity = _HrmEntity.varCity;
                    company.varAdd = _HrmEntity.varAdd;
                    company.intPincode = _HrmEntity.intPincode;
                    company.varMobile = _HrmEntity.varMobile;
                    company.varPhone = _HrmEntity.varPhone;
                    company.varEmail = _HrmEntity.varEmail;
                    company.varBankAccNo = _HrmEntity.varBankAccNo;
                    company.varBankCode = _HrmEntity.varBankCode;
                    company.varBankName = _HrmEntity.varBankName;
                    company.varBankCity = _HrmEntity.varBankCity;
                    company.varPFNo = _HrmEntity.varPFNo;
                    company.varESINo = _HrmEntity.varESINo;
                    company.intCompEsiPer = _HrmEntity.intCompEsiPer;
                    company.intEmpEsiPer = _HrmEntity.intEmpEsiPer;
                    company.intPfLimit = _HrmEntity.intPfLimit;
                    company.intEsiLimit = _HrmEntity.intEsiLimit;
                    company.intEsiDailyWage = _HrmEntity.intEsiDailyWage;
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
                dbcontext.mtblCompanies.DeleteOnSubmit(dbcontext.mtblCompanies.Single(p => p.intCompanyId == _HrmEntity.intCompanyId));
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
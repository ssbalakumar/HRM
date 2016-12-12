using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_Employee
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public string GetMaxEmpNo()
        {
            string strMaxEmpId, strEmpId;

            var results = (from emp in dbcontext.mtblEmployees
                           orderby emp.intEmpId descending
                           select emp.varEmpId);

            if (results.Any())
            {
                strMaxEmpId = results.First().ToString();
                strEmpId = GenerateEmpId(strMaxEmpId);
            }
            else
            {
                strEmpId = "gss0001";
            }
            return strEmpId;
        }

        public decimal GetMaxEmpID()
        {
            decimal intEmpId = 0;
            var qry1 = from u in dbcontext.mtblEmployees select u;
            if (qry1.Any())
            {
                var qry = from u in dbcontext.GetIdentity("mtblEmployee") select u;
                foreach (var r in qry)
                {
                    intEmpId = decimal.Parse(r.IdentValue.ToString());
                }
                if (intEmpId >= 1)
                {
                    intEmpId += 1;
                }
            }
            else
            {
                intEmpId = 1;
            }
            return intEmpId;
        }

        public string GenerateEmpId(string strMaxEmpId)
        {
            int intlen = strMaxEmpId.Length;

            int len = intlen - 3;

            string str = strMaxEmpId.Substring(3, len);
            int intMaxEmpNumber = int.Parse(str);
            intMaxEmpNumber = intMaxEmpNumber + 1;

            if (intMaxEmpNumber > 1 && intMaxEmpNumber <= 9)
            {
                str = "gss000" + intMaxEmpNumber.ToString();
            }

            if (intMaxEmpNumber > 9 && intMaxEmpNumber <= 99)
            {
                str = "gs00" + intMaxEmpNumber.ToString();
            }

            if (intMaxEmpNumber > 99 && intMaxEmpNumber <= 999)
            {
                str = "gss0" + intMaxEmpNumber.ToString();
            }
            if (intMaxEmpNumber > 999)
            {
                str = "gss" + intMaxEmpNumber.ToString();
            }
            return str;
        }

        public List<Entity_HRM> GetAllEmp()
        {
            List<Entity_HRM> listEmp = new List<Entity_HRM>();
            try
            {
                var query = (from emp in dbcontext.mtblEmployees
                             join country in dbcontext.mtblCountries on emp.intCountryId equals country.intCountryId
                             join state in dbcontext.ctblStates on emp.intStateId equals state.intStateId
                             join district in dbcontext.ctblDistricts on emp.intDistrictId equals district.intDistrictId
                             select new
                             {
                                 emp.intEmpId,
                                 emp.varEmpId,
                                 emp.varPwd,
                                 emp.varImage,
                                 emp.varPdf,
                                 emp.intCompanyId,
                                 emp.intEmpType,
                                 emp.varTitle,
                                 emp.varInitial,
                                 emp.varEmpName,
                                 emp.varSex,
                                 emp.varBloodGroup,
                                 emp.varFHName,
                                 emp.varGuardian,
                                 emp.varEducation,
                                 emp.varReligion,
                                 emp.varCommunity,
                                 emp.varMarital,
                                 emp.intCountryId,
                                 country.varCountryName,
                                 emp.intStateId,
                                 state.varStateName,
                                 emp.intDistrictId,
                                 district.varDistrict,
                                 emp.varAdd,
                                 emp.varCity,
                                 emp.intPincode,
                                 emp.varMobile,
                                 emp.varEmail,
                                 emp.dtDOB,
                                 emp.dtDOJ,
                                 emp.varDisability,
                                 emp.varPFNo,
                                 emp.varESINo,
                                 emp.varDispensaryName,
                                 emp.varDispensaryCity,
                                 emp.varCompanyESINo,
                                 emp.intCategoryId,
                                 emp.intDepartmentId,
                                 emp.intDesignationId,
                                 emp.varBankAccNo,
                                 emp.varBankCode,
                                 emp.varBankCity,
                                 emp.intSalaryMode,
                                 emp.intSalaryType,
                                 emp.intSalary,
                                 emp.intHra,
                                 emp.intDa,
                                 emp.intWa,
                                 emp.intTa,
                                 emp.intOa,
                                 emp.intOtPerHour,
                                 emp.intGrossPay,
                                 emp.dtResignDate,
                                 emp.varResignReason,
                                 emp.dtRejoinDate,
                                 emp.intUserId,
                                 emp.dtDate,
                                 emp.bitApprove,
                             }).AsEnumerable();

                foreach (var emp in query)
                {
                    listEmp.Add(new Entity_HRM
                    {
                        intEmpId = (decimal)emp.intEmpId,
                        varEmpId = emp.varEmpId,
                        varPwd = emp.varPwd,
                        varImage = !string.IsNullOrEmpty(emp.varImage) ? "~/photos/emp/" + emp.varImage : (emp.varSex == "Male" ? "~/photos/emp/male-no-image.jpg" : "~/photos/emp/female-no-image.jpg"),
                        varPdf = "~/resume/emp/" + emp.varPdf,
                        varViewImage = string.IsNullOrEmpty(emp.varPdf) ? "true" : "false",
                        intCompanyId = (decimal)emp.intCompanyId,
                        intEmpType = (decimal)emp.intEmpType,
                        varTitle = emp.varTitle,
                        varInitial = emp.varInitial,
                        varEmpName = emp.varEmpName,
                        varSex = emp.varSex,
                        varBloodGroup = emp.varBloodGroup,
                        varFHName = emp.varFHName,
                        varGuardian = emp.varGuardian,
                        varEducation = emp.varEducation,
                        varReligion = emp.varReligion,
                        varCommunity = emp.varCommunity,
                        varMarital = emp.varMarital,
                        varAdd = emp.varAdd,
                        varCity = emp.varCity,
                        intCountryId = (decimal)emp.intCountryId,
                        varCountryName = emp.varCountryName,
                        intStateId = (decimal)emp.intStateId,
                        varStateName = emp.varStateName,
                        intDistrictId = (decimal)emp.intDistrictId,
                        varDistrict = emp.varDistrict,
                        intPincode = (decimal)emp.intPincode,
                        varMobile = emp.varMobile,
                        varEmail = emp.varEmail,
                        dtDOB = emp.dtDOB.Value.Date,
                        dtDOJ = emp.dtDOJ.Value.Date,
                        varDisability = emp.varDisability,
                        varPFNo = emp.varPFNo,
                        varESINo = emp.varESINo,
                        varDispensaryName = emp.varDispensaryName,
                        varDispensaryCity = emp.varDispensaryCity,
                        varCompanyESINo = emp.varCompanyESINo,
                        intCategoryId = (decimal)emp.intCategoryId,
                        intDepartmentId = (decimal)emp.intDepartmentId,
                        intDesignationId = (decimal)emp.intDesignationId,
                        varBankAccNo = emp.varBankAccNo,
                        varBankCode = emp.varBankCode,
                        varBankCity = emp.varBankCity,
                        intSalaryMode = (decimal)emp.intSalaryMode,
                        intSalaryType = (decimal)emp.intSalaryType,
                        intSalary = (decimal)emp.intSalary,
                        intHra = (decimal)emp.intHra,
                        intDa = (decimal)emp.intDa,
                        intWa = (decimal)emp.intWa,
                        intTa = (decimal)emp.intTa,
                        intOa = (decimal)emp.intOa,
                        intOtPerHour = (decimal)emp.intOtPerHour,
                        intGrossPay = (decimal)emp.intGrossPay,
                        dtResignDate = Convert.ToDateTime(emp.dtResignDate),
                        varResignReason = emp.varResignReason,
                        dtRejoinDate = Convert.ToDateTime(emp.dtRejoinDate),
                        intUserId = (decimal)emp.intUserId,
                        dtDate = Convert.ToDateTime(emp.dtDate),
                        strApprove = emp.bitApprove == true ? "Approve" : "UnApprove",
                        bitApprove = (bool)emp.bitApprove
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listEmp;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listEmp = new List<Entity_HRM>();
            try
            {
                var query = (from emp in dbcontext.mtblEmployees
                             join country in dbcontext.mtblCountries on emp.intCountryId equals country.intCountryId
                             join state in dbcontext.ctblStates on emp.intStateId equals state.intStateId
                             join district in dbcontext.ctblDistricts on emp.intDistrictId equals district.intDistrictId
                             where emp.intEmpId == _HrmEntity.intEmpId
                             select new
                             {
                                 emp.intEmpId,
                                 emp.varEmpId,
                                 emp.varPwd,
                                 emp.varImage,
                                 emp.varPdf,
                                 emp.intCompanyId,
                                 emp.intEmpType,
                                 emp.varTitle,
                                 emp.varInitial,
                                 emp.varEmpName,
                                 emp.varSex,
                                 emp.varBloodGroup,
                                 emp.varFHName,
                                 emp.varGuardian,
                                 emp.varEducation,
                                 emp.varReligion,
                                 emp.varCommunity,
                                 emp.varMarital,
                                 emp.varAdd,
                                 emp.varCity,
                                 emp.intCountryId,
                                 country.varCountryName,
                                 emp.intStateId,
                                 state.varStateName,
                                 emp.intDistrictId,
                                 district.varDistrict,
                                 emp.intPincode,
                                 emp.varMobile,
                                 emp.varEmail,
                                 emp.dtDOB,
                                 emp.dtDOJ,
                                 emp.varDisability,
                                 emp.varPFNo,
                                 emp.varESINo,
                                 emp.varDispensaryName,
                                 emp.varDispensaryCity,
                                 emp.varCompanyESINo,
                                 emp.intCategoryId,
                                 emp.intDepartmentId,
                                 emp.intDesignationId,
                                 emp.varBankAccNo,
                                 emp.varBankCode,
                                 emp.varBankCity,
                                 emp.intSalaryMode,
                                 emp.intSalaryType,
                                 emp.intSalary,
                                 emp.intHra,
                                 emp.intDa,
                                 emp.intWa,
                                 emp.intTa,
                                 emp.intOa,
                                 emp.intOtPerHour,
                                 emp.intGrossPay,
                                 emp.dtResignDate,
                                 emp.varResignReason,
                                 emp.dtRejoinDate,
                                 emp.intUserId,
                                 emp.dtDate,
                                 emp.bitApprove,
                             }).AsEnumerable();

                foreach (var emp in query)
                {
                    listEmp.Add(new Entity_HRM
                    {
                        intEmpId = (decimal)emp.intEmpId,
                        varEmpId = emp.varEmpId,
                        varPwd = emp.varPwd,
                        varImage = !string.IsNullOrEmpty(emp.varImage) ? "~/photos/emp/" + emp.varImage : (emp.varSex == "Male" ? "~/photos/emp/male-no-image.jpg" : "~/photos/emp/female-no-image.jpg"),
                        varPdf = "~/resume/emp/" + emp.varPdf,
                        varViewImage = string.IsNullOrEmpty(emp.varPdf) ? "true" : "false",
                        intCompanyId = (decimal)emp.intCompanyId,
                        intEmpType = (decimal)emp.intEmpType,
                        varTitle = emp.varTitle,
                        varInitial = emp.varInitial,
                        varEmpName = emp.varEmpName,
                        varSex = emp.varSex,
                        varBloodGroup = emp.varBloodGroup,
                        varFHName = emp.varFHName,
                        varGuardian = emp.varGuardian,
                        varEducation = emp.varEducation,
                        varReligion = emp.varReligion,
                        varCommunity = emp.varCommunity,
                        varMarital = emp.varMarital,
                        varAdd = emp.varAdd,
                        varCity = emp.varCity,
                        intCountryId = (decimal)emp.intCountryId,
                        varCountryName = emp.varCountryName,
                        intStateId = (decimal)emp.intStateId,
                        varStateName = emp.varStateName,
                        intDistrictId = (decimal)emp.intDistrictId,
                        varDistrict = emp.varDistrict,
                        intPincode = (decimal)emp.intPincode,
                        varMobile = emp.varMobile,
                        varEmail = emp.varEmail,
                        dtDOB = emp.dtDOB.Value.Date,
                        dtDOJ = emp.dtDOJ.Value.Date,
                        varDisability = emp.varDisability,
                        varPFNo = emp.varPFNo,
                        varESINo = emp.varESINo,
                        varDispensaryName = emp.varDispensaryName,
                        varDispensaryCity = emp.varDispensaryCity,
                        varCompanyESINo = emp.varCompanyESINo,
                        intCategoryId = (decimal)emp.intCategoryId,
                        intDepartmentId = (decimal)emp.intDepartmentId,
                        intDesignationId = (decimal)emp.intDesignationId,
                        varBankAccNo = emp.varBankAccNo,
                        varBankCode = emp.varBankCode,
                        varBankCity = emp.varBankCity,
                        intSalaryMode = (decimal)emp.intSalaryMode,
                        intSalaryType = (decimal)emp.intSalaryType,
                        intSalary = (decimal)emp.intSalary,
                        intHra = (decimal)emp.intHra,
                        intDa = (decimal)emp.intDa,
                        intWa = (decimal)emp.intWa,
                        intTa = (decimal)emp.intTa,
                        intOa = (decimal)emp.intOa,
                        intOtPerHour = (decimal)emp.intOtPerHour,
                        intGrossPay = (decimal)emp.intGrossPay,
                        dtResignDate = Convert.ToDateTime(emp.dtResignDate),
                        varResignReason = emp.varResignReason,
                        dtRejoinDate = Convert.ToDateTime(emp.dtRejoinDate),
                        intUserId = (decimal)emp.intUserId,
                        dtDate = Convert.ToDateTime(emp.dtDate),
                        strApprove = emp.bitApprove == true ? "Approve" : "UnApprove",
                        bitApprove = (bool)emp.bitApprove
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listEmp;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblEmployees.SingleOrDefault(i => i.varEmpName.ToUpper() == _HrmEntity.varEmpName.ToUpper() && i.varMobile == _HrmEntity.varMobile);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblEmployee emp = new mtblEmployee();
                    emp.varEmpId = GetMaxEmpNo();
                    emp.varPwd = _HrmEntity.varPwd;
                    emp.varImage = _HrmEntity.varImage;
                    emp.varPdf = _HrmEntity.varPdf;
                    emp.intCompanyId = _HrmEntity.intCompanyId;
                    emp.intEmpType = _HrmEntity.intEmpType;
                    emp.varTitle = _HrmEntity.varTitle;
                    emp.varInitial = _HrmEntity.varInitial;
                    emp.varEmpName = _HrmEntity.varEmpName;
                    emp.varSex = _HrmEntity.varSex;
                    emp.varBloodGroup = _HrmEntity.varBloodGroup;
                    emp.varFHName = _HrmEntity.varFHName;
                    emp.varGuardian = _HrmEntity.varGuardian;
                    emp.varEducation = _HrmEntity.varEducation;
                    emp.varReligion = _HrmEntity.varReligion;
                    emp.varCommunity = _HrmEntity.varCommunity;
                    emp.varMarital = _HrmEntity.varMarital;
                    emp.intCountryId = _HrmEntity.intCountryId;
                    emp.intStateId = _HrmEntity.intStateId;
                    emp.intDistrictId = _HrmEntity.intDistrictId;
                    emp.varAdd = _HrmEntity.varAdd;
                    emp.varCity = _HrmEntity.varCity;
                    emp.intPincode = _HrmEntity.intPincode;
                    emp.varMobile = _HrmEntity.varMobile;
                    emp.varEmail = _HrmEntity.varEmail;
                    emp.dtDOB = _HrmEntity.dtDOB;
                    emp.dtDOJ = _HrmEntity.dtDOJ;
                    emp.varDisability = _HrmEntity.varDisability;
                    emp.varPFNo = _HrmEntity.varPFNo;
                    emp.varESINo = _HrmEntity.varESINo;
                    emp.varDispensaryName = _HrmEntity.varDispensaryName;
                    emp.varDispensaryCity = _HrmEntity.varDispensaryCity;
                    emp.varCompanyESINo = _HrmEntity.varCompanyESINo;
                    emp.intCategoryId = _HrmEntity.intCategoryId;
                    emp.intDepartmentId = _HrmEntity.intDepartmentId;
                    emp.intDesignationId = _HrmEntity.intDesignationId;
                    emp.varBankAccNo = _HrmEntity.varBankAccNo;
                    emp.varBankCode = _HrmEntity.varBankCode;
                    emp.varBankCity = _HrmEntity.varBankCity;
                    emp.intSalaryMode = _HrmEntity.intSalaryMode;
                    emp.intSalaryType = _HrmEntity.intSalaryType;
                    emp.intSalary = _HrmEntity.intSalary;
                    emp.intHra = _HrmEntity.intHra;
                    emp.intDa = _HrmEntity.intDa;
                    emp.intWa = _HrmEntity.intWa;
                    emp.intTa = _HrmEntity.intTa;
                    emp.intOa = _HrmEntity.intOa;
                    emp.intOtPerHour = _HrmEntity.intOtPerHour;
                    emp.intGrossPay = _HrmEntity.intGrossPay;
                    emp.dtResignDate = _HrmEntity.dtResignDate;
                    emp.varResignReason = _HrmEntity.varResignReason;
                    emp.dtRejoinDate = _HrmEntity.dtRejoinDate;
                    emp.intUserId = _HrmEntity.intUserId;
                    emp.dtDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(System.DateTime.Now, TimeZoneInfo.Local.Id, "India Standard Time");
                    emp.bitApprove = true;
                    dbcontext.mtblEmployees.InsertOnSubmit(emp);
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
                System.Console.Error.Write(ex.Message);
            }
            return id;
        }

        public int Update(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblEmployees.SingleOrDefault(i => i.varEmpName.ToUpper() == _HrmEntity.varEmpName.ToUpper() && i.varMobile == _HrmEntity.varMobile && i.intEmpId != _HrmEntity.intEmpId);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblEmployee emp = dbcontext.mtblEmployees.Single(p => p.intEmpId == _HrmEntity.intEmpId);
                    emp.varEmpId = GetMaxEmpNo();
                    emp.varPwd = _HrmEntity.varPwd;
                    emp.varImage = _HrmEntity.varImage;
                    emp.varPdf = _HrmEntity.varPdf;
                    emp.intCompanyId = _HrmEntity.intCompanyId;
                    emp.intEmpType = _HrmEntity.intEmpType;
                    emp.varTitle = _HrmEntity.varTitle;
                    emp.varInitial = _HrmEntity.varInitial;
                    emp.varEmpName = _HrmEntity.varEmpName;
                    emp.varSex = _HrmEntity.varSex;
                    emp.varBloodGroup = _HrmEntity.varBloodGroup;
                    emp.varFHName = _HrmEntity.varFHName;
                    emp.varGuardian = _HrmEntity.varGuardian;
                    emp.varEducation = _HrmEntity.varEducation;
                    emp.varReligion = _HrmEntity.varReligion;
                    emp.varCommunity = _HrmEntity.varCommunity;
                    emp.varMarital = _HrmEntity.varMarital;
                    emp.intCountryId = _HrmEntity.intCountryId;
                    emp.intStateId = _HrmEntity.intStateId;
                    emp.intDistrictId = _HrmEntity.intDistrictId;
                    emp.varAdd = _HrmEntity.varAdd;
                    emp.varCity = _HrmEntity.varCity;
                    emp.intPincode = _HrmEntity.intPincode;
                    emp.varMobile = _HrmEntity.varMobile;
                    emp.varEmail = _HrmEntity.varEmail;
                    emp.dtDOB = _HrmEntity.dtDOB;
                    emp.dtDOJ = _HrmEntity.dtDOJ;
                    emp.varDisability = _HrmEntity.varDisability;
                    emp.varPFNo = _HrmEntity.varPFNo;
                    emp.varESINo = _HrmEntity.varESINo;
                    emp.varDispensaryName = _HrmEntity.varDispensaryName;
                    emp.varDispensaryCity = _HrmEntity.varDispensaryCity;
                    emp.varCompanyESINo = _HrmEntity.varCompanyESINo;
                    emp.intCategoryId = _HrmEntity.intCategoryId;
                    emp.intDepartmentId = _HrmEntity.intDepartmentId;
                    emp.intDesignationId = _HrmEntity.intDesignationId;
                    emp.varBankAccNo = _HrmEntity.varBankAccNo;
                    emp.varBankCode = _HrmEntity.varBankCode;
                    emp.varBankCity = _HrmEntity.varBankCity;
                    emp.intSalaryMode = _HrmEntity.intSalaryMode;
                    emp.intSalaryType = _HrmEntity.intSalaryType;
                    emp.intSalary = _HrmEntity.intSalary;
                    emp.intHra = _HrmEntity.intHra;
                    emp.intDa = _HrmEntity.intDa;
                    emp.intWa = _HrmEntity.intWa;
                    emp.intTa = _HrmEntity.intTa;
                    emp.intOa = _HrmEntity.intOa;
                    emp.intOtPerHour = _HrmEntity.intOtPerHour;
                    emp.intGrossPay = _HrmEntity.intGrossPay;
                    emp.dtResignDate = _HrmEntity.dtResignDate;
                    emp.varResignReason = _HrmEntity.varResignReason;
                    emp.dtRejoinDate = _HrmEntity.dtRejoinDate;
                    emp.intUserId = _HrmEntity.intUserId;
                    emp.dtDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(System.DateTime.Now, TimeZoneInfo.Local.Id, "India Standard Time");
                    emp.bitApprove = true;
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
                dbcontext.mtblEmployees.DeleteOnSubmit(dbcontext.mtblEmployees.Single(p => p.intEmpId == _HrmEntity.intEmpId));
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
            var pwd = from p in dbcontext.mtblEmployees where p.intEmpId == _HrmEntity.intEmpId select p;
            if (pwd.Any())
            {
                strPassword = pwd.Single().varPwd;
            }
            return strPassword;
        }

        public void UpdatePassword(Entity_HRM _HrmEntity)
        {
            mtblEmployee pwd = dbcontext.mtblEmployees.Single(p => p.intEmpId == _HrmEntity.intEmpId);
            pwd.varPwd = _HrmEntity.varPwd;
            dbcontext.SubmitChanges();
        }

        public void AddLoginDetail(Entity_HRM _HrmEntity)
        {
            ctblLoginDetail login = new ctblLoginDetail();
            login.dtDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(System.DateTime.Now, TimeZoneInfo.Local.Id, "India Standard Time");
            login.varIPNo = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].Trim();
            login.intId = _HrmEntity.intEmpId;
            login.varType = "E";
            dbcontext.ctblLoginDetails.InsertOnSubmit(login);
            dbcontext.SubmitChanges();
        }

        public string GetMasterPage(decimal intUserId)
        {
            string strMasterPage = string.Empty;
            var users = from userrole in dbcontext.mtblUserRoles
                        join user in dbcontext.mtblEmployees on userrole.intUserRoleId equals user.intEmpId
                        where user.intUserId == intUserId
                        select userrole;
            if (users.Any())
            {
                strMasterPage = users.Single().varMasterFile;
            }
            return strMasterPage;
        }

        public string getImage(Entity_HRM _HrmEntity)
        {
            string strImage = string.Empty;
            try
            {
                var query = from emp in dbcontext.mtblEmployees
                            where emp.intEmpId == _HrmEntity.intEmpId
                            select new
                            {
                                emp.varImage,
                            };
                if (query.Any())
                {
                    strImage = query.Single().varImage.ToString();
                }
            }
            catch (Exception ex)
            {
                strImage = string.Empty;
            }
            return strImage;
        }

        public string getPdf(Entity_HRM _HrmEntity)
        {
            string strPdf = string.Empty;
            try
            {
                var query = from pdf in dbcontext.mtblEmployees
                            where pdf.intEmpId == _HrmEntity.intEmpId
                            select new
                            {
                                pdf.varPdf,
                            };
                if (query.Any())
                {
                    strPdf = query.Single().varPdf.ToString();
                }
            }
            catch (Exception ex)
            {
                strPdf = string.Empty;
            }
            return strPdf;
        }

        public decimal GetTotalEmpCount()
        {
            decimal intCount = 0;

            var qry = from u in dbcontext.mtblEmployees select u;
            if (qry.Any())
            {
                intCount = qry.Count();
            }

            return intCount;
        }
    }
}
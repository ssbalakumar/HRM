using System;

namespace EntityLayer
{
    public class Entity_HRM
    {
        // Table Name : mtblUser
        public decimal intUserId { get; set; }

        public string varUserName { get; set; }
        public string varPwd { get; set; }
        public DateTime dtDate { get; set; }

        // Table Name : mtblUserRole
        public decimal intUserRoleId { get; set; }

        public string varUserRole { get; set; }
        public string varMasterFile { get; set; }

        // Table Name : ctblLoginDetail
        public decimal intLoginDetailsId { get; set; }

        public string varIPNo { get; set; }
        public decimal intId { get; set; }
        public string varType { get; set; }

        // Table Name : mtblAccYear
        public decimal intAccYear { get; set; }

        public string varAccYear { get; set; }
        public DateTime dtBeginDate { get; set; }
        public DateTime dtEndDate { get; set; }

        // Table Name : mtblCountry

        public decimal intCountryId { get; set; }
        public string varCountryName { get; set; }

        // Table Name : ctblState
        public decimal intStateId { get; set; }

        public string varStateName { get; set; }

        // Table Name : ctblDistrict
        public decimal intDistrictId { get; set; }

        public string varDistrict { get; set; }

        // Table Name : ctblCity
        public decimal intCityId { get; set; }

        public string varCity { get; set; }

        // Table Name : mtblCategory
        public decimal intCategoryId { get; set; }

        public string varCategory { get; set; }

        // Table Name : mtblDepartment
        public decimal intDepartmentId { get; set; }

        public string varDepartment { get; set; }

        // Table Name : mtblDesignation
        public decimal intDesignationId { get; set; }

        public string varDesignation { get; set; }

        // Table Name : mtblEmployee
        public decimal intEmpId { get; set; }

        public string varEmpId { get; set; }
        public decimal intEmpType { get; set; }
        public string varTitle { get; set; }
        public string varInitial { get; set; }
        public string varEmpName { get; set; }
        public string varFHName { get; set; }
        public string varGuardian { get; set; }
        public string varEducation { get; set; }
        public string varReligion { get; set; }
        public string varCommunity { get; set; }
        public string varMarital { get; set; }
        public decimal intAge { get; set; }
        public DateTime dtDOB { get; set; }
        public DateTime dtDOJ { get; set; }
        public string varDisability { get; set; }
        public string varDispensaryName { get; set; }
        public string varDispensaryCity { get; set; }
        public string varCompanyESINo { get; set; }
        public decimal intSalaryMode { get; set; }
        public decimal intSalaryType { get; set; }
        public decimal intSalary { get; set; }
        public decimal intHra { get; set; }
        public decimal intDa { get; set; }
        public decimal intWa { get; set; }
        public decimal intTa { get; set; }
        public decimal intOa { get; set; }
        public decimal intOtPerHour { get; set; }
        public decimal intGrossPay { get; set; }
        public DateTime dtResignDate { get; set; }
        public string varResignReason { get; set; }
        public DateTime dtRejoinDate { get; set; }

        //Table Name: mtblCompany
        public decimal intCompanyId { get; set; }

        public string varCompTitle { get; set; }
        public string varCompanyName { get; set; }
        public decimal intCompEsiPer { get; set; }
        public decimal intEmpEsiPer { get; set; }
        public decimal intPfLimit { get; set; }
        public decimal intEsiLimit { get; set; }
        public decimal intEsiDailyWage { get; set; }

        // Common Variables
        public string varSex { get; set; }

        public string varBloodGroup { get; set; }
        public string varImage { get; set; }
        public string varPdf { get; set; }
        public string varAdd { get; set; }
        public decimal intPincode { get; set; }
        public string varPhone { get; set; }
        public string varMobile { get; set; }
        public string varEmail { get; set; }
        public string varBankAccNo { get; set; }
        public string varBankCode { get; set; }
        public string varBankName { get; set; }
        public string varBankCity { get; set; }
        public string varPFNo { get; set; }
        public string varESINo { get; set; }
        public bool bitApprove { get; set; }
        public string strApprove { get; set; }

        // Extra Variables
        public decimal decCurrDue { get; set; }

        public decimal decTotalAmt { get; set; }
        public string varViewImage { get; set; }
        public string varAttendanceType { get; set; }
        public string strLoginName { get; set; }
        public string strUserId { get; set; }
        public string UserType { get; set; }
        public string strImage { get; set; }
    }
}
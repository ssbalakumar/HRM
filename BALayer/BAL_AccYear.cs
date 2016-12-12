using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DALayer;
using EntityLayer;

namespace BALayer
{
    public class BAL_ACCYear
    {
        private HRMDataContext dbcontext = new HRMDataContext();

        public List<Entity_HRM> GetAll()
        {
            List<Entity_HRM> listYear = new List<Entity_HRM>();

            try
            {
                var query = (from year in dbcontext.mtblAccYears
                             select new
                             {
                                 year.intAccYear,
                                 year.varAccYear,
                                 year.dtBeginDate,
                                 year.dtEndDate,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listYear.Add(new Entity_HRM
                    {
                        intAccYear = (decimal)usr.intAccYear,
                        varAccYear = usr.varAccYear,
                        dtBeginDate = usr.dtBeginDate.Value.Date,
                        dtEndDate = usr.dtEndDate.Value.Date,
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listYear;
        }

        public List<Entity_HRM> GetAllDataById(Entity_HRM _HrmEntity)
        {
            List<Entity_HRM> listYear = new List<Entity_HRM>();
            try
            {
                var query = (from year in dbcontext.mtblAccYears
                             where year.intAccYear == _HrmEntity.intAccYear
                             select new
                             {
                                 year.intAccYear,
                                 year.varAccYear,
                                 year.dtBeginDate,
                                 year.dtEndDate,
                             }).AsEnumerable();

                foreach (var usr in query)
                {
                    listYear.Add(new Entity_HRM
                    {
                        intAccYear = (int)usr.intAccYear,
                        varAccYear = usr.varAccYear,
                        dtBeginDate = usr.dtBeginDate.Value.Date,
                        dtEndDate = usr.dtEndDate.Value.Date,
                    });
                }
            }
            catch (Exception ex)
            {
            }
            return listYear;
        }

        public int Add(Entity_HRM _HrmEntity)
        {
            int id = 0;
            try
            {
                var query = dbcontext.mtblAccYears.SingleOrDefault(i => i.varAccYear.ToUpper() == _HrmEntity.varAccYear.ToUpper() && i.dtBeginDate.Value.Year == _HrmEntity.dtBeginDate.Year && i.dtEndDate.Value.Year == _HrmEntity.dtEndDate.Year);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblAccYear year = new mtblAccYear();

                    year.varAccYear = _HrmEntity.varAccYear;
                    year.dtBeginDate = _HrmEntity.dtBeginDate;
                    year.dtEndDate = _HrmEntity.dtEndDate;
                    dbcontext.mtblAccYears.InsertOnSubmit(year);
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
                var query = dbcontext.mtblAccYears.SingleOrDefault(i => i.varAccYear.ToUpper() == _HrmEntity.varAccYear.ToUpper() && i.dtBeginDate.Value.Year == _HrmEntity.dtBeginDate.Year && i.dtEndDate.Value.Year == _HrmEntity.dtEndDate.Year && i.intAccYear != _HrmEntity.intAccYear);
                if (string.IsNullOrEmpty(Convert.ToString(query)))
                {
                    mtblAccYear year = dbcontext.mtblAccYears.Single(p => p.intAccYear == _HrmEntity.intAccYear);
                    year.varAccYear = _HrmEntity.varAccYear;
                    year.dtBeginDate = _HrmEntity.dtBeginDate;
                    year.dtEndDate = _HrmEntity.dtEndDate;
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
                dbcontext.mtblAccYears.DeleteOnSubmit(dbcontext.mtblAccYears.Single(p => p.intAccYear == _HrmEntity.intAccYear));
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
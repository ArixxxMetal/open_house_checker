using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using open_house_checker.Models.DB;
using open_house_checker.Models.StoredProcedures;
using open_house_checker.Models.ViewModels.CheckIn;

namespace open_house_checker.Controllers
{
    public class CheckController : Controller
    {
        private readonly TEST_PROJECTSContext _context;

        public CheckController(TEST_PROJECTSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SetEmployeeAsChecked([FromBody] CheckEmployeeViewModel employee)
        {
            string employeenumberwithprefix = FixEmployeeNumber(employee.employeenumber);
            try
            {
                var emp_id_hr = new SqlParameter("@PARAM_HR_ID", employeenumberwithprefix);

                List<Sp_Return> lst = new List<Sp_Return>();
                List<Sp_Return> _returncheck = _context.CheckReturnSP.FromSqlRaw("USE [db_a87bdd_fbc] EXEC [open_house].[set_employee_check] @PARAM_HR_ID", emp_id_hr).ToList();

                return Json(_returncheck);
            }
            catch (Exception ex)
            {
                var ExceptionResponse = ex.Message;
                return Json(ExceptionResponse);
            }

        }

        [HttpPost]
        public JsonResult SaveEmployeeVisit([FromBody] List<VisitParameterViewModel> visits)
        {

            string employeenumberwithprefix = FixEmployeeNumber(visits[0].visit_emp_num);
            try
            {
                var check_id_hr = new SqlParameter("@PARAM_HR_ID", employeenumberwithprefix);

                List<Sp_Return> lst = new List<Sp_Return>();
                List<Sp_Return> _returncheck = _context.CheckReturnSP.FromSqlRaw("USE [db_a87bdd_fbc] EXEC [open_house].[set_employee_check] @PARAM_HR_ID", check_id_hr).ToList();

                foreach (var visit in visits)
                {
                    var emp_id_hr = new SqlParameter("@PARAM_HR_ID", employeenumberwithprefix);
                    var emp_visit_type = new SqlParameter("@PARAM_VISIT_TYPE", visit.visit_type);
                    var emp_visit_sex = new SqlParameter("@PARAM_VISIT_SEX", visit.visit_sex);
                    var emp_visit_age = new SqlParameter("@PARAM_VISIT_AGE", visit.visit_age);

                    _context.CheckReturnSP.FromSqlRaw("USE [db_a87bdd_fbc] EXEC [open_house].[set_employee_visits] @PARAM_HR_ID, @PARAM_VISIT_TYPE, @PARAM_VISIT_SEX, @PARAM_VISIT_AGE", 
                        emp_id_hr, emp_visit_type, emp_visit_sex, emp_visit_age).ToList();
                }

                return Json(_returncheck);
            }
            catch (Exception ex)
            {
                var ExceptionResponse = ex.Message;
                return Json(ExceptionResponse);
            }

        }

        private string FixEmployeeNumber(string employee)
        {
            if (employee.Length <= 6)
            {
                return employee;
            }
            else
            {
                int prefix, SumEmployeeCharList;
                List<string> EmployeeCharList = new List<string>();
                string SumValuePrefix;
                string EmployeeNumberwithPrefixRemoved;

                string employeenumberprefix = employee.Substring(0, 3);

                if (employee.Length == 9)
                {
                    // for 000000 employees
                    SumEmployeeCharList = 0;
                    EmployeeNumberwithPrefixRemoved = employee.Remove(0, 3);
                    for (int counter = 0; counter < EmployeeNumberwithPrefixRemoved.Length; counter++)
                    {
                        EmployeeCharList.Add(EmployeeNumberwithPrefixRemoved[counter].ToString());
                        SumEmployeeCharList = SumEmployeeCharList + int.Parse(EmployeeNumberwithPrefixRemoved[counter].ToString());
                    }

                    SumValuePrefix = employee.Substring(0, 2);
                    if (SumEmployeeCharList == int.Parse(SumValuePrefix))
                    {
                        return EmployeeNumberwithPrefixRemoved.TrimStart('0');
                    }
                    else
                    {
                        // for 00000 employees
                        SumEmployeeCharList = 0;
                        EmployeeNumberwithPrefixRemoved = employee.Remove(0, 2);
                        for (int counter = 0; counter < EmployeeNumberwithPrefixRemoved.Length; counter++)
                        {
                            EmployeeCharList.Add(EmployeeNumberwithPrefixRemoved[counter].ToString());
                            SumEmployeeCharList = SumEmployeeCharList + int.Parse(EmployeeNumberwithPrefixRemoved[counter].ToString());
                        }

                        SumValuePrefix = employee.Substring(0, 1);
                        if (SumEmployeeCharList == int.Parse(SumValuePrefix))
                        {
                            return EmployeeNumberwithPrefixRemoved.TrimStart('0');
                        }
                        else
                        {
                            return "EmployeedataNotMatch";
                        }
                    }
                }

                else
                {
                    // for 00000 employees
                    SumEmployeeCharList = 0;
                    EmployeeNumberwithPrefixRemoved = employee.Remove(0, 3);
                    for (int counter = 0; counter < EmployeeNumberwithPrefixRemoved.Length; counter++)
                    {
                        EmployeeCharList.Add(EmployeeNumberwithPrefixRemoved[counter].ToString());
                        SumEmployeeCharList = SumEmployeeCharList + int.Parse(EmployeeNumberwithPrefixRemoved[counter].ToString());
                    }

                    SumValuePrefix = employee.Substring(0, 2);
                    if (SumEmployeeCharList == int.Parse(SumValuePrefix))
                    {
                        return EmployeeNumberwithPrefixRemoved.TrimStart('0');
                    }
                    else
                    {
                        // for 00000 employees
                        SumEmployeeCharList = 0;
                        EmployeeNumberwithPrefixRemoved = employee.Remove(0, 2);
                        for (int counter = 0; counter < EmployeeNumberwithPrefixRemoved.Length; counter++)
                        {
                            EmployeeCharList.Add(EmployeeNumberwithPrefixRemoved[counter].ToString());
                            SumEmployeeCharList = SumEmployeeCharList + int.Parse(EmployeeNumberwithPrefixRemoved[counter].ToString());
                        }

                        SumValuePrefix = employee.Substring(0, 1);
                        if (SumEmployeeCharList == int.Parse(SumValuePrefix))
                        {
                            return EmployeeNumberwithPrefixRemoved.TrimStart('0');
                        }
                        else
                        {
                            return "EmployeedataNotMatch";
                        }
                    }
                }
            }

        }
    }
}

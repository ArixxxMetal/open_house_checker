using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using open_house_checker.Models.ViewModels.Login;
using open_house_checker.Models.DB;

namespace open_house_checker.Controllers
{
    public class LoginController : Controller
    {
        private readonly TEST_PROJECTSContext _context;

        public LoginController(TEST_PROJECTSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public static string EncriptPass(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña en una matriz de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir la matriz de bytes en una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAccessViewModel _User)
        {
            try
            {
                string pass = EncriptPass(_User.Password);
                var employeenumberParam = new SqlParameter("@PARAM_EMP_NUM", _User.Username);
                var employeepasswordParam = new SqlParameter("@PARAM_PASS", pass);

                List<SessionUserViewModel> lst = new List<SessionUserViewModel>();
                List<SessionUserViewModel> _LoggedUser = _context.GetLoginUserSP.FromSqlRaw
                    ("USE [db_a87bdd_fbc] EXEC [open_house].[get_loginuservalues] @PARAM_EMP_NUM, @PARAM_PASS", employeenumberParam, employeepasswordParam).ToList();

                if (_LoggedUser[0].isallowed == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, _User.Username)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("EmployeesReport", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            catch (Exception ex)
            {
                var ExceptionResponse = ex.Message;
                return RedirectToAction("Index", "Login");
            }


        }

        public async Task<IActionResult> CloseSession()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}

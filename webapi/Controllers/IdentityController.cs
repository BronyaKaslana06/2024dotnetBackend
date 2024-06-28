using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Dynamic;
using EntityFramework.Context;
using EntityFramework.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Transactions;
using Idcreator;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service;

namespace webapi.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly JwtHelper _jwtHelper;
        private readonly IdentityService _service;

        public IdentityController(IdentityService service,ModelContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult LoginCheck([FromBody] dynamic _user)
        {
            dynamic user = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(_user));
            char usertype = Convert.ToString(user.user_id)[0];
            string account_serial = user.user_id;
            string password = user.password;
            var result = _service.AuthenticateUser(account_serial, password);
            Console.WriteLine(result.userData);
            if (!result.IsAuthenticated)
                return NewContent("", new { }, 1, result.ErrorMessage);
            var token = _jwtHelper.CreateToken(result.userData.account_serial, password);
            return NewContent(token, result.userData);
        }

        [HttpPost("sign-up")]
        public ActionResult SignupCheck([FromBody] dynamic _user)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                dynamic user = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(_user));
                int usertype = user.user_type ?? -1;
                //可注册的用户类型：员工/车主
                if (usertype < 0 || usertype > 2 )
                    return NoContent();
                IdentityType user_type = (IdentityType)Convert.ToInt32(usertype);
                //员工和车主共用的参数
                string username = user.username ?? "新用户";
                string password = user.password ?? "123456";
                string nickname = user.nickname ?? "蔚来";
                string gender = user.gender ?? "男";
                DateTime create_time = Convert.ToDateTime(user.create_time) ?? System.DateTime.Now.ToString();
                string phone_number = user.phone_number ?? string.Empty;
                //定义返回对象
                dynamic obj = new ExpandoObject();
                obj.data = new
                {
                    user_id = "-1"
                };
                obj.msg = "注册失败";
                if (user_type == IdentityType.车主) //注册车主
                {
                    //生成新id
                    long snake = Idcreator.EasyIDCreator.CreateId(_context);
                    string uid = ((int)user_type).ToString() + snake.ToString();
                    //定义新tuple
                    VehicleOwner owner = new VehicleOwner
                    {
                        //OwnerId = _context.VehicleOwners.Max(x=>x.OwnerId) + 1,
                        AccountSerial = uid,
                        Username = username,
                        Password = password,
                        CreateTime = create_time,
                        PhoneNumber = phone_number,
                        Email = user.email ?? string.Empty,
                        Gender = gender,
                        Birthday = Convert.ToDateTime(user.birthday == null ? "2000-01-01" : user.birthday),   
                    };
                    _context.VehicleOwners.Add(owner);
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        return Conflict();
                    }
                   /* _context.VehicleOwners.Add(owner);
                    _context.SaveChanges();*/
                    OwnerPos OP = new OwnerPos
                    {
                        OwnerId = owner.OwnerId,
                        Address = user.address ?? string.Empty
                    };
                    _context.OwnerPos.Add(OP);
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        return Conflict();
                    }
                    obj.data = new
                    {
                        account_serial = uid,
                        user_id = owner.OwnerId
                    };
                    obj.msg = "注册成功";
                }
                else if (user_type == IdentityType.员工) //注册员工
                {
                    string invite_code = user.invite_code ?? string.Empty;
                    if (invite_code != "123456")
                    {
                        obj.data = new
                        {
                            code =1 ,
                            msg = "注册码无效",
                            user_id = "-1"
                        };
                        return Content(JsonConvert.SerializeObject(obj), "application/json");
                    }
                    //生成新id
                    long snake = Idcreator.EasyIDCreator.CreateId(_context);
                    string uid = ((int)user_type).ToString() + snake.ToString();
                    Employee employee = new Employee
                    {
                        //EmployeeId = _context.Employees.Max(x => x.EmployeeId) + 1,
                        AccountSerial = uid,
                        UserName = username,
                        Password = password,
                        Name = nickname,
                        CreateTime = create_time,
                        PhoneNumber = phone_number,
                        Gender = gender,
                        IdentityNumber = user.identity_number,
                        Position = user.position ?? (int)PositionEnum.其它,
                        Salary = 0
                    };
                    _context.Employees.Add(employee);
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        return Conflict();
                    }
                    obj.msg = "注册成功";
                    obj.data = new
                    {
                        account_serial = uid,
                        user_id = employee.EmployeeId
                    };
                }
                else if(user_type == IdentityType.管理员)
                {
                    string invite_code = user.invite_code ?? string.Empty;
                    if (invite_code != "123456")
                    {
                        obj.data = new
                        {
                            code = 1,
                            msg = "注册码无效",
                            user_id = "-1"
                        };
                        return Content(JsonConvert.SerializeObject(obj), "application/json");
                    }
                    //生成新id
                    long snake = Idcreator.EasyIDCreator.CreateId(_context);
                    string uid = ((int)user_type).ToString() + snake.ToString();
                    Administrator admin = new Administrator
                    {
                        //AdminId = _context.Administrators.Max(x => x.AdminId) + 1,
                        AccountSerial = uid,
                        Email = user.email,
                        Password = password
                    };
                    _context.Administrators.Add(admin);
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        return Conflict();
                    }
                    obj.msg = "注册成功";
                    obj.data = new
                    {
                        account_serial = uid,
                        user_id = admin.AdminId
                    };
                }
                tx.Complete();
                return Content(JsonConvert.SerializeObject(obj), "application/json");
            }
        }

        private bool OwnerExists(long id)
        {
            return _context.VehicleOwners?.Any(e => e.OwnerId == id) ?? false;
        }
        private bool StaffExists(long id)
        {
            return _context.Employees?.Any(e => e.EmployeeId == id) ?? false;
        }

        ContentResult NewContent<T>(string token, T data, int _code = 0, string _msg = "success")
        {
            var a = new
            {
                code = _code,
                msg = _msg,
                data,
                token
            };
            return Content(JsonConvert.SerializeObject(a), "application/json");
        }
    }
}

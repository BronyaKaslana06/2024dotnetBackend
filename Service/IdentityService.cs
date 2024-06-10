using DAL;
using EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.IdentityService;

namespace Service
{
    public class IdentityService
    {
        private readonly IdentityRepository _repository;
        public class UserData
        {
            public int user_type { get; set; }
            public long user_id { get; set; }
            public string account_serial { get; set; }
            public string? username { get; set; }
            public string? phone_number { get; set; }
            public string? gender { get; set; }
            public string email { get; set; }
            public long? station_id { get; set; } // 可能为空，仅员工有
            public PositionEnum? position { get; set; } // 可能为空，仅员工有
        }

        public IdentityService(IdentityRepository repository)
        {
            _repository = repository;
        }
        public class AuthenticationResult
        {
            public bool IsAuthenticated { get; set; }
            public UserData userData { get; set; }
            public string ErrorMessage { get; set; }
        }
        public AuthenticationResult AuthenticateUser(string userId, string password)
        {
            var userChar = userId[0];
            IdentityType user_type = (IdentityType)Convert.ToInt32(userChar - '0');
            var account_serial = userId;

            switch (user_type)
            {
                case IdentityType.车主:
                    var owner = _repository.GetOwnerByAccount(account_serial);
                    if (owner == null)
                        return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = "User ID error." };
                    if (owner.Password != password)
                        return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = "Password error." };
                    return new AuthenticationResult
                    {
                        IsAuthenticated = true,
                        userData = new UserData
                        {
                            user_type = (int)user_type,
                            user_id = owner.OwnerId,
                            account_serial = owner.AccountSerial,
                            username = owner.Username,
                            phone_number = owner.PhoneNumber,
                            gender = owner.Gender,
                            email = owner.Email,
                        }
                    };

                case IdentityType.员工:
                    var staff = _repository.GetEmployeeByAccount(account_serial);
                    if (staff == null)
                        return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = "User ID error." };
                    if (staff.Password != password)
                        return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = "Password error." };
                    return new AuthenticationResult
                    {
                        IsAuthenticated = true,
                        userData = new UserData
                        {
                            user_type = (int)user_type,
                            user_id = staff.EmployeeId,
                            account_serial = staff.AccountSerial,
                            username = staff.UserName,
                            phone_number = staff.PhoneNumber,
                            gender = staff.Gender,
                            station_id = staff.switchStation == null ? 0 : staff.switchStation.StationId,
                            position = (PositionEnum)staff.Position,
                            email = staff.Email,
                        }
                    };

                case IdentityType.管理员:
                    var admin = _repository.GetAdministratorByAccount(account_serial);
                    if (admin == null)
                        return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = "User ID error." };
                    if (admin.Password != password)
                        return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = "Password error." };
                    return new AuthenticationResult
                    {
                        IsAuthenticated = true,
                        userData = new UserData
                        {
                            user_type = (int)user_type,
                            user_id = admin.AdminId,
                            account_serial = admin.AccountSerial,
                            email = admin.Email
                        }
                    };

                default:
                    return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = "身份无法识别" };
            }
        }
        public enum EntityType
        {
            Administrator,
            Employee,
            VehicleOwner
        }

        public enum IdentityType
        {
            车主 = 0,
            员工 = 1,
            管理员 = 2
        }

    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.EDM;
using WebApplication.Helpers;
using WebApplication.ViewModel;

namespace WebApplication.Repository
{
    public class UserRepository
    {
        EmployeeEntities db = new EmployeeEntities();


        public UserVM Login(UserVM userVM)
        {
            user profile = null;
            try
            {
                profile = db.users.FirstOrDefault(x => x.UserName == userVM.UserName && x.Password == userVM.Password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            var _data = Mapper.Map<user, UserVM>(profile);
            _data.RoleName = db.Roles.FirstOrDefault(r => r.RoleId == _data.RoleId).RoleName;
            return _data;
        }

        public UserVM GetProfile(int id)
        {
            user user = new user();
            try
            {
                user = db.users.FirstOrDefault(i => i.UserId == id);
                //if (user == null)
                //{
                //    throw new Exception(ConstMessages.BAD_DATA);
                //}
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<user, UserVM>(user);
        }

        public UserVM SaveProfile(UserVM userVM)
        {
            try
            {
                user _data = Mapper.Map<UserVM, user>(userVM);
                var rawString = userVM.mealarr.Select(x => x.Trim(','));
                string meal = string.Join(",", rawString);

                _data.mealpreference = meal;

                if (_data != null)
                {
                    _data.RoleId = 2;
                    db.users.Add(_data);
                    db.SaveChanges();
                }

                return Mapper.Map<user, UserVM>(_data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<user> GetEmployees()
        {
            return db.users.ToList();
        }

        public UserVM GetEmployeesById(int id)
        {
            try
            {
                var lstEmployee = db.users.Where(d => d.UserId == id).FirstOrDefault();
                return Mapper.Map<user, UserVM>(lstEmployee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Roles
        public int getRoleId(int id)
        {
            return db.Roles.FirstOrDefault(r => r.RoleId == id).RoleId;
        }

        public string getRoleNameByUserId(int id)
        {
            return db.users.FirstOrDefault(r => r.UserId == id).Role.RoleName;
        }
        public int getRoleIdByRoleName(string role)
        {
            return db.Roles.FirstOrDefault(r => r.RoleName == role).RoleId;
        }
        #endregion
    }
}
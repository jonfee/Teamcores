using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Data.DataAccess
{
    public class UsersAccessor
    {
        /// <summary>
        /// 编辑用户基本信息
        /// </summary>
        /// <param name="user"></param>
        public static void Edit(Users user)
        {
            using (var db = new DataContext())
            {
                db.Users.Attach(user);
                var entry = db.Entry(user);
                entry.Property(p => p.Email).IsModified = true;
                entry.Property(p => p.Username).IsModified = true;
                entry.Property(p => p.Password).IsModified = true;
                entry.Property(p => p.Mobile).IsModified = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 分页获取用户列表信息
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="mobile"></param>
        /// <param name="promoter"></param>
        /// <returns></returns>
        public static PagerModel<Users> Get(PagerModel<Users> pager, string name, string email, string mobile)
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Users
                            orderby p.CreateTime descending
                            select p;
                if (!string.IsNullOrEmpty(name))
                {
                    query = from p in query
                            where p.Username.Contains(name)
                            orderby p.CreateTime descending
                            select p;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    query = from p in query
                            where p.Email.Contains(email)
                            orderby p.CreateTime descending
                            select p;
                }
                if (!string.IsNullOrEmpty(mobile))
                {
                    query = from p in query
                            where p.Mobile.Contains(mobile)
                            orderby p.CreateTime descending
                            select p;
                }

                pager.Count = query.Count();
                var list = query.Skip((pager.Index - 1) * pager.Size).Take(pager.Size).ToList();
                pager.Table = list;
                return pager;
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="users"></param>
        /// <param name="profile"></param>
        /// <param name="study"></param>
        public static void Add(Users users, UserStudy study)
        {
            using (var db = new DataContext())
            {
                db.Users.Add(users);
                db.UserStudy.Add(study);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据用户ID，获取用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Users Get(long userId)
        {
            using (var db = new DataContext())
            {
                return db.Users.SingleOrDefault(p => p.UserId == userId);
            }
        }

        /// <summary>
        /// 通过手机号码获取用户信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static Users GetByMobile(string mobile)
        {
            using (var db = new DataContext())
            {
                return db.Users.SingleOrDefault(p => p.Mobile == mobile);
            }
        }

        /// <summary>
        /// 用户Email获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Users GetByEmail(string email)
        {
            using (var db = new DataContext())
            {
                return db.Users.SingleOrDefault(p => p.Email == email);
            }
        }

        /// <summary>
        /// 判断用户是否存在，true表示存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool UsernameExists(string name, long? userid = 0)
        {
            using (var db = new DataContext())
            {
                return db.Users.Where(p => p.UserId != userid.Value && p.Username == name).Count() > 0;
            }
        }


        /// <summary>
        /// 判断手机号码是否存在，true表示存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool MobileExists(string mobile, long? userid = 0)
        {
            using (var db = new DataContext())
            {
                return db.Users.Where(p => p.UserId != userid.Value && p.Mobile == mobile).Count() > 0;
            }
        }

        /// <summary>
        /// 判断邮箱是否存在，true表示存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmailExists(string email, long? userid = 0)
        {
            using (var db = new DataContext())
            {
                return db.Users.Where(p => p.UserId != userid.Value && p.Email == email).Count() > 0;
            }
        }

        public static long GetId(string name)
        {
            using (var db = new DataContext())
            {
                return db.Users.SingleOrDefault(p => p.Username == name).UserId;
            }
        }


    }
}

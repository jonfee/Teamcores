using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
    public class OptionsAccessor
    {
        /// <summary>
        /// 新增配置项
        /// </summary>
        /// <param name="option"></param>
        public static void Add(Options option)
        {
            using (var db = new DataContext())
            {
                db.Options.Add(option);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取配置信息，通过ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Options Get(long id)
        {
            using (var db = new DataContext())
            {
                return db.Options.SingleOrDefault(p => p.OptionId == id);
            }
        }

        /// <summary>
        /// 获取配置信息，通过key name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Options Get(string name)
        {
            using (var db = new DataContext())
            {
                return db.Options.SingleOrDefault(p => p.Name == name);
            }
        }

        /// <summary>
        /// 通过key集合获取配置信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static List<Options> Get(List<string> keys)
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Options
                            where keys.Contains(p.Name)
                            select p;
                return query.ToList();
            }
        }
    }
}

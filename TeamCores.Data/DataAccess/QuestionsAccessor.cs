using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
    /// <summary>
    /// 题目仓储服务
    /// </summary>
    public static class QuestionsAccessor
    {
        /// <summary>
        /// 添加题目到库
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool Insert(Questions question)
        {
            using (var db = new DataContext())
            {
                db.Questions.Add(question);

                return db.SaveChanges() > 0;
            }
        }
    }
}

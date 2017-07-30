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

        /// <summary>
        /// 根据题目ID获取题目信息
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public static Questions Get(long questionId)
        {
            using (var db = new DataContext())
            {
                return db.Questions.Find(questionId);
            }
        }

        /// <summary>
        /// 设置题目状态
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool SetStatus(long questionId, int status)
        {
            using (var db = new DataContext())
            {
                var item = db.Questions.Find(questionId);

                item.Status = status;

                db.Questions.Update(item);

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新题目信息
        /// </summary>
        /// <param name="questionId">题目ID</param>
        /// <param name="courseId">课程ID</param>
        /// <param name="subjectId">科目ID</param>
        /// <param name="type">题目类型</param>
        /// <param name="marking">是否需要阅卷</param>
        /// <param name="topic">题目标题</param>
        /// <param name="answer">答案选项</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static bool Update(long questionId, long courseId, long subjectId, int type, bool marking, string topic, string answer, int status)
        {
            using (var db = new DataContext())
            {
                var item = db.Questions.Find(questionId);

                item.CourseId = courseId;
                item.SubjectId = subjectId;
                item.Type = type;
                item.Marking = marking;
                item.Topic = topic;
                item.Answer = answer;
                item.Status = status;

                db.Questions.Update(item);

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public static bool Delete(long questionId)
        {
            using (var db = new DataContext())
            {
                var item = db.Questions.Find(questionId);

                db.Questions.Remove(item);

                return db.SaveChanges() > 0;
            }
        }
    }
}

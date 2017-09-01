using System;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Infrastructure.StudyProgress
{
    /// <summary>
    /// 课程学习进度
    /// </summary>
    internal class CourseStudingProgressComputer
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId;

        /// <summary>
        /// 课程ID
        /// </summary>
        public long CourseId;

        public CourseStudingProgressComputer(long userId, long courseId)
        {
            UserId = userId;
            CourseId = courseId;
        }

        /// <summary>
        /// 计算课程的学习进度
        /// </summary>
        /// <returns></returns>
        public float Calculate()
        {
            //获取用户学习过的章节数
            long[] studied = StudyRecordAccessor.GetChapterIdsFor(UserId, CourseId);
            //当前课程未学习过任何章节，学习进度为0
            if (studied == null || studied.Length == 0) return 0F;

            //获取课程下的章节数
            var chapters = ChapterAccessor.GetChaptersAllFor(CourseId);
            //有效的章节（即状态为启用的章节）
            var effectIds = chapters.Where(p => p.Status == (int)ChapterStatus.ENABLED).Select(p=>p.ChapterId);

            //有效章节的学习数量
            var studiedCount = (decimal)effectIds.Intersect(studied).Count();

            return (float)Math.Round(studiedCount / effectIds.Count(), 2, MidpointRounding.ToEven);
        }
    }
}

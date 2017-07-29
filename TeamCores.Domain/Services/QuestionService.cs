using TeamCores.Domain.Models.Answer;
using TeamCores.Domain.Models.Question;

namespace TeamCores.Domain.Services
{
    /// <summary>
    /// 题目领域业务服务
    /// </summary>
    public class QuestionService
    {
        public bool Add(long userId, long courseId, long subjectId, int type, bool marking, string topic, QuestionAnswer answerOptions)
        {
            NewQuestion question = new NewQuestion
            {
                CourseId = courseId,
                Marking = marking,
                SubjectId = subjectId,
                Topic = topic,
                Type = type,
                UserId = userId
            };

            question.SetAnswerOptions(answerOptions);

            return question.Save();
        }
    }
}

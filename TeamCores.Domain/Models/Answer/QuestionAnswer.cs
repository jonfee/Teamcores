namespace TeamCores.Domain.Models.Answer
{
    /// <summary>
    /// 题目备选答案项抽象类
    /// </summary>
    public abstract class QuestionAnswer
    {
        /// <summary>
        /// 所有备选答案项或知识点的JSON格式字符串数据
        /// </summary>
        public abstract string ToJson();

        /// <summary>
        /// 验证答案信息是否有效
        /// </summary>
        /// <returns></returns>
        public abstract bool Validate();

        /// <summary>
        /// 验证当前答案项是否跟题目类型匹配，匹配成功时返回Ture，匹配失败返回False
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract bool RegexType(int type);
    }
}

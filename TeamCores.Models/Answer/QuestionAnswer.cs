namespace TeamCores.Models.Answer
{
    /// <summary>
    /// 题目备选答案项抽象类
    /// </summary>
    public abstract class QuestionAnswer
    {
        /// <summary>
        /// 所有备选答案项或知识点的JSON格式字符串数据
        /// </summary>
        public abstract string Serialize();

		/// <summary>
		/// 验证答案信息是否有效
		/// </summary>
		/// <returns></returns>
		public abstract bool Validate();
    }
}

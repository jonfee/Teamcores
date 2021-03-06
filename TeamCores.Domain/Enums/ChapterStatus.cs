﻿using System.ComponentModel;

namespace TeamCores.Domain.Enums
{
	/// <summary>
	/// 课程章节状态枚举
	/// </summary>
	public enum ChapterStatus
    {
		/// <summary>
		/// 启用
		/// </summary>
		[Description("启用")]
		ENABLED = 1,
		/// <summary>
		/// 禁用
		/// </summary>
		[Description("禁用")]
		DISABLED = 0
	}
}

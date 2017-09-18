/**
 * 枚举类定义
 */
var Enum = (function()
{
    var hasOwnProperty = Object.prototype.hasOwnProperty,
        toString = function(format)
        {
            var _value = this.value;
            switch(format)
            {
                case "d":
                case "value":
                    _value = this.value;
                    break;
                case "a":
                case "alias":
                case "text":
                case "desc":
                case "description":
                    _value = this.text;
                    break;
                case "n":
                case "name":
                default:
                    _value = this.name;
                    break;
            }

            return _value;
        },
        valueOf = function()
        {
            return this.toString("d");
        };

    var I = function(key, item)
    {
        this.name = key;
        this.value = item.value;
        this.text = item.text;
    };

    I.prototype.valueOf = valueOf;
    I.prototype.toString = toString;

    var E = function(items)
    {
        this.items = [];
        this.names = [];
        this.values = [];
        this.texts = [];

        var self = this,
            item,
            key;

        for(key in items)
        {
            if(hasOwnProperty.call(items, key))
            {
                item = items[key];

                var _i = new I(key, item);

                this.items.push(_i);
                this.names.push(_i.name);
                this.values.push(_i.value);
                this.texts.push(_i.text);

                self[key] = _i;
            }
        }
    };

    E.prototype =
    {
        constructor : E,

        getItem : function(valueOrName)
		{
            for(var index in this.items)
            {
                var item = this.items[index];

                if(item.value === valueOrName || item.name === valueOrName)
				{
                    return item;
                }
            }

            return undefined;
        },

        get : function(name)
        {
            var item;

            if(hasOwnProperty.call(this, name))
            {
                item = this[name];

                return item;
            }
        },

        set : function(name, value, text)
        {
            var item;

            if(hasOwnProperty.call(this, name))
            {
                item = this[name];

                if(value)
                {
                    item.value = value;
                }

                if(text)
                {
                    item.text = text;
                }
            }
        }
    };

    return E;
})();

/**
 * API返回结果枚举
 */
const ApiResult = new Enum({
    NO_ACCESS : {
        text : "权限不足",
        value : 1
    },
    LOGIN_TIMEROUT : {
        text : "未登录或登录已超时",
        value : 2
    }
});

/**
 * 用户状态枚举
 */
const UserStatus = new Enum({
    ENABLED : {
        text : "启用",
        value : 1
    },
    DISABLED : {
        text : "禁用",
        value : 0
    }
});

/**
 * 科目状态
 */
const SubjectStatus = new Enum({
    ENABLED : {
        text : "启用",
        value : 1
    },
    DISABLED : {
        text : "禁用",
        value : 0
    }
});

/**
 * 课程状态
 */
const CourseStatus = new Enum({
    ENABLED : {
        text : "启用",
        value : 1
    },
    DISABLED : {
        text : "禁用",
        value : 0
    }
});

/**
 * 学习计划状态
 */
const StudyPlanStatus = new Enum({
    ENABLED : {
        text : "启用",
        value : 1
    },
    DISABLED : {
        text : "禁用",
        value : 0
    }
});

/**
 * 用户学习计划学习状态
 */
const StudyStatus = new Enum({
    NOTSTARTED : {
        text : "未开始",
        value : 1
    },
    STUDYING : {
        text : "学习中",
        value : 2
    },
    COMPLETE : {
        text : "已完成",
        value : 3
    }
});

/**
 * 考卷模板类型
 */
const ExamType = new Enum({
	TEST_EXAM: {
		text: "练习卷",
		value: 1
	},
	LIVE_EXAM: {
		text: "考试卷",
		value: 0
	}
});

/**
 * 考卷模板状态
 */
const ExamStatus = new Enum({
	ENABLED: {
		text: "启用",
		value: 1
	},
	DISABLED: {
		text: "禁用",
		value: 0
	}
});

/**
 * 考题状态
 */
const QuestionStatus = new Enum({
    ENABLED : {
        text : "启用",
        value : 1
    },
    DISABLED : {
        text : "禁用",
        value : 0
    }
});

/**
 * 考题类型
 */
const QuestionType = new Enum({
    SINGLE_CHOICE : {
        text : "单选题",
        value : 1
    },
    MULTIPLE_CHOICE : {
        text : "多选题",
        value : 2
    },
    TRUE_OR_FALSE : {
        text : "判断题",
        value : 3
    },
    GAP_FILLING : {
        text : "填空题",
        value : 4
    },
    ESSAY_QUESTION : {
        text : "问答题",
        value : 5
    }
});

/**
 * 用户考卷阅卷状态
 */
const ExamMarkingStatus = new Enum({
    READED:{
        text: "已阅卷",
        value: 1
    },
    DIDNOT_READ: {
        text: "未阅卷",
        value: 0
    }
});
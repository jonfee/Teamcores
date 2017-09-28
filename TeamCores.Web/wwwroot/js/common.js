/*
* axios 封装
*/
var Ajax = (function () {

	//默认设置
	var _defaults = {
		baseURL: '',
		timeout: 60000,
		headers: { 'X-Requested-With': 'XMLHttpRequest' },
		withCredentials: false,
		responseType: 'json',
		method: 'get',
		url: '',
		params: null,
		data: null,
		transformRequest: [function (data) {
			let ret = '';
			for (let it in data) {
				ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&';
			}
			return ret;
		}],
		transformResponse: [function (data) {
			return data;
		}],
		success: null,
		error: null
	};

	var extend = function (source, target) {
		for (let t in target) {
			if (target.hasOwnProperty(t) && !source.hasOwnProperty(t)) {
				source[t] = target[t];
			}
		}
	};

	var execute = function (settings) {
		extend(settings, _defaults);
		axios(settings).then(function (response) {
			if (settings.success && typeof settings.success === "function") {
				settings.success(response);
			}
		}).catch(function (error) {
			if (settings.error && typeof settings.error === "function") {
				settings.error(error);
			}
		});
	};

	this.post = function (settings) {
		settings["method"] = "post";

		execute(settings);
	};

	this.get = function (settings) {
		settings["method"] = "get";

		execute(settings);
	};

	return this;
})();

function compailePara(object, opts) {
	var obj = object;
	var options = opts || {};

	if (options.encoder !== null && options.encoder !== undefined && typeof options.encoder !== 'function') {
		throw new TypeError('Encoder has to be a function.');
	}

	let percentTwenties = /%20/g;

	let formatters = {
		RFC1738: function (value) {
			return replace.call(value, percentTwenties, '+');
		},
		RFC3986: function (value) {
			return value;
		}
	}


	var defaults = {
		delimiter: '&',
		encode: true,
		encoder: null,
		encodeValuesOnly: false,
		serializeDate: function serializeDate(date) { // eslint-disable-line func-name-matching
			return Date.prototype.toISOString.call(date);
		},
		skipNulls: false,
		strictNullHandling: false
	};

	var delimiter = typeof options.delimiter === 'undefined' ? defaults.delimiter : options.delimiter;
	var strictNullHandling = typeof options.strictNullHandling === 'boolean' ? options.strictNullHandling : defaults.strictNullHandling;
	var skipNulls = typeof options.skipNulls === 'boolean' ? options.skipNulls : defaults.skipNulls;
	var encode = typeof options.encode === 'boolean' ? options.encode : defaults.encode;
	var encoder = typeof options.encoder === 'function' ? options.encoder : defaults.encoder;
	var sort = typeof options.sort === 'function' ? options.sort : null;
	var allowDots = typeof options.allowDots === 'undefined' ? false : options.allowDots;
	var serializeDate = typeof options.serializeDate === 'function' ? options.serializeDate : defaults.serializeDate;
	var encodeValuesOnly = typeof options.encodeValuesOnly === 'boolean' ? options.encodeValuesOnly : defaults.encodeValuesOnly;
	if (typeof options.format === 'undefined') {
		options.format = 'RFC3986';
	} else if (!Object.prototype.hasOwnProperty.call(formatters, options.format)) {
		throw new TypeError('Unknown format option provided.');
	}

	var formatter = formatters[options.format];
	var objKeys;
	var filter;



	if (typeof options.filter === 'function') {
		filter = options.filter;
		obj = filter('', obj);
	} else if (Array.isArray(options.filter)) {
		filter = options.filter;
		objKeys = filter;
	}

	var keys = [];

	if (typeof obj !== 'object' || obj === null) {
		return '';
	}



	if (!objKeys) {
		objKeys = Object.keys(obj);
	}

	if (sort) {
		objKeys.sort(sort);
	}


	for (var i = 0; i < objKeys.length; ++i) {
		var key = objKeys[i];

		if (skipNulls && obj[key] === null) {
			continue;
		}

		keys = keys.concat(stringify(
			obj[key],
			key,
			null,
			strictNullHandling,
			skipNulls,
			encode ? encoder : null,
			filter,
			sort,
			allowDots,
			serializeDate,
			formatter,
			encodeValuesOnly
		));

	}


	return keys.join(delimiter);
}

function stringify(
	object,
	prefix,
	generateArrayPrefix,
	strictNullHandling,
	skipNulls,
	encoder,
	filter,
	sort,
	allowDots,
	serializeDate,
	formatter,
	encodeValuesOnly
) {
	var obj = object;
	if (typeof filter === 'function') {
		obj = filter(prefix, obj);
	} else if (obj instanceof Date) {
		obj = serializeDate(obj);
	} else if (obj === null) {
		if (strictNullHandling) {
			return encoder && !encodeValuesOnly ? encoder(prefix) : prefix;
		}

		obj = '';
	}

	if (typeof obj === 'string' || typeof obj === 'number' || typeof obj === 'boolean' || isBuffer(obj)) {
		if (encoder) {
			var keyValue = encodeValuesOnly ? prefix : encoder(prefix);
			return [formatter(keyValue) + '=' + formatter(encoder(obj))];
		}
		return [formatter(prefix) + '=' + formatter(String(obj))];
	}

	var values = [];

	if (typeof obj === 'undefined') {
		return values;
	}

	var objKeys;
	if (Array.isArray(filter)) {
		objKeys = filter;
	} else {
		var keys = Object.keys(obj);
		objKeys = sort ? keys.sort(sort) : keys;
	}

	for (var i = 0; i < objKeys.length; ++i) {
		var key = objKeys[i];

		if (skipNulls && obj[key] === null) {
			continue;
		}

		if (Array.isArray(obj)) {
			values = values.concat(stringify(
				obj[key],
				indices(prefix, key),
				generateArrayPrefix,
				strictNullHandling,
				skipNulls,
				encoder,
				filter,
				sort,
				allowDots,
				serializeDate,
				formatter,
				encodeValuesOnly
			));
		} else {
			values = values.concat(stringify(
				obj[key],
				prefix + (allowDots ? '.' + key : '[' + key + ']'),
				generateArrayPrefix,
				strictNullHandling,
				skipNulls,
				encoder,
				filter,
				sort,
				allowDots,
				serializeDate,
				formatter,
				encodeValuesOnly
			));
		}
	}

	return values;
};

function isBuffer(obj) {
	if (obj === null || typeof obj === 'undefined') {
		return false;
	}

	return !!(obj.constructor && obj.constructor.isBuffer && obj.constructor.isBuffer(obj));
};
function indices(prefix, key) {
	return prefix + '[' + key + ']';
};

/*
* 当前时间减去date2后的时差
* @param {Date} date2 减去的时间
* @returns {days:0,hours:0,minutes:0,seconds:1,totalDays:0,totalHours:0,totalMinutes: 0,totalSeconds: 0,totalMilliseconds: 0}
*/
Date.prototype.deduct = function (date2) {
	var _date = new Date(date2);

	//时间差:总毫秒数
	var totalMilliseconds = this.getTime() - _date.getTime();
	//时间差：总秒数
	var totalSeconds = Math.floor(totalMilliseconds / 1000);
	//时间差：总分钟数
	var totalMinutes = Math.floor(totalMilliseconds / (60 * 1000));
	//时间差：总小时数
	var totalHours = Math.floor(totalMilliseconds / (3600 * 1000));
	//时间差：总天数
	var totalDays = Math.floor(totalMilliseconds / (24 * 3600 * 1000));

	//相差单位部分剩余时间（毫秒），默认为总毫秒数（尚未开始计算）
	var afterTs = totalMilliseconds;

	//相差的天数部分
	var days = Math.floor(afterTs / (24 * 3600 * 1000));
	afterTs = afterTs % (24 * 3600 * 1000)
	//相差的小时部分
	var hours = Math.floor(afterTs / (3600 * 1000));
	afterTs = afterTs % (3600 * 1000);
	//相差的分钟部分
	var minutes = Math.floor(afterTs / (60 * 1000));
	afterTs = afterTs % (60 * 1000)
	//相差的秒钟部分
	var seconds = Math.floor(afterTs / 1000);

	return {
		days: days,
		hours: hours,
		minutes: minutes,
		seconds: seconds,
		totalDays: totalDays,
		totalHours: totalHours,
		totalMinutes: totalMinutes,
		totalSeconds: totalSeconds,
		totalMilliseconds: totalMilliseconds
	};
}

/*
* 时间扩展
*/
Date.prototype.format = function (format) {
	const z = {
		y: this.getFullYear(),
		M: this.getMonth() + 1,
		d: this.getDate(),
		h: this.getHours(),
		m: this.getMinutes(),
		s: this.getSeconds()
	};

	format = format || "yyyy-MM-dd hh:mm:ss";

	return format.replace(/(y+|M+|d+|h+|m+|s+)/g,
		function (v) {
			return ((v.length > 1 ? "0" : "") + eval(`z.${v.slice(-1)}`)).slice(-(v.length > 2 ? v.length : 2));
		});
};

/**
* 时间字符串对象转换为指定格式的时间字符串
*/
String.prototype.toDateTime = function (format) {

	var dt = this && this != null ? this : '';

	if (dt == '') return '';

	format = format || 'yyyy-MM-dd hh:mm:ss';

	return new Date(Date.parse(dt)).format(format);
}

///集合取交集
Array.intersect = function () {
	const result = new Array();
	const obj = {};
	for (let i = 0; i < arguments.length; i++) {
		for (let j = 0; j < arguments[i].length; j++) {
			const str = arguments[i][j];
			if (!obj[str]) {
				obj[str] = 1;
			}
			else {
				obj[str]++;
				if (obj[str] == arguments.length) {
					result.push(str);
				}
			} //end else
		} //end for j
	} //end for i
	return result;
};

//集合去掉重复
Array.prototype.uniquelize = function () {
	const tmp = {};
	const ret = [];
	for (var i = 0, j = this.length; i < j; i++) {
		if (!tmp[this[i]]) {
			tmp[this[i]] = 1;
			ret.push(this[i]);
		}
	}

	return ret;
};
//并集
Array.union = function () {
	const arr = new Array();
	const obj = {};
	for (let i = 0; i < arguments.length; i++) {
		for (let j = 0; j < arguments[i].length; j++) {
			const str = arguments[i][j];
			if (!obj[str]) {
				obj[str] = 1;
				arr.push(str);
			}
		} //end for j
	} //end for i
	return arr;
};

//2个集合的差集 在arr不存在
Array.prototype.minus = function (arr) {
	const result = new Array();
	const obj = {};
	for (let i = 0; i < arr.length; i++) {
		obj[arr[i]] = 1;
	}
	for (let j = 0; j < this.length; j++) {
		if (!obj[this[j]]) {
			obj[this[j]] = 1;
			result.push(this[j]);
		}
	}
	return result;
};

//数组包含元素检测
Array.prototype.contains = function (obj) {
	var i = this.length;
	while (i--) {
		if (this[i] === obj) {
			return true;
		}
	}
	return false;
}

/**
 * 跳转页面到指定地址
 * @param {string} url 需要跳转到的页面地址
 * @param {number} timeout 跳转页面前等待的时间（单位：毫秒）
 */
function goTo(url, timeout) {
	if (timeout) {
		timeout = isNaN(timeout) ? 0 : timeout;
		if (timeout < 1) timeout = 0;

		setTimeout(() => {
			location = url;
		}, timeout);
	} else {
		location = url;
	}
};

/**
 * 获取上一个来源请求页地址，若与当前页一致时忽略
 */
function getReferrer() {
	var referrer = document.referrer;

	if (referrer == location.href.toString()) {
		referrer = "";
	}

	return referrer;
}

/**
 * 页面返回
 * @param {string} urlIfReferrerEmpty 当来源页不存在时默认返回的页面地址
 * @param {number} timeout 跳转页面前等待的时间（单位：毫秒）
 */
function goBack(urlIfReferrerEmpty, timeout) {
	urlIfReferrerEmpty = urlIfReferrerEmpty || "/";
	var referrer = getReferrer();

	if (referrer == null || referrer == "") {
		referrer = urlIfReferrerEmpty;
	}

	if (timeout && timeout > 0) {
		setTimeout(() => {
			location = referrer;
		}, timeout);
	} else {
		location = referrer;
	}
}

/**
 * 构建页面URL地址
 * @param {string} path 加载的页面相对路径
 * @param {any} params 参数对象{key:value}
 */
function buildUrl(path, params) {
	if (typeof path !== "string") return;
	if (path === null || path === "") return;

	if (params === undefined || params == null) return path;

	let baseUrl,			//不带参数的URL地址
		key,				//遍历params对象时属性名的变量定义
		item,				//遍历params对象时解析出适应于URL地址的一个参数（及值）的变量定义
		queries = [],		//存储所有遍历params后适应于URL地址的参数集合变量定义
		newUrl;				//新的需加载的页在URL地址

	//新传入的参数信息
	for (key in params) {
		item = key + "=" + params[key];
		queries.push(item);
	}

	//原URL的参数信息
	let arr = path.split("?");
	baseUrl = arr[0];
	if (arr.length > 1) {
		arr[1].split("&").forEach((q) => {
			let oKey = q.split("=")[0];
			if (!(oKey in params)) {
				queries.push(q);
			}
		});
	}

	newUrl = baseUrl + "?" + queries.join("&");

	return newUrl;
}

/**
 * 重新加载页面
 * @param {Object} params 页面URL的新参数对象
 */
function reload(params) {
	if (this instanceof Vue) {
		params = this.queries || params || null;
	}
	var url = buildUrl.call(window, location.href.toString(), params);
	location.href = url;
}

/**
 * 获取有效时间的提示说明
 * @param {any} st 开始时间
 * @param {any} et 结束时间
 * @param {any} allEmptyTip 开始及结束时间均为空时，表示永久生效，此状态下的提示说明，默认为“永久有效”
 * @param {any} stEmptyTip  仅开始时间为空时（此状态下的提示说明，表示忽略开始时间），默认为“XXX前有效”
 * @param {any} etEmptyTip  仅结束时间为空时（此状态下的提示说明，表示忽略结束时间），默认为“XXX后有效”
 */
function getExpiryTimeTip(st, et, allEmptyTip, stEmptyTip, etEmptyTip) {
	st = st && st != null ? st.toString() : '';
	et = et && st != null ? et.toString() : '';
	var stDesc = st.toDateTime();
	var etDesc = et.toDateTime();

	allEmptyTip = allEmptyTip || "永久有效";
	stEmptyTip = stEmptyTip || "前有效";
	etEmptyTip = etEmptyTip || "后有效";

	if (stDesc === '' && etDesc === '') {
		return allEmptyTip;
	} else if (stDesc === '') {
		return etDesc + stEmptyTip;
	} else {
		return stDesc + etEmptyTip;
	}
}

/**
 * API接口错误时通用处理方式
 * @param {string} code 错误编号
 */
function apiError(code, errorMsg) {
	if (code === undefined) return;

	if (code === ApiResult.LOGIN_TIMEROUT.toString("name")) {
		location = "/home/login";
	} else if (code === ApiResult.NO_ACCESS) {
		var container = document.getElementById("#content")
		if (container) {
			container.innerHTML = "<p stylel='font-weight: bold; color: red;'>权限不足！<p>";
		}
	} else {
		var refUrl = getReferrer();
		if (refUrl != "") {
			goTo(refUrl);
		} else {
			errorMsg = errorMsg || "请求失败，请重试！";
			this.$Message.error(errorMsg);
		}
	}
}
/*
* axios 封装
*/
var Ajax = (function () {

	//默认设置
	var _defaults = {
		baseURL: '',
		timeout: 1000,
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
		for (var t in target) {
			if (target.hasOwnProperty(t) && !source.hasOwnProperty(t)) {
				source[t] = target[t];
			}
		}
	};

	var execute = function (settings) {
		extend(settings, _defaults);
		axios(settings).then(function (response) {
			if (settings.success && typeof settings.success === 'function') {
				settings.success(response);
			}
		}).catch(function (error) {
			if (settings.error && typeof settings.error === 'function') {
				settings.error(error);
			}
		});
	};

	this.post = function (settings) {
		settings['method'] = 'post';

		execute(settings);
	};

	this.get = function (settings) {
		settings['method'] = 'get';

		execute(settings);
	};

	return this;
})();

/*
* 时间扩展
*/
Date.prototype.format = function (format) {
	var z = {
		y: this.getFullYear(),
		M: this.getMonth() + 1,
		d: this.getDate(),
		h: this.getHours(),
		m: this.getMinutes(),
		s: this.getSeconds()
	};

	format = format || 'yyyy-MM-dd hh:mm:ss';

	return format.replace(/(y+|M+|d+|h+|m+|s+)/g, function (v) {
		return ((v.length > 1 ? "0" : "") + eval('z.' + v.slice(-1))).slice(-(v.length > 2 ? v.length : 2));
	});
};

///集合取交集
Array.intersect = function () {
	var result = new Array();
	var obj = {};
	for (var i = 0; i < arguments.length; i++) {
		for (var j = 0; j < arguments[i].length; j++) {
			var str = arguments[i][j];
			if (!obj[str]) {
				obj[str] = 1;
			}
			else {
				obj[str]++;
				if (obj[str] == arguments.length) {
					result.push(str);
				}
			}//end else
		}//end for j
	}//end for i
	return result;
}

//集合去掉重复
Array.prototype.uniquelize = function () {
	var tmp = {},
		ret = [];
	for (var i = 0, j = this.length; i < j; i++) {
		if (!tmp[this[i]]) {
			tmp[this[i]] = 1;
			ret.push(this[i]);
		}
	}

	return ret;
}
//并集
Array.union = function () {
	var arr = new Array();
	var obj = {};
	for (var i = 0; i < arguments.length; i++) {
		for (var j = 0; j < arguments[i].length; j++) {
			var str = arguments[i][j];
			if (!obj[str]) {
				obj[str] = 1;
				arr.push(str);
			}
		}//end for j
	}//end for i
	return arr;
}

//2个集合的差集 在arr不存在
Array.prototype.minus = function (arr) {
	var result = new Array();
	var obj = {};
	for (var i = 0; i < arr.length; i++) {
		obj[arr[i]] = 1;
	}
	for (var j = 0; j < this.length; j++) {
		if (!obj[this[j]]) {
			obj[this[j]] = 1;
			result.push(this[j]);
		}
	}
	return result;
};


/*
* 跳转页面
*/
window.goTo = function (url, timeout) {
	timeout = timeout || 1000;

	setTimeout(() => {
		location = url;
	}, timeout);
}
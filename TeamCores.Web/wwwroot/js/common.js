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
		params: {},
		data: {},
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
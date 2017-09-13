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

    let formatters= {
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
* 时间字符串转换为指定格式的时间字符串
*/
String.prototype.toDateTime = function (format) {
	
	if (this == null || this == '') return '';

	format = format || 'yyyy-MM-dd hh:mm:ss';

	return new Date(Date.parse(this)).format(format);
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

/*
* 跳转页面
*/
window.goTo = function (url, timeout) {
    timeout = timeout || 1000;

    setTimeout(() => {
        location = url;
    },
        timeout);
};
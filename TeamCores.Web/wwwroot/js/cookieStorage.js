
//《JavaScript权威指南》一书中有实现基于cookie的存储API，我把代码敲下来

var Storage = (function(){
	//默认配置
	var defaults = {
		maxage: 60*60*24*1000,  //默认1000天
		path: '/'
	}

	// 是否为json对象
	var isJSON = function(obj) {
		return typeof obj === "object" && Object.prototype.toString.call(obj).toLowerCase() === "[object object]" && !obj.length;
	};

	// 序列化
	var serialize = function(value) {
		return value === undefined || typeof value === "function" ? value + "" : JSON.stringify(value);
	};

	// 反序列化
	var deserialize = function(value) {
		if (typeof value !== "string") {
			return undefined;
		}

		try {
			return JSON.parse(value);
		} catch (e) {
			return value || undefined;
		}
	};

	var getCookie = function(){
		var _cookie = {};
		var all = document.cookie;
		if(all === "")
			return _cookie;
		var list = all.split("; ");
		for(var i=0; i < list.length; i++){
			var cookieData = list[i].split("=");
			var name = cookieData[0];
			var value = cookieData[1];
			value = decodeURIComponent(value);
			_cookie[name] = value;
		}
		return _cookie;
	}

	var cookie = getCookie();
	var keys = [];

	this.set = function(key, value, maxage, path){
        if(!(key in cookie)){
            keys.push(key);
        }

        cookie[key] = value;

        maxage = maxage || defaults.maxage;
        path = path || defaults.path;

        var newCookie = key + "=" + encodeURIComponent(value);
        newCookie +="; max-age=" + maxage;
        newCookie += "; path=" + path;
        document.cookie = newCookie;
    };

    this.get = function(key){
        return cookie[key] || null;
    };

    this.remove = function(key){
        if(!(key in cookie)) return;

        delete cookie[key];

        for(var i = 0; i < keys.length; i++){
            if(keys[i] === key){
                keys.splice(i,1);
                break;
            }
        }

        document.cookie = key + "=; max-age=0";
    };
    this.clear = function(){
        for(var key in keys){
            remove(key);
        }

        cookie = {};
        keys = [];
    }
})();

//《JavaScript权威指南》一书中有实现基于cookie的存储API，我把代码敲下来

/**
 * 存储器,本例使用document.cookie方案
 * 此为单例实例
 */
var Cookie =(function(){
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
	
	/**
	 * 构建缓存处理对象
	 */
	var C = function(){
		//缓存数据集（以键值形式存储）
		this.cookies = {};
		//缓存数据中的所有缓存键名称
		this.keys = [];

		var all = document.cookie;
		if(all !== ""){
			var list = all.split("; ");
			var i,
				len=list.length;
			for(i=0; i < len; i++){
				var cookieData = list[i].split("=");
				var name = cookieData[0];
				var value = cookieData[1];
				value = decodeURIComponent(value);

				this.cookies[name] = value;
				this.keys.push(name);
			}
		}
	}

	C.prototype = {

		constructor: C,

		/**
		* 设置缓存
		* @param {string} key 缓存名称
		* @param {object} value 缓存的值
		* @param {Number} maxage 缓存的时间（单位：毫秒）
		* @param {string} path 缓存的存储路径
		*/
		set: function(key, value, maxage, path){
			if(!(key in this.cookies)){
				this.keys.push(key);
			}

			this.cookies[key] = value;

			maxage = maxage || defaults.maxage;
			path = path || defaults.path;

			var newCookie = key + "=" + encodeURIComponent(value);
			newCookie +="; max-age=" + maxage;
			newCookie += "; path=" + path;
			document.cookie = newCookie;
		},

		/**
		* 获取缓存值
		* @param {string} key 缓存的名称
		* @returns {string} 缓存值
		*/
		get : function(key){
			return this.cookies[key] || null;
		},

		/**
		* 移除缓存信息
		* @param {string} key 将要移除的缓存名称
		*/
		remove : function(key){
			if(!(key in this.cookies)) return;

			delete this.cookies[key];

			var i,
				len = this.keys.length;

			for(i = 0; i < len; i++){
				if(this.keys[i] === key){
					this.keys.splice(i,1);
					break;
				}
			}

			document.cookie = key + "=; max-age=0";
		},

		/**
		* 清空所有缓存
		*/
		clear : function(){
			var _keys = this.keys;
			for(var key in _keys){
				remove(key);
			}

			this.cookies = {};
			this.keys = [];
		}
	}

	return new C();
}());
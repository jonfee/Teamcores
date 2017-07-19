/**
 * 枚举
 */
var Enum = (function () {
	var hasOwnProperty = Object.prototype.hasOwnProperty,
		toString = function (format) {
			var _value = this.value;
			switch (format) {
				case "d":
					_value = this.value;
					break;
				case "a":
				case "alias":
				case "desc":
				case "description":
					_value = this.text;
					break;
				case "D":
				case "n":
				case "name":
				default:
					_value = this.name;
					break;
			}

			return _value;
		};

	var E = function (items) {
		var self = this,
			item,
			key;

		for (key in items) {
			if (hasOwnProperty.call(items, key)) {
				item = items[key];

				// 挂入原型方法
				item.constructor.prototype.valueOf = toString;
				item.constructor.prototype.toString = toString;

				self[key] = item;
			}
		}
	}

	E.prototype =
		{
			constructor: E,

			get: function (name) {
				var item;

				if (hasOwnProperty.call(this, name)) {
					item = this[name];

					return item;
				}
			},

			set: function (name, value, text) {
				var item;

				if (hasOwnProperty.call(this, name)) {
					item = this[name];

					if (value) {
						item.value = value;
					}

					if (text) {
						item.text = text;
					}
				}
			}
		};

	return E;

})();
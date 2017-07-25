/**
 * 枚举类定义
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
				case "text":
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
		},
		valueOf = function () {
			return this.toString('d');
		};

	var I = function (key, item) {
		this.name = key;
		this.value = item.value;
		this.text = item.text;
	};

	I.prototype.valueOf = valueOf;
	I.prototype.toString = toString;

	var E = function (items) {
		this.items = [];
		this.names = [];
		this.values = [];
		this.texts = [];

		var self = this,
			item,
			key;

		for (key in items) {
			if (hasOwnProperty.call(items, key)) {

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
			constructor: E,

			getItem: function (valueOrName) {
				for (var index in this.items) {

					var item = this.items[index];

					if (item.value === valueOrName || item.name === valueOrName) {
						return item;
					}
				}

				return undefined;
			},
			
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

/**
 * 用户状态枚举
 */
const UserStatus = new Enum({
	ENABLED: {
		text: '启用',
		value: 1
	},
	DISABLED: {
		text: '禁用',
		value: 0
	}
});
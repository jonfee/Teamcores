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
		}

	var I = function (key, item) {
		this.name = key;
		this.value = item.value;
		this.text = item.text;
	}
	I.prototype.valueOf = valueOf;
	I.prototype.toString = toString;

	var E = function (items) {
		this.items = [];

		var self = this,
			item,
			key;

		for (key in items) {
			if (hasOwnProperty.call(items, key)) {

				item = items[key];

				var _i = new I(key, item);

				this.items.push(_i);

				self[key] = _i;
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
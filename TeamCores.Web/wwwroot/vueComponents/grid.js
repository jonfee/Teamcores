Vue.component('study-grid', {
	template: '\
		<table v-if="data.length">\
		<thead>\
			<tr>\
				<th v-for="col in columns" @click="sortBy(col.key)" :class="{ active: sortKey == col.key }">{{ col.name }}\
					<span class="arrow" :class="sortOrders[col.key] > 0 ? \'asc\' : \'dsc\'"></span>\
				</th>\
			</tr>\
		</thead>\
		<tbody>\
			<tr v-for="entry in data">\
				<td v-for="col in columns">{{ entry[col.key] }}</td>\
			</tr>\
		</tbody>\
		</table >\
		<p v-else>No matches found.</p>\
	',
	replace: true,
	props: {
		data: Array,
		columns: Array,
		query: Object,
		searcher: Function
	},
	data: function () {
		var sortOrders = {}
		this.columns.forEach(function (col) {
			sortOrders[col.key] = 1
		})
		return {
			query: this.query || {},
			sortKey: '',
			sortOrders: sortOrders
		}
	},
	methods: {
		sortBy: function (key) {
			this.sortKey = key;
			this.sortOrders[key] = this.sortOrders[key] * -1;
			//this.$emit(this.searcher(this.query));
		}
	}
})
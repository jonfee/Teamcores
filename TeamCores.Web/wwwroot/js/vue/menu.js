Vue.component('v-menu', {
	template: '\
		<div class="v-sidebar" >\
			<ul>\
				<li>\
					<a :href="basepath" :class="{active:openmenu==home}">控制台</a>\
				</li>\
			</ul>\
			<template v-for="module in menus">\
			<div class="v-title">{{ module.title }}</div>\
			<ul>\
				<li v-for="item in module.items">\
					<a @click.prevent="open(item)" :href="item.href" :class="{active:item.name == openmenu,tempactive:item.name == tempopen && item.name != openmenu}">{{ item.title }}</a>\
					<ul v-if="item.subitems" :class="{show:item.name == tempopen}">\
						<li v-for="sub in item.subitems">\
							<a :class="{active:sub.name == opensub}" :href="basepath+sub.href">{{ sub.title }}</a>\
						</li>\
					</ul>\
				</li>\
			</ul>\
			</template>\
		</div>'
	,
    props: {
        basepath: String,
        openmenu: String,
        opensub: String,
        menus: Object,
    },
    data() {
        return {
            home: '',
            tempopen: this.openmenu
        }
    },
    methods: {
        open(item) {
            if (item.subitems != null) {
                this.tempopen = item.name
            }
            else {
                location.href = item.href
            }
        }
    }
})
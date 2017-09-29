var menudata = [
	{
		title: '在线学习',
		items: [
			{
				title: '在线课程',
				name: 'course',
				href: '',
				icon: '',
				subitems: [
					{
						title: '课程管理',
						name: 'course_index',
						href: 'course/index',
						new: ''
					},
					{
						title: '章节管理',
						name: 'chapter_index',
						href: 'chapter/index',
						new: ''
					},
					{
						title: '新增课程',
						name: 'course_add',
						href: 'course/add',
						new: ''
					}
				]
			},
			{
				title: '科目管理',
				name: 'subjects_index',
				href: '/subjects/index',
				icon: ''
			},
			{
				title: '考题管理',
				name: 'questions_index',
				href: '/questions/index',
				icon: ''
			},
			{
				title: '考卷&阅卷',
				name: 'exams',
				href: '',
				icon: '',
				subitems: [
					{
						title: '考卷模板',
						name: 'exams_index',
						href: 'exams/index',
						new: ''
					},
					{
						title: '阅卷中心',
						name: 'exams_reviewcenter',
						href: 'exams/reviewcenter',
						new: ''
					},
					{
						title: '练习&考试',
						name: 'exams_testlist',
						href: 'exams/testlist',
						new: ''
					},
					{
						title: '我的考卷',
						name: 'exams_mytestlist',
						href: 'exams/mytestlist',
						new: ''
					}
				]
			},
			{
				title: '学习计划',
				name: 'studyplan_index',
				href: '/studyplan/index',
				icon: ''
			}
		]
	},
	{
		title: '用户和消息',
		items: [
			{
				title: '用户管理',
				name: 'users_index',
				href: '/user/index',
				icon: ''
			},
			{
				title: '消息短讯',
				name: 'message_index',
				href: '/message/index',
				icon: ''
			}
		]
	}
	/*,
	{
		title: '系统配置',
		items: [
			{
				title: '基本配置',
				name: 'base',
				href: '',
				icon: ''
			},
			{
				title: '附件配置',
				name: 'attach',
				href: '',
				icon: ''
			},
			{
				title: '接口配置',
				name: 'api',
				href: '',
				icon: '',
				subitems: [
					{
						title: '支付接口',
						name: 'pay',
						href: 'http://www.baidu.com',
						new: ''
					},
					{
						title: '登陆接口',
						name: 'login',
						href: 'http://www.baidu.com',
						new: ''
					},
					{
						title: '短信接口',
						name: 'sms',
						href: 'http://www.baidu.com',
						new: ''
					}
				]
			}
		]
	}*/
]

window.MyMenu = (function (menuConfig) {
	this.openMenu = '';
	this.openSub = '';

	return {
		init: function () {
			let currentUrl = location.href.toString();

			//匹配出controller跟action
			let reg = /^https?:\/{2}[^/]+(\/([^/]*))?(\/([^/?]*))?([?/].+)?/ig;

			let fields = reg.exec(currentUrl);

			let controller = fields[2];
			let action = fields[4] || "index";

			let subName = controller + "_" + action;
			let menuName = getMenuName();
			
			//设置菜单项
			this.set(menuName, subName);

			/**
			 * 获取父菜单名称
			 */
			function getMenuName() {
				for (var i = 0; i < menuConfig.length; i++) {
					var items = menuConfig[i].items;
					for (var j = 0; j < items.length; j++) {
						var item = items[j];
						if (item.name === subName) {
							return subName;
						} else if (item.subitems && item.subitems.length > 0) {
							for (var k = 0; k < item.subitems.length; k++) {
								var sub = item.subitems[k];
								if (sub.name === subName) {
									return item.name;
								}
							}
						}
					}
				}
				return "";
			}
		},
		set: function (menuName, subName) {
			this.openMenu = menuName;
			this.openSub = subName;
		},
		get: function () {
			return {
				menuName: this.openMenu,
				subName: this.openSub
			}
		}
	};
})(menudata);

MyMenu.init();

var sidebar = new Vue({
	el: '#sidebar',
	data: {
		menus: menudata,
		openmenu: MyMenu.get().menuName,
		opensub: MyMenu.get().subName
	}
})
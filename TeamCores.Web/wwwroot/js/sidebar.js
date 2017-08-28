var menudata = [
	{
		title: '在线学习',
		items: [
			{
				title: '在线课程',
				name: 'content',
				href: '',
				icon: '',
				subitems: [
					{
						title: '课程管理',
						name: '',
						href: 'course',
						new: ''
                    },
                    {
						title: '章节管理',
						name: '',
                        href: 'chapter',
						new: ''
					},
					{
						title: '新增课程',
						name: 'charge',
						href: 'course/add',
						new: ''
					}
				]
			},
			{
				title: '科目管理',
				name: 'report',
				href: '/subjects/index',
				icon: ''
			},
			{
				title: '考题管理',
				name: 'comment',
				href: '/questions/index',
				icon: ''
			},
			{
				title: '考卷&阅卷',
				name: 'comment',
                href: '/exams/index',
				icon: '',
				subitems: [
					{
						title: '考卷管理',
						name: '',
                        href: '/exams/index',
						new: ''
					},
					{
						title: '考试阅卷',
						name: 'charge',
						href: 'http://www.baidu.com',
						new: ''
					}
				]
			},
			{
				title: '学习计划',
				name: 'studyplan',
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
				name: 'users',
				href: '/user/index',
				icon: ''
			},
			{
				title: '消息短讯',
				name: 'assets',
				href: '',
				icon: '',
				subitems: [
					{
						title: '消息管理',
						name: '',
						href: 'http://www.baidu.com',
						new: ''
					},
					{
						title: '发布消息',
						name: 'charge',
						href: 'http://www.baidu.com',
						new: ''
					}
				]
			}
		]
	},
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
	}
]

window.MyMenu = (function () {
	var openMenu, openSub;

	return {
		set: function (menuName, subName) {
			this.openMenu = menuName;
			this.openSub = subName;
		},
		get: function () {
			return {
				menuName: this.openMenu || '',
				subName: this.openSub || ''
			}
		}
	};
})();

var sidebar = new Vue({
	el: '#sidebar',
	data: {
		menus: menudata,
		openmenu: MyMenu.get().menuName,
		opensub: MyMenu.get().subName
	}
})
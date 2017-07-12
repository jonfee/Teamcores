var menudata = [
    {
        title: '内容运营',
        items: [
            {
                title: '内容管理',
                name: 'content',
                href: '',
                icon: '',
                subitems: [
                    {
                        title: '所有文章',
                        name: 'postlist',
                        href: '/content',
                        new: ''
                    },
                    {
                        title: '新增文章',
                        name: 'post',
                        href: '/content/post',
                        new: ''
                    },
                    {
                        title: '分类管理',
                        name: 'category',
                        href: '/category',
                        new: ''
                    }
                ]
            },
            {
                title: '举报管理',
                name: 'report',
                href: '/content/report',
                icon: ''
            },
            {
                title: '评论管理',
                name: 'comment',
                href: '/content/comment',
                icon: ''
            }
        ]
    },
    {
        title: '会员和资产',
        items: [
            {
                title: '用户管理',
                name: 'users',
                href: '',
                icon: ''
            },
            {
                title: '资产管理',
                name: 'assets',
                href: '',
                icon: '',
                subitems: [
                    {
                        title: '提现申请',
                        name: '',
                        href: 'http://www.baidu.com',
                        new: ''
                    },
                    {
                        title: '充值记录',
                        name: 'charge',
                        href: 'http://www.baidu.com',
                        new: ''
                    }
                ]
            }
        ]
    },
    {
        title: '运营分析',
        items: [
            {
                title: '趋势报告',
                name: 'status',
                href: '',
                icon: '',
                subitems: [
                    {
                        title: '日趋势',
                        name: 'day',
                        href: 'http://www.baidu.com',
                        new: ''
                    },
                    {
                        title: '实时在线',
                        name: 'online',
                        href: 'http://www.baidu.com',
                        new: ''
                    }
                ]
            },
            {
                title: '访问记录',
                name: 'views',
                href: '',
                icon: '',

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

var sidebar = new Vue({
    el: '#sidebar',
    data: {
        menus: menudata,
        openmenu: menuname,
        opensub: subname
    }
})
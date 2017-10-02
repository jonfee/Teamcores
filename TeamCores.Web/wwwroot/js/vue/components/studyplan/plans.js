/**
 * 学习计划列表 组件
 */
var studyPlans = {
	template: '\
			<div id="study_plans">\
				<template v-if="hasAccess">\
					<div class="searcher">\
						<span>\
							关键词：\
							<i-input v-model="currentQueries.keyword" placeholder="学习计划标题..." style="width: 250px"></i-input>\
						</span>\
						<span>\
							状态：\
							<i-select v-model="currentQueries.status" style="width: 100px">\
								<i-option value="" key="">全部</i-option>\
								<i-option v-for="item in planStatus" :value="item.value" :key="item.value">{{ item.text }}</i-option>\
							</i-select>\
						</span>\
						<span>\
							<i-button type="primary" icon="ios-search" v-on:click="search">搜索</i-button>\
						</span>\
					</div>\
					<i-table :columns="gridColumns" :data="gridData"></i-table>\
					<Page class-name="pager" :total="currentQueries.total" :current="currentQueries.p" :page-size="currentQueries.size" v-on:on-change="pagerChanged" show-total></Page>\
				</template>\
				<template v-else>\
					<p class="error-tip">无访问权限</p>\
				</template>\
			</div>\
		',
	props:['loading','queries'],
	data: function(){
		var _self = this;
		return {
				hasAccess : true,
                currentQueries: {
                    keyword: '',
                    status: 0
                },
				planStatus: StudyPlanStatus.items,
				gridColumns: [
					{ key: 'Title', title: '标题'},
					{ key: 'Creator', title: '制定者' },
					{ key: 'Student', title: '学员数' },
					{
						key: 'Status',
						title: '状态',
						render(h, params) {
							return StudyPlanStatus.getItem(params.row.Status);
						}
					},
					{
						key: 'CreateTime',
						title: '创建时间',
						render(h, params) {
							return new Date(Date.parse(params.row.CreateTime)).format('yyyy-MM-dd hh:mm:ss');
						}
					},
					{
						key: 'actions',
						title: '操作',
						width: 200,
						render(h, params) {
							return h('p',
								[
									h('i-button',
										{
											props: {
												type: 'primary',
												size: 'small'
											},
											style: {
												marginRight: '5px'
											},
											on: {
												click: () => {
													_self.goDetails(params.row.PlanId);
												}
											}
										},
										'查看'),
									h('i-button',
										{
											props: {
												type: 'error',
												size: 'small'
											},
											on: {
												click: () => {
													_self.setStatus(params.index);
												}
											}
										}, _self.getStatusActionName(params.row.Status))
								]);
						}
					}
				],
				gridData: []
			}
	},
	created() {
		if (this.queries != null) {
			this.loadQueries();
			this.loadData();
		}
	},
	methods: {
		/**
		* 加载筛选条件
		*/
		loadQueries() {
			this.currentQueries["p"] = this.queries.p || 1;
			this.currentQueries["size"] = this.queries.size || 15;
			this.currentQueries["keyword"] = this.queries.keyword;
			this.currentQueries["status"] = this.queries.status;
		},
		/**
		 * 分页页码改变回调事件
		 * @@param {number} current 当前页码
		 */
		pagerChanged(current) {
			this.currentQueries["p"] = current;
			reload.call(this, this.currentQueries);
		},
		/**
		 * 搜索
		*/
		search() {
			this.currentQueries["p"] = 1;
            this.currentQueries["total"] = 0;
			reload.call(this, this.currentQueries);
		},
		/**
		 * 加载数据，会自动从searchQuery中解析搜索的参数
		 */
		loadData: function() {
			var postData = {
				keyword: this.currentQueries.keyword,
				status: this.currentQueries.status,
				pageindex: this.currentQueries.p,
				pagesize: this.currentQueries.size
			};

			Ajax.post({
				url: "/api/studyplan/search",
				data: postData,
				success: (response) => {
					var data = response.data;

					if (!data.Error) {
						var pager = data.Data;
						this.gridData = pager.Table;

						this.currentQueries["total"] = pager.Count;
                    } else {
                        apiError.call(this, data.Code, data.Data);
                    }
                },
                error: (error) => {
                    apiError.call(this);
                }
			});
		},
		/**
		 * 获取当前数据项的状态操作名称
		 * @@param {int} status 状态
		 */
		getStatusActionName: function(status) {
			var actionName = '';
			if (StudyPlanStatus.ENABLED == status) {
				actionName = '禁用';
			} else if (StudyPlanStatus.DISABLED == status) {
				actionName = '启用';
			}

			return actionName;
		},
		/**
		 * 根据数据项所在索引跳转到详情页
		 * @@param {long} planId 计划ID
		 */
		goDetails: function(planId) {
			location = '/studyplan/details/' + planId;
		},
		/**
		 * 根据数据项所在索引设置对应的相反状态
		 * @@param  {int} index 数据项在数组中的索引
		 */
		setStatus: function(index) {
			//操作
			var action, nextStatus;

			var item=this.gridData[index];

			//如果当前状态为“启用”，则将改变为“禁用”,反之则改变为“启用”状态
			if (item.Status == StudyPlanStatus.ENABLED) {
				action = 'setdisable';
				nextStatus = StudyPlanStatus.DISABLED;
			} else {
				action = 'setenable';
				nextStatus = StudyPlanStatus.ENABLED;
			}

			Ajax.post({
				url: '/api/studyplan/' + action,
				data: { id: item.PlanId },
				success: (response) => {
					var data = response.data;
					
					if (!data.Error) {
						item.Status = nextStatus.toString('d');
                    } else {
                        apiError.call(this, data.Code, data.Data);
                    }
                },
                error: (error) => {
                    apiError.call(this);
                }
			});
		}
	}
}

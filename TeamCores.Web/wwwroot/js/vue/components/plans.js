var studyPlans = {
	template: '\
			<div id="study_plans">\
				<div class="searcher">\
					<span>\
						关键词：\
						<i-input v-model="searchQuery.keyword" placeholder="学习计划标题..." style="width: 250px"></i-input>\
					</span>\
					<span>\
						状态：\
						<i-select v-model="searchQuery.status" style="width: 100px">\
							<i-option value="" key="">全部</i-option>\
							<i-option v-for="item in planStatus" :value="item.value" :key="item.value">{{ item.text }}</i-option>\
						</i-select>\
					</span>\
					<span>\
						<i-button type="primary" icon="ios-search" v-on:click="search">搜索</i-button>\
					</span>\
				</div>\
				<i-table :columns="gridColumns" :data="gridData"></i-table>\
				<Page class-name="pager" :total="searchQuery.total" :current="searchQuery.pageindex" :paeg-size="searchQuery.pagesize" show-total></Page>\
			</div>\
		',
	props:['loading'],
	data: function(){
		var _self = this;
		return {
				searchQuery: {},
				planStatus: StudyPlanStatus.items,
				gridColumns: [
					{ key: 'Title', title: '标题'},
					{ key: 'Creator', title: '制定者' },
					{ 
						key: 'Student', 
						title: '学员数',
						render(h, params) {
							return h('Button', {
                                    props: {
                                        type: 'text',
                                        size: 'small'
                                    },
									on:{
										click:()=>{
											_self.goPlanStudents(params.row.PlanId);
										}
									}
                                }, params.row.Student)
						}
					},
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
											style: {
												marginRight: '5px'
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
	watch:{
		loading: {
			handler: function(newVal){
				if(newVal){
					this.search();
				}
			},
			deep:true
		}
	},
	mounted(){
		if(this.loading){
			this.search();
		}
	},
	methods: {
		/**
		 * 执行搜索，并默认将当前页码重置为第一页
		 */
		search: function() {
			this.reviseSearchQuery(15, 1);
			this.loadData();
		},
		/**
		 * 校正searchQuery的参数值
		 *
		 * @@param {int} pageSize 每页条数
		 * @@param {int} pageIndex 当前页码
		 * @@param {int} totalResult 数据总数
		 * @@param {string} keyword 搜索的关键词
		 * @@param {int} status 筛选的计划状态
		 */
		reviseSearchQuery: function(pageSize, pageIndex, totalResult, keyword, status) {
			if (pageIndex) this.searchQuery['pageindex'] = pageIndex;
			if (pageSize) this.searchQuery['pagesize'] = pageSize;
			if (totalResult) this.searchQuery['total'] = totalResult;
			if (keyword) this.searchQuery['keyword'] = keyword;
			if (status) this.searchQuery['status'] = status;
		},
		/**
		 * 加载数据，会自动从searchQuery中解析搜索的参数
		 */
		loadData: function() {
			var _this = this;
			var pagesize = _this.searchQuery.pagesize,
				pageindex = _this.searchQuery.pageindex,
				keyword = _this.searchQuery.keyword || '',
				status = typeof (_this.searchQuery.status) === 'undefined' ? '' : _this.searchQuery.status;

			var postData = { keyword: keyword, status: status, pageindex: pageindex, pagesize: pagesize };

			Ajax.post({
				url: "/api/studyplan/search",
				data: postData,
				success: (response) => {
					var data = response.data;
					if (!data.Error) {
						var pager = data.Data;
						_this.gridData = pager.Table;
						_this.reviseSearchQuery(pager.Size, pager.Index, pager.Count);
					}
				},
				error: (error) => {

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
		 * 跳转到新增学习计划页面
		 */
		goAdd: function() {
			location = '/studyplan/add';
		},
		/**
		 * 根据数据项所在索引跳转到详情页
		 * @@param {long} planId 计划ID
		 */
		goDetails: function(planId) {
			location = '/studyplan/details/' + planId;
		},
		/**
		*	跳转到该计划的学员列表
		* @@param {long} planId 计划ID
		*/
		goPlanStudents:function(planId){
			location='/studyplan/students/'+planId;
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
						this.$Message.error(data.Message);
					}
				},
				error: (error) => {
					this.$Message.error('操作失败，请重试！');
				}
			});
		}
	}
}

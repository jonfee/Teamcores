/**
 * 我的学习计划 组件
 */
var myPlans = {
	template: '\
			<div id="study_plans">\
				<div class="searcher">\
					<span>\
						学习状态：\
						<i-select v-model="currentQueries.status" style="width: 100px">\
							<i-option value="" key="">全部</i-option>\
							<i-option v-for="item in studyStatus" :value="item.value" :key="item.value">{{ item.text }}</i-option>\
						</i-select>\
					</span>\
					<span>\
						<i-button type="primary" icon="ios-search" v-on:click="search">搜索</i-button>\
					</span>\
				</div>\
				<i-table :columns="gridColumns" :data="gridData"></i-table>\
				<Page class-name="pager" :total="currentQueries.total" :current="currentQueries.p" :page-size="currentQueries.size" v-on:on-changed="pagerChanged" show-total></Page>\
			</div>\
		',
	props:['loading','queries'],
	data: function(){
		var _self = this;
		return {
				currentQueries: {},
				studyStatus: StudyStatus.items,
				gridColumns: [
					{ key: 'Title', title: '标题'},
					{ key: 'Creator', title: '制定者' },
					{ key: 'StudentCount',title: '学员数'},
					{
						key: 'Status',
						title: '学习状态',
						render(h, params) {
							return StudyStatus.getItem(params.row.StudyStatus);
						}
					},
					{
						key: 'Progress',
						title: '学习进度',
						render(h, params) {
							return  (params.row.Progress*100).toFixed(0) +'%';
						}
					},
					{
						key: 'LastStudyTime',
						title: '最后学习时间',
						render(h, params){
							if(params.row.LastStudyTime){
								return new Date(Date.parse(params.row.LastStudyTime)).format('yyyy-MM-dd hh:mm:ss');
							}else{
								return '--';
							}
						}
					},
					{
						key: 'actions',
						title: '操作',
						width: 200,
						render(h, params) {
							return h('i-button',{
									props: {
										type: 'primary',
										size: 'small'
									},
									on: {
										click: () => {
											_self.goDetails(params.row.PlanId);
										}
									}
								},'查看')
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
				studystatus: this.currentQueries.status,
				pageindex: this.currentQueries.p,
				pagesize: this.currentQueries.size
			};

			Ajax.post({
				url: "/api/userstudyplan/myplans",
				data: postData,
				success: (response) => {
					var data = response.data;

					if (!data.Error) {
						var pager = data.Data;
						this.gridData = pager.Table;

						this.currentQueries["total"] = pager.Count;
					} else {
						apiError(data.Code);
					}
				},
				error: (error) => {
					this.$Message.error('数据加载失败，请重试！');
				}
			});
		},
		/**
		 * 根据数据项所在索引跳转到详情页
		 * @@param {long} planId 计划ID
		 */
		goDetails: function(planId) {
			location = '/userstudyplan/details/' + planId;
		}
	}
}

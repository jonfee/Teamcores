/**
 * 我的学习计划 组件
 */
var myPlans = {
	template: '\
			<div id="study_plans">\
				<div class="searcher">\
					<span>\
						学习状态：\
						<i-select v-model="searchQuery.status" style="width: 100px">\
							<i-option value="" key="">全部</i-option>\
							<i-option v-for="item in studyStatus" :value="item.value" :key="item.value">{{ item.text }}</i-option>\
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
				studyStatus: StudyStatus.items,
				gridColumns: [
					{ key: 'Title', title: '标题'},
					{ key: 'Creator', title: '制定者' },
					{ 
						key: 'StudentCount', 
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
                                }, params.row.StudentCount)
						}
					},
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
		 * @@param {int} status 筛选的计划学习状态
		 */
		reviseSearchQuery: function(pageSize, pageIndex, totalResult,status) {
			if (pageIndex) this.searchQuery['pageindex'] = pageIndex;
			if (pageSize) this.searchQuery['pagesize'] = pageSize;
			if (totalResult) this.searchQuery['total'] = totalResult;
			if (status) this.searchQuery['status'] = status;
		},
		/**
		 * 加载数据，会自动从searchQuery中解析搜索的参数
		 */
		loadData: function() {
			var _this = this;
			var pagesize = _this.searchQuery.pagesize,
				pageindex = _this.searchQuery.pageindex,
				status = typeof (_this.searchQuery.status) === 'undefined' ? '' : _this.searchQuery.status;

			var postData = { studystatus: status, pageindex: pageindex, pagesize: pagesize };

			Ajax.post({
				url: "/api/userstudyplan/myplans",
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
		 * 根据数据项所在索引跳转到详情页
		 * @@param {long} planId 计划ID
		 */
		goDetails: function(planId) {
			location = '/userstudyplan/details/' + planId;
		},
		/**
		*	跳转到该计划的学员列表
		* @@param {long} planId 计划ID
		*/
		goPlanStudents:function(planId){
			location='/studyplan/students/'+planId;
		}
	}
}

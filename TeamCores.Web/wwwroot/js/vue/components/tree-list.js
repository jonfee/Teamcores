var treeNode = {
    name : "tree-node",
    props : {
        model : Object,
        level : Number // 当前递归层级
    },
    template : `
                <a>
                <div :class="['bg-level','level' + level]" :style="{'paddingLeft': (level-1)*55 + 20 + 'px'}" v-on:click="onToggle()">
                    <span class="tree-operation">
                         <Icon v-if="isParent" class="tree-item-icon" :class="{ 'open' : isSpread }" type="arrow-right-b"></Icon>
                         <span>{{model.Title}}</span>
                         <span>：</span>
                         <span>{{model.Content}}</span>
                    </span>
                    
                    <!-- 最多添加三层 -->
                    <span v-if="level < 4" class="tree-item-options" v-show="true">
                        <i-button v-on:click.stop="onPreview" type="text" size="small">查看</i-button>
                        <i-button v-if="level < 3" v-on:click.stop="onAddChild" type="text" size="small">添加子章节</i-button>
                        <i-button v-on:click.stop="onModify" type="text" size="small">编辑</i-button>
                        <i-button v-on:click.stop="onRemove" type="text" size="small">删除</i-button>
                    </span>
                </div>
                 <nav class="tree-nav" :class="{'show' : isSpread}" v-if="isParent">
                        <tree-node  class="tree-item"  
                                    v-for='item in model.Children' 
                                    :model='item' :level="level+1"
                                    v-on:addchild="onEmitAddChild"
                                    v-on:preview="onEmitPreview"
                                    v-on:modify="onEmitModify"
                                    v-on:remove="onEmitRemove"
                                    ><tree-node>
                 </nav>
                </a>
                `,
    mounted()
    {
    },
    computed :
    {
        isParent()
        {
            return this.model.Children && this.model.Children.length;
        }
    },
    data()
    {
        return {
            isSpread : false //是否展开
        };
    },
    methods : {
        /**
         * 查看详情
         */
        onPreview()
        {
            this.$emit("preview", this.model);
        },
        /**
         * 添加子章节点击事件
         */
        onAddChild()
        {
            this.$emit("addchild", this.model);
        },
        /**
         * 编辑按钮点击事件
         */
        onModify()
        {
            this.$emit("modify", this.model);
        },
        /**
         * 删除按钮点击事件
         */
        onRemove()
        {
            this.$emit("remove", this.model);
        },

        /***********递归子组件的事件开始**********************/
        /**
         * 查看详情
         */
        onEmitPreview(selectedItem)
        {
            this.$emit("preview", selectedItem ? selectedItem : this.model);
        },
        /**
         * 添加子章节点击事件
         */
        onEmitAddChild(selectedItem)
        {
            this.$emit("addchild", selectedItem ? selectedItem : this.model);
        },
        /**
         * 编辑按钮点击事件
         */
        onEmitModify(selectedItem)
        {
            this.$emit("modify", selectedItem ? selectedItem : this.model);
        },
        /**
         * 删除按钮点击事件
         */
        onEmitRemove(selectedItem)
        {
            this.$emit("remove", selectedItem ? selectedItem : this.model);
        },
        /***********递归子组件的事件结束**********************/

        /**
         * 展开/收起
         */
        onToggle()
        {
            this.isSpread = !this.isSpread;
        }

    }
};
var treeList = {
    name : "tree-list",
    props : {
        model : Array // 数据源
    },
    template : `
                <nav class="tree-list">
                <tree-node class="tree-item top" 
                            v-for="item in model" :model="item" :level="1" 
                            v-on:preview="onPreview"
                            v-on:addchild="onAddChild"
                            v-on:modify="onModify"
                            v-on:remove="onRemove"

                            >
                </tree-node>
                 </nav>
                `,
    mounted()
    {
    },
    components : {
        'tree-node' : treeNode
    },
    methods : {
        /**
         * 查看详情
         */
        onPreview(selectedItem)
        {
            this.$emit("preview", selectedItem);
        },
        /**
         * 添加子章节点击事件
         */
        onAddChild(selectedItem)
        {
            this.$emit("addchild", selectedItem);
        },
        /**
         * 编辑按钮点击事件
         */
        onModify(selectedItem)
        {
            this.$emit("modify", selectedItem);
        },
        /**
         * 删除按钮点击事件
         */
        onRemove(selectedItem)
        {
            this.$emit("remove", selectedItem);
        }
    }
};
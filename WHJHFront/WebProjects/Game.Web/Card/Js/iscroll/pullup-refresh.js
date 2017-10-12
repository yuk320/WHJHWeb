/* 
  Pull组件
*/
function Pull() {
    this._hasClass = IScroll.utils.hasClass;
}

Pull.prototype = {
    constructor: Pull,

    /**
     * 工具方法，添加类名
     * 
     * @param {any} e 
     * @param {any} name 
     * @returns 
     */
    _addClass: function(e, name) {
        if (this._hasClass(e, name)) {
            return;
        }
        var newclass = e.className.split(' ');
        newclass.push(name);
        e.className = newclass.join(' ');
    },

    /**
     * 工具方法，移除类名
     * 
     * @param {any} e 
     * @param {any} name 
     * @returns 
     */
    _removeClass: function(e, name) {
        if (!this._hasClass(e, name)) {
            return;
        }

        var re = new RegExp("(^|\\s)" + name + "(\\s|$)", 'g');
        e.className = e.className.replace(re, ' ');
    },

    /**
     * 初始化适合IScroll的容器结构
     * 
     */
    init: function(parameter) {
        // 默认的配置
        this.options = {
            bounce: true,
            probeType: 2
        }

        // 初始化配置对象
        IScroll.utils.extend(this.options, parameter.options);

        /* Pull对象的状态 */

        // 是否允许上拉下拉动作
        this.canPullUp = parameter.canPullUp;
        this.canPullDown = parameter.canPullDown;


        // 防止重复执行Pull组件动作的标识符
        this.isPull = false;

        // pullUp组件的状态（可能会添加左右状态）
        this.isUp = false;
        this.isUpAfter = false;
        this.isUpBegin = false;

        // pullDown组件的状态
        this.isDown = false;
        this.isDownAfter = false;
        this.isDownBegin = false;

        // 容器id和内容选择器
        this.id = parameter.id;
        this.content = parameter.content;

        // 组件高度（回弹高度）和能拉动超出边界的最大距离 
        this.pullHeight = parameter.pullHeight;
        this.pullMaxHeight = parameter.pullMaxHeight;

        // 上拉下拉组件文字显示
        this.upBeginText = parameter.upBeginText;
        this.upAfterText = parameter.upAfterText;
        this.downBeginText = parameter.downBeginText;
        this.downAfterText = parameter.downAfterText;

        // 下拉上拉动作（可能会加左右拉动作）
        this.upAction = parameter.pullUpAction;
        this.downAction = parameter.pullDownAction;

        this._initEl();

        this._initIScroll();
    },

    /**
     * 完成wrapper容器的初始化，包括添加scroller容器，将之前的内容放入scroller
     * 分别在内容的尾头添加pullUp和pullDown组件
     * 
     */
    _initEl: function() {
        // 创建scroller容器
        var wrapper = document.getElementById(this.id);
        var div = document.createElement('div');

        div.className = 'scroller';

        wrapper.appendChild(div);

        this.scroller = wrapper.querySelector('.scroller');
        var list = wrapper.querySelector('#' + this.id + ' ' + this.content);

        this.scroller.insertBefore(list, this.scroller.childNodes[0]);

        if (this.canPullDown) {
            this.pullDown = this._initPull("down");
            this.scroller.insertBefore(this.pullDown.main, this.scroller.childNodes[0]);
        }

        if (this.canPullUp) {
            this.pullUp = this._initPull("up");
            this.scroller.appendChild(this.pullUp.main);
        }

        this.wrapper = wrapper;
    },

    /**
     * 初始化额外添加的上拉下拉显示部分
     * 
     * @param {any} name 
     */
    _initPull: function(name) {
        // 组件容器
        var pull = document.createElement('div');
        pull.className = "pull";

        // 加载动画
        var loader = document.createElement('div');
        loader.className = "loader";
        for (var i = 0; i < 4; i++) {
            var span = document.createElement('span');
            loader.appendChild(span);
        }
        // 加载文字显示
        var label = document.createElement('div');
        label.className = "label";
        // 组成组件
        pull.appendChild(loader);
        pull.appendChild(label);

        // 将组件归入对象中
        // pull = $(pull);
        // loader = $(loader);
        // label = $(label);

        // 确定组件类型
        this._addClass(pull, name);
        this._addClass(pull, "hide");
        this._addClass(loader, "hide");

        return {
            main: pull,
            loader: loader,
            label: label
        };
    },

    /**
     * 根据所给的滚动容器的当前位置和最大滚动距离确定Pull对象中的相关属性的值
     * 修改Pull状态
     * @param {any} value 
     * @param {any} maxValue 
     */
    _status: function(value, maxValue) {

        switch (true) {
        case value > 0 && value < this.pullHeight && !this.isDownBegin:
            this.isPull = true;
            this.isDownBegin = true;
            break;
        case value > this.pullHeight && !this.isDownAfter:
            this.isPull = true;
            this.isDown = true;
            this.isDownBegin = false;
            this.isDownAfter = true;
            break;
        case value < maxValue && value > maxValue - this.pullHeight && !this.isUpBegin:
            this.isPull = true;
            this.isUpBegin = true;
            break;
        case value < maxValue - this.pullHeight && !this.isUpAfter:
            this.isPull = true;
            this.isUp = true;
            this.isUpBegin = false;
            this.isUpAfter = true;
            break;
        }

        if (!this.canPullDown) {
            this.isDown = false;
            this.isDownAfter = false;
            this.isDownBegin = false;
        }

        if (!this.canPullUp) {
            this.isUp = false;
            this.isUpAfter = false;
            this.isUpBegin = false;
        }
    },

    /**
     * 上拉超出边界执行
     * 显示上拉组件
     * 显示提示文字“上拉加载”
     * 
     */
    upBegin: function() {
        this._removeClass(this.pullUp.main, "hide");
        this._addClass(this.pullUp.main, "show");
        this.pullUp.label.innerText = this.upBeginText;
    },

    /**
     * 上拉超出可加载边界执行
     * 提示文字改为"松开加载下一页"
     * 
     */
    upAfter: function() {
        this.pullUp.label.innerText = this.upAfterText;
    },

    /**
     * 下拉超出边界执行
     * 显示下拉组件
     * 显示提示文字"下拉加载"
     * 
     */
    downBegin: function() {
        this._removeClass(this.pullDown.main, "hide");
        this._addClass(this.pullDown.main, "show");
        this.pullDown.label.innerText = this.downBeginText;
    },

    /**
     * 下拉超出可加载边界执行
     * 提示文字改为"松开加载上一页"
     * 
     */
    downAfter: function() {
        this.pullDown.label.innerText = this.downAfterText;
    },

    /**
     * 松开手指后加载之前执行
     * 提示文字消除，显示loader动画
     * 
     * @param {any} pull 
     */
    load: function(pull) {
        pull.label.innerText = "";
        this._removeClass(pull.loader, "hide");
        this._addClass(pull.loader, "show");
    },

    /**
     * 加载完成执行
     * 隐藏loader动画
     * 隐藏pull组件
     * 刷新滚动容器的位置和滚动对象的参数 
     * 
     * @param {Object} pull 
     */
    loaded: function (pull) {
        this._removeClass(pull.loader, "show");
        this._addClass(pull.loader, "hide");

        this.iScroll.refresh(pull);
        this.iScroll.scrollTo(0, 0);
    },

    /**
     * 刷新Pull对象的状态属性 
     * 
     */
    refresh: function(pull) {
        if (pull) {
            this._removeClass(pull.main, "show");
            this._addClass(pull.main, "hide");
        }

        // 防止重复执行Pull组件动作的标识符
        this.isPull = false;

        // pullUp组件的状态
        this.isUp = false;
        this.isUpAfter = false;
        this.isUpBegin = false;

        // pullDown组件的状态
        this.isDown = false;
        this.isDownAfter = false;
        this.isDownBegin = false;
    },


    /**
     * 创建IScroll对象，并绑定相关事件
     * 
     */
    _initIScroll: function() {
        document.addEventListener('touchmove', function(e) { e.preventDefault(); }, false);

        // 创建IScroll对象
        this.iScroll = new IScroll(this.wrapper || this.id, this.options);
        // 缓存this
        var that = this;

        // 滚动之前，重置pull对象的属性
        this.iScroll.on("beforeScrollStart",
            function() {
                that.refresh();
            });

        // 滚动时，根据Pull的不同状态，执行相应方法,尽量只执行一次
        this.iScroll.on("scroll",
            function() {
                this.y = this.y > that.pullMaxHeight
                    ? that.pullMaxHeight
                    : (this.y < this.maxScrollY - that.pullMaxHeight ? this.maxScrollY - that.pullMaxHeight : this.y);
                var y = Math.floor(this.y);
                that._status(y, this.maxScrollY);

                switch (true) {
                case that.isUpBegin && that.isPull:
                    that.isPull = false;
                    that.upBegin();
                    break;
                case that.isUpAfter && that.isPull:
                    that.isPull = false;
                    this.minScrollY = this.maxScrollY === 0 ? -that.pullHeight : that.pullHeight;
                    that.upAfter();
                    break;
                case that.isDownBegin && that.isPull:
                    that.isPull = false;
                    that.downBegin();
                    break;
                case that.isDownAfter && that.isPull:
                    that.isPull = false;
                    this.minScrollY = that.pullHeight;
                    that.downAfter();
                    break;
                }
            });

        // 当滚动结束后，根据Pull是否处于相应状态，执行相应方法
        this.iScroll.on("scrollEnd",
            function() {

                if (that.isUp) {
                    that.load(that.pullUp);
                    that.upAction && that.upAction(that.pullUp);
                    that.isUp = false;
                } else if (that.isDown) {
                    that.load(that.pullDown);
                    that.downAction && that.downAction(that.pullDown);
                    that.isDown = false;
                } else {
                    that.refresh(that.pullDown);
                    that.refresh(that.pullUp);
                }
            });

    }
}
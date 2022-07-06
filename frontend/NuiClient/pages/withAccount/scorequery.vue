<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">成绩查询</block>
		</cu-custom>
		
		<view class="cu-list menu" style="margin-top: 50rpx;" v-if="listshwo==false">
				<view class="cu-item" >
					<view class="content" style="text-align:center;" >
						
						<text class="text-grey" >您未参加过考试</text>
					</view>
				</view>				
			</view>
		
		<mescroll-body ref="mescrollRef" @init="mescrollInit" @down="downCallback" @up="upCallback" :up="upOption">
			<view class="cu-list menu" style="margin-top:50rpx;">
				<view class="cu-item"  @tap="getcertificate" v-for="(item,index) in dataList"
			 :data-item="JSON.stringify(item)" v-if="listshwo==true">
					<view class="content">
						<text class="cuIcon-btn text-green"></text>
						<text class="text-grey">{{item.ks}}</text>
					</view>
					<view class="action">
						<button class="cu-btn round bg-green shadow">
							{{item.fen}}</button>
					</view>
				</view>
			</view>
		</mescroll-body>
		
		<view class="cu-bar tabbar bg-white shadow foot">
			<view class="action" @click="NavChange" data-cur="kslist">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/icon/kslist' + [PageCur=='kslist'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='kslist'?'text-green':'text-gray'">考试列表</view>
			</view>
			
			<view class="action" @click="NavChange" data-cur="grzx">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/icon/grzx' + [PageCur == 'grzx'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='grzx'?'text-green':'text-gray'">个人中心</view>
			</view>
		</view>

	</view>
</template>

<script>
	import MescrollMixin from "../../components/mescroll-uni/mescroll-mixins.js";
	import MescrollBody from "../../components/mescroll-uni/mescroll-body.vue"; // 注意.vue后缀不能省
	export default {
		mixins: [MescrollMixin], // 使用mixin
		components: {
			MescrollBody
		},
		data() {
			return {
				dataList: [],
				listshwo: false,
				PageCur: '',
				mescroll: null,
				upOption: {
					page: {
						size: 10 // 每页数据的数量,默认10
					},
					noMoreSize: 5, // 配置列表的总数量要大于等于5条才显示'-- END --'的提示
					empty: {
						tip: '暂无相关数据'
					}
				},
			}
		},
		methods: {
			downCallback() {
				this.mescroll.endSuccess();
			},
			upCallback(page) {
				let pageNum = page.num; // 页码, 默认从1开始
				let pageSize = page.size; // 页长, 默认每页10条
				var that = this;
				const urlBase = this.Common.urlBase;
				var access_token = uni.getStorageSync('access_token');
				uni.request({
					url: urlBase + 'api/ExaminationSetupWithAcApi/ExaminationRecord?pageNum=' + pageNum + '&pageSize=' + pageSize,
					header: {
						Authorization: 'Bearer ' + access_token
					},
					success: (request) => {
						// 接口返回的当前页数据列表 (数组)
						let curPageData = request.data.data;
						// 接口返回的当前页数据长度 (如列表有26个数据,当前页返回8个,则curPageLen=8)
						let curPageLen = curPageData.length;
						// 接口返回的总页数 (如列表有26个数据,每页10条,共3页; 则totalPage=3)
						let totalPage = request.data.totalPage;
						// 接口返回的总数据量(如列表有26个数据,每页10条,共3页; 则totalSize=26)
						let totalSize = request.data.totalSize;
						// 接口返回的是否有下一页 (true/false)
						let hasNext = request.data.hasNext;
						//设置列表数据
						if (page.num == 1) that.dataList = []; //如果是第一页需手动置空列表
						that.dataList = that.dataList.concat(curPageData); //追加新数据	

						// 请求成功,隐藏加载状态
						//方法一(推荐): 后台接口有返回列表的总页数 totalPage
						that.mescroll.endByPage(curPageLen, totalPage);
						if (curPageLen > 0) {
							that.listshwo = true;
						}						
						setTimeout(() => {
							that.mescroll.endSuccess(curPageLen)
						}, 20);
					},
					fail: () => {
						//  请求失败,隐藏加载状态
						that.mescroll.endErr()
					}
				})
			},
			NavChange: function(e) {
				this.PageCur = e.currentTarget.dataset.cur;
				if(this.PageCur=='kslist'){
					uni.navigateTo({
					    url: '../withAccount/testlist'
					});
				}else{
					uni.navigateTo({
					    url: '../withAccount/userCentre'
					});
				}				
			},
			getcertificate(e){
				var item = e.currentTarget.dataset.item;
				var data = JSON.parse(item);						
				uni.setStorageSync('acid', data.acid);		
				const userinfo = {
					examinationsetupid : data.acid,
					username: data.xm,
					unitwork: data.dw,
					phone: ''
				};
				uni.setStorageSync('userinfo', userinfo);							
				uni.navigateTo({
					url: "../public/resultview?fen=" + data.fen
				});		
			},
		}
	}
</script>

<style>

</style>

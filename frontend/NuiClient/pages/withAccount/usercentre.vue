<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" :isBack="true">
			<block slot="backText">返回</block>
		<block slot="content">个人中心</block>
		</cu-custom>
				
		<view class="cu-list menu-avatar">
			<view class="cu-item">
				<view class="cu-avatar round lg" style="background-image:url(https://ossweb-img.qq.com/images/lol/web201310/skin/big10001.jpg);"></view>
				<view class="content">
					<view class="text-grey">用户一</view>
					<view class="text-gray text-sm flex">
						<view class="text-cut">
							
							
						</view>
					</view>
				</view>
				
			</view>		
		</view>
						
		<view class="cu-list menu" >			
			<view class="cu-item"  data-ac="xgmm" @click="gotopage">
				<view class="content">
					<image src="/static/icon/xgmm.png" class="png" mode="aspectFit"></image>
					<text class="text-grey">修改密码</text>
				</view>
			</view>
			<view class="cu-item" data-ac="cjcx" @click="gotopage">
				<view class="content">
					<image src="/static/icon/wdcj.png" class="png" mode="aspectFit"></image>
					<text class="text-grey">成绩查询</text>
				</view>
			</view>
			<view class="cu-item" data-ac="ctcx" @click="gotopage">
				<view class="content">
					<image src="/static/icon/ctk.png" class="png" mode="aspectFit"></image>
					<text class="text-grey">错题查看</text>
				</view>
			</view>			
		</view>
		
		
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
	export default {		
		onLoad() {
			this.Common.chkLogin();					
		},	
		data() {
			return {
				PageCur: 'grzx',				
			}
		},
		methods: {
			NavChange: function(e) {
				this.PageCur = e.currentTarget.dataset.cur;
				if(this.PageCur=='kslist'){
					uni.navigateTo({
					    url: '../withAccount/testlist'
					});
				}else{
					uni.navigateTo({
					    url: '../withAccount/usercentre'
					});
				}				
			},
			gotopage(e) {
				 var ac = e.currentTarget.dataset.ac;
				 if(ac=='xgmm'){
					 uni.navigateTo({
					     url: '../withAccount/changepwd'
					 });
				 }else if(ac=='cjcx'){
					 uni.navigateTo({
					     url: '../withAccount/scorequery'
					 });
				 }else{
					 uni.navigateTo({
					     url: '../withAccount/wrongquestionrecord'
					 });
				 }
				
			},
		}
	}
</script>

<style>

</style>

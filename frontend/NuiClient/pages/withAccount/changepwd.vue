<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" :isBack="true">
			<block slot="backText">返回</block>
		<block slot="content">密码修改</block>
		</cu-custom>
		
		<view class="cu-form-group">
			<view class="title">旧 密 码</view>
			<input placeholder="您的旧密码" v-model="pwdold" type="password"></input>
		</view>
		
		<view class="cu-form-group">
			<view class="title">新 密 码</view>
			<input placeholder="您的新密码" v-model="pwdnew" type="password"></input>
		</view>
		
		
		<view class="cu-form-group">
			<view class="title">确认密码</view>
			<input placeholder="确认您的新密码" v-model="pwdnewc" type="password"></input>
		</view>
		
		<view class="padding flex flex-direction">		
			<button class="cu-btn block bg-blue margin-tb-sm lg" @click="cpwd">修改</button>
		</view>
		
		<view class="cu-modal" :class="modalName=='DialogModal1'?'show':''">
			<view class="cu-dialog">
				<view class="cu-bar bg-white justify-end">
					<view class="content">消息</view>
		
				</view>
				<view class="padding-xl">
					{{errmsg}}
				</view>
				<view class="cu-bar bg-white justify-end">
					<view class="action">
						<button v-if="btnchose==='close'" class="cu-btn bg-green margin-left" @tap="gotopage">好的</button>
						
					</view>
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
				pwdold:'',
				pwdnew:'',
				pwdnewc:'',
				modalName: null,
				errmsg: "",
				btnchose: "close",
				PageCur: '',
			}
		},
		methods: {
			cpwd(){
				if(this.pwdold.length <= 0 || this.pwdnew.length <= 0 || this.pwdnewc.length <= 0){
					this.errmsg = "旧密码，新密码，确认密码不能为空"
					this.showModal("Modal");
				}else{
					const urlBase = this.Common.urlBase;
					const access_token = uni.getStorageSync('access_token');
					var that = this;					
					var postdata = {
						pwdold:that.pwdold,
						pwdnew:that.pwdnew,
						pwdnewc:that.pwdnewc
					}
					uni.request({
						url: urlBase + 'api/AuthenticationApi/ChangePwd',
						header:{Authorization:'Bearer ' + access_token},
						data:JSON.stringify(postdata),								
						method: 'POST',
						success: (res) => {
							console.log(res)
							if (res.statusCode == 200) {
								if(res.data=='ok'){
								that.errmsg = "修改成功";
								that.showModal("DialogModal1");
								}else{
									uni.showToast({
										title: res.data,
										duration: 2000,
										icon: 'none',									
									});
								}								
							} else {
								var strtemp = "";
								for (var k in res.data) {
									strtemp += res.data[k] + ",";
								}
								that.errmsg = strtemp;
							}
						},
						fail: () => {
							uni.showToast({
							    title: '系统错误',
							    duration: 2000
							});
						}
					})
				}
			},
			showModal(name) {
				this.modalName = name
			},
			hideModal() {
				this.modalName = null
			},
			gotopage() {
				this.modalName = null;
				uni.navigateTo({
				    url: "../withAccount/testlist"
				});
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
		}
	}
</script>

<style>

</style>

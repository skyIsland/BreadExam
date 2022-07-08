<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue">
			<block slot="content">系统登录</block>
		</cu-custom>
	
		<view class="image-item">
			<view class="image-content">
				<image style="width: 100%; height: 200px; background-color: #eeeeee;" src="../../static/逢考必过.jpg"></image>
			</view>
		</view>

		<view class="cu-form-group">
			<view class="title">账 号</view>
			<input placeholder="请输入账号" v-model="name"></input>
		</view>

		<view class="cu-form-group">
			<view class="title">密 码</view>
			<input placeholder="请输入密码" v-model="pwd" type="password"></input>
		</view>



		<view class="padding flex flex-direction">

			<button class="cu-btn block bg-blue margin-tb-sm lg" @click="singup">登录</button>
		</view>
		
		

		<view class="cu-modal" :class="modalName=='Modal'?'show':''">
			<view class="cu-dialog">
				<view class="cu-bar bg-white justify-end">
					<view class="content">消息</view>
					<view class="action" @tap="hideModal">
						<text class="cuIcon-close text-red"></text>
					</view>
				</view>
				<view class="padding-xl">
					{{errmsg}}
				</view>
			</view>
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
						<button v-if="btnchose==='close'" class="cu-btn bg-green margin-left" @tap="hideModal">关闭</button>
						<button v-if="btnchose==='goto'" class="cu-btn bg-green margin-left" @tap="gotopage">好的</button>
					</view>
				</view>
			</view>
		</view>

		<view class="flex solid-bottom  justify-center">
			<view class="padding-sm margin-xs radius">
				<view class="cu-avatar xl round margin-left" style="background-image:url(../../static/WX.jpg);" ></view>
			</view>
			<view class="padding-sm margin-xs radius">
				<view class="cu-avatar xl round margin-left" style="background-image:url(../../static/QQ.jpg);"></view>
			</view>

			<view class="padding-sm margin-xs radius">
				<view class="cu-avatar xl round margin-left" style="background-image:url(../../static/GITHUB.jpg);" @click="GitHubLogin"></view>
			</view>
		</view>
		
		<view class="flex solid-bottom padding justify-end" >
			<view class="bg-red padding-sm margin-xs radius" @click="gotoreg">注册</view>			
		</view>


	</view>
</template>

<script>
	export default {
		data() {
			return {
				name: "",
				pwd: "",
				phone: "",
				modalName: null,
				errmsg: "",
				btnchose: "close"
			};
		},
		methods: {
			singup() {
				if (this.name.length <= 0 || this.pwd.length <= 0) {
					this.errmsg = "姓名，密码不能为空"
					this.showModal("Modal");
				} else {													
					const urlBase = this.Common.urlBase;
					var that = this;
					uni.request({
						url: urlBase + 'api/_Account/LoginJwt', 
						// header: {
						// 	"Content-Type": "application/x-www-form-urlencoded"
						// },
						data: {
							account: this.name,
							password: this.pwd						
						},
						method: 'POST',
						success: (res) => {
							if (res.statusCode == 200) {
								uni.setStorageSync('access_token', res.data.access_token);
								uni.setStorageSync('refresh_token', res.data.refresh_token);
								uni.showToast({
									title: '登录成功',
									duration: 2000,
									icon: 'none',
									success() {																	
										uni.navigateTo({
											url: '../withAccount/testlist'
										});
									}
								});
							}else if(res.statusCode == 400){
								uni.showToast({
									title: res.data,
									duration: 2000,
									icon: 'none'
								});
							} else {
								var strtemp = "";
								for (var k in res.data) {
									strtemp += res.data[k] + ",";
								}
								that.errmsg = strtemp;
							}
						}
					});
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
					url: "../noAccount/testview"
				})
			},
			gotoreg(){
				
				uni.navigateTo({
					url: "../withAccount/registered"
				})
			},
			GitHubLogin(){
				const urlBase = this.Common.urlBase;
				//window.location.href = 'https://github.com/login/oauth/authorize?client_id=d08c83abe14c9f4e20f8';
				window.location.href = urlBase + "ExternalLogin?provider=github&terminal=mobile";
			}

		}
	}
</script>

<style>
	.tower-swiper .tower-item {
		transform: scale(calc(0.5 + var(--index) / 10));
		margin-left: calc(var(--left) * 100upx - 150upx);
		z-index: var(--index);
	}
</style>

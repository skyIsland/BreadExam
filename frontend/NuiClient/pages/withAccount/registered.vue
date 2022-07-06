<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" :isBack="true"><block slot="backText">返回</block><block slot="content">
		用户注册</block></cu-custom>
		
		<view class="cu-form-group margin-top">
			<view class="title">单 位</view>
			<picker @change="PickerChange" :value="index" :range="picker">
				<view class="picker">
					{{index>-1?picker[index]:'请选择单位'}}
				</view>
			</picker>
		</view>
		
		<view class="cu-form-group">
			<view class="title">账 号</view>
			<input placeholder="请输入账号" v-model="itcode"></input>
		</view>
		
		<view class="cu-form-group">
			<view class="title">姓 名</view>
			<input placeholder="请输入姓名" v-model="username"></input>
		</view>
		
		
		<view class="cu-form-group">
			<view class="title">密 码</view>
			<input placeholder="请输入密码" v-model="pwd" type="password"></input>
		</view>
		
		
		<view class="cu-form-group">
			<view class="title">确认密码</view>
			<input placeholder="请输入密码" v-model="pwdc" type="password"></input>
		</view>
		
		<view class="padding flex flex-direction">
		
			<button class="cu-btn block bg-blue margin-tb-sm lg" @click="reg">注册</button>
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
		
		
		<view class="cu-bar tabbar bg-white shadow foot">
			<view class="action" @click="NavChange" data-cur="kslist">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/icon/kslist' + [PageCur=='kslist'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='basics'?'text-green':'text-gray'">考试列表</view>
			</view>
			
			<view class="action" @click="NavChange" data-cur="grzx">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/icon/grzx' + [PageCur == 'grzx'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='plugin'?'text-green':'text-gray'"></view>
			</view>
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
		
	
		
		
	</view>
</template>

<script>
	export default {
		created() {
			const urlBase = this.Common.urlBase;
			uni.request({
				url: urlBase + 'api/PublicInformationApi/UnitWorkInFo', 
				method: 'GET',
				success: (res) => {
					for(var i=0;i<res.data.length;i++){
						this.picker.push(res.data[i].UnitWorkName)
					}
				}
				
			})
		},
		data() {
			return {
				index: -1,
				picker: [],
				itcode: "",
				username: "",
				pwd: "",
				pwdc: "",
				modalName: null,
				errmsg: "",
				btnchose: "close",
				PageCur: ''
			}
		},
		methods: {
			reg(){
				var unitwork = this.picker[this.index];			
				if (this.itcode.length <= 0 || this.username.length <= 0 || this.pwd.length <= 0 || this.pwdc.length <= 0 || unitwork.length<=0)  {
					this.errmsg = "单位，账号，姓名，密码不能为空"
					this.showModal("Modal");
				} else {													
					const urlBase = this.Common.urlBase;
					var that = this;
					var postdata = {
						   unitwork:unitwork,
							itcode: this.itcode,
							username: this.username,
							pwd: this.pwd,
							pwdc: this.pwdc
						};
					uni.request({
						url: urlBase + 'api/AuthenticationApi/Registered', 
						data:JSON.stringify(postdata),						
						method: 'POST',
						success: (res) => {
							if (res.statusCode == 200) {
								if(res.data=='ok'){
								that.errmsg = "注册成功";
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
					});
				}
			},
			
			PickerChange(e) {
				this.index = e.detail.value
			},
			showModal(name) {
				this.modalName = name
			},
			hideModal() {
				this.modalName = null
			},
			gotopage() {
				this.modalName = null;
				 uni.navigateBack();
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

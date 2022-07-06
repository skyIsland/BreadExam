<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" :isBack="true">
			<block slot="content">活动报名</block>
		</cu-custom>
		<view class="image-item">
			<view class="image-content">
				<image style="width: 100%; height: 200px; background-color: #eeeeee;" src="../../static/timg.jpg"></image>
			</view>
		</view>

		<view class="cu-form-group">
			<view class="title">姓 名</view>
			<input placeholder="请输入姓名" v-model="name"></input>
		</view>

		<view class="cu-form-group">
			<view class="title">单 位</view>
			<input placeholder="请输入单位" v-model="unit"></input>
		</view>

		<view class="cu-form-group">
			<view class="title">手 机</view>
			<input placeholder="请输入手机号" v-model="phone"></input>
			<view class="cu-capsule radius">
				<view class='cu-tag bg-blue '>
					+86
				</view>
				<view class="cu-tag line-blue">
					中国大陆
				</view>
			</view>
		</view>

		<view class="padding flex flex-direction">

			<button class="cu-btn block bg-blue margin-tb-sm lg" @click="singup">报名</button>
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



	</view>
</template>

<script>
	export default {
		data() {
			return {
				name: "",
				unit: "",
				phone: "",
				modalName: null,
				errmsg: "",
				acid: "",
				btnchose:"close"
			};
		},
		methods: {
			singup() {
				if (this.name.length <= 0 || this.unit.length <= 0 || this.phone.length <= 0) {
					this.errmsg = "姓名，单位，手机号码不能为空"
					this.showModal("Modal");
				} else {
					if (!(/^1[34578]\d{9}$/.test(this.phone))) {
						this.errmsg = "手机号码格式不对"
						this.showModal("Modal");
					} else {
						const acid = uni.getStorageSync('acid');
						const postdata = {
							examinationsetupid : acid,
							username: this.name,
							unitwork: this.unit,
							phone: this.phone
						};
						const urlBase = this.Common.urlBase;
						var that = this;
						uni.request({
							url: urlBase + 'api/ExaminationSetupNoAcApi/SingUp',
							data: JSON.stringify(postdata),
							method: 'POST',
							success: (res) => {
								if (res.statusCode == 200) {
									if (res.data == '您已参加过本场考试') {
										that.errmsg = res.data;
										that.showModal("DialogModal1");
										that.btnchose = "close";
									} else {
										that.errmsg = "报名成功";
										uni.setStorageSync('userinfo', postdata);
										uni.setStorageSync('rnaid', res.data);										
										that.showModal("DialogModal1");	
									    that.btnchose = "goto"
									}
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

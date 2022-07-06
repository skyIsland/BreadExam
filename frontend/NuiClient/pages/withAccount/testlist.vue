<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue">
			<block slot="content">欢迎参加考试</block>
		</cu-custom>
		<scroll-view class="page" >

			<view @tap="gettestpage" v-for="(item,index) in testlist" :data-item="item.id">
				<view class="cu-bar bg-white solid-bottom margin-top">
					<view class="action">
						<text class="cuIcon-infofill text-orange"></text> {{item.title}}
					</view>
				
				</view>
				<view class="cu-list menu">
					<view class="cu-item">
						<view class="content padding-tb-sm">
							<view>
								<text class="cuIcon-timefill text-blue margin-right-xs"></text>开始时间：{{item.strTime}}
							</view>
							<view>
								<text class="cuIcon-time text-blue margin-right-xs"></text>结束时间：{{item.endTime}}
							</view>
				
						</view>
						<view class="action">
							<button class="cu-btn bg-green shadow">考试时间{{item.testTime}}分钟</button>
						</view>
					</view>
				</view>
			</view>
			


		</scroll-view>

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
		data() {
			return {
				testlist: [],
				PageCur: 'kslist'
			};
		},
		onLoad() {
			const urlBase = this.Common.urlBase;
			this.Common.chkLogin();
			var access_token = uni.getStorageSync('access_token');
			var that = this;
			uni.request({
				url: urlBase + 'api/ExaminationSetupWithAcApi/GetUserInfo',
				header: {
					Authorization: 'Bearer ' + access_token
				},
				success: (res) => {
					const userinfo = {
						userid: res.data.ID,
						username: res.data.UserName,
						unitwork: res.data.UnitWork,
					};
					uni.setStorageSync('userinfo', userinfo);
				}
			});
			uni.request({
				url: urlBase + 'api/ExaminationSetupWithAcApi/AllAtctivitiesList',
				header: {
					Authorization: 'Bearer ' + access_token
				},
				success: (res) => {
					that.testlist = res.data
				}
			});
		},
		methods: {
			NavChange: function(e) {
				this.PageCur = e.currentTarget.dataset.cur;
				if (this.PageCur == 'kslist') {
					uni.navigateTo({
						url: '../withAccount/testlist'
					});
				} else {
					uni.navigateTo({
						url: '../withAccount/usercentre'
					});
				}
			},
			gettestpage(e) {
				const urlBase = this.Common.urlBase;
				var access_token = uni.getStorageSync('access_token');
				const id = e.currentTarget.dataset.item;
				uni.setStorageSync("acid", id);
				uni.request({
					url: urlBase + 'api/ExaminationSetupWithAcApi/PartExamination?id=' + id,
					header: {
						Authorization: 'Bearer ' + access_token
					},
					success: (res) => {
						if (res.statusCode === 200) {
							if (res.data === "您已参加过本场考试，感谢您的参与" || res.data === "系统错误") {
								uni.showModal({
									title: '提示',
									content: res.data,
									showCancel: false,
								});

							} else {
								uni.setStorageSync('rnaid', res.data);
								uni.navigateTo({
									url: "../withAccount/testview"
								})

							}
						} else {
							uni.showModal({
								title: '提示',
								content: '系统错误',
								showCancel: false,
							});
						}


					},
					fail() {
						uni.showModal({
							title: '提示',
							content: 系统错误,
							showCancel: false,
						});
					}

				});
			}
		}
	}
</script>

<style>
	.page {
		height: 100Vh;
		width: 100vw;
	}

	.page.show {
		overflow: hidden;
	}

	.switch-sex::after {
		content: "\e716";
	}

	.switch-sex::before {
		content: "\e7a9";
	}

	.switch-music::after {
		content: "\e66a";
	}

	.switch-music::before {
		content: "\e6db";
	}
</style>

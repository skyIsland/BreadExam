<template>
	<view>
		<scroll-view scroll-y class="page">

			<view style="margin-top: 200px;">
				<view class="image-content" style="text-align: center; width: 100%;">
					<image style="width: 200px; height: 200px; background-color: #eeeeee;margin-top:100;" src="../../static/刷新.png"></image>
				</view>
				<view class="solids-bottom  flex align-center">
					<view class="flex-sub text-center">
						<view class="solid-bottom text-xl padding">
							<text class="text-black text-bold">登录中......</text>
						</view>

					</view>
				</view>
			</view>


		</scroll-view>
	</view>
</template>

<script>
	export default {
		data() {
			return {

			}
		},
		onLoad(op) {
			console.log(op);
			const acc = op.acc;
			var that = this;

			uni.request({
				url: 'https://api.github.com/user?' + acc,
				success: (res) => {
					console.log(res.data);
					if (res.statusCode === 200) {
						let openid = res.data.login;
						that.autoLogin(openid);
					} else {
						uni.showModal({
							title: '提示',
							content: '网络问题，稍后重试',
							showCancel: false,
							success: function(res) {
								if (res.confirm) {
									uni.navigateTo({
										url: '../withAccount/Login'
									});
								}
							}
						});
					}



				},
				fail() {

					uni.showModal({
						title: '提示',
						content: '网络问题，稍后重试',
						showCancel: false,
						success: function(res) {
							if (res.confirm) {
								uni.navigateTo({
									url: '../withAccount/Login'
								});
							}
						}
					});
				}
			});

		},
		methods: {
			registered() {
				uni.navigateTo({
					url: '../withAccount/registered'
				})
			},
			autoLogin(id) {
				var that = this;
				console.log(id)
				const urlBase = this.Common.urlBase;
				uni.request({
					url: urlBase + "api/AuthenticationApi/AutoLogin?openid=" + id,
					method: "POST",
					success: (res) => {

						console.log(res.data);
						if (res.statusCode === 200) {
							let access_token = res.data.access_token;
							if (access_token === 'no') {
								that.registered();
							} else {
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
							}
						} else {
							uni.showToast({
								title: '登录失败',
								duration: 2000,
								icon: 'none',
								success() {
									uni.navigateTo({
										url: '../withAccount/Login'
									});
								}
							});
						}
					}
				});
			}
		}
	}
</script>

<style>

</style>

<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" :isBack="true">
			<block slot="content">参加考试</block>
		</cu-custom>
		<scroll-view scroll-y class="page">
			<test-page :acid="acid" :userpagetype="pagetyp" @func="getData" />
			<view class="padding flex flex-direction">

				<button class="cu-btn block bg-blue margin-tb-sm lg" @click="isEnd">交卷</button>
			</view>

		</scroll-view>

	</view>
</template>

<script>
	export default {
		data() {
			return {
				acid: "",
				useranswer: [],
				pagetyp: "考试",
			}
		},
		onLoad() {
			const acid = uni.getStorageSync('acid');
			this.acid = acid;
			const endtime = uni.getStorageSync('endtime');
			var that = this;
			setTimeout(function() {
				uni.showToast({
					title: '时间到自动交卷',
					duration: 2000,
					success: () => {
						that.isEnd();
					}
				});
			}, endtime);
		},
		methods: {
			getData(useranswer) {
				this.useranswer = useranswer;
			},
			isEnd() {
				const urlBase = this.Common.urlBase;
				const Acid = uni.getStorageSync('acid');
				const Jlid = uni.getStorageSync('rnaid');
				var postdata = {
					Acid: Acid,
					Jlid: Jlid,
					ExamineeAnswers: this.useranswer,
					Type: "非账号"
				}
				uni.request({
					url: urlBase + 'api/TestQuestionsApi/Fraction',
					data: JSON.stringify(postdata),
					method: 'POST',
					success: (res) => {
						if (res.statusCode == 200) {
							uni.navigateTo({
								url: "../public/resultview?fen=" + res.data
							});							
						} else {
							uni.showToast({
								title: '系统错误',
								duration: 2000
							});
						}
					}
				});
			},

		}
	}
</script>

<style>

</style>

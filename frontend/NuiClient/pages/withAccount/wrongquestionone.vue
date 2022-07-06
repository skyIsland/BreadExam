<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">错题学习</block>
		</cu-custom>


		<view v-if="tx == '单选'">
			<view class="cu-bar bg-white solid-bottom ">
				<view class="action">
					<text class="cuIcon-title text-blue"></text>
					{{ questions.QuestionText }}
				</view>
			</view>
			<br />
			<view class="padding bg-white">
				<radio-group class="block">
					<view class="cu-form-group margin-top">
						<view class="title">A、{{ questions.OptionA }}</view>
					</view>
					<view class="cu-form-group margin-top" :id="'id' + currentPage">
						<view class="title">B、{{ questions.OptionB }}</view>
					</view>
					<view class="cu-form-group margin-top" :id="'id' + currentPage">
						<view class="title">C、{{ questions.OptionC }}</view>
					</view>
					<view class="cu-form-group margin-top" :id="'id' + currentPage">
						<view class="title">D、{{ questions.OptionD }}</view>
					</view>
				</radio-group>
			</view>
		</view>

		<!--多选-->

		<view v-if="tx == '多选'">
			<view class="cu-bar bg-white solid-bottom ">
				<view class="action">
					<text class="cuIcon-title text-blue"></text>
					{{ questions.QuestionText }}
				</view>
			</view>
			<br />
			<view class="padding bg-white">
				<checkbox-group class="block" >
					<view class="cu-form-group margin-top">
						<view class="title">A、{{ questions.OptionA }}</view>

					</view>
					<view class="cu-form-group margin-top">
						<view class="title">B、{{ questions.OptionB }}</view>

					</view>
					<view class="cu-form-group margin-top">
						<view class="title">C、{{ questions.OptionC }}</view>

					</view>
					<view class="cu-form-group margin-top">
						<view class="title">D、{{ questions.OptionD }}</view>

					</view>
				</checkbox-group>
			</view>
		</view>
		<!--判断-->
		<view v-if="tx == '判断'">
			<view class="cu-bar bg-white solid-bottom ">
				<view class="action">
					<text class="cuIcon-title text-blue"></text>
					{{ questions.QuestionText }}
				</view>
			</view>
			<br />
			<view class="padding bg-white">
				<radio-group class="block" >
					<view class="cu-form-group margin-top">
						<view class="title">A、{{ questions.OptionA }}</view>

					</view>
					<view class="cu-form-group margin-top">
						<view class="title">B、{{ questions.OptionB }}</view>

					</view>
				</radio-group>
			</view>
		</view>




		<view class="padding-xl">
			<view class="flex  justify-around">
				<view class=" padding-sm margin-xs radius">
					<button class="cu-btn bg-green shadow-blur round">
						正确答案：{{ questions.Anwser }}
					</button>
				</view>

			</view>
		</view>

		<view class="cu-bar bg-white solid-bottom ">
			<view class="action">

				解析：{{ questions.Pars}}
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
		onLoad(options) {
			let id = options.id;

			const urlBase = this.Common.urlBase;
			var that = this;
			uni.request({
				url: urlBase + 'api/TestQuestionsApi/GetOneQuestion?id=' + id,
				method: 'GET',
				success: (request) => {
					console.log(request)
					if (request.statusCode === 200) {
						that.questions = request.data;
						that.tx = request.data.QuestionType;
					} else {
						uni.showModal({
							title: '提示',
							content: '题目不存在或被删除',
							showCancel: false,
							success: function(res) {
								if (res.confirm) {
									uni.navigateTo({
										url: '../withAccount/wrongquestionrecord'
									});
								}
							}
						});
					}
				},
				fail: () => {
					uni.showModal({
						title: '提示',
						content: '题目不存在或被删除',
						showCancel: false,
						success: function(res) {
							if (res.confirm) {
								uni.navigateTo({
									url: '../withAccount/wrongquestionrecord'
								});
							}
						}
					});
				}
			})

		},
		data() {
			return {
				questions: null,
				tx: '',
				PageCur: '',
			}
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
		}
	}
</script>

<style>

</style>

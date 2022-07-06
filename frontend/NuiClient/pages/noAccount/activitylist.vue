<template>
	<view>
		<cu-custom bgColor="bg-gradual-blue" ><block slot="content">欢迎参加活动</block></cu-custom>
		<scroll-view  class="page" @tap="gettestpage" v-for="(item,index) in testlist" :data-item="item.id">
							
				<view class="cu-bar bg-white solid-bottom margin-top">
					<view class="action">
						<text class="cuIcon-infofill text-orange"></text> {{item.title}}
					</view>
				
				</view>
				<view class="cu-list menu" >
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
			
			
		</scroll-view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
			   testlist:[]
			};
		},
		onLoad() {
			const urlBase = this.Common.urlBase;
			var that = this;
			uni.request({
			    url: urlBase + 'api/ExaminationSetupNoAcApi/AllAtctivitiesList', 
			    success: (res) => {		
			         that.testlist = res.data
			    }
			});
		},
		methods: {			
			gettestpage(e){
				const id = e.currentTarget.dataset.item;
				uni.setStorage({
				    key: 'acid',
				    data: id,
					success: function () {
					        uni.navigateTo({
					        	url: "../noAccount/signup"
					        })
					    }
				});
					
			console.log()
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

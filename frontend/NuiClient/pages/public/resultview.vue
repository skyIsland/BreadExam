<template>
	<view>

		<cu-custom bgColor="bg-gradual-blue">
			<block slot="content">考试结果</block>
		</cu-custom>
		<scroll-view scroll-y class="page">

			<view class="solids-bottom  flex align-center">

				<view class="flex-sub text-center">
					<view class="solid-bottom text-xl padding">
						<text class="text-black text-bold">{{useronfo.unitwork}}:{{useronfo.username}}</text>
					</view>
					<view class="padding text-black text-bold">考试结果：{{fen}}分</view>
				</view>
			</view>

			<view class="image-content" style="text-align: center; width: 100%;">
				<image v-if="imgcalculation==='wtg'" style="width: 200px; height: 200px; background-color: #eeeeee;margin-top:100;"
				 src="../../static/wtg.png"></image>
				<!-- <image v-if="imgcalculation==='tg'" style="width:85%;height:400px; #eeeeee;" :src="imgdata"></image> -->
				<image v-if="imgcalculation==='tg'" style="width: 200px; height: 200px; background-color: #eeeeee;margin-top:100;"
				 src="../../static/tg.png"></image>
			</view>

			<view class="padding flex flex-direction" v-if="imgcalculation==='tg'">
				<button class="cu-btn block bg-blue margin-tb-sm lg" @click="upLoad">下载证书</button>
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
				fen: 0,
				useronfo: [],
				imgcalculation: "",
				imgdata: "",
				PageCur: '',
			}
		},
		onLoad(options) {
			this.fen = options.fen;
			this.useronfo = uni.getStorageSync('userinfo');
			if (this.fen >= 60) {
				this.imgcalculation = "tg";
				const urlBase = this.Common.urlBase;
				const acid = uni.getStorageSync("acid")
			    const imgsrc = urlBase + 'api/TestQuestionsApi/Certificate?id=' + acid + '&&username=' + this.useronfo.username + '&&unitwork=' + this.useronfo.unitwork + '&&fen=' + this.fen;
				var that = this;
				uni.request({
					url: imgsrc,
					responseType: "arraybuffer",
					success: (response) => {
						console.log(response)
						that.imgdata = 'data:image/png;base64,' + btoa(
							new Uint8Array(response.data).reduce((data, byte) => data + String.fromCharCode(byte), '')
						);
					}
				});

				

			} else {
				this.imgcalculation = "wtg";
			}

		},
		methods: {
			upLoad(){
				const urlBase = this.Common.urlBase;
				var username = this.useronfo.username;
				var unitwork = this.useronfo.unitwork;								
				this.downloadFile(unitwork+':' + username + '.JPG', this.imgdata);								
			},
			downloadFile(fileName, content){
				let aLink = document.createElement('a');
				        let blob = this.base64ToBlob(content); 				 
				        let evt = document.createEvent("HTMLEvents");
				        evt.initEvent("click", true, true);
				        aLink.download = fileName;
				        aLink.href = URL.createObjectURL(blob);
				        aLink.click()
			},
			base64ToBlob(code) {
				let parts = code.split(';base64,');
				        let contentType = parts[0].split(':')[1];
				        let raw = window.atob(parts[1]);
				        let rawLength = raw.length;				 
				        let uInt8Array = new Uint8Array(rawLength);				 
				        for (let i = 0; i < rawLength; ++i) {
				          uInt8Array[i] = raw.charCodeAt(i);
				        }
			    return new Blob([uInt8Array], {type: contentType});	
			},
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
			
		}

	}
</script>

<style>

</style>

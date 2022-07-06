<template>
	<view>


		<view class="padding bg-white ">
			<view class="flex">
				<view class="cu-progress round">
					<view class="bg-green" :style="[{ width: loading ? percentage : '' }]"></view>
				</view>
				<text class="cuIcon-qrcode text-green margin-left-sm" @click="shwotestnav"></text>
			</view>
		</view>

		<div>

			<view class="flex  justify-center">
				<view class="padding-sm">
					<view class="cu-tag bg-blue">{{minute}}</view>
					<view class="cu-tag bg-orange">{{second}}</view>
				</view>
			</view>
		</div>


		<!--单选-->
		<view v-if="tx == '单选'">
			<view class="cu-bar bg-white solid-bottom ">
				<view class="action">
					<text class="cuIcon-title text-blue"></text>
					{{ currentPage + 1 }}、{{ questions[currentPage].QuestionText }}
				</view>
			</view>
			<br />
			<view class="padding bg-white">
				<radio-group class="block" @change="RadioChange">
					<view class="cu-form-group margin-top">
						<view class="title">A、{{ questions[currentPage].OptionA }}</view>
						<radio :checked="useranswer[currentPage] === 'A' ? true : false" value="A"></radio>
					</view>

					<view class="cu-form-group margin-top" :id="'id' + currentPage">
						<view class="title">B、{{ questions[currentPage].OptionB }}</view>
						<radio :checked="useranswer[currentPage] === 'B'? true : false" value="B"></radio>
					</view>

					<view class="cu-form-group margin-top" :id="'id' + currentPage">
						<view class="title">C、{{ questions[currentPage].OptionC }}</view>
						<radio :checked="useranswer[currentPage] === 'C'? true : false" value="C"></radio>
					</view>

					<view class="cu-form-group margin-top" :id="'id' + currentPage">
						<view class="title">D、{{ questions[currentPage].OptionD }}</view>
						<radio :checked="useranswer[currentPage] === 'D'? true : false" value="D"></radio>
					</view>
				</radio-group>
			</view>
		</view>

		<!--多选-->

		<view v-if="tx == '多选'">
			<view class="cu-bar bg-white solid-bottom ">
				<view class="action">
					<text class="cuIcon-title text-blue"></text>
					{{ currentPage + 1 }}、{{ questions[currentPage].QuestionText }}
				</view>
			</view>
			<br />
			<view class="padding bg-white">
				<checkbox-group class="block" @change="CheckboxChange">
					<view class="cu-form-group margin-top">
						<view class="title">A、{{ questions[currentPage].OptionA }}</view>
						<checkbox :class="useranswer[currentPage].indexOf('A')!=-1?'checked':''" :checked="useranswer[currentPage].indexOf('A')!=-1?true:false"
						 value="A"></checkbox>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">B、{{ questions[currentPage].OptionB }}</view>
						<checkbox :class="useranswer[currentPage].indexOf('B')!=-1?'checked':''" :checked="useranswer[currentPage].indexOf('B')!=-1?true:false"
						 value="B"></checkbox>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">C、{{ questions[currentPage].OptionC }}</view>
						<checkbox :class="useranswer[currentPage].indexOf('C')!=-1?'checked':''" :checked="useranswer[currentPage].indexOf('C')!=-1?true:false"
						 value="C"></checkbox>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">D、{{ questions[currentPage].OptionD }}</view>
						<checkbox :class="useranswer[currentPage].indexOf('D')!=-1?'checked':''" :checked="useranswer[currentPage].indexOf('D')!=-1"
						 value="D"></checkbox>
					</view>
				</checkbox-group>
			</view>
		</view>
		<!--判断-->
		<view v-if="tx == '判断'">
			<view class="cu-bar bg-white solid-bottom ">
				<view class="action">
					<text class="cuIcon-title text-blue"></text>
					{{ currentPage + 1 }}、{{ questions[currentPage].QuestionText }}
				</view>
			</view>
			<br />
			<view class="padding bg-white">
				<radio-group class="block" @change="JudgeChange">
					<view class="cu-form-group margin-top">
						<view class="title">A、{{ questions[currentPage].OptionA }}</view>
						<radio :checked="useranswer[currentPage] === '对' ? true : false" value="对"></radio>
					</view>

					<view class="cu-form-group margin-top">
						<view class="title">B、{{ questions[currentPage].OptionB }}</view>
						<radio :checked="useranswer[currentPage] === '错' ? true : false" value="错"></radio>
					</view>
				</radio-group>
			</view>
		</view>
		<br />

		<view class="flex solid-bottom  justify-around">
			<view class=" padding-sm margin-xs radius"><button class="cu-btn bg-green shadow-blur round" @click="topic">上一题</button></view>
			<view class=" padding-sm margin-xs radius">
				<button v-if="pagetype==='考试'" class="cu-btn bg-orange shadow-blur round" @click="mark">标记</button>
				<button v-if="pagetype==='练习'" class="cu-btn bg-orange shadow-blur round" @click="practice">查看解析</button>
			</view>
			<view class=" padding-sm margin-xs radius"><button class="cu-btn bg-blue shadow-blur round" @click="next">下一题</button></view>
		</view>



		<!--做题进度-->
		<view class="cu-modal" :class="modalName == 'Modal' ? 'show' : ''">
			<view class="cu-dialog">
				<view class="cu-bar bg-white justify-end">
					<view class="content">题目导航</view>
					<view class="action" @tap="hideModal"><text class="cuIcon-close text-red"></text></view>
				</view>
				<view class="padding-xl">
					<view class="padding-sm flex flex-wrap">
						<view class="padding-xs">
							<view class="cu-tag bg-green">未答</view>
							<view class="cu-tag bg-blue">已答</view>
							<view class="cu-tag bg-orange">标记</view>
						</view>
					</view>

					<view class="cu-bar bg-white margin-top">
						<view class="action">
							<text class="cuIcon-title text-blue"></text>
							单选
						</view>
					</view>
					<view class="padding-sm flex flex-wrap">
						<view class="padding-xs" v-for="(item, index) in listprogressdx" :key="index" @click=gotoq(item.id)>
							<view :class="[item.istest === true ? 'cu-tag bg-blue' : 'cu-tag bg-green']">{{ item.id + 1 }}</view>
						</view>
					</view>

					<view class="cu-bar bg-white margin-top">
						<view class="action">
							<text class="cuIcon-title text-blue"></text>
							判断
						</view>
					</view>
					<view class="padding-sm flex flex-wrap">
						<view class="padding-xs" v-for="(item, index) in listprogresspd" :key="index" @click=gotoq(item.id)>
							<view :class="[item.istest === true ? 'cu-tag bg-blue' : 'cu-tag bg-green']">{{ item.id + 1 }}</view>
						</view>
					</view>

					<view class="cu-bar bg-white margin-top">
						<view class="action">
							<text class="cuIcon-title text-blue"></text>
							多选
						</view>
					</view>
					<view class="padding-sm flex flex-wrap">
						<view class="padding-xs" v-for="(item, index) in listprogressddx" :key="index" @click=gotoq(item.id)>
							<view :class="[item.istest === true ? 'cu-tag bg-blue' : 'cu-tag bg-green']">{{ item.id + 1 }}</view>
						</view>
					</view>


					<view class="cu-bar bg-white margin-top">
						<view class="action">
							<text class="cuIcon-title text-blue"></text>
							标记
						</view>
					</view>
					<view class="padding-sm flex flex-wrap">
						<view class="padding-xs" v-for="(item, index) in marklist" :key="index" @click=gotoq(item.id)>
							<view class="cu-tag bg-orange">{{ item.id + 1 }}</view>
						</view>
					</view>

				</view>

				<view class="cu-bar bg-white justify-end">
					<view class="action"><button class="cu-btn bg-green margin-left" @tap="hideModal">关闭</button></view>
				</view>
			</view>
		</view>

		<!--查看解析-->
		<view class="cu-modal bottom-modal" :class="modalName=='bottomModal'?'show':''">
			<view class="cu-dialog">
				<view class="cu-bar bg-white">
					<view class="action text-green" @tap="hideModal">收藏</view>
					<view class="action text-blue" @tap="hideModal">关闭</view>
				</view>
				<view class="padding-xl">
					<view class="flex  justify-around">
						<view class=" padding-sm margin-xs radius">
							<button class="cu-btn bg-green shadow-blur round">
								正确答案：{{ questions[currentPage].Anwser }}
							</button>
						</view>
						<view class=" padding-sm margin-xs radius"><button class="cu-btn bg-blue shadow-blur round">
								你的答案：{{ useranswer[currentPage] }}
							</button>
						</view>
					</view>
				</view>

				<view class="cu-bar bg-white ">
					<view class="action">
						<text class="cuIcon-title text-orange "></text>
						{{questions[currentPage].Pars}}
					</view>
					<br>
				</view>

			</view>
		</view>

	</view>
</template>

<script>
	export default {
		name: "testpage",
		props: {
			acid: {
				type: String,
				default: ''
			},
			userpagetype: {
				type: String,
				default: '',
				//类型支持 非账号 考试 和 练习
			},
		},
		data() {
			return {
				minutes: 10,
				seconds: 0,
				loading: true,
				modalName: '',
				active: false,
				listprogressdx: [],
				listprogressddx: [],
				listprogresspd: [],
				questions: [],
				tx: '',
				currentPage: 0,
				percentage: '',
				allcount: 0,
				useranswer: [],
				marklist: [],
				pagetype: "练习",
			};
		},
		mounted() {
			this.add();
		},
		created() {
			const urlBase = this.Common.urlBase;
			var that = this;
			const Jlid = uni.getStorageSync('rnaid');
			that.pagetype = that.userpagetype;
			uni.request({
				url: urlBase + 'api/TestQuestionsApi/GetTestPaper?acid=' + that.acid + '&&jlid='+ Jlid + '&&type=' + this.userpagetype ,
				success: res => {
				    console.log(res.data)
                    uni.setStorageSync('endtime', res.data.jssj);
					that.minutes = res.data.sj;					
					that.questions = res.data.questions;
					that.allcount = that.questions.length;
					that.tx = res.data.questions[that.currentPage].QuestionType;
					for (let i = 0; i < that.questions.length; i++) {
						var progress = {
							id: i,
							istest: false,
							type: that.questions[i].QuestionType
						};
						var an = "未做";
						that.useranswer.push(an);
						if (progress.type === '单选') {
							that.listprogressdx.push(progress);
						} else if (progress.type === '多选') {
							that.listprogressddx.push(progress);
						} else {
							that.listprogresspd.push(progress);
						}
					}
					setTimeout(function(){
						uni.showToast({
						    title: '时间剩余5分钟',
						    duration:2000
						});
					}, res.data.txsj);
				},
			}); 
		    
			

		},
		methods: {
			RadioChange(e) {
				var radio = e.detail.value;
				this.$set(this.useranswer, this.currentPage, radio);
				this.listprogressdx[this.currentPage].istest = true;
				this.changgeprogress();
				this.returndata();

			},
			CheckboxChange(e) {
				var values = e.detail.value;
				const chk = values.join('');
				this.$set(this.useranswer, this.currentPage, chk);
				this.listprogressddx[this.currentPage - this.listprogressdx.length - this.listprogresspd.length].istest = true;
				this.changgeprogress();
				this.returndata();
			},
			JudgeChange(e) {
				var radio = e.detail.value;
				this.$set(this.useranswer, this.currentPage, radio);
				this.listprogresspd[this.currentPage - this.listprogressdx.length].istest = true;
				this.changgeprogress();
				this.returndata();
			},
			shwotestnav() {
				this.showModal('Modal');
			},
			showModal(name) {
				this.modalName = name;
			},
			hideModal() {
				this.modalName = null;
			},
			topic() {
				if (this.currentPage === 0) {
					alert('这是第一题');
					return;
				} else {
					this.currentPage--;
					this.tx = this.questions[this.currentPage].QuestionType;

				}
			},
			next() {
				if (this.currentPage === this.questions.length - 1) {
					alert('这是最后一题');
					return;
				} else {
					this.currentPage++;
					this.tx = this.questions[this.currentPage].QuestionType;

				}
			},
			mark() {
				var progress = {
					id: this.currentPage,
					ischoos: true,
					type: this.tx
				};
				this.marklist.push(progress);
				alert("标记成功");
			},
			testq() {
				if (this.tx === '单选') {
					this.listprogressdx[this.currentPage].istest = true;
				} else if (tx === '多选') {
					this.listprogressddx[this.currentPage - this.listprogresspd.length - this.listprogressddx.length].istest = true;
				} else {
					this.listprogresspd[this.currentPage - 10].istest = true;
				}
			},
			gotoq(id) {
				const ids = id;
				this.currentPage = ids;
				this.tx = this.questions[ids].QuestionType;
				this.hideModal();
			},
			changgeprogress() {
				var xarr = this.useranswer.filter(item => item === '未做');
				var userprocesscount = this.allcount - xarr.length;
				this.percentage = userprocesscount / this.allcount * 100 + '%';
			},
			returndata() {
				this.$emit('func', this.useranswer);
			},
			num: function(n) {
				return n < 10 ? '0' + n : '' + n
			},
			add() {
				var _this = this
				var time = window.setInterval(function() {
					if (_this.seconds === 0 && _this.minutes !== 0) {
						_this.seconds = 59
						_this.minutes -= 1
					} else if (_this.minutes === 0 && _this.seconds === 0) {
						_this.seconds = 0
						window.clearInterval(time)

					} else {
						_this.seconds -= 1
					}
				}, 1000)
			},
			practice() {
				this.modalName = "bottomModal";
			},

		},
		watch: {
			second: {
				handler(newVal) {
					this.num(newVal)
				}
			},
			minute: {
				handler(newVal) {
					this.num(newVal)
				}
			},
		},
		computed: {
			second: function() {
				return this.num(this.seconds)
			},
			minute: function() {
				return this.num(this.minutes)
			}
		},
	};
</script>

<style></style>

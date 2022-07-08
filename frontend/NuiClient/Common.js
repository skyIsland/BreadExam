
const urlBase = 'http://localhost:8931/';
function chkLogin() {

	var access_token = uni.getStorageSync('access_token');
	if (access_token) {
		retoken();
	} else {
		uni.navigateTo({
			url: '../withAccount/Login'
		});
	}
}
function retoken() {
	var access_token = uni.getStorageSync('access_token');
	var refresh_token = uni.getStorageSync('refresh_token');
	uni.request({
		url: urlBase + 'api/_login/RefreshToken?refreshToken=' + refresh_token,
		header: {
			'Authorization': 'Bearer ' + access_token
		},
		method: "POST",
		success: (res) => {
			if (res.statusCode === 200) {
				uni.setStorageSync('access_token', res.data.access_token);
				uni.setStorageSync('refresh_token', res.data.refresh_token);
			} else {
				uni.navigateTo({
					url: '../withAccount/Login'
				});
			}
		},
		fail() {
			uni.navigateTo({
				url: '../withAccount/Login'
			});
		}
	});
}

export default {
	urlBase,
	chkLogin
}


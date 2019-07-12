export const getWelcomeSpeech = () => {
    let message = '';
    let emotion = null;
    let now = new Date();
    let hour = now.getHours()
    if (hour < 6) {
        message = "凌晨好";
        emotion = 'icon-siliao';
    }
    else if (hour < 9) {
        message = "早上好";
        emotion = 'icon-shuixing';
    }
    else if (hour < 12) {
        message = "上午好";
        emotion = 'icon-mengbi';
    }
    else if (hour < 14) {
        message = "中午好";
        emotion = 'icon-lengku';
    }
    else if (hour < 17) {
        message = "下午好";
        emotion = 'icon-xiaolian';
    }
    else if (hour < 19) {
        message = "傍晚好";
        emotion = 'icon-kaixin';
    }
    else {
        message = "晚上好";
        emotion = 'icon-shuizhao';
    }
    return { message, emotion }
}
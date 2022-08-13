function doFirst() {
    // 先跟HTML畫面產生關聯，再建事件聆聽功能
    let canvas = document.getElementById('canvas');
    let context = canvas.getContext('2d');

    // context.fillStyle = 'red';
    // context.strokeStyle = 'blue';

    //sans-serif 有襯線
    //serif 無襯線    

    // context.textBaseline='top | hanging | middle | alphabetic | ideographic | bottom';

    context.textBaseline = 'alphabetic'; //100,100
    context.font = 'bold 50px Tahoma';

    // 第一個字
    context.fillText('omgRen', 100, 100);

    context.moveTo(100, 100);
    context.lineTo(400, 100);
    context.stroke();

    //第二個字
    context.shadowColor = 'red';
    context.shadowOffsetX = 5;
    context.shadowOffsetY = 5;
    context.shadowBlur = 5;

    context.fillText('omgRen', 100, 250);

    //第三個字
    context.fillStyle = '#fff';
    context.shadowOffsetX = 5;
    context.shadowOffsetY = 5;
    context.shadowBlur = 10;

    context.fillText('omgRen', 100, 400);

    //第四個字 -- 多重陰影
    context.fillText('omgRen', 100, 550);

    context.shadowColor = 'blue';
    context.fillText('omgRen', 100, 550);


}
window.addEventListener('load', doFirst)

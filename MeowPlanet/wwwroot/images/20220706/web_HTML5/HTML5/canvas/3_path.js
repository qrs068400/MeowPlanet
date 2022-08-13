function doFirst() {
    // 先跟HTML畫面產生關聯，再建事件聆聽功能
    let canvas = document.getElementById('canvas');
    let context = canvas.getContext('2d');

    context.fillStyle = 'red';
    context.strokeStyle = 'blue';
    context.lineWidth = 5;


    context.moveTo(100, 100);
    context.lineTo(250, 250);
    context.lineTo(400, 50);
    context.lineTo(50, 200);
    context.closePath();

    context.fill();
    context.stroke();








}
window.addEventListener('load', doFirst)

$(document).ready(function () {
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
});

// 畫面切換
$('#f1-b1').click(function () {
    $('#p1').css('display', 'none');
    $('#p2').css('display', 'block');
    $('#f1').css('display', 'none');
    $('#f2').css('display', 'block');
})


$('#f2-b4').click(function () {
    $('#p2').css('display', 'none');
    $('#p3').css('display', 'block');
    $('#f2').css('display', 'none');
    $('#f3').css('display', 'block');
})

$('#f2-b3').click(function () {
    $('#p1').css('display', 'block');
    $('#p2').css('display', 'none');
    $('#f1').css('display', 'block');
    $('#f2').css('display', 'none');
})

$('#f3-b1').click(function () {
    $('#p3').css('display', 'none');
    $('#p2').css('display', 'block');
    $('#f3').css('display', 'none');
    $('#f2').css('display', 'block');
})

$('#f3-b2').click(function () {
    $('#p3').css('display', 'none');
    $('#p4').css('display', 'block');
    $('#f3').css('display', 'none');
    $('#f4').css('display', 'block');
})

$('#f4-b1').click(function () {
    $('#p4').css('display', 'none');
    $('#p3').css('display', 'block');
    $('#f4').css('display', 'none');
    $('#f3').css('display', 'block');
})

$('#f4-b2').click(function () {
    $('#p4').css('display', 'none');
    $('#p5').css('display', 'block');
    $('#f4').css('display', 'none');
    $('#f5').css('display', 'block');
})

$('#f5-b1').click(function () {
    $('#p5').css('display', 'none');
    $('#p4').css('display', 'block');
    $('#f5').css('display', 'none');
    $('#f4').css('display', 'block');
})

$('#f5-b2').click(function () {
    $('#p5').css('display', 'none');
    $('#p6').css('display', 'block');
    $('#f5').css('display', 'none');
    $('#f6').css('display', 'block');
})

$('#f6-b1').click(function () {
    $('#p6').css('display', 'none');
    $('#p5').css('display', 'block');
    $('#f6').css('display', 'none');
    $('#f5').css('display', 'block');
})

let inputList = [] // 放input dom的list
let imgList = [] //放img dom的list

for (var i = 0; i < $(":file").length; i++) {
    inputList.push($(":file")[i])  //把所有的input dom加進去
}

for (var i = 0; i < $(".dropImg").length; i++) {
    imgList.push($(".dropImg")[i]) //把所有的img dom加進去
}

let remainNum = 5;

$(".dropZone, .dropZone1").on({

    "dragover": function (event) {
        event.preventDefault();
    },


    "drop": function (event) {

        event.preventDefault();
        event.stopPropagation();

        let fileList = event.originalEvent.dataTransfer.files;  //把滑鼠抓住的若干檔案assign進去

        for (let i = 0; i < fileList.length; i++) {   

            let file = fileList[i];          //把fileList拆分成單獨file跑迴圈


            //預覽功能
            let reader = new FileReader();
            reader.readAsDataURL(file);
            reader.addEventListener("load", function (event) {
                for (let i = 0; i < imgList.length; i++) {

                    //如果該img為空則把該圖片的url assign進去
                    if (imgList[i].src == '') {
                        imgList[i].src = event.target.result;
                        $(`#plus${i+1}`).css('display', 'none')
                        $(`#dropZone${i+1}`).css('border', '2px rgb(115, 244, 222) solid')
                        remainNum -= 1;
                        $('#re-p').text(`還剩${remainNum}張可以選擇`)
                        break;  //跳脫出for迴圈
                    }
                }
            })

            //檔案上傳
            for (let i = 0; i < inputList.length; i++) {

                //如果該input裡的file為空則把file assign進去
                if (inputList[i].files.length == 0) {

                    let dt = new DataTransfer();
                    dt.items.add(file);
                    inputList[i].files = dt.files;
                    break;
                }
            }
        }       
        
    }
})


$(function () {
    $('#theFile1').change(function () {
        fileChange(1);
    })
    $('#theFile2').change(function() {
        fileChange(2);
    })
    $('#theFile3').change(function () {
        fileChange(3);
    })
    $('#theFile4').change(function () {
        fileChange(4);
    })
    $('#theFile5').change(function () {
        fileChange(5);
    })
})

function fileChange(num) {
    let file = $(`#theFile${num}`)[0].files[0]
    let readFile = new FileReader()
    readFile.readAsDataURL(file)
    readFile.addEventListener('load', function () {
        $(`#dropImg${num}`)[0].src = readFile.result
    })
    remainNum -= 1;
    $('#re-p').text(`還剩${remainNum}張可以選擇`)
    $(`#plus${num}`).css('display', 'none')
    $(`#dropZone${num}`).css('border', '2px rgb(115, 244, 222) solid')
}
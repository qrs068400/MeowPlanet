// 畫面切換
$('#f1-b1').click(function () {
    $('#p1').css('display', 'none');
    $('#p2').css('display', 'block');
    $('#f1').css('display', 'none');
    $('#f2').css('display', 'block');
})


$('#f2-b4').click(function () {

    if ($('#ad-in').attr('value') == '') {
        $('#bre-sp').text('此為必填');
    }
    else {
        $('#p2').css('display', 'none');
        $('#p3').css('display', 'block');
        $('#f2').css('display', 'none');
        $('#f3').css('display', 'block');
    }
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

$('#f6-b2').click(function () {
    $('#p6').css('display', 'none');
    $('#p7').css('display', 'block');
    $('#f6').css('display', 'none');
    $('#f7').css('display', 'block');
})

$('#f7-b1').click(function () {
    $('#p7').css('display', 'none');
    $('#p6').css('display', 'block');
    $('#f7').css('display', 'none');
    $('#f6').css('display', 'block');
})

//品種輸入
$('.bre-btn').click(function () {
    $('#bre-in').attr('value', this.value);
})

//性別輸入
$('.sex-b').click(function () {
    $('#sex-in').attr('value', this.value);
})

//領養輸入
$('.st-b').click(function () {

    $('.st-b').removeClass('st-b-press');
    $(this).addClass('st-b-press');

    $('#ad-in').attr('value', this.value);
})

function doFirst() {
    // 圖片拖曳
    document.getElementById('dropZone1').ondragover = dragOver1
    document.getElementById('dropZone1').ondrop = dropped1
    document.getElementById('dropZone2').ondragover = dragOver2
    document.getElementById('dropZone2').ondrop = dropped2
    document.getElementById('dropZone3').ondragover = dragOver3
    document.getElementById('dropZone3').ondrop = dropped3
    document.getElementById('dropZone4').ondragover = dragOver4
    document.getElementById('dropZone4').ondrop = dropped4
    document.getElementById('dropZone5').ondragover = dragOver5
    document.getElementById('dropZone5').ondrop = dropped5
    
    //document.getElementById('dropZone1').ondragover = dragOver
    //document.getElementById('dropZone1').ondrop = multiDrag
}


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
    $(`#plus${num}`).css('display', 'none')
    $(`#dropZone${num}`).css('border', '2px rgb(115, 244, 222) solid')
}
function dragOver(e) {
    e.preventDefault()
}
function dragOver1(e) {
    e.preventDefault()
}
function dropped1(e) {
    e.preventDefault()
    let file = e.dataTransfer.files[0]
    theFile1.files = e.dataTransfer.files;
    let readFile = new FileReader()
    readFile.readAsDataURL(file)
    readFile.addEventListener('load', function () {
        document.getElementById('dropImg1').src = readFile.result
    })
    document.getElementById('theFile1').files[0] = file
    $('#plus1').css('display', 'none')
    $('.dropZone1').css('border', '2px rgb(115, 244, 222) solid')
}

//let fileList = []
//function multiDrag(e) {
    
//    e.preventDefault()
    

//    for (let i = 0; i < e.dataTransfer.files.length; i++) {
//        fileList.push(e.dataTransfer.files[i]);
//    }


//    fileList.forEach(function (file, index) {


//        $(`#theFile${index + 1}`)[0].files.push(file);

//        let readFile = new FileReader();
//        readFile.addEventListener('load', function () {
//            readFile.readAsDataURL(file);
//            document.getElementById(`dropImg${index + 1}`).src = readFile.result;
//        })
//        //$(`#theFile${index + 1}`)[0].files[0] = file;
//    })
//}

function dragOver2(e) {
    e.preventDefault()
}
function dropped2(e) {
    e.preventDefault()
    let file = e.dataTransfer.files[0]
    theFile2.files = e.dataTransfer.files;
    let readFile = new FileReader()
    readFile.readAsDataURL(file)
    readFile.addEventListener('load', function () {
        document.getElementById('dropImg2').src = readFile.result
    })
    $('#plus2').css('display', 'none')
    $('#dropZone2').css('border', '2px rgb(115, 244, 222) solid')
}

function dragOver3(e) {
    e.preventDefault()
}
function dropped3(e) {
    e.preventDefault()
    let file = e.dataTransfer.files[0]
    theFile3.files = e.dataTransfer.files;
    let readFile = new FileReader()
    readFile.readAsDataURL(file)
    readFile.addEventListener('load', function () {
        document.getElementById('dropImg3').src = readFile.result
    })
    $('#plus3').css('display', 'none')
    $('#dropZone3').css('border', '2px rgb(115, 244, 222) solid')
}


function dragOver4(e) {
    e.preventDefault()
}
function dropped4(e) {
    e.preventDefault()
    let file = e.dataTransfer.files[0]
    theFile4.files = e.dataTransfer.files;
    let readFile = new FileReader()
    readFile.readAsDataURL(file)
    readFile.addEventListener('load', function () {
        document.getElementById('dropImg4').src = readFile.result
    })
    $('#plus4').css('display', 'none')
    $('#dropZone4').css('border', '2px rgb(115, 244, 222) solid')
}

function dragOver5(e) {
    e.preventDefault()
}
function dropped5(e) {
    e.preventDefault()
    let file = e.dataTransfer.files[0]
    theFile5.files = e.dataTransfer.files;
    let readFile = new FileReader()
    readFile.readAsDataURL(file)
    readFile.addEventListener('load', function () {
        document.getElementById('dropImg5').src = readFile.result
    })
    $('#plus5').css('display', 'none')
    $('#dropZone5').css('border', '2px rgb(115, 244, 222) solid')
}
window.addEventListener('load', doFirst)


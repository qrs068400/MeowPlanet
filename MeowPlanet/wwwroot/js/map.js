let map;

//使用者設置的圖標
let newMarker;

//從資料庫撈到的所有貓咪物件
let catList = [];

//顯示在地圖上的marker物件 會因中心點改變而增減
let markerList = [];

function removeMarker() {
    if (newMarker != null) {
        newMarker.setMap(null);
    }
}


function initMap() {

    //初始化地圖
    map = new google.maps.Map($('#map')[0], {
        center: { lat: 22.629314218928563, lng: 120.29299528465663 },
        zoom: 16,
        maxZoom: 18,
        disableDefaultUI: true,
        mapId: 'a5f4cec6781c8dda'
    });

    //定位按鈕
    const locationButton = document.createElement("button");
    locationButton.innerHTML = '<i class="fa-solid fa-crosshairs fa-lg"></i>';
    $(locationButton).addClass('btn btn-primary btn-location');
    $(locationButton).css('border-radius', '10px');
    map.controls[google.maps.ControlPosition.RIGHT_BOTTOM].push(locationButton);
    $(locationButton).on('click', function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    let pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };
                    map.setCenter(pos);
                }
            )
        };
    })


    //刊登協尋
    $('#pre-publish').on('click', function () {

        google.maps.event.removeListener(listener);

        //把資料庫內圖標隱藏
        for (var i = 0; i < markerList.length; i++) {
            markerList[i].setMap(null);
        }

        map.setOptions({ draggableCursor: 'url(../images/marker-cursor.png) 15 45, auto' });

        $('#div-items').addClass('items-activate');
        $('#div-map').addClass('map-activate');

        let listenerHandle = map.addListener("click", (e) => {

            removeMarker();

            newMarker = new google.maps.Marker({
                position: e.latLng,
                map,
                Draggable: true,
                icon: {
                    url: "images/marker.png",
                    scaledSize: new google.maps.Size(40, 57)
                }
            });

            map.panTo(newMarker.getPosition());

            const contentString = '<span class="h5 my-3">您的愛貓在這裡走失的嗎?</span>' + '<div class="d-flex mt-3 mb-1" style="justify-content: space-around">' +
                '<button class="btn btn-primary" onclick="setMarkerPos()" data-bs-toggle="modal" data-bs-target="#exampleModal">' +
                'Yes' + '</button>' + '<button onclick="removeMarker();" class="btn btn-danger">No</button>' + '</div>';

            const infowindow = new google.maps.InfoWindow({
                content: contentString,
            });

            infowindow.open({
                anchor: newMarker,
                map,
                shouldFocus: false,
            });
        });

        

        //切換刊登&取消刊登顯示
        $(this).toggleClass('d-none');
        $('#cancel-publish').toggleClass('d-none');

        //取消刊登按鈕 綁定單次事件
        $('#cancel-publish').one('click', function () {
            map.setOptions({ draggableCursor: '' });
            google.maps.event.removeListener(listenerHandle);
            $(this).toggleClass('d-none');
            $('#pre-publish').toggleClass('d-none');

            $('#div-items').removeClass('items-activate');
            $('#div-map').removeClass('map-activate');

            removeMarker();

            //把資料庫內圖標顯示回來
            for (var i = 0; i < markerList.length; i++) {
                markerList[i].setMap(map);
            }

            listener = map.addListener('idle', searchCat);
        })
    })


    //搜尋框
    const input = document.getElementById("search-input");
    const searchBox = new google.maps.places.SearchBox(input);

    map.controls[google.maps.ControlPosition.TOP_CENTER].push(input);

    map.addListener("bounds_changed", () => {
        searchBox.setBounds(map.getBounds());
    });

    searchBox.addListener("places_changed", () => {
        const places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }
        const bounds = new google.maps.LatLngBounds();

        places.forEach((place) => {
            if (!place.geometry || !place.geometry.location) {
                console.log("Returned place contains no geometry");
                return;
            }

            if (place.geometry.viewport) {
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
    });

    //隨中心依距離把貓貓圖標加進地圖
    function searchCat() {

        catList.forEach((value, index) => {
            let cat = value;
            let marker = cat.marker;
            let LatLng = new google.maps.LatLng(cat.lat, cat.lng);
            let center = map.getCenter();
            let distance = google.maps.geometry.spherical.computeDistanceBetween(center, LatLng);


            //距離小於1.25公里就加入圖標
            if (distance < 1250) {
                marker.setMap(map);

                if (!markerList.includes(marker)) {
                    markerList.push(marker);
                }
            }
            else {
                marker.setMap(null);
                if (markerList.includes(marker)) {
                    markerList.splice(markerList.indexOf(marker), 1); 
                }
            }
        })
        
    };
    

    //綁定移動事件
    var listener = map.addListener('idle', searchCat)
}
    


//日期選擇框
$(function () {
    $("#datepicker").datepicker($.datepicker.regional['tw']);
});

//把marker位置寫進資料庫
function setMarkerPos() {
    $('#lat').val(newMarker.getPosition().lat());
    $('#lng').val(newMarker.getPosition().lng());
}



let catWindow;

//把所有走失貓咪抓進catList
window.onload = $.get("Missings/GetMissing", function (data, status) {
    data.forEach((value, index) => {
        let cat = value;
        let LatLng = new google.maps.LatLng(cat.lat, cat.lng);
        let marker = new google.maps.Marker({
            position: LatLng,
            icon: {
                url: "images/marker.png",
                scaledSize: new google.maps.Size(30, 43)
            }
        });

        //此marker內容
        marker.addListener('click', function () {

            if (catWindow != null) {
                catWindow.close();
            }          

            catWindow = new google.maps.InfoWindow({
                content: `貓貓id = ${cat.catId}`,
            });

            catWindow.open({
                anchor: this,
                map,
                shouldFocus: false
            });
        })

        cat.marker = marker;
        catList.push(cat);
    })
})

window.initMap = initMap;
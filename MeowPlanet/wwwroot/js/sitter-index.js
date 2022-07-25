import createHTMLMapMarker from "./html-map-marker.js";
//!!!!! 地圖   

let map;
function initMap() {
    
        //初始化地圖(應該定位到使用者位置)
        map = new google.maps.Map($('#map')[0], {
            center: { lat: 22.62931421, lng: 120.29299528 },
            zoom: 16,
            // minZoom: 12,
            // maxZoom: 17,
            disableDefaultUI: true,
            mapId: 'a5f4cec6781c8dda',
            gestureHandling: 'cooperative'
        });

        //定位按鈕
        const locationButton = document.createElement("button");
        locationButton.innerHTML = '<i class="fa-solid fa-crosshairs fa-lg"></i>';
        $(locationButton).addClass('btn btn-dark btn-location');
        $(locationButton).css('border-radius', '10px');
        $(locationButton).css('border-color', 'white');
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


        //搜尋框----------------------------------------------------------
        const input = document.getElementById("mapInput");
        const searchBox = new google.maps.places.SearchBox(input);
        // map.controls[google.maps.ControlPosition.TOP_CENTER].push(input);

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


        let sitterList = [];
        //! 取得所有保母的所有資訊
        $(function () {
            $.get('Sitter/GetSitter', function (data, status) {
                data.forEach((value, index) => {
                    let sitter = value.sitter;
                    let latlng = new google.maps.LatLng(sitter.lat, sitter.lng);
                    let marker = createHTMLMapMarker({
                        latlng,
                        map,
                        html: `<div class="marker">$ ${sitter.pay} TWD</div>`,
                    });
                    //綁定marker 事件
                    marker.addListener("click", () => {
                        alert("Partyin Partyin Yeah!", this);
                    })

                    sitter.marker = marker;
                    sitter.latlng = latlng;
                    sitter.featureList = value.sitterfeatureList;
                    sitter.orderList = value.OrderCommentList;
                    sitterList.push(sitter);
                })

                searchCat();
            })
        })

        //綁定地圖停止時，做搜尋
        var listener = map.addListener('idle', searchSitter);
    function searchSitter() {
        sitterList.forEach((sitter, index) => {
            let marker = sitter.marker;
            let LatLng = sitter.latlng;
                let center = map.getCenter();
                let distance = google.maps.geometry.spherical.computeDistanceBetween(center, LatLng);

                //距離小於1公里就加入圖標 & 顯示在左邊item列
                if (distance < 3000) {

                    marker.setMap(map);

                    //if (!showingCatList.includes(cat)) {
                    //    showingCatList.push(cat);
                    //    $(`#missing-${cat.missingId}`).remove().appendTo('#div-items').delay(100).fadeIn(600);
                    //}
                    //$(`#missing-${cat.missingId}`).fadeIn(600);
                }
                else {

                    marker.setMap(null);

                    //if (showingCatList.includes(cat)) {
                    //    showingCatList.splice(showingCatList.indexOf(cat), 1);
                    //    $(`#missing-${cat.missingId}`).fadeOut(600);
                    //}
                }
            })

        }

        //function scrollToItem(item) {
        //    let id = $(item).data('id');
        //    catList.filter(x => x.missingId == id)[0].marker.setAnimation(google.maps.Animation.BOUNCE);
        //}
}
$(initMap());


//! 地圖彈出
{
    let map_div01 = document.getElementById("map_div01")
    let searchResult_div03 = document.getElementById("search-result_div03")

    document.getElementById('btn01').addEventListener('click', () => {
        map_div01.style.transform = "translateX(0%)";
        searchResult_div03.style.width = "560px";
        searchResult_div03.style.transform = "translateX(27vw)";

        document.getElementById('btn01').classList.add("btnHidden");
    })

    document.getElementById('btn02').addEventListener('click', () => {
        map_div01.style.transform = "translateX(-100%)";
        searchResult_div03.removeAttribute("style");

        document.getElementById('btn01').classList.remove("btnHidden");
    })
}
//!  照片輪播 
{
    $('.pic_div4-1').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
    });
}




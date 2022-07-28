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
    model.forEach((value, index) => {
        let sitter = value.sitter;
        let latlng = new google.maps.LatLng(sitter.posLat, sitter.posLng);
        let marker = createHTMLMapMarker({
            latlng,
            html: `<div class="marker">$ ${sitter.pay} TWD</div>`,
        });
        let center = map.getCenter();
        let distanceToMe = (google.maps.geometry.spherical.computeDistanceBetween(center, latlng) / 1000).toFixed(1);
        //綁定marker 事件
        marker.addListener("click", () => {
            alert("Partyin Partyin Yeah!", this);
        })

        sitter.marker = marker;
        sitter.latlng = latlng;
        sitter.memberPhoto = value.memberPhoto;
        sitter.distanceToMe = distanceToMe;
        sitter.featureList = value.sitterfeatureList;
        sitter.orderList = value.OrderCommentList;
        sitterList.push(sitter);
        console.log(sitterList);
    })

    //-------!@##$$%^**()---!!!!!!!!!!照片來源還需要修改!!!!!!!!!---------!@#$%^&*()-------
    function pic() {
        let pic = "";
        for (var j = 1; j < 3; j++) {
            pic += `<div class="slide-${j}"><img src="../images/sitter/cat${j}.jpg"></div>`
        }
        return pic;
    };

    function avg_star(sitter) {
        let star = "";
        let solidstar = parseInt(sitter.avgStar / 1);
        let halfstar = sitter.avgStar % 1 == 0;
        //產生實星星
        for (var i = 0; i < solidstar; i++) {
            star += '<i class="fa-solid mr-1 fa-star fa-xs"></i>';
        }
        //產生半星及空星
        if (halfstar) {
            for (var i = 0; i < 5 - solidstar; i++) {
                star += '<i class="fa-regular mr-1 fa-star fa-xs"></i>';
            }
        } else {
            star += '<i class="fa-solid mr-1 fa-star-half-stroke fa-xs"></i>';
            for (var i = 0; i < 5 - 1 - solidstar; i++) {
                star += '<i class="fa-regular mr-1 fa-star fa-xs"></i>';
            }
        }
        star += sitter.avgStar;
        star += `(${sitter.featureList.length})`
        return star;
    };

    function featureOrNot(sitter) {
        if (sitter.featureList.length ==0) {
            return "無設備";
        } else {
            return "有設備";
        }
    };

    for (var i = 0; i < sitterList.length; i++) {
        $('.row').append(
            `<div class="card_div4" style="display:none;" data-id="${sitterList[i].serviceId}">
                <div class="pic_div4-1">
                    ${pic()}
                </div>
                <div class="sitter-box_div5">
                    <div class="d-flex">
                        <div class="sitter-pic_div5-1">
                            <img src=" ${sitterList[i].memberPhoto}" />
                        </div>
                        <div class="sitter-info_div5-2">
                            <p class="sitter-name_div5-2-1">
                                ${sitterList[i].name}
                            </p>
                            <p class=" d-flex sitter-star">
                                ${avg_star(sitterList[i])}
                            </p>
                        </div>
                    </div>
                    <div class="sitter-location_div5-3 ">
                        <i class="fa-solid fa-location-dot fa-sm"></i>
                        <span>&nbsp高雄市 前金區</span>
                        <span>${sitterList[i].distanceToMe} Km</span>
                    </div>
                    <div class="sitter-feature_div6">
                        <div class="feature_span1"> ${sitterList[i].licence} </div>
                        <div class="feature_span1"> ${sitterList[i].meal} </div>
                        <div class="feature_span1"> ${sitterList[i].cage} </div>
                        <div class="feature_span1"> ${sitterList[i].catNumber} </div>
                        <div class="feature_span1"> ${featureOrNot(sitterList[i])} </div>
                        <div class="feature_span1"> ${sitterList[i].monitor} </div>
                    </div>
                    <a class="sitter-btn_div7" href="/Sitter/Detail/${sitterList[i].serviceId}">
                        <i class="fa-regular fa-calendar-check"></i>
                        預約
                    </a>
                </div>
            </div>`
        )
    };
    let showingSitterList = [];
    searchSitter();


    //綁定地圖停止時，做搜尋
    map.addListener('idle', searchSitter);

    function searchSitter() {
        sitterList.forEach((sitter, index) => {
            let marker = sitter.marker;
            let latlng = sitter.latlng;
            let center = map.getCenter();
            let distance = google.maps.geometry.spherical.computeDistanceBetween(center, latlng);

            //距離小於1公里就加入圖標 & 顯示在左邊item列
            if (distance < 1000) {

                marker.setMap(map);

                if (!showingSitterList.includes(sitter)) {
                    showingSitterList.push(sitter);
                    $(`[data-id="${sitter.serviceId}"]`).remove().appendTo('.row').delay(100).fadeIn(600);
                }
                $(`[data-id="${sitter.serviceId}"]`).fadeIn(600);
            }
            else {

                marker.setMap(null);

                if (showingSitterList.includes(sitter)) {
                    showingSitterList.splice(showingSitterList.indexOf(sitter), 1);
                    $(`[data-id="${sitter.serviceId}"]`).fadeOut(600);
                }
            }
        })

    };

    //function scrollToItem(item) {
    //    let id = $(item).data('id');
    //    catList.filter(x => x.missingId == id)[0].marker.setAnimation(google.maps.Animation.BOUNCE);
    //}
};
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




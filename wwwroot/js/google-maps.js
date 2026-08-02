window.evLaneMaps = (() => {
    let loader;
    let map;
    let markers = [];

    function load(apiKey) {
        if (window.google?.maps) return Promise.resolve();
        if (loader) return loader;
        loader = new Promise((resolve, reject) => {
            const callback = `evLaneGoogleMapsLoaded_${Date.now()}`;
            window[callback] = () => { delete window[callback]; resolve(); };
            const script = document.createElement("script");
            script.async = true;
            script.defer = true;
            script.src = `https://maps.googleapis.com/maps/api/js?key=${encodeURIComponent(apiKey)}&callback=${callback}`;
            script.onerror = () => reject(new Error("Google Maps failed to load."));
            document.head.appendChild(script);
        });
        return loader;
    }

    async function render(elementId, apiKey, latitude, longitude, stations) {
        if (!apiKey) return;
        await load(apiKey);
        const element = document.getElementById(elementId);
        if (!element) return;
        const center = { lat: latitude, lng: longitude };
        map = new google.maps.Map(element, {
            center,
            zoom: 12,
            mapTypeControl: false,
            streetViewControl: false
        });
        markers.forEach(marker => marker.setMap(null));
        markers = [];
        markers.push(new google.maps.Marker({
            position: center,
            map,
            title: "Your location",
            icon: "https://maps.google.com/mapfiles/ms/icons/blue-dot.png"
        }));
        for (const station of stations || []) {
            if (station.latitude == null || station.longitude == null) continue;
            markers.push(new google.maps.Marker({
                position: { lat: station.latitude, lng: station.longitude },
                map,
                title: station.name || "Charging station"
            }));
        }
    }

    return { render };
})();

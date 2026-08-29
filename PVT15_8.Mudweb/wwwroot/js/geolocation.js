window.getBrowserLocation = async () => {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation || !window.isSecureContext) {
            reject("Geolocation is not supported or not in a secure context.");
            return;
        }

        const cacheKey = "user_cached_location";
        const cacheTTL = 5 * 60 * 1000;

        try {
            const cachedData = sessionStorage.getItem(cacheKey);
            if (cachedData) {
                const parsedCache = JSON.parse(cachedData);
                const isCacheValid = (Date.now() - parsedCache.timestamp) < cacheTTL;

                if (isCacheValid) {
                    resolve(parsedCache.location);
                    return;
                }
            }
        } catch (e) {
            console.warn("Session storage is disabled or inaccessible. Proceeding to live fetch.");
        }

        const options = {
            enableHighAccuracy: true,
            timeout: 30000,
            maximumAge: 0
        };

        navigator.geolocation.getCurrentPosition(
            (position) => {
                const locationData = {
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                    accuracy: position.coords.accuracy
                };

                try {
                    const cachePayload = {
                        location: locationData,
                        timestamp: Date.now()
                    };
                    sessionStorage.setItem(cacheKey, JSON.stringify(cachePayload));
                } catch (e) {
                    console.warn("Failed to save location to session storage.");
                }

                resolve(locationData);
            },
            (error) => {
                let message;
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        message = "User denied geolocation.";
                        break;
                    case error.POSITION_UNAVAILABLE:
                        message = "Location information unavailable.";
                        break;
                    case error.TIMEOUT:
                        message = "Location request timed out (try again).";
                        break;
                    default:
                        message = "Unknown error occurred.";
                }
                reject(message);
            },
            options
        );
    });
};

// export async function initMap() {
//     const [{ Map }, { AdvancedMarkerElement }] = await Promise.all([
//         google.maps.importLibrary('maps'),
//         google.maps.importLibrary('marker'),
//     ]);

//     await customElements.whenDefined('gmp-map');
//     const mapElement = document.querySelector('gmp-map');
//     const innerMap = mapElement.innerMap;

//     if (innerMap) {
//         innerMap.setOptions({
//             mapTypeControl: false,
//         });

//         const marker = new AdvancedMarkerElement({
//             map: innerMap,
//             position: { lat: 59.3293, lng: 18.0686 },
//             title: 'Stockholm',
//         });
//     }
// }

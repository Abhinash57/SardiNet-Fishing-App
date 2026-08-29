window.cameraHelper = {
    takePhoto: function (dotNetHelper) {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        input.capture = 'environment';
        input.onchange = (e) => {
            const file = e.target.files[0];
            if (!file) return;
            const reader = new FileReader();
            reader.onload = (event) => {
                const base64 = event.target.result.split(',')[1];
                dotNetHelper.invokeMethodAsync('OnImageSelected', base64, file.name, file.type);
            };
            reader.readAsDataURL(file);
        };
        input.click();
    },
    chooseFromGallery: function (dotNetHelper) {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        input.onchange = (e) => {
            const file = e.target.files[0];
            if (!file) return;
            const reader = new FileReader();
            reader.onload = (event) => {
                const base64 = event.target.result.split(',')[1];
                dotNetHelper.invokeMethodAsync('OnImageSelected', base64, file.name, file.type);
            };
            reader.readAsDataURL(file);
        };
        input.click();
    }
};
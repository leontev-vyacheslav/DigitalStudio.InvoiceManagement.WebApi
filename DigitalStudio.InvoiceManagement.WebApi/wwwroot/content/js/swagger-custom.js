(function (){
    window.addEventListener('load', (event) => {
        const linkIcon = document.createElement('link');
        linkIcon.type = 'image/icon';
        linkIcon.rel = 'icon';
        linkIcon.href = '/content/images/favicon.ico';
        document.querySelector('head').appendChild(linkIcon);
    });
})();
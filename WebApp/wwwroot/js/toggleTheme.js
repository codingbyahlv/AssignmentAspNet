document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#toggle-btn')

    sw.addEventListener('click', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else
                    console.log('theme trubble')
            })
    })
})

document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#toggle-btn-mobile')

    sw.addEventListener('click', function (e) {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok) {
                    window.location.reload()
                }
                else
                    console.log('theme trouble')
            })
    })
})
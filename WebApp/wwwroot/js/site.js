function getAndShow() {
    const category = document.getElementById("select").value;
    const searchQuery = document.getElementById("searchQuery").value;
    const url = `/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`
    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.course-card-wrapper').innerHTML = dom.querySelector('.course-card-wrapper').innerHTML
            const pagination = dom.querySelector('.pagination-wrapper') ? dom.querySelector('.pagination-wrapper').innerHTML : ''
            document.querySelector('.pagination-wrapper').innerHTML = pagination
        })
}

document.getElementById("select").addEventListener("change", function () { getAndShow(); });

document.getElementById("searchQuery").addEventListener("keyup", function () { getAndShow() });
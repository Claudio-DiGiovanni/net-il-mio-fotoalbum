
const loadImages = filter => getImages(filter).then(renderImages);

const getImages = title => axios.get('api/image', title ? { params: { title } } : {})
        .then(res => res.data)
        .catch(e => console.log(e));

const renderImages = images => {
    const page = document.querySelector('#page');
    const loader = document.querySelector('#images-loader');
    const noImages = document.querySelector('#no-images'); 
    const filter = document.querySelector("#filter");

    if (!images && images.length < 0) {
        noImages.classList.remove('d-none');
        page.classList.add('d-none');
        filter.classList.add('d-none');
    } else {
        noImages.classList.add('d-none');
        page.classList.remove('d-none');
    }

    loader.classList.add('d-none');

    page.innerHTML = images.map(imageComponent).join('');
};

const imageComponent = image => {
    if (image.visible) {
        return ` <div class="col">
                    <div class="card">
                        <img src="${image.url}" class="card-img-top" alt="${image.title}">
                        <div class="card-body">
                            <h5 class="card-title">${image.title}</h5>
                            <p class="card-text">${image.description}</p>
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                                Categorie:
                        
                            </li>
                        </ul>
                    </div>
                </div>`
    } else {
        return '';
    }
}

const categoryComponent = category => `<span>${category.name}, </span>`


const initFilter = ()  => {
    const filter = document.querySelector("#filter");
    filter.addEventListener("input", (e) => loadImages(e.target.value))
};

const getImage = id => axios
    .get(`/api/image/${id}`)
    .then(res => res.data);

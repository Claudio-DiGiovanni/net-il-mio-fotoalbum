
const loadImages = filter => getImages(filter).then(renderImages);

const getImages = title => axios
    .get('api/image', title ? { params: { title } } : {})
    .then(res => res.data)
    .catch(e => console.log(e.response.data.errors));

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



const renderCategories = image => {
    return image.categories.map(categoryComponent).join(', ');
}

const categoryComponent = category => `${category.name}`


const initFilter = ()  => {
    const filter = document.querySelector("#filter");
    filter.addEventListener("input", (e) => loadImages(e.target.value))
};

const getImage = id => axios
    .get(`/api/image/${id}`)
    .then(res => res.data);

const imageComponent = image => {
    if (image.visible) {
        return ` <div class="col">
                    <div class="card h-100">
                        <img src="${image.url}" class="card-img-top" alt="${image.title}">
                        <div class="card-body">
                            <h5 class="card-title">${image.title}</h5>
                            <p class="card-text">${image.description}</p>
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                                <strong>Categori${image.categories.length == 1 ? "a" : "e"}:</strong>
                               ${renderCategories(image)}
                            </li>
                        </ul>
                    </div>
                </div>`
    } else {
        return '';
    }
}

const CreateMessage = message => axios
    .post('api/image', message)
    .then(res => console.log(res.data))
    .catch(e => renderErrors(e.response.data.errors));

const initMessageForm = () => {
    const form = document.querySelector('#message-form');
    const email = document.querySelector('#email');
    const messageText = document.querySelector('#message');

    form.addEventListener("submit", e => {
        e.preventDefault();

        const message = getMessageFromForm(form);
        CreateMessage(message);
        email.value = '';
        messageText.value = '';
    });

}

const getMessageFromForm = form => {
    const email = form.querySelector('#email').value;
    const messageText = form.querySelector('#message').value;
    return {
        email,
        messageText
    }
}

const renderErrors = errors => {
    const emailErrors = document.querySelector('#email-error');
    const messageErrors = document.querySelector('#message-error');

    emailErrors.innerText = errors.Email?.join('\n') ?? '';
    messageErrors.innerText = errors.MessageText?.join('\n') ?? '';
}
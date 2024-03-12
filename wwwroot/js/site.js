const uri = '/todo';
let pizzas = [];
const token = localStorage.getItem("token");
link();
getItems(token);

function link() {
    const link = document.getElementById('link');
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("/user/GetAll", requestOptions)
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => { link.style = 'display:none';});
}

function getItems(token) {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch(uri, requestOptions)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => {
            location.href = "index.html";
            console.log('Unable to get items.', error)
        });
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');

    const item = {
        IsDo: false,
        name: addNameTextbox.value.trim()
    };
    console.log(item);

    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    // myHeaders.append("Accept", "application/json");
    fetch(uri, {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow',
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems(token);
            addNameTextbox.value = '';
        })
        .catch(error => {
            location.href = "index.html";
            console.log('Unable to get items.', error)
        });
}

function deleteItem(id) {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: myHeaders,
        redirect: 'follow'
    })
        .then(() => getItems(token))
        .catch(error => {
            location.href = "index.html";
            console.log('Unable to get items.', error)
        });
}

function displayEditForm(id) {
    const item = pizzas.find(item => item.id === id);
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-IsDo').checked = item.IsDo;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        IsDo: document.getElementById('edit-IsDo').checked,
        name: document.getElementById('edit-name').value.trim()
    };
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    // myHeaders.append("Accept", "application/json");
    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: myHeaders,
        redirect: 'follow',
        body: JSON.stringify(item)
    })
        .then(() => getItems(token))
        .catch(error => {
            location.href = "index.html";
            console.log('Unable to get items.', error)
        });

    closeInput();

    return false
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'todo' : 'todo kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('pizzas');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let IsDoCheckbox = document.createElement('input');
        IsDoCheckbox.type = 'checkbox';
        IsDoCheckbox.disabled = true;
        IsDoCheckbox.checked = item.isDo;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(IsDoCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);

    });

    pizzas = data;
}

const editFormUser = document.getElementById('editFormUser');
const editusername = document.getElementById('edit-username');
const edituserpassword = document.getElementById('edit-userpassword');
const saveuser = document.getElementById('saveUser');

const urlUser = '/user/';
let users = [];
getUser();

function getUser() {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch(urlUser, requestOptions)
        .then(response => response.json())
        .then(data => showUser(data))
        .catch(error => {
            location.href = "index.html";
            console.log('Unable to get items.', error)
        });
}

let useridto;
let Adminorno;
function showUser(data) {
        useridto = data.userId;
        Adminorno=data.admin;
        document.getElementById('edit-username').value = data.username;
        document.getElementById('edit-userpassword').value = data.password;
}

function uptadeUser() {
    const newuser = {
        UserId: useridto,
        username: document.getElementById('edit-username').value.trim(),
        password: document.getElementById('edit-userpassword').value.trim(),
        Admin: Adminorno
    };
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    fetch(urlUser, {
        method: 'PUT',
        headers: myHeaders,
        redirect: 'follow',
        body: JSON.stringify(newuser)
    })
        .then(() => getUser(token))
        .catch(error => {
            console.log('Unable to get items.', error)
        });
    return false;
}

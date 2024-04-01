const uri = '/user';
let users = [];
const token = localStorage.getItem("token");

var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer " + token);
myHeaders.append("Content-Type", "application/json");
getItems();

function getItems() {
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch(`${uri}/GetAll`, requestOptions)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => {
            alert("The Token finish")
            console.log('Unable to get items.', error)
            location.href = "index.html";
        });
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addpassTextbox = document.getElementById('add-pass');
    const Admin = document.getElementById('add-Admin');

    const item = {
        Admin: Admin.checked,
        username: addNameTextbox.value.trim(),
        Password: addpassTextbox.value.trim()
    };

    fetch(uri, {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addpassTextbox.value = '';
            Admin.checked = false;
        })
        .catch(error => {
            alert("The Token finish")
            console.log('Unable to get items.', error)
            location.href = "index.html";
        });
}

function deleteItem(id) {
    fetch(`${uri}/${ id }`, {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow'
        })
        .then(() => getItems())
        .catch(error => {
            alert("The Token finish")
            console.log('Unable to get items.', error)
            location.href = "index.html";
        });
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'todo' : 'todo kinds';
    document.getElementById('counter').innerText = `${ itemCount }${ name }`;
}

function _displayItems(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let IsDoCheckbox = document.createElement('input');
        IsDoCheckbox.type = 'checkbox';
        IsDoCheckbox.disabled = true;
        IsDoCheckbox.checked = item.admin;

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${ item.userId })`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(IsDoCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.username);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });

    users = data;
}
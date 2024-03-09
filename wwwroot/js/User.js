const uri = '/Admin/Get';
const url = '/Admin';
let users = [];
const token = localStorage.getItem("token");
getItems(token);

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
    const addpassTextbox = document.getElementById('add-pass');
    const Admin = document.getElementById('add-Admin').checked;

    const item = {
        Admin: Admin,
        username: addNameTextbox.value.trim(),
        Password: addpassTextbox.value.trim()
    };
    console.log(item);

    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    fetch(url, {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems(token);
            addNameTextbox.value = '';
            addpassTextbox.value = '';
            Admin.checked = false;
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
    fetch(`/Admin/${id}`, {
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

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'todo' : 'todo kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        console.log(item);
        let IsDoCheckbox = document.createElement('input');
        IsDoCheckbox.type = 'checkbox';
        IsDoCheckbox.disabled = true;
        IsDoCheckbox.checked = item.admin;

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.userId})`);

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
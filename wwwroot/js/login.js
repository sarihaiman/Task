const url = '/Login';
var myHeaders = new Headers();

function toConnect() {
    const name2 = document.getElementById('name').value.trim();
    const password2 = document.getElementById('password').value.trim();
    Login(name2, password2);
    // name2.value = "";
    // password2.value = "";
}

function Login(Name, Password) {
    localStorage.clear();
    myHeaders.append("Content-Type", "application/json");
    var raw = JSON.stringify({
        Username: Name,
        Password: Password
    })
    var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
    };
    fetch(url, requestOptions)
        .then((response) => response.text())
        .then((result) => {
            if (result.includes("401")) {
                alert("not exist!!")
            } else {
                token = result;
                localStorage.setItem("token", token)
                location.href = "task.html";
            }
        }).catch((error) => alert("error", error));
}


function handleCredentialResponse(response) {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = parseJwt(idToken);
        var userId = decodedToken.sub;
        var userName = decodedToken.name;
        Login(userName, userId);
    } else {
        console.log('Google Sign-In was cancelled.');
    }
}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}
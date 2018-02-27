function validate() {
    var regObj = {
        firstName: document.getElementById("regFirstName").value,
        lastName: document.getElementById("regLastName").value,
        eMail: document.getElementById("regEmail").value,
        phoneNo: document.getElementById("regPhoneNo").value,
        username: document.getElementById("regUsername").value,
        password: document.getElementById("regPassword").value
    };

    var regexArray = [0, 0, 0, 0, 0, 0];
    regexArray[0] = /^[a-zA-Z]{1,}$/;
    regexArray[1] = /^[a-zA-Z]{1,}$/;
    regexArray[2] = /^(([^<>()\[\]\\.,;:\s@@"]+(\.[^<>()\[\]\\.,;:\s@@"]+)*)|(".+"))@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    regexArray[3] = /^\d{10}$/;
    regexArray[4] = /^([a-zA-Z0-9]){1,15}$/;
    regexArray[5] = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@@$!%*?&])[A-Za-z\d$@@$!%*?&]{8,}/;
    Check1();
}

function Check1() {
    var torfArray = [false, false, false, false, false, false];

    if (regexArray[0].test(regObj.firstName) !== false) {
        torfArray[0] = true;
    } if (regexArray[1].test(regObj.lastName) !== false) {
        torfArray[1] = true;
    } if (regexArray[2].test(regObj.eMail) !== false) {
        torfArray[2] = true;
    } if (regexArray[3].test(regObj.phoneNo) !== false) {
        torfArray[3] = true;
    } if (regexArray[4].test(regObj.username) !== false) {
        torfArray[4] = true;
    } if (regexArray[5].test(regObj.password) !== false) {
        torfArray[5] = true;
    } else if (regexArray.forEach(Check2(element)) !== false) {
        return true;
    } else {
        return false;
    }
}

function Check2() {
    if (element === false) {
        return false;
    } else {
        return true;
    }
}
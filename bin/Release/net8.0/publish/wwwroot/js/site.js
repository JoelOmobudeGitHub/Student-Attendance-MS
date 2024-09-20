// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let presents = [];
absent = [];
isChecked = false;

function showLoginForm(role) {
    const loginFormContainer = document.getElementById('login-form-container');
    const loginTitle = document.getElementById('login-title');
    loginFormContainer.style.display = 'flex';
    switch (role) {
        case 'admin':
            loginTitle.textContent = 'Admin Login';
            break;
        case 'supervisor':
            loginTitle.textContent = 'Supervisor Login';
            break;
        case 'student':
            loginTitle.textContent = 'Student Login';
            break;
    }
}

function hideLoginForm() {
    const loginFormContainer = document.getElementById('login-form-container');
    loginFormContainer.style.display = 'none';
}

document.getElementById('login-form').addEventListener('submit', function(event) {
    event.preventDefault();
    // Here you can add the login logic
    alert('Login successful!');
});



function assignCheck(present)
{
    
        isChecked = true;
        presents.push(present);

        
}


function submitAttendance(supervisorId) {
   console.log(presents)
        let formData = new FormData 
        formData.append('StudentPresent', JSON.stringify(presents)),
        formData.append('SupervisorId'  , JSON.stringify(supervisorId))
        
       
    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Attendance/Create",
        data:  formData,
        dataType: "text",
        contentType: false,
        processData: false,
        success: function (r) {
            window.location.href = "/Supervisor/Index?id="+supervisorId
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};
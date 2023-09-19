// Get references to all HTML elements with class names
const counters = document.querySelectorAll('.counter');
const incrementButtons = document.querySelectorAll('.increment');

const show_hide_button = document.getElementById("show_elements");
const csv_button = document.getElementById("exportCsvButton");
const count_station_1 = document.getElementById("station_1");
const count_station_2 = document.getElementById("station_2");
const count_station_3 = document.getElementById("station_3");
const count_station_4 = document.getElementById("station_4");
const count_station_5 = document.getElementById("station_5");
const count_station_6 = document.getElementById("station_6");
const count_station_7 = document.getElementById("station_7");
const count_station_8 = document.getElementById("station_8");

show_hide_button.addEventListener("click", function () {
    // Toggle the display property to hide/show the div
    if (csv_button.style.display === "none" || csv_button.style.display === "") {
        csv_button.style.display = "block"; // Show the div
        count_station_1.style.display = "block";
        //count_station_1.style.color = "black";
        count_station_2.style.display = "block";
        count_station_3.style.display = "block";
        count_station_4.style.display = "block";
        count_station_5.style.display = "block";
        count_station_6.style.display = "block";
        count_station_7.style.display = "block";
        count_station_8.style.display = "block";
    } else {
        csv_button.style.display = "none"; // Hide the div
        count_station_1.style.display = "none";
        //count_station_1.style.color = "white";
        count_station_2.style.display = "none";
        count_station_3.style.display = "none";
        count_station_4.style.display = "none";
        count_station_5.style.display = "none";
        count_station_6.style.display = "none";
        count_station_7.style.display = "none";
        count_station_8.style.display = "none";
    }
});

// Initialize all counters from local storage
for (let i = 0; i < counters.length; i++) {
    const counterId = counters[i].id;
    let counterValue = localStorage.getItem(counterId) || 0;
    counters[i].textContent = counterValue;

    // Event listeners for the increment and decrement buttons
    incrementButtons[i].addEventListener('click', () => {
        updateCounter(counterId, 1);
        Swal.fire({
            position: 'top',
            icon: 'success',
            title: 'Registro Exitoso',
            showConfirmButton: false,
            timer: 1000
        })
    });

}

// Function to update a specific counter value and save it to local storage
function updateCounter(counterId, value) {
    
    const counterElement = document.getElementById(counterId);
    let counterValue = parseInt(localStorage.getItem(counterId)) || 0;
    counterValue += value;
    localStorage.setItem(counterId, counterValue);
    counterElement.textContent = counterValue;
}

function updateAllCounter() {
    debugger
    updateCounter("station_1", 1)
    updateCounter("station_2", 1)
    updateCounter("station_3", 1)
    updateCounter("station_4", 1)
    updateCounter("station_5", 1)
    updateCounter("station_6", 1)
    updateCounter("station_7", 1)
    updateCounter("station_8", 1)

    Swal.fire({
        position: 'top',
        icon: 'success',
        title: 'Registro Exitoso',
        showConfirmButton: false,
        timer: 1000
    })
}

// Function to export data from local storage as a CSV file
function exportDataToCsv() {
    // Retrieve data from local storage
    const data = {};

    // You can loop through the local storage keys and add them to the data object
    for (let i = 0; i < localStorage.length; i++) {
        const key = localStorage.key(i);
        const value = localStorage.getItem(key);
        data[key] = value;
    }

    // Convert data to CSV format
    const csvContent = Object.keys(data)
        .map(key => `${key},${data[key]}`)
        .join('\n');

    // Create a Blob object containing the CSV data
    const blob = new Blob([csvContent], { type: 'text/csv' });

    // Create a download link for the Blob
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'local_storage_data.csv';

    // Trigger a click event to download the file
    a.click();

    // Clean up
    URL.revokeObjectURL(url);
}

// Attach the exportDataToCsv function to the button click event
document.getElementById('exportCsvButton').addEventListener('click', exportDataToCsv);


// Get a reference to the button element
const button = document.getElementById('myButton');
// Add a click event listener to the button
button.addEventListener('click', updateAllCounter);

// Add a custom confirmation dialog for page reload
window.addEventListener('beforeunload', function (e) {
    // Customize the confirmation message
    var confirmationMessage = 'Are you sure you want to leave this page?';

    // Display the custom confirmation message to the user
    e.returnValue = confirmationMessage;
    return confirmationMessage;
});
document.getElementById("sidebarToggle").addEventListener("click", function () {
    document.getElementById("wrapper").classList.toggle("toggled");
});

function clearSearch() {
    const url = new URL(window.location.href);
    url.searchParams.delete('searchTerm');
    window.location.assign(url.toString());
}

/**
 * Populates a dropdown with data fetched from the server.
 * @param {string} url - The endpoint URL to fetch data.
 * @param {string} dropdownId - The ID of the dropdown to populate.
 * @param {string} defaultOptionText - The default option text to display.
 * @param {string} preSelectedValue - Value to pre-select, if any.
 */
function populateDropdown(url, dropdownId, defaultOptionText, preSelectedValue = null) {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            let dropdown = $(`#${dropdownId}`);
            dropdown.empty();
            dropdown.append(`<option value="" disabled selected>${defaultOptionText}</option>`);

            data.forEach(function (item) {
                dropdown.append(`<option value="${item.id}">${item.name || item.fullName}</option>`);
            });

            if (preSelectedValue) {
                dropdown.val(preSelectedValue);
            }
        },
        error: function (error) {
            console.error(`Error fetching data for ${dropdownId}:`, error);
        }
    });
}

/**
 * Populates a multi-select dropdown with data fetched from the server and sets pre-selected values.
 * @param {string} url - The endpoint URL to fetch data.
 * @param {string} dropdownId - The ID of the multi-select dropdown to populate.
 * @param {array} preSelectedValues - Array of pre-selected values (GUIDs).
 */
function populateMultiSelectDropdown(url, dropdownId, preSelectedValues = []) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            let dropdown = $(`#${dropdownId}`);
            dropdown.empty();

            data.forEach(function (item) {
                const isSelected = preSelectedValues.includes(item.id);
                dropdown.append(`<option value="${item.id}" ${isSelected ? 'selected' : ''}>${item.name || item.fullName}</option>`);
            });

            dropdown.trigger('change');
        },
        error: function (error) {
            console.error(`Error fetching data for ${dropdownId}:`, error);
        }
    });
}

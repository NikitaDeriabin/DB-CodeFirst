const uri = 'api/Teams';
let teams = [];

function getTeams() {

    fetch(uri)
        .then(response => response.json())
        .then(data => _displayTeams(data))
        .catch(error => console.error('Unable to get teams.', error));
}

function addTeams() {
    const addNameTextBox = document.getElementById('add-name');
    const addWinAmountTextBox = document.getElementById('add-winamount');
    const addLoseAmountTextBox = document.getElementById('add-loseamount');
    const addDrawAmountTextBox = document.getElementById('add-drawamount');

    const team = {
        name: addNameTextBox.value.trim(),
        winamount: addWinAmountTextBox.value.trim(),
        loseamount: addLoseAmountTextBox.value.trim(),
        drawamount: addDrawAmountTextBox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(team)
    })
        .then(response => response.json())
        .then(() => {
            getTeams();
            addNameTextBox.value = '';
            addWinAmountTextBox.value = '';
            addLoseAmountTextBox.value = '';
            addDrawAmountTextBox.value = '';
        })
        .catch(error => console.error('Unable to add team.', error));
}

function deleteTeams(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getTeams())
        .catch(error => console.error('Unable to delete team.', error));
}

function displayEditForm(id) {
    const team = teams.find(team => team.id === id);

    document.getElementById('edit-id').value = team.id;
    document.getElementById('edit-name').value = team.name;
    document.getElementById('edit-winamount').value = team.winamount;
    document.getElementById('edit-loseamount').value = team.loseamount;
    document.getElementById('edit-drawamount').value = team.drawamount;
}

function uptadeTeam() {
    const teamId = document.getElementById('edit-id').value;
    const team = {
        id: parseInt(teamId, 10),
        name: document.getElementById('edit-name').value.trim(),
        winamount: document.getElementById('edit-winamount').value.trim(),
        loseamount: document.getElementById('edit-loseamount').value.trim(),
        drawamount: document.getElementById('edit-drawamount').value.trim()
    };

    fetch(`${uri}/${teamId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(team)
    })
        .then(() => getTeams())
        .catch(error => console.error('Unable to update team.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayTeams(data) {

    const tBody = document.getElementById('teams');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(team => {
        let editButton = button.cloneNode(false);

        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${team.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteTeam(${team.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNodeName = document.createTextNode(team.name);
        td1.appendChild(textNodeName);

        let td2 = tr.insertCell(1);
        let textNodeWinAmount = document.createTextNode(team.winamount);
        td2.appendChild(textNodeWinAmount);

        let td3 = tr.insertCell(2);
        let textNodeLoseAmount = document.createTextNode(team.loseamount);
        td3.appendChild(textNodeLoseAmount);

        let td4 = tr.insertCell(3);
        let textNodeDrawAmount = document.createTextNode(team.drawamount);
        td4.appendChild(textNodeDrawAmount);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    team = data;   
}

"use strict";

const messageInput = document.getElementById('message-input');
const nameInput = document.getElementById('name-input');

window.addEventListener('load', async () => {
    const response = await fetch('/api/messages');
    const data = await response.json();
    const list = document.getElementById('message-container');



    data.forEach(item => {
        const userName = document.createElement('p');
        userName.textContent = item.user;
        userName.classList.add('user-name');

        const message = document.createElement('p');
        message.textContent = item.messageSent;
        message.classList.add('message');

        list.appendChild(userName);
        list.appendChild(message);

        console.log(list);
    })  
})
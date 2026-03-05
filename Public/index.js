"use strict";

const userName = document.getElementById('user-name');
const message = document.getElementById('message');

window.addEventListener('load', async () => {
    const response = await fetch('/api/messages');
    const data = await response.json();
    console.log(data);

    for (const item of data) {
        console.log(item);
    }

    
})


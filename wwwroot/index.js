"use strict";

const messageInput = document.getElementById("message-input");
const nameInput = document.getElementById("name-input");

window.addEventListener("load", async () => {
  await getMessages("no");
});

// window.addEventListener("keypress", async (event) => {
//   if (event.key === "Enter") {
//     event.preventDefault();
//     await sendMessage();
//   }
// });

async function sendMessage() {
  const message = messageInput.value;
  let name = nameInput.value.trim()
  
  if (name === "") {
    name = "Anonymous";
  }

  if (message && name) {
    const response = await fetch("/api/messages", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ user: name, messageSent: message }),
    });
  }
  
  messageInput.value = '';
  nameInput.value = '';
}

async function getMessages(pollHeader) {
  const response = await fetch("/api/messages", {
    headers: {
      "x-poll": pollHeader || "yes",
    },
  });

  const data = await response.json();
  const list = document.getElementById("message-container");

  list.innerHTML = "";

  data.forEach((item) => {
    const listItem = document.createElement("div");
    listItem.classList.add("message-item");
    const userName = document.createElement("p");
    userName.textContent = item.user;
    userName.classList.add("user-name");

    const message = document.createElement("p");
    message.textContent = item.messageSent;
    message.classList.add("message");

    listItem.appendChild(userName);
    listItem.appendChild(message);
    list.appendChild(listItem);

    console.log(list);
  });
}

async function pollMessages() {
  while (true) {
    await getMessages();
  }
}

pollMessages();

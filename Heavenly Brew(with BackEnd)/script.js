let cart = JSON.parse(localStorage.getItem("cart")) || [];

function changeQty(name, price, delta) {
    let item = cart.find(i => i.Name === name);
    if (!item) {
        item = { Name: name, Price: price, Qty: 0 };
        cart.push(item);
    }
    item.Qty += delta;
    if (item.Qty <= 0) { 
        cart = cart.filter(i => i.Name !== name); 
        item.Qty = 0; 
    }
    localStorage.setItem("cart", JSON.stringify(cart));
    let el = document.getElementById("q_" + name);
    if (el) el.innerText = item.Qty;
}

async function sendOrder() {
    let orderData = {
        CustomerName: localStorage.getItem("name"),
        TableNumber: localStorage.getItem("tableNum") + " (" + (localStorage.getItem("orderType") || "N/A") + ")",
        Items: cart,
        Total: cart.reduce((s, i) => s + (i.Price * i.Qty), 0)
    };

    console.log("Sending Data:", orderData); // Debug check

    try {
        const response = await fetch("http://localhost:5267/orders", { // CLEANED URL
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(orderData)
        });

        if (response.ok) {
            window.location.href = "payment.html";
        } else {
            console.error("Server Error:", response.status);
            alert("Server error. Check your Backend console.");
        }
    } catch (e) { 
        console.error("Connection Error:", e);
        alert("Cannot reach server! Check your IP or Firewall."); 
    }
}

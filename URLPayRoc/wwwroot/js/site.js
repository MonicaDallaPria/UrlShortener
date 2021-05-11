var submitBtn = document.querySelector("#submit");
var urlInput = document.querySelector("#urlshort");

submitBtn.onclick = function (ev) {
    let url = urlInput.value;
    fetch("/", {
        method: "POST",
        body: JSON.stringify(url),
        headers: { 'Content-Type': 'application/json' }
    }).then(response => response.json())
        .then(data => {
            document.querySelector("#resultText").href = url;
            document.querySelector("#resultText").innerHTML = data;
        })
}

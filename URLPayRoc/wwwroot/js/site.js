var submitBtn = document.querySelector("#submit");
var urlInput = document.querySelector("#urlshort");
var oldUrl = "";
submitBtn.onclick = function (ev) {
    let url = urlInput.value;
    fetch("/", {
        method: "POST",
        body: JSON.stringify(url),
        headers: { 'Content-Type': 'application/json' }
       
    }).then(res => res.json())
        .then(response => {
            oldUrl = response;
            console.log(oldUrl);
            document.querySelector("#resultText").href = url;
            document.querySelector("#resultText").innerHTML = oldUrl;
        }
    )}

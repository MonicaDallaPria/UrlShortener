
var submitBtn = document.querySelector("#submit");
var urlInput = document.querySelector("#urlshort");
submitBtn.onclick = function (ev) {
    let url = urlInput.value;
    fetch("/", {
        method: "POST",
        body: JSON.stringify(url),
        headers: { 'Content-Type': 'application/json' }
       
    }).then(res => res.json())
        .then(response => {
            console.log(response);
        }
       ) }

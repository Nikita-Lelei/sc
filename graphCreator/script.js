window.onload = function load() {
    render();
};

var nodes = [{
    x: 100,
    y: 80,
    linkedWith: [
        1,
        2,
        3
    ]
}, {
    x: 100,
    y: 300,
    linkedWith: []
}, {
    x: 300,
    y: 400,
    linkedWith: []
},
{
    x: 500,
    y: 400,
    linkedWith: []
}];

function render() {
    var canvas = document.getElementById('viewport');
    var context = canvas.getContext('2d');
    context.fillStyle = "#f3f3f3";
    context.fillRect(0, 0, canvas.width, canvas.height);
    nodes.forEach(node => { createNode(node) });
}

function connectNodes(node) {
    var context = document.getElementById('viewport').getContext('2d');
    node.linkedWith.forEach(link => {
        context.fillStyle = "#000";
        context.lineWidth = 2;
        context.beginPath();
        context.moveTo(node.x, node.y);
        context.lineTo(nodes[link].x, nodes[link].y);
        context.stroke();
    });
}

function createNode(node) {
    var context = document.getElementById('viewport').getContext('2d');
    var img = new Image();
    connectNodes(node);
    img.onload = function () {
        context.drawImage(img, node.x - img.naturalWidth / 2, node.y - img.naturalHeight / 2);
    }
    img.src = "images/" + nodes.indexOf(node) + ".png"
}

function addNode() {
    var canvas = document.getElementById('viewport');
    var inputX = document.getElementById("inputX").value;
    var inputY = document.getElementById("inputY").value;
    var inputLink = document.getElementById("inputLink").value.split(",");

    if (inputX >= 0 && inputX <= canvas.width &&
        inputY >= 0 && inputY <= canvas.height) {
        nodes.push({
            x: inputX,
            y: inputY,
            linkedWith: inputLink
        });
    } else {
        alert("Incorrect value");
    }
    render();
}


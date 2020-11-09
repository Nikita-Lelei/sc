window.onload = function load() {
    render();
};

var nodes = [{
    x: 300,
    y: 300,
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
            if (link == "") {
                return false;
            }
            context.fillStyle = "#000";
            context.lineWidth = 2;
            context.beginPath();
            context.moveTo(node.x, node.y);
            context.lineTo(nodes[link].x, nodes[link].y);
            context.stroke();
        });
   
}
//тут много странных костылей
function checkInput(node) {
    for (var i = 0; i < node.linkedWith.length; i++) {
        if(isNaN(Number(node.linkedWith[i]))) {
            alert("incorrect value");
            return false;}
        
        if (node.linkedWith[i] >= nodes.length-1 || node.linkedWith[i] < 0) {
            alert("incorrect value");

            return false;
        }

    }
    return true;
}
function createNode(node) {
    var context = document.getElementById('viewport').getContext('2d');
    var img = new Image();

    if ( checkInput(node) !== false) {
        connectNodes(node);
        img.onload = function () {
            context.drawImage(img, node.x - img.naturalWidth / 2, node.y - img.naturalHeight / 2);
        }
        
        img.src = "images/" + nodes.indexOf(node) + ".png"
    }

    else {
        nodes.pop();
    nodeCount--;
    alphaAngle += 30;
        return false;
    }
}

var hypotenuse = 150;
var nodeCount = 0;
var alphaAngle = 160;

function addNode() {
    //тут при помощи формулы нахождения катетов при гипотенузе и 
    //углу идет расчет относительно каждой вершины на какое расстояние ее удалить от предыдущей
    //вершины добавляются по принципу кругового расположения графа
    var inputLink = document.getElementById("inputLink").value.split(",");
    var rad = alphaAngle * Math.PI / 180;
    var catA = Math.cos(rad) * hypotenuse;
    var catB = Math.sin(rad) * hypotenuse;

    if (nodeCount < 10) {
        nodes.push({
            x: nodes[nodeCount].x + catB,
            y: nodes[nodeCount].y + catA,
            linkedWith: inputLink
        });
        render();
        alphaAngle -= 30;
        nodeCount++;
    } else {
        return false;
    }
}



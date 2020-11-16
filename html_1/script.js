var numberArray = [];
var primeNumbers = [];

function createArray(size) {
    for (let i = 0; i <= size; i++) {
        numberArray[i] = i;
    }
    primeNumbers = numberArray.slice();

}

function createTable(size) {
    createArray(size);
    var sizeSqrt = Math.sqrt(size);
    var raws = Math.ceil(sizeSqrt);
    var columns = Math.round(sizeSqrt)
    var tableRef = document.getElementById("result");
    var newRow;
    var newCell;
    var newText;
    var count = 1;
    for (let i = 0; i < raws; i++) {
        newRow = tableRef.insertRow();
        for (let j = 0; j < columns; j++) {
            newCell = newRow.insertCell();
            if (count <= size) {
                newText = document.createTextNode(count);
                newCell.appendChild(newText);
            }
            count++;
        }
    }
    eratosfen();
}

function deleteNumber(arr, number) {
    for (let i = 0; i < arr.length; i++) {
        if (arr[i] == number) {
            arr.splice(i, 1)
        }
    }
}

async function eratosfen() {
    for (let i = 2; Math.pow(primeNumbers[i], 2) <= numberArray.length; i++) {
        await findPrimeNumber(primeNumbers[i])
    }
}

var changes_ = new Set();
var changes = [];
var styleCount = 0;

async function changeBG(arr) {
    return new Promise((resolve, reject) => {
        var index = 0;
        var result = document.getElementById("result").getElementsByTagName("td");
        function useStyle() {
            if (index < arr.length && arr.length !== null) {
                if (result[arr[index]].style.background == "") {
                    switch (styleCount) {
                        case 1: result[arr[index]].style.background = "red"; break;
                        case 2: result[arr[index]].style.background = "green"; break;
                        case 3: result[arr[index]].style.background = "navy"; break;
                        case 4: result[arr[index]].style.background = "orange"; break;
                        case 5: result[arr[index]].style.background = "purple"; break;
                        case 6: result[arr[index]].style.background = "pink"; break;
                        case 7: result[arr[index]].style.background = "blue"; break;
                        default: result[arr[index]].style.background = "yellow"; break;
                    }
                }
                index++;
            } else {
                clearInterval(q);
                resolve(0);
            }
        }
        styleCount++;
        var q = setInterval(useStyle, 100);
    })

}

async function findPrimeNumber(p) {

    for (let i = Math.pow(p, 2); i < numberArray.length; i += p) {
        changes_.add(i - 1)
        deleteNumber(primeNumbers, i);
    }
    changes = Array.from(changes_);
    changes_.clear();
    await changeBG(changes);
    changes = [];
}
function refresh() {
    document.getElementById("result").innerHTML = "";
    styleCount = 0;
    changes_.clear();
    changes = [];
     numberArray = [];
primeNumbers = [];
}

function completeTask() {
    refresh();
    var input = +prompt("enter number (100k max):");
    if (typeof input === 'number' && isFinite(input) && input <=100000) {
        createTable(input);
    } else {
        alert("incorrect value");
        completeTask();
    }
}


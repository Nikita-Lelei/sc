async function crashScreen__await() {
var img = document.createElement('img');
img.src = await imgAfterSomeTime();
img.className = "crash-image";
document.body.append(img);
await deleteImageAfterSomeTime(img);

  }

  function imgAfterSomeTime(){
    return new Promise(resolve=> {
        setTimeout(() => {
            resolve('1.png');
        }, 3000);
    } )
  }

  function deleteImageAfterSomeTime(img) {
    return new Promise(resolve=> {
setTimeout(() => {
    img.remove();
    resolve();
}, 3000);
    } )
  }

async function crashScreen__then() {

    new Promise(function (resolve, reject) {
        setTimeout(() => resolve('1.png'), 3000);
    }).then(function (imageSrc) {
        var img = document.createElement('img');
        img.src = imageSrc;
        img.className = "crash-image";
        document.body.append(img);
        return img;
    }).then(function (img) {
        setTimeout(() => img.remove(), 3000)
    });

}

//вывести граф в канвас


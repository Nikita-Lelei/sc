
function setCensure(message) {
   const censure = '*';
   const badWords = ['черный', 'пиво', 'алко']
   const res = badWords.map((word) => {
      return message.indexOf(word)
   })
   return {
      string: message.replace(new RegExp(badWords.join('|'), 'gi'), word => censure.repeat(word.length)),
      wasNormal: res.every((index) => index === -1) ? true : false
   }
}

export default setCensure
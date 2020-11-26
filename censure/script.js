const messageWithBadWords = 'черный покупал пиво в алкомаркете';
const messageWithoutBadWords = 'жизнь не ролик из интернета - назад не отмотаешь'
 
import setCensure from "./censure.js";

console.log(setCensure(messageWithBadWords));
console.log(setCensure(messageWithoutBadWords));

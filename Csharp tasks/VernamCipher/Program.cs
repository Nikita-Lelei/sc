using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VernamCipher
{
    class Program
    {
        public static string Xor(string text,byte[] key,int a)
        {
   
            byte[] result = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (byte)(text[i] ^ key[i+a]);
                // Console.WriteLine($" xor {text[i]} with {key[i]} result {result[i]}");  
            }
            //  Console.WriteLine("after xor "+Encoding.ASCII.GetString(result));
             return BinaryToString(Encoding.ASCII.GetString(result));
        }

     public static string Crypt(string messageToCrypt, byte[] keyBytes)
        {
            string message = ToBinary(messageToCrypt);
            byte[] key = new byte[message.Length];
            string[] messageBytes = new string[message.Length];
            string result = "";

            CreateKey(0, key, keyBytes, message.Length);

            for (var i = 0; i < message.Length/8; i++)
            {
                messageBytes[i] = message.Substring(i * 8, 8);
               // Console.WriteLine("mess bin "+testarr[i]);
            }

            for (var i = 0; i <messageBytes.Length/8; i++)
            {
                result += Xor(messageBytes[i],key,i*8);
            }

            return result;
        }
    
        public static string ToBinary(string data)
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static void CreateKey(int num, byte[] key, byte[] keyBytes, int finalLenght)
        {
            if(num < finalLenght)
            {
                for (int i = 0; i < keyBytes.Length; i++)
                {
                    if(num < finalLenght)
                    {
                        key[num] = keyBytes[i];
                      
                        num++;
                    }
                    else
                    { 
                        break;
                    }
                }
               
              CreateKey(num, key, keyBytes, finalLenght);
            }
        
          
        }

        public static string BinaryToString(string binaryString)
        {  
            var bytes = new byte[binaryString.Length / 8];
            var ilen = (int)(binaryString.Length / 8);
   
            for (var i = 0; i < ilen; i++)
            {
                bytes[i] = Convert.ToByte(binaryString.Substring(i * 8, 8), 2);
             
            }

            return Encoding.ASCII.GetString(bytes);
        }
        static void Main(string[] args)
        {
            string message = "message";

            byte[] keyBytes = { 0, 1, 1, 0,0, 0,0};

            //////////////////////////////////////////////////////
            //01100000 11000001 10000011 00000110 00001100 00011000 00110000  ключ
            //01101101 01100101 01110011 01110011 01100001 01100111 01100101  2ич сообщения
            //   m        e        s         s        a        g        e       
            //00001101 10100100 11110000 01110101 01101101 01111111 01010101 после ксора
            //я считаю что выдает неизвестные знаки так как мы ксорим с несуществующими символами, 
            //которые имеют совсем другой двочиный код, если перевести еще раз,
            //ибо ключ создается циклично,
            //но если ввести ключ с 2ич кодом существующего символа то все будет норм
            //код с обычным ключем на 8 байт:   
            ///////////////////////////////////////////////////////
            //
            //  byte[] normalKey = { 0, 1, 1, 1, 0, 0, 1, 1 };
            //  Console.WriteLine("encrypt______________");
            //  var encrypted2 = Crypt(message, normalKey);
            //  Console.WriteLine(encrypted2);
            //  Console.WriteLine("decrypt______________");
            //  Console.WriteLine(Crypt(encrypted2, normalKey));
            //
            ////////////////////////////////////////////////////////

            Console.WriteLine("encrypt______________");
            var encrypted = Crypt(message, keyBytes);
            Console.WriteLine(encrypted);
            Console.WriteLine("decrypt______________");
            Console.WriteLine(Crypt(encrypted, keyBytes));
            Console.ReadLine();
        }
    }
}

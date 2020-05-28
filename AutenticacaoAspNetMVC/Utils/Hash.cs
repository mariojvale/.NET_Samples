using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AutenticacaoAspNet.Utils
{
    public class Hash
    {

        public static string GerarHash(string texto)//metodo que recebe um texto e gera um Hash a partir dele, utilizando SHA256
        {
            SHA256 sHA256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(texto);//converte o texto para bytes
            byte[] hash = sHA256.ComputeHash(bytes);//gera o hash a partir dos bytes convertidos, retornando um array de bytes
            StringBuilder result = new StringBuilder();//classa para concatenar diversos strings

            for (int i = 0; i < hash.Length; i++)//percorre o array de bytes
            {
                result.Append(hash[i].ToString("X"));//gera a string convertendo o array de bytes para algarismo alfanumerico Maiusculo
            }

            return result.ToString();
        }

    }
}
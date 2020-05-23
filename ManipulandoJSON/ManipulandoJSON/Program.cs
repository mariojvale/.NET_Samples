using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ManipulandoJSON
{
    class Program
    {
        static void Main(string[] args)
        {

            ConverterObjetoParaJSON();
            Console.WriteLine("----------------------------");
            ConverterJsonParaObjeto();
            Console.WriteLine("----------------------------");
            SalvarObjetoEmArquivo();
            Console.WriteLine("Arquivo Gerado com Sucesso!");
            Console.WriteLine("----------------------------");
            Console.WriteLine("----------------- LEITURA DO ARQUIVO ABAIXO ---------------");
            LerObjetoEmArquivo();
        }

        static void ConverterObjetoParaJSON()
        {
            Topico topico = new Topico
                (
                1,
                "Vingadores Ultimato",
                "Em Vingadores: Ultimato, após Thanos eliminar metade das criaturas vivas, " +
                "os Vingadores precisam lidar com a dor da perda de amigos e seus entes queridos. " +
                "Com Tony Stark (Robert Downey Jr.) vagando perdido no espaço sem água nem comida, " +
                "Steve Rogers (Chris Evans) e Natasha Romanov (Scarlett Johansson) precisam liderar a resistência contra o titã louco.",
                "Mario Vale"
                );

            string json = JsonConvert.SerializeObject(topico);

            Console.WriteLine(json);

        }

        static void ConverterJsonParaObjeto()
        { 
           string json = "{" +
                          " 'Id':1, " +
                          " 'Titulo':'Vingadores Ultimato'," +
                          " 'Sinopse':'Em Vingadores: Ultimato, após Thanos eliminar metade das criaturas vivas, " +
                          "os Vingadores precisam lidar com a dor da perda de amigos e seus entes queridos. " +
                          "Com Tony Stark (Robert Downey Jr.) vagando perdido no espaço sem água nem comida, " +
                          "Steve Rogers (Chris Evans) e Natasha Romanov (Scarlett Johansson) precisam liderar a resistência contra o titã louco.'," +
                          " 'Usuario':'Mario Vale',}";

            Topico topico = JsonConvert.DeserializeObject<Topico>(json);

            Console.WriteLine($"{topico.Id}\n{topico.Titulo}\n{topico.Sinopse}\n{topico.Usuario}");

        }

        static void SalvarObjetoEmArquivo()
        {
            List<Topico> topicos = new List<Topico>();

            topicos.Add(new Topico(1, "Dark", "viagem no tempo", "Jonas"));
            topicos.Add(new Topico(2, "Stranger Things", "Dimensao Paralela", "Eleven"));
            topicos.Add(new Topico(3, "Friends", "Reuniao de Amigos", "Chandler"));

            StreamWriter stream = new StreamWriter("D:\\Projetos\\Samples\\Series.json"); //Cria o arquivo no local indicado
            JsonTextWriter jsonText = new JsonTextWriter(stream); //Escreve o conteudo do objeto em JSON
            JsonSerializer jsonSerializer = new JsonSerializer(); // Serializacao do arquivo em JSON

            jsonText.Formatting = Formatting.Indented; //Formata o arquivo JSON

            jsonSerializer.NullValueHandling = NullValueHandling.Ignore; //Funcao para ignorar valores nulos caso hajam
            jsonSerializer.DefaultValueHandling = DefaultValueHandling.Ignore; // Funcao para ignorar valores Default(ex:data, decimal)

            jsonSerializer.Serialize(jsonText, topicos);

            stream.Close();
        }

        static void LerObjetoEmArquivo()
        {
            StreamReader stream = new StreamReader("D:\\Projetos\\Samples\\SeriesNovas.json"); //Le o conteudo do arquivo
            JsonTextReader jsonText = new JsonTextReader(stream); //Le o conteudo do strem em JSON
            JsonSerializer jsonSerializer = new JsonSerializer();// Serializacao do arquivo em JSON

            List<Topico> topicos = jsonSerializer.Deserialize<List<Topico>>(jsonText); //Le o arquivo e formata em List

            foreach (var n in topicos)
            {
                Console.WriteLine(n);
            }
            stream.Close();
        }


    }
}

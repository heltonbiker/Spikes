using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace SQLiteHelloWorld
{
    class Program {                

        // http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/

        static void Main(string[] args) {

            SQLiteCommand comando;

            // CONSTRUINDO A STRING PRA CRIAR A CONEXÃO
            var builder = new SQLiteConnectionStringBuilder();
            builder.Add("Data Source", "c:/Miotec/Vert3d/temp/sqlitehelloworld.sqlite");
            builder.Add("DateTimeKind", "Local");
            builder.Add("FailIfMissing", "False");
            //builder.Add("Read Only", "True");



            // CRIANDO A CONEXÃO
            var conexao = new SQLiteConnection(builder.ConnectionString);
            
            
            // CRIANDO A TABELA E INSERINDO ALGUNS DADOS
            conexao.Open();
            RodaComando("create table pacientes (nome varchar(50), peso int)", conexao);

            RodaComando("insert into pacientes (nome, peso) values ('Helton', 86)", conexao);
            RodaComando("insert into pacientes (nome, peso) values ('Tiago', 76)", conexao);
            RodaComando("insert into pacientes (nome, peso) values ('Vinicius', 100)", conexao);

         

            // SELECIONANDO DADOS DE ACORDO COM CRITÉRIOS
            comando = new SQLiteCommand("select * from pacientes where peso > 80", conexao);
            SQLiteDataReader reader = comando.ExecuteReader();



            // FAZENDO ALGUMA COISA COM CADA UM DOS DADOS RETORNADOS PELA QUERY
            while (reader.Read())
                   Console.WriteLine("Name: " + reader["nome"] + "\t" +
                                     "Weight: " + reader["peso"]);

            conexao.Close();
        }



        private static void RodaComando(String string_comando, SQLiteConnection conexao)
        {
            var comando = new SQLiteCommand(string_comando, conexao);
            comando.ExecuteNonQuery();
        }
    }
}

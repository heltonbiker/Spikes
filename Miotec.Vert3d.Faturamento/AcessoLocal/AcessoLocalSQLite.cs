using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.IO;

namespace Miotec.Vert3d.Faturamento
{
    internal class AcessoLocalSQLite : IFaturamentoAcessoLocal {

        string CaminhoArquivoLocal { get { return "c:/Miotec/Vert3d/Config/registro.sqlite"; } }

        SQLiteConnection Conexao { get; set; }

        public List<ColetasFeitas> listaColetas { get; set; }
        
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);



        // CONSTRUTOR
        internal AcessoLocalSQLite () {

            listaColetas = new List<ColetasFeitas>();

            // verificar se tem banco, e se não tiver, criar
                var builder = new SQLiteConnectionStringBuilder();
                builder.Add("Data Source", CaminhoArquivoLocal);
                builder.Add("DateTimeKind", "Local");
                builder.Add("FailIfMissing", "False");

                Conexao = new SQLiteConnection(builder.ConnectionString);
                Conexao.Open();

                // esquema do nosso banco:
                //     - datetime horacoleta
                //     - varchar nomepaciente
                //     - (listinha) statusconclusao
                //     - (listinha) statussincronizacao
                
                // CRIA A TABELA DE REGISTROS.
                var commandsb = new StringBuilder("create table if not exists registros (");
                commandsb.Append("horacoleta int, ");
                commandsb.Append("nomepaciente varchar(100), "); // PRECISA TANTOS CARACTERES?
                commandsb.Append("statusconclusao bool, ");
                commandsb.Append("statussincronizacao bool");
                commandsb.Append(")");
                var comando = new SQLiteCommand(commandsb.ToString(), Conexao);                    
                comando.ExecuteNonQuery();
               
                //CRIA A TABELA DE ULTIMA CONEXAO FEITA.
                var comandoconexaosb = new StringBuilder("create table if not exists dataConexoes(");
                comandoconexaosb.Append("dataconexao int,PRIMARY KEY(dataconexao))");
                var comandoconexao = new SQLiteCommand(comandoconexaosb.ToString(), Conexao);
                comandoconexao.ExecuteNonQuery();

                AdicionaPacientesSQLite();
         

        }

        public void AdicionaPacientesSQLite()
        {
            SQLiteCommand insert = new SQLiteCommand("INSERT INTO registros(horacoleta, nomepaciente, statusconclusao, statussincronizacao) VALUES (@dataunix, 'Helton', 0, 0)", Conexao);
            insert.Parameters.Add(new SQLiteParameter("@dataunix", GetUnixTimeFromDateTime(DateTime.UtcNow))); 
            insert.ExecuteNonQuery();
        }

        public bool PossuiExamesPendentes()
        {
            var comando = new SQLiteCommand("select * from registros where statussincronizacao = 0", Conexao);
            SQLiteDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                listaColetas.Add(new ColetasFeitas(reader.GetString(1), booleanFromInt(reader.GetInt32(2)), reader.GetInt32(0)));
            }
            if (listaColetas.Count > 0)
            {
                return true;
            }
            
            return false;

        }

        public void AtualizaDataUltimaConexao()
        {
            
            // DESIGN: data da última conexão vai salva em uma lista de conexões feitas
            // por isso, "AtualizaDataUltimaConexao" poderia ser renomeado para
            // "InserirConexãoFeita" ou alguma coisa assim

            var builder = new SQLiteConnectionStringBuilder();
            builder.Add("Data Source", CaminhoArquivoLocal);
            builder.Add("DateTimeKind", "Local");
            builder.Add("FailIfMissing", "False");


            var commandsb = new StringBuilder("UPDATE registros SET statussincronizacao = 1 WHERE statussincronizacao = 0");
            var comando = new SQLiteCommand(commandsb.ToString(), Conexao);
            comando.ExecuteNonQuery();

            var comandoconexao = new SQLiteCommand("INSERT INTO dataConexoes(dataconexao) VALUES (@dataunix)", Conexao);
            comandoconexao.Parameters.Add(new SQLiteParameter("@dataunix", GetUnixTimeFromDateTime(DateTime.UtcNow)));
            comandoconexao.ExecuteNonQuery();
            
        }

        public DateTime UltimaConexao {
            get
            {
                var comando = new SQLiteCommand("SELECT * FROM dataConexoes WHERE ID = (SELECT MAX(ID) FROM dataConexoes)", Conexao);
                SQLiteDataReader reader = comando.ExecuteReader();
                return GetDateTimeFromUnixTime(reader.GetInt32(0));           
            }
        }

        private bool booleanFromInt(int valor)
        {
            if (valor == 1)
            {
                return true;
            }
            else if (valor == 0)
            {
                return false;
            }
            else
            {
                throw new Exception("Valor invalido");
            }
        }

        public static long GetUnixTimeFromDateTime(DateTime data)
        {
            return (long)(data - UnixEpoch).TotalSeconds;
        }

        public static DateTime GetDateTimeFromUnixTime(int seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }
    }
}

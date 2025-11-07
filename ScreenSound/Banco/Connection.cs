using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ScreenSound.Banco
{
    internal class Connection
    {
        private string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = ScreenSound; Integrated Security = True; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(connectionString);
        }      

        public IEnumerable<Artista> Listar() {
            var lista = new List<Artista>();

            //using var connection, que vai chamar o método ObterConexao()
            using var connection = ObterConexao();

            //chamaremos connection.Open() para abrir a nossa conexão.
            connection.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(sql, connection);
            //command.ExecuteReader(): Executando a Consulta
            //SqlDataReader: Para navegar pelos resultados. O Leitor de Dados
            //dataReader : Ele permite que você leia linha por linha
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string nomeArtista = Convert.ToString(dataReader["Nome"]);
                string bioArtista = Convert.ToString(dataReader["Bio"]);
                int idArtista = Convert.ToInt32(dataReader["id"]);

                Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };
                lista.Add(artista);
            }

            return lista;
        }





    }
}

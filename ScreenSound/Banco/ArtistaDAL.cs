using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();

            //using var connection, que vai chamar o método ObterConexao()
            using var connection = new Connection().ObterConexao();

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


        public void Adicionar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");

        }

    }
}

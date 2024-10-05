using System.Data;
using System.Data.SqlClient;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly string _connectionString;

        // Construtor
        public NoteRepository(IConfiguration configuration)
        {
            // Verifica se a configuração é nula e obtém a string de conexão
            _connectionString = configuration?.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void AddNote(Note note)
        {
            string query = "INSERT INTO dbo.Notes (Description, CreatedAt) VALUES (@Description, @CreatedAt)";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, myCon);
                cmd.Parameters.AddWithValue("@Description", note.Description);
                cmd.Parameters.AddWithValue("@CreatedAt", note.CreatedAt);

                myCon.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteNote(int id)
        {
            string query = "DELETE FROM dbo.Notes WHERE Id = @Id";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, myCon);
                cmd.Parameters.AddWithValue("@Id", id);

                myCon.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Note? GetNoteById(int id)
        {
            string query = "SELECT * FROM dbo.Notes WHERE Id = @Id";
            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, myCon);
                cmd.Parameters.AddWithValue("@Id", id);

                myCon.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Note
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        // Usando 'as' para conversão e verificando nulos
                        Description = reader["Description"] as string ?? string.Empty,
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                }
            }
            return null; // Retorna null se a nota não for encontrada
        }


        public IEnumerable<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            string query = "SELECT * FROM dbo.Notes";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, myCon);
                myCon.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Note note = new Note
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Description = reader["Description"]?.ToString() ?? string.Empty,
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                    notes.Add(note);
                }
            }
            return notes;
        }

        public void UpdateNote(Note note)
        {
            string query = "UPDATE dbo.Notes SET Description = @Description WHERE Id = @Id";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, myCon);
                cmd.Parameters.AddWithValue("@Id", note.Id);
                cmd.Parameters.AddWithValue("@Description", note.Description);

                myCon.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

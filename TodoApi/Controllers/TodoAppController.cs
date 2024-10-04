using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TodoApi.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoAppController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TodoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetNotes")]
        public IActionResult GetNotes()
        {
            string query = "SELECT * FROM dbo.Notes";
            DataTable table = new DataTable();
            // Recupera a string de conexão do appsettings.json ou lança uma exceção caso seja null
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


            // Verifica se a string de conexão não é nula ou vazia
            if (string.IsNullOrEmpty(sqlDatasource))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        using (SqlDataReader myReader = myCommand.ExecuteReader())
                        {
                            table.Load(myReader);  // Preenche a tabela com os dados
                            myReader.Close();
                        }
                    }
                }
                return new JsonResult(table);
            }
            catch (Exception ex)
            {
                // Retorna uma resposta de erro com o detalhe da exceção (apenas em ambiente de desenvolvimento)
                return StatusCode(500, $"Erro ao buscar notas: {ex.Message}");
            }
        }

        // POST - Adiciona uma nova nota
        [HttpPost]
        [Route("AddNote")]
        public IActionResult AddNote([FromBody] string description)
        {
            string query = @"INSERT INTO dbo.Notes (description) VALUES (@description)";

            // Recupera a string de conexão do appsettings.json ou lança uma exceção caso seja null
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@description", description);
                        myCommand.ExecuteNonQuery();
                    }
                }
                return Ok("Nota adicionada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar a nota: {ex.Message}");
            }
        }

        // PUT - Atualiza uma nota existente
        [HttpPut]
        [Route("UpdateNote/{id}")]
        public IActionResult UpdateNote(int id, [FromBody] string description)
        {
            string query = @"UPDATE dbo.Notes SET description = @description WHERE id = @id";

            // Recupera a string de conexão do appsettings.json ou lança uma exceção caso seja null
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id", id);
                        myCommand.Parameters.AddWithValue("@description", description);
                        myCommand.ExecuteNonQuery();
                    }
                }
                return Ok("Nota atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar a nota: {ex.Message}");
            }
        }

        // DELETE - Remove uma nota
        [HttpDelete]
        [Route("DeleteNote/{id}")]
        public IActionResult DeleteNote(int id)
        {
            string query = @"DELETE FROM dbo.Notes WHERE id = @id";

            // Recupera a string de conexão do appsettings.json ou lança uma exceção caso seja null
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id", id);
                        myCommand.ExecuteNonQuery();
                    }
                }
                return Ok("Nota removida com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover a nota: {ex.Message}");
            }
        }
    }
}

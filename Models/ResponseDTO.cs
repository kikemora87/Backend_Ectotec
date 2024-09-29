namespace Maqueta_Backend_EctoTec.Models
{
    public class ResponseDTO
    {
        public List<Project> Projects { get; set; } = new List<Project>();

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }
    }
}

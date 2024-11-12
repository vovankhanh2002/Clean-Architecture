namespace eComm.Application.DTOs.Identites
{
    public class CreateUser : BaseModel
    {
        public required string Fullname { get; set; }
        public required string ComfirmPassword { get; set; }
    }
}

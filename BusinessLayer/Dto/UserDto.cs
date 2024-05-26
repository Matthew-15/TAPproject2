namespace BusinessLayer.Dto
{
    public class UserDto : BaseEntityDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool Student { get; set; }


        public UserDto() { }
        public UserDto(string Username, string Email, string Password, bool IsStudent) : base()
        {
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.Active = true;
            this.Student = true;
        }


        public void activateUser()
        {
            this.Active = true;
        }

        public void deactivateUser()
        {
            this.Active = false;
        }

        public void SetIsStudent(bool Student)
        {
            this.Student = Student;
        }


    }
}

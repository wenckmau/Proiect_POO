namespace AutoServiceApp
{
    public abstract class User
    {
        private string _CodUnic;
        private string _Nume;
        private string _Prenume;
        private string _Email;
        private string _Parola;
        private UserRole _userRole;
        public string CodUnic { get=> _CodUnic; set=>_CodUnic=value; }
        public string Nume { get=>_Nume; set=>_Nume=value; }
        public string Prenume { get=>_Prenume; set=>_Prenume=value; }
        public string Email { get=>_Email; set=>_Email=value; }
        public string Parola { get=>_Parola; set=>_Parola=value; }
        public UserRole Role { get=>_userRole; set=>_userRole=value; }
      

        protected User(string codUnic, string nume, string prenume, string email, string parola, UserRole role)
        {
            CodUnic = codUnic;
            Nume = nume;
            Prenume = prenume;
            Email = email;
            Parola = parola;
            Role = role;
        }
        
    }

    public enum UserRole
    {
        Admin,
        Mechanic
    }
    
}
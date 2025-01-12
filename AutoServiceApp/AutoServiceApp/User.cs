namespace AutoServiceApp
{
    public abstract class User
    {
        public string CodUnic { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }

        protected User(string codUnic, string nume, string prenume, string email, string parola)
        {
            CodUnic = codUnic;
            Nume = nume;
            Prenume = prenume;
            Email = email;
            Parola = parola;
        }
    }
}
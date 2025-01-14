namespace AutoServiceApp
{
    public class Administrator : User
    {
        public Administrator(string codUnic, string nume, string prenume, string email, string parola)
            : base(codUnic, nume, prenume, email, parola, UserRole.Admin) { }

     public override void AdaugaCerere(AutoService autoService, User utilizatorAutentificat)
        {
            Console.Write("Introduceți numele clientului: ");
            var numeClient = Console.ReadLine();
            Console.Write("Introduceți numărul mașinii: ");
            var numarMasina = Console.ReadLine();
            Console.Write("Introduceți descrierea problemei: ");
            var descriereProblema = Console.ReadLine();

            var cerere = new CerereRezolvare(Guid.NewGuid().ToString(), numeClient, numarMasina, descriereProblema, RequestStatus.InPreluare);

            autoService.AddRequest(cerere, utilizatorAutentificat);
            Console.WriteLine("Cerere adăugată cu succes.");
        }

        public override void VizualizeazaCereri(AutoService autoService, User utilizatorAutentificat)
        {
            var cereri = autoService.GetCereri();
            foreach (var cerere in cereri)
            {
                Console.WriteLine($"Cerere: {cerere.CodUnic}, Status: {cerere.Status}, Client: {cerere.NumeClient}");
            }
        }

        public override void AdaugaComandaPiese(AutoService autoService, User utilizatorAutentificat)
        {
            Console.WriteLine("Doar mecanicii pot adăuga comenzi de piese.");
        }

        public override void VizualizeazaComenziPiese(AutoService autoService, User utilizatorAutentificat)
        {
            var comenziPiese = autoService.GetPartOrders();
            foreach (var comanda in comenziPiese)
            {
                Console.WriteLine($"Comanda: {comanda.Avb}, Status: {comanda.Status}, Detalii: {comanda.DetaliiPiese}");
            }
        }

        public override void FinalizeazaComandaPiese(AutoService autoService, User utilizatorAutentificat)
        {
            Console.Write("Introduceți ID-ul comenzii de piese: ");
            var idComandaPiese = int.Parse(Console.ReadLine());

            var comanda = autoService.GetPartOrders().FirstOrDefault(c => c.Avb == idComandaPiese);
            if (comanda != null)
            {
                comanda.Status = PartOrderStatus.Finalizat;
                Console.WriteLine("Comanda de piese finalizată cu succes.");
            }
            else
            {
                Console.WriteLine("Comanda de piese nu a fost găsită.");
            }
        }
        public override void LogIn(AutoService autoService)
        {
            var user = autoService.Authenticate(this.Email, this.Parola);
            if (user != null)
            {
                Console.WriteLine("Authentication successful.");
            }
            else
            {
                Console.WriteLine("Authentication failed.");
            }
        }
        public override void AddUser(AutoService autoService)
        {
            Console.Write("Introduceti codul unic: ");
            var code = Console.ReadLine();
            Console.Write("Introduceti prenumele: ");
            var firstName = Console.ReadLine();
            Console.Write("Introduceti numele: ");
            var lastName = Console.ReadLine();
            Console.Write("Introduceti email: ");
            var email = Console.ReadLine();
            Console.Write("Introduceti parola: ");
            var password = Console.ReadLine();

            var admin = new Administrator(code, firstName, lastName, email, password);
            autoService.AddUser(admin);
            Console.WriteLine("Administrator adăugat cu succes.");
        }
        
    }
}
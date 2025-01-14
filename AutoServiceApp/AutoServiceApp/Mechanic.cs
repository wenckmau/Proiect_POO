using System.Text.Json.Serialization;
namespace AutoServiceApp
{
    public class Mechanic : User
    {
        private CerereRezolvare currentRequest;
        [JsonConstructor]
        public Mechanic(string codUnic, string nume, string prenume, string email, string parola)
            : base(codUnic, nume, prenume, email, parola, UserRole.Mechanic){}
       

        public void PreiaCerere(AutoService autoService)
        {
            if (currentRequest != null)
            {
                Console.WriteLine("You already have an ongoing request.");
                return;
            }

            var cerere = autoService.GetCereri().FirstOrDefault(c => c.Status == RequestStatus.InPreluare);
            if (cerere != null)
            {
                currentRequest = cerere;
                currentRequest.Status = RequestStatus.InPreluare;
                Console.WriteLine("Request taken successfully.");
            }
            else
            {
                Console.WriteLine("No requests available.");
            }
        }

        public void InvestigheazaProblema()
        {
            if (currentRequest == null)
            {
                Console.WriteLine("No ongoing request to investigate.");
                return;
            }

            Console.WriteLine("Investigating the problem...");
           
            bool partsNeeded = true; 

            if (partsNeeded)
            {
                Console.WriteLine("Parts are needed to resolve the issue.");
            }
            else
            {
                Console.WriteLine("No parts are needed to resolve the issue.");
            }
        }

        public override void AdaugaComandaPiese(AutoService autoService, User utilizatorAutentificat)
        {
            if (currentRequest == null)
            {
                Console.WriteLine("No ongoing request to add parts order.");
                return;
            }

            Console.Write("Enter part order details: ");
            var detalii = Console.ReadLine();

            var comandaPiese = new CererePiese(autoService.GetPartOrders().Count + 1, utilizatorAutentificat.Nume, detalii, PartOrderStatus.InAsteptare);
            autoService.AddPartOrder(comandaPiese, utilizatorAutentificat);
            Console.WriteLine("Part order added successfully.");
        }

        public void RezolvaProblema(AutoService autoService)
        {
            if (currentRequest == null)
            {
                Console.WriteLine("No ongoing request to resolve.");
                return;
            }

            var comandaPiese = autoService.GetPartOrders().FirstOrDefault(c => c.Avb == int.Parse(currentRequest.CodUnic) && c.Status == PartOrderStatus.InAsteptare);
            if (comandaPiese != null)
            {
                Console.WriteLine("Waiting for parts to resolve the issue.");
                return;
            }

            currentRequest.Status = RequestStatus.Finalizat;
            Console.WriteLine($"Request resolved by {Nume}.");
            currentRequest = null;
        }

        public override void AdaugaCerere(AutoService autoService, User utilizatorAutentificat)
        {
            Console.Write("Enter request details: ");
            var detalii = Console.ReadLine();
            var cerere = new CerereRezolvare((autoService.GetCereri().Count + 1).ToString(), utilizatorAutentificat.Nume, "NumarMasina", detalii, RequestStatus.InPreluare);
            autoService.AddRequest(cerere, utilizatorAutentificat);
            Console.WriteLine("Request added successfully.");
        }

        public override void VizualizeazaCereri(AutoService autoService, User utilizatorAutentificat)
        {
            var cereri = autoService.GetCereri();
            foreach (var cerere in cereri)
            {
                Console.WriteLine($"Request ID: {cerere.CodUnic}, Status: {cerere.Status}, Client: {cerere.NumeClient}");
            }
        }

        public override void VizualizeazaComenziPiese(AutoService autoService, User utilizatorAutentificat)
        {
            var comenziPiese = autoService.GetPartOrders();
            foreach (var comanda in comenziPiese)
            {
                Console.WriteLine($"Order ID: {comanda.Avb}, Status: {comanda.Status}, Mechanic: {comanda.NumeMecanic}");
            }
        }

        public override void FinalizeazaComandaPiese(AutoService autoService, User utilizatorAutentificat)
        {
            Console.Write("Enter part order ID to finalize: ");
            var avb = int.Parse(Console.ReadLine());

            var comandaPiese = autoService.GetPartOrders().FirstOrDefault(c => c.Avb == avb);
            if (comandaPiese != null)
            {
                comandaPiese.Status = PartOrderStatus.Finalizat;
                Console.WriteLine("Part order finalized successfully.");
            }
            else
            {
                Console.WriteLine("Part order not found.");
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
            Console.Write("Introduceți codul unic: ");
            var code = Console.ReadLine();
            Console.Write("Introduceți prenumele: ");
            var firstName = Console.ReadLine();
            Console.Write("Introduceți numele de familie: ");
            var lastName = Console.ReadLine();
            Console.Write("Introduceți emailul: ");
            var email = Console.ReadLine();
            Console.Write("Introduceți parola: ");
            var password = Console.ReadLine();

            var mechanic = new Mechanic(code, firstName, lastName, email, password);
            autoService.AddUser(mechanic);
            Console.WriteLine("Mecanic adăugat cu succes.");
        }
    }
}
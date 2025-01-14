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
                Console.WriteLine("Aveti deja o cerere in desfasurare.");
                return;
            }

            var cerere = autoService.GetCereri().FirstOrDefault(c => c.Status == RequestStatus.InPreluare);
            if (cerere != null)
            {
                currentRequest = cerere;
                currentRequest.Status = RequestStatus.InPreluare;
                Console.WriteLine("Cerere preluata cu succes.");
            }
            else
            {
                Console.WriteLine("Nu sunt cereri disponibile.");
            }
        }

        public void InvestigheazaProblema()
        {
            if (currentRequest == null)
            {
                Console.WriteLine("Nu exista nicio cerere in desfasurare de investigat.");
                return;
            }

            Console.WriteLine("Investigati problema si decideti daca este necesara o comanda de piese (da/nu): ");
            var necesitaPiese = Console.ReadLine().ToLower();

            if (necesitaPiese == "da")
            {
                currentRequest.NecesitaPiese = true;
                Console.WriteLine("Problema necesita piese auto pentru a fi rezolvata.");
            }
            else
            {
                currentRequest.NecesitaPiese = false;
                Console.WriteLine("Problema poate fi rezolvata fara piese auto.");
            }
        }


        public override void AdaugaComandaPiese(AutoService autoService, User utilizatorAutentificat)
        {
            if (currentRequest == null)
            {
                Console.WriteLine("Nu exista nicio cerere in desfasurare pentru a adauga comanda de piese.");
                return;
            }

            Console.Write("Introduceti detaliile comenzii de piese: ");
            var detalii = Console.ReadLine();

            var comandaPiese = new CererePiese(int.Parse(currentRequest.CodUnic), utilizatorAutentificat.Nume, detalii, PartOrderStatus.InAsteptare);
            autoService.AddPartOrder(comandaPiese, utilizatorAutentificat);
            Console.WriteLine("Comanda de piese adaugata cu succes.");
        }

        public void RezolvaProblema(AutoService autoService)
        {
            if (currentRequest == null)
            {
                Console.WriteLine("Nu exista nicio cerere in desfasurare de rezolvat.");
                return;
            }

            if (currentRequest.NecesitaPiese)
            {
                var comandaPiese = autoService.GetPartOrders().FirstOrDefault(c => c.Avb == int.Parse(currentRequest.CodUnic) && c.Status == PartOrderStatus.InAsteptare);
                
                    Console.WriteLine("Asteptam piesele pentru a rezolva problema.");
                    return;
                
            }

            currentRequest.Status = RequestStatus.Finalizat;
            currentRequest.RezolvatDe = this.Nume;
            Console.WriteLine($"Cerere rezolvata de {Nume}.");
            currentRequest = null;
        }
        public override void AdaugaCerere(AutoService autoService, User utilizatorAutentificat)
        {
            Console.Write("Introduceti detaliile cererii: ");
            var detalii = Console.ReadLine();
            var cerere = new CerereRezolvare((autoService.GetCereri().Count + 1).ToString(), utilizatorAutentificat.Nume, "NumarMasina", detalii, RequestStatus.InPreluare);
            autoService.AddRequest(cerere, utilizatorAutentificat);
            Console.WriteLine("Cerere adaugata cu succes.");
        }

        public override void VizualizeazaCereri(AutoService autoService, User utilizatorAutentificat)
        {
            var cereri = autoService.GetCereri();
            if (cereri == null || cereri.Count == 0)
            {
                Console.WriteLine("Nu exista cereri de vizualizat.");
                return;
            }
            foreach (var cerere in cereri)
            {
                Console.WriteLine($"ID Cerere: {cerere.CodUnic}, Status: {cerere.Status}, Client: {cerere.NumeClient}");
            }
        }

        public override void VizualizeazaComenziPiese(AutoService autoService, User utilizatorAutentificat)
        {
            
            var comenziPiese = autoService.GetPartOrders();
            if (comenziPiese == null || comenziPiese.Count == 0)
            {
                Console.WriteLine("Nu exista cereri de vizualizat.");
                return;
            }
            foreach (var comanda in comenziPiese)
            {
                Console.WriteLine($"ID Comanda: {comanda.Avb}, Status: {comanda.Status}, Mecanic: {comanda.NumeMecanic}");
            }
        }

        public override void FinalizeazaComandaPiese(AutoService autoService, User utilizatorAutentificat)
        {
            Console.Write("Introduceti ID-ul comenzii de piese pentru a finaliza: ");
            var avb = int.Parse(Console.ReadLine());

            var comandaPiese = autoService.GetPartOrders().FirstOrDefault(c => c.Avb == avb);
            if (comandaPiese != null)
            {
                comandaPiese.Status = PartOrderStatus.Finalizat;
                Console.WriteLine("Comanda de piese finalizata cu succes.");
            }
            else
            {
                Console.WriteLine("Comanda de piese nu a fost gasita.");
            }
        }

        public override void LogIn(AutoService autoService)
        {
            var user = autoService.Authenticate(this.Email, this.Parola);
            if (user != null)
            {
                Console.WriteLine("Autentificare realizata cu succes.");
            }
            else
            {
                Console.WriteLine("Autentificare nereusita.");
            }
        }

        public override void AddUser(AutoService autoService)
        {
            Console.Write("Introduceti codul unic: ");
            var code = Console.ReadLine();
            Console.Write("Introduceti prenumele: ");
            var firstName = Console.ReadLine();
            Console.Write("Introduceti numele de familie: ");
            var lastName = Console.ReadLine();
            Console.Write("Introduceti emailul: ");
            var email = Console.ReadLine();
            Console.Write("Introduceti parola: ");
            var password = Console.ReadLine();

            var mechanic = new Mechanic(code, firstName, lastName, email, password);
            autoService.AddUser(mechanic);
            Console.WriteLine("Mecanic adaugat cu succes.");
        }
    }
}
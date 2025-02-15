﻿namespace AutoServiceApp
{
    public class Administrator : User
    {
        public Administrator(string codUnic, string nume, string prenume, string email, string parola)
            : base(codUnic, nume, prenume, email, parola, UserRole.Admin) { }

        public override void AdaugaCerere(AutoService autoService, User utilizatorAutentificat)
        {
            Console.Write("Introduceti numele clientului: ");
            var numeClient = Console.ReadLine();
            Console.Write("Introduceti numărul masinii: ");
            var numarMasina = Console.ReadLine();

            bool isValid = (numarMasina.StartsWith("B") && (numarMasina.Length == 6 || numarMasina.Length == 7) &&
                            char.IsDigit(numarMasina[1]) && char.IsDigit(numarMasina[2]) &&
                            char.IsLetter(numarMasina[3]) && char.IsLetter(numarMasina[4]) && char.IsLetter(numarMasina[5])) ||
                           ((numarMasina.Length == 7 || numarMasina.Length == 8) &&
                            char.IsLetter(numarMasina[0]) && char.IsLetter(numarMasina[1]) &&
                            char.IsDigit(numarMasina[2]) && char.IsDigit(numarMasina[3]) &&
                            char.IsLetter(numarMasina[4]) && char.IsLetter(numarMasina[5]) && char.IsLetter(numarMasina[6]));

            if (!isValid)
            {
                Console.WriteLine("Numarul de înmatriculare nu este valid.");
                return;
            }
            Console.Write("Introduceți descrierea problemei: ");
            var descriereProblema = Console.ReadLine();

            
            var random = new Random();
            var codUnic = random.Next(1000, 9999).ToString();

            var cerere = new CerereRezolvare(codUnic, numeClient, numarMasina, descriereProblema, RequestStatus.InPreluare);

            autoService.AddRequest(cerere, utilizatorAutentificat);
            Console.WriteLine("Cerere adăugată cu succes.");
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
            if (comenziPiese == null || comenziPiese.Count == 0)
            {
                Console.WriteLine("Nu exista cereri de vizualizat.");
                return;
            }
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

                Console.WriteLine($"Căutare cerere cu CodUnic: {comanda.Avb}");
                var cerere = autoService.GetCereri().FirstOrDefault(c => c.CodUnic == comanda.Avb.ToString());
                if (cerere != null)
                {
                    cerere.NecesitaPiese = false;
   
                }
                else
                {
                    Console.WriteLine("Cererea corespunzătoare nu a fost găsită.");
                }
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
            Console.Write("Introduceti numele: ");
            var lastName = Console.ReadLine();
            Console.Write("Introduceti email: ");
            var email = Console.ReadLine();
            Console.Write("Introduceti parola: ");
            var password = Console.ReadLine();

            var admin = new Administrator(code, firstName, lastName, email, password);
            autoService.AddUser(admin);
            Console.WriteLine("Administrator adaugat cu succes.");
        }
        
    }
}
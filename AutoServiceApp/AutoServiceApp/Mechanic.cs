namespace AutoServiceApp
{
    public class Mechanic : User
    {
        public Mechanic(string codUnic, string nume, string prenume, string email, string parola)
            : base(codUnic, nume, prenume, email, parola, UserRole.Mechanic)
        {
        }

        public CerereRezolvare PreluareCerere(List<CerereRezolvare> cereri)
        {
            var cerere = cereri.FirstOrDefault(c => c.Status == RequestStatus.InPreluare);
            if (cerere != null)
            {
                cerere.Status = RequestStatus.Investigare;
                Console.WriteLine($"Cererea cu Cod Unic {cerere.CodUnic} a fost preluată pentru investigare.");
            }
            return cerere;
        }

        public bool InvestigareProblema(CerereRezolvare cerere)
        { 
            if ( cerere != null)
                     {
                         cerere.Status = RequestStatus.Investigare;
                         Console.WriteLine($"Cererea cu Cod Unic {cerere.CodUnic} este în investigare.");
                         return true;
                     }
                     return false;

        }

        public void CreeazaCererePiese(List<CererePiese> comenziPiese, int avb, string detalii)
        {
            var comanda = new CererePiese(avb, this.Nume, detalii, PartOrderStatus.InAsteptare);
            comenziPiese.Add(comanda);
            Console.WriteLine($"Cererea de piese cu AVB {avb} a fost creată.");
        }
        
        public void RezolvaProblema(CerereRezolvare cerere)
        {
            if (cerere != null)
            {
                cerere.Status = RequestStatus.Finalizat;
                Console.WriteLine($"Cererea cu Cod Unic {cerere.CodUnic} a fost rezolvată.");
            }
        }
       
    }


  

}

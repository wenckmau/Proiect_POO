namespace AutoServiceApp
{
    public class Mechanic : User
    {
        public Mechanic(string codUnic, string nume, string prenume, string email, string parola)
            : base(codUnic, nume, prenume, email, parola) { }

        public CerereRezolvare PreluareCerere()
        {

        }

        public bool InvestigareProblema()
        {

        }

        public void CreeazaCererePiese(List<CererePiese> comenziPiese, int avb, string detalii)
        {
            var comanda = new CererePiese(avb, this.Nume, detalii, PartOrderStatus.InAsteptare);
            comenziPiese.Add(comanda);
            Console.WriteLine($"Cererea de piese cu AVB {avb} a fost creată.");
        }


        public void RezolvaProblema()
        {

        }
    }
}

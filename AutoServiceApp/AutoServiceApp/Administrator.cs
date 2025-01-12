namespace AutoServiceApp
{
    public class Administrator : User
    {
        public Administrator(string codUnic, string nume, string prenume, string email, string parola)
            : base(codUnic, nume, prenume, email, parola) { }

        public void VizualizareCereri()
        {

        }
        public void VizualizareComenziPiese(List<CererePiese> comenziPiese)
        {
            foreach (var comanda in comenziPiese)
            {
                Console.WriteLine($"AVB: {comanda.Avb}, Nume Mecanic: {comanda.NumeMecanic}, Status: {comanda.Status}");
            }
        }

        public void FinalizareComandaPiese(int avb, List<CererePiese> comenziPiese)
        {
            var comanda = comenziPiese.FirstOrDefault(c => c.Avb == avb);
            if (comanda != null)
            {
                comanda.Status = PartOrderStatus.Finalizat;
                Console.WriteLine($"Comanda cu AVB {avb} a fost finalizată.");
            }
            else
            {
                Console.WriteLine($"Comanda cu AVB {avb} nu a fost găsită.");
            }
        }

        public void AdaugaCerere()
        {

        }
    }
}
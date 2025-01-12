namespace AutoServiceApp
{
    public enum PartOrderStatus
    {   InAsteptare,
        InProcesare,
        InPreluare,
        Finalizat
    }

    public class CererePiese
    {
        public int Avb { get; set; }
        public string NumeMecanic { get; set; }
        public string DetaliiPiese { get; set; }
        public PartOrderStatus Status { get; set; }

        public CererePiese(int avb, string numeMecanic, string detaliiPiese, PartOrderStatus status)
        {
            Avb = avb;
            NumeMecanic = numeMecanic;
            DetaliiPiese = detaliiPiese;
            Status = status;
        }
    }
}
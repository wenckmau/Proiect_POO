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
        private int _avb;
        private string _numeMecanic;
        private string _detaliiPiese;
        private PartOrderStatus _status;
        
        public int Avb { get=>_avb; set=>_avb=value; }
        public string NumeMecanic { get=>_numeMecanic; set=>_numeMecanic=value; }
        public string DetaliiPiese { get=>_detaliiPiese; set=>_detaliiPiese=value; }
        public PartOrderStatus Status { get=>_status; set=>_status=value; }

        public CererePiese(int avb, string numeMecanic, string detaliiPiese, PartOrderStatus status)
        {
            Avb = avb;
            NumeMecanic = numeMecanic;
            DetaliiPiese = detaliiPiese;
            Status = status;
        }
    }
}
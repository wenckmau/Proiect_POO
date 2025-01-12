namespace AutoServiceApp
{
    public enum RequestStatus
    {
        InPreluare,
        Investigare,
        AsteptarePiese,
        Finalizat
    }

    public class CerereRezolvare
    {
        public string CodUnic { get; set; }
        public string NumeClient { get; set; }
        public string NumarMasina { get; set; }
        public string DescriereProblema { get; set; }
        public RequestStatus Status { get; set; }

        public CerereRezolvare(string codUnic, string numeClient, string numarMasina, string descriereProblema, RequestStatus status)
        {
            CodUnic = codUnic;
            NumeClient = numeClient;
            NumarMasina = numarMasina;
            DescriereProblema = descriereProblema;
            Status = status;
        }
    }
}
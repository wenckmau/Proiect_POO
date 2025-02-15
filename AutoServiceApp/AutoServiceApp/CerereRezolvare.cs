﻿namespace AutoServiceApp
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
        private string _codUnic;
        private string _numeClient;
        private string _numarMasina;
        private string _descriereProblema;
        private RequestStatus _status;
        private bool _necesitaPiese;
        private string _rezolvatDe;

        public string CodUnic { get => _codUnic; set => _codUnic = value; }
        public string NumeClient { get => _numeClient; set => _numeClient = value; }
        public string NumarMasina { get => _numarMasina; set => _numarMasina = value; }
        public string DescriereProblema { get => _descriereProblema; set => _descriereProblema = value; }
        public RequestStatus Status { get => _status; set => _status = value; }
        public bool NecesitaPiese { get => _necesitaPiese; set => _necesitaPiese = value; }
        public string RezolvatDe { get => _rezolvatDe; set => _rezolvatDe = value; }

        public CerereRezolvare(string codUnic, string numeClient, string numarMasina, string descriereProblema, RequestStatus status)
        {
            CodUnic = codUnic;
            NumeClient = numeClient;
            NumarMasina = numarMasina;
            DescriereProblema = descriereProblema;
            Status = status;
            NecesitaPiese = false;
            RezolvatDe = string.Empty;
        }
    }
}
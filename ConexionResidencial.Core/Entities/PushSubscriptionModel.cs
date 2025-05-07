namespace ConexionResidencial.Core.Entities
{
    public class PushSubscriptionModel
    {
        public string Endpoint { get; set; }
        public SubscriptionKeys Keys { get; set; }
    }

    public class SubscriptionKeys
    {
        public string P256DH { get; set; }
        public string Auth { get; set; }
    }
}
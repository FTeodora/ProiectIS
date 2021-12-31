namespace ProiectIS.Models
{   
    [Serializable]
    public class Notification
    {
        public long recipientID { get; set; }
        public long senderID { get; set; }
        public string message { get; set; } 
        public bool accepted { get; set; }
        public bool rejected { get; set; }
        public Notification()
        {

        }
        public Notification(List<Object> src)
        {
            recipientID = (long)src[0];
                senderID = (long)src[1];
            message = (string)src[2];
            accepted = (bool)src[3];
            rejected = (bool)src[4];

        }
    }
}

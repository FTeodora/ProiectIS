namespace ProiectIS.Models
{   
    [Serializable]
    public class Notification
    {
        public long id { get; set; }
        public long recipientID { get; set; }
        public long senderID { get; set; }
        public string message { get; set; } 
        public bool accepted { get; set; }
        public bool rejected { get; set; }
        public long lobbyID { get; set; }
        public Notification()
        {

        }
        public Notification(List<Object> src)
        {
            id=(long)src[0];
            recipientID = (long)src[1];
                senderID = (long)src[2];
            message = (string)src[3];
            accepted = (bool)src[4];
            rejected = (bool)src[5];
            lobbyID = (long)src[6];

        }
    }
}

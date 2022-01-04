//select * FROM grup WHERE grup.ID NOT IN (SELECT grupID FROM grupMember WHERE studentID=4);
namespace ProiectIS.Models
{
    [Serializable]
    public class DateGrup:IObserver<ScheduledQuiz>
    {
        public long id { get; set; }
        public string nume { get; set; }
        public string descriere { get; set; }
        public long notificationID { get; set; }
        private IDisposable unsubscriber;
        public DateGrup()
        {

        }
        public DateGrup(List<Object> list) { id = (long)list[0]; nume = (string)list[1]; descriere = (string)list[2]; }
        public void OnCompleted()
        {
            unsubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.ToString());
        }

        public void OnNext(ScheduledQuiz value)
        {
            Database db = Database.Instance;
            db.openConnection();
            List<string> fields = new List<string>();
            fields.Add("recipientID");
            fields.Add("senderID");
            fields.Add("message");
            fields.Add("lobbyID");
            List<Object> vals = new List<Object>();
            vals.Add(value.challengedID);
            vals.Add(value.challengerID);
            value.getName();
            value.getChallengedName();
            vals.Add("The match "+value.challengerName+" against "+value.challengedName+"is about to start!");
            vals.Add(0);

            db.genericInsert("groupNotification", fields, vals);
            db.closeConnection();
        }
        public static long getMemberAmount(long groupID)
        {
            Database db = Database.Instance;
            db.openConnection();
            List<List<Object>> rez = db.genericSelect("grupMember", "count(*)", "grupID=" + groupID);
            db.closeConnection();
            return (long)rez[0][0];
        }

        
    }
}

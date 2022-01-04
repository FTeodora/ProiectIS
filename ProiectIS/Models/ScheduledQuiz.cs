namespace ProiectIS.Models
{
    public class ScheduledQuiz
    {
        public long challengerID { get; set; }
        public long challengedID { get; set;}
        public DateTime scheduledTime { get; set; }
        public string challengerName { get; set; } 
        public string challengedName { get; set; } 
        public ScheduledQuiz()
        {

        }
        public ScheduledQuiz(List<Object> src)
        {
            challengerID=(long)src[0];
            challengedID=(long)src[1];
            scheduledTime=(DateTime)src[2];
            challengedName = null;
            challengerName = null;

        }
        public void getName()
        {
            if(challengerName == null)
            {
                Database db = Database.Instance;
                db.openConnection();
                List<List<Object>> res = db.genericSelect("grup", "nume", "id=" + challengerID);
                db.closeConnection();
                challengerName = (string)res[0][0];
            }

        }
        public void getChallengedName()
        {
            if(challengedName == null)
            {
                Database db = Database.Instance;
                db.openConnection();
                List<List<Object>> res = db.genericSelect("grup", "nume", "id=" + challengedID);
                db.closeConnection();
                challengedName = (string)res[0][0];
            }

        }
        public static bool checkBusy(ScheduledQuiz src)
        {
            Database db=Database.Instance;
            db.openConnection();
            List<List<Object>> res = db.genericSelect("scheduledMatch", "challengerID", "challengedID=" + src.challengedID + " AND ABS(TIME_TO_SEC(TIMEDIFF('"+src.scheduledTime.ToString("yyyy-MM-dd HH:mm:ss")+"', scheduledTime))/60)<40");
            db.closeConnection();
            return res.Count > 0;
        }
    }
}

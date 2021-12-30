namespace ProiectIS.Models
{
    public class DateGrup
    {
        public long id { get; set; }
        public string nume { get; set; }
        public string descriere { get; set; }


        public DateGrup(List<Object> list) { id = (long)list[0]; nume = (string)list[1]; descriere = (string)list[2]; }
    }
}

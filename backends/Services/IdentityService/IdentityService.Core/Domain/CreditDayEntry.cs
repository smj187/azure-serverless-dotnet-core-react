namespace IdentityService.Core.Domain
{
    public class Credit
    {
        private DateTimeOffset _date;
        private List<Entry> _entries;

        public Credit()
        {
            Date = DateTimeOffset.UtcNow.Date;
            _entries = new List<Entry>();
        }

        public DateTimeOffset Date { get => _date; private set => _date = value; }
        public List<Entry> Entries { get => _entries; private set => _entries = new List<Entry>(value); }



        public int GetUsage() => Entries.Select(e => e.Credits).Sum();

        public void PushCredit(string type, string value, int credits)
        {
            Entries.Add(new Entry(type, value, credits));
        }
    }
}

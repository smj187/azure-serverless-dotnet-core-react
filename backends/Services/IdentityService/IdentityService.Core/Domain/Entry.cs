using BuildingBlocks.Domain;

namespace IdentityService.Core.Domain
{
    public class Entry : ValueObject
    {
        private string _type;
        private string _value;
        private int _credits;
        private DateTimeOffset _time;

        public Entry(string type, string value, int credits)
        {
            _type = type;
            _value = value;
            _credits = credits;

            _time = DateTimeOffset.UtcNow;
        }

        public string Type { get => _type; private set => _type = value; }
        public string Value { get => _value; private set => _value = value; }
        public int Credits { get => _credits; private set => _credits = value; }
        public DateTimeOffset Time { get => _time; private set => _time = value; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Value;
            yield return Credits;
            yield return Time;
        }
    }
}

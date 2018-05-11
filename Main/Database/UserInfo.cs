namespace Main.ORM
{
    public class UserInfo
    {
        public const int TEST_USER_ID = 5;

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public string Rank { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string Shortinfo { get; set; }

        public int NumberOfRates { get; set; }

        public override string ToString()
        {
            return "ID: " + Id + ", Nickname: " + Nickname + ", Jméno a příjmeni: " + Firstname + " " + Lastname + ", Email: " + Email + ", points: " + Points;
        }
    }
}

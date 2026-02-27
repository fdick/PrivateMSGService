namespace PrivateMSGService.Core.Models
{
    public class User
    {
        private User(Guid id, string nickname, string name, string lastName, string email)
        {
            this.ID = id;
            Nickname = nickname;
            Name = name;
            LastName = lastName;
            Email = email;
        }

        public Guid ID { get; }
        public string Nickname { get; }
        public string Name { get; }
        public string LastName { get; }
        public string Email { get; }

        public static (User, string) Create(Guid id, string nickname, string name, string lastname, string email)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                error = $"{nameof(nickname)}, {nameof(name)}, {nameof(email)} can not be null!";
            }
            var user = new User(id, nickname, name, lastname, email);
            return (user, error);
        }
    }
}

using System;

namespace Persistence.Models
{
    public class UserReadModel
    {
        public Guid UserId { get; set; }
        
        public string FirebaseId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

    }
}

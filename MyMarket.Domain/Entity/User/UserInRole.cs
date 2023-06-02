namespace MyMarket.Domain.Entity.User
{
    public class UserInRole
    {
        public long Id { get; set; }

        public virtual User user { get; set; }
        public long UserId { get; set; }

        public virtual Role Role { get; set; }
        public long RoleId { get; set; }
    }

    

}

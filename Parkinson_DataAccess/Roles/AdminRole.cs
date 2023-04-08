namespace Parkinson_API.Helpers.Roles
{
    public class AdminRole
    {
        public static string Id = Guid.NewGuid().ToString();
        public static string Name = "Admin";
        public static string NormalizedName = "Admin".ToUpper();
        public static string ConcurrencyStamp = Guid.NewGuid().ToString();
    }
}

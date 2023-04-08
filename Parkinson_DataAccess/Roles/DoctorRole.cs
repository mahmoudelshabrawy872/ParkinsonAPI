namespace Parkinson_API.Helpers.Roles
{
    public class DoctorRole
    {
        public static string Id = Guid.NewGuid().ToString();
        public static string Name = "Doctor";
        public static string NormalizedName = "Doctor".ToUpper();
        public static string ConcurrencyStamp = Guid.NewGuid().ToString();
    }
}

namespace Parkinson_API.Helpers.Roles
{
    public class PatientRole
    {
        public static string Id = Guid.NewGuid().ToString();
        public static string Name = "Patient";
        public static string NormalizedName = "Patient".ToUpper();
        public static string ConcurrencyStamp = Guid.NewGuid().ToString();
    }
}

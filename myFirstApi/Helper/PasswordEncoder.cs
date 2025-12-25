namespace myFirstApi.Helper
{
    public static class PasswordEncoder
    {
        public static string Encode(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool Verification(string password, string passhash)
        {
           return BCrypt.Net.BCrypt.Verify(password, passhash);
        }
    }
}

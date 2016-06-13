using System;

namespace TokenApp.Helpers
{
    public class AuthorizationHelper
    {
        public static byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}

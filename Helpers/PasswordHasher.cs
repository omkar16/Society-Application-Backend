using System.Security.Cryptography;

namespace MyGate.Helpers
{
    public class PasswordHasher
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 1000;

        public static string HashPassword(string password)
        {
            byte[] salt;                                                    //Generate random salt to add in front of Hash for Security Enhancement
            rng.GetBytes(salt = new byte[SaltSize]);                        //Add random bytes in salt[] of bytes[]
            var key = new Rfc2898DeriveBytes(password,salt, Iterations);    //using this random salt and password generate random key
            var hash = key.GetBytes(HashSize);                              //Generate hash using this key in bytes[] form
            var hashBytes = new byte[SaltSize + HashSize];                  //Empty Hashbytes[] to store complete hash/ Encryption
            Array.Copy(salt,0,hashBytes,0,SaltSize);                        //Store first 16 bits of Encrypted text will be Salt and last is Hash 
            Array.Copy(hash,0,hashBytes,SaltSize,HashSize);

            var base64hash = Convert.ToString(hashBytes);                   //Convert Bytes[] into string as a result

            return base64hash;
        }

        public static bool VerifyPassword(string password, string base64hash) { 
            var hashBytes = Convert.FromBase64String(base64hash);               //take out Salt from hashBytes as first 16 bits and using that salt and password to verify, generate key and using that key, generate hash
                                                                                //at last just check the newly generated encrypted text (hash) and last hashSize bits from hashBytes coz first SaltSize bits is Salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes,0,salt,0,SaltSize);

            var key = new Rfc2898DeriveBytes(password,salt,Iterations);
            byte[] hash = key.GetBytes(HashSize);

            for (var i=0; i<HashSize; i++) {
                if (hashBytes[SaltSize + i] != hash[i]) return false;
            }
            return true;
        }
    }
}

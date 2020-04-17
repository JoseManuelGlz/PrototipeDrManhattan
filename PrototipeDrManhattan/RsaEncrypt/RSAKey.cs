using System;

namespace PrototipeDrManhattan.S3TestWebApi.RsaEncrypt
{
    public class RSAKey
    {
        /// <summary>
        /// Rsa public key
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Rsa private key
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Rsa public key Exponent
        /// </summary>
        public string Exponent { get; set; }

        /// <summary>
        /// Rsa public key Modulus
        /// </summary>
        public string Modulus { get; set; }
    }

    public enum RsaKeyType
    {
        XML,
        JSON
    }

    public enum RsaSize
    {
        R2048 = 2048,
        R3072 = 3072,
        R4096 = 4096
    }
}

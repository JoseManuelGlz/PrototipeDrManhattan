using System;
using System.Text;
using System.Security.Cryptography;
using PrototipeDrManhattan.S3TestWebApi.RsaEncrypt;
using PrototipeDrManhattan.S3TestWebApi.RsaEncrypt.Extensions;
using PrototipeDrManhattan.RsaEncrypt;
using PrototipeDrManhattan.RsaEncrypt.Extensions;
//using NETCore.Encrypt.Shared;
//using NETCore.Encrypt.Extensions;
//using NETCore.Encrypt.Internal;
//using NETCore.Encrypt.Extensions.Internal;

namespace S3TestWebApi.RsaEncrypt
{
    public class EncryptProvider
    {

        #region RSA

        /// <summary>
        /// RSA Converter to pem
        /// </summary>
        /// <param name="isPKCS8"></param>
        /// <returns></returns>
        public static (string publicPem, string privatePem) RSAToPem(bool isPKCS8)
        {
            var rsaKey = CreateRsaKey();

            using (RSA rsa = RSA.Create())
            {
                rsa.FromJsonString(rsaKey.PrivateKey);

                var publicPem = RsaProvider.ToPem(rsa, false, isPKCS8);
                var privatePem = RsaProvider.ToPem(rsa, true, isPKCS8);

                return (publicPem, privatePem);
            }
        }

        /// <summary>
        /// RSA From pem
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>
        public static RSA RSAFromPem(string pem)
        {
            return RsaProvider.FromPem(pem);
        }

        /// <summary>
        /// RSA Sign
        /// </summary>
        /// <param name="conent">raw cotent </param>
        /// <param name="privateKey">private key</param>
        /// <returns></returns>
        public static string RSASign(string conent, string privateKey)
        {
            return RSASign(conent, privateKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1, Encoding.UTF8);
        }

        /// <summary>
        /// RSA Sign
        /// </summary>
        /// <param name="content">raw content </param>
        /// <param name="privateKey">private key</param>
        /// <param name="hashAlgorithmName">hashAlgorithm name</param>
        /// <param name="rSASignaturePadding">ras siginature padding</param>
        /// <param name="encoding">text encoding</param>
        /// <returns></returns>
        public static string RSASign(string content, string privateKey, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSASignaturePadding, Encoding encoding)
        {
            byte[] dataBytes = encoding.GetBytes(content);

            using (RSA rsa = RSA.Create())
            {
                rsa.FromJsonString(privateKey);
                var signBytes = rsa.SignData(dataBytes, hashAlgorithmName, rSASignaturePadding);

                return Convert.ToBase64String(signBytes);
            }
        }

        /// <summary>
        /// RSA Verify
        /// </summary>
        /// <param name="content">raw content</param>
        /// <param name="signStr">sign str</param>
        /// <param name="publickKey">public key</param>
        /// <returns></returns>
        public static bool RSAVerify(string content, string signStr, string publickKey)
        {
            return RSAVerify(content, signStr, publickKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1, Encoding.UTF8);
        }

        /// <summary>
        /// RSA Verify
        /// </summary>
        /// <param name="content">raw content</param>
        /// <param name="signStr">sign str</param>
        /// <param name="publickKey">public key</param>
        /// <param name="hashAlgorithmName">hashAlgorithm name</param>
        /// <param name="rSASignaturePadding">ras siginature padding</param>
        /// <param name="encoding">text encoding</param>
        /// <returns></returns>
        public static bool RSAVerify(string content, string signStr, string publickKey, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSASignaturePadding, Encoding encoding)
        {
            byte[] dataBytes = encoding.GetBytes(content);
            byte[] signBytes = Convert.FromBase64String(signStr);

            using (RSA rsa = RSA.Create())
            {
                rsa.FromJsonString(publickKey);
                return rsa.VerifyData(dataBytes, signBytes, hashAlgorithmName, rSASignaturePadding);
            }
        }

        /// <summary>
        /// RSA encrypt 
        /// </summary>
        /// <param name="publicKey">public key</param>
        /// <param name="srcString">src string</param>
        /// <returns>encrypted string</returns>
        public static string RSAEncrypt(string publicKey, string srcString)
        {
            string encryptStr = RSAEncrypt(publicKey, srcString, RSAEncryptionPadding.OaepSHA512);
            return encryptStr;
        }

        /// <summary>
        /// RSA encrypt with pem key
        /// </summary>
        /// <param name="publicKey">pem public key</param>
        /// <param name="scrString">src string</param>
        /// <returns></returns>
        public static string RSAEncryptWithPem(string publicKey, string srcString)
        {
            string encryptStr = RSAEncrypt(publicKey, srcString, RSAEncryptionPadding.Pkcs1, true);
            return encryptStr;
        }

        /// <summary>
        /// RSA encrypt 
        /// </summary>
        /// <param name="publicKey">public key</param>
        /// <param name="srcString">src string</param>
        /// <param name="padding">rsa encryptPadding <see cref="RSAEncryptionPadding"/> RSAEncryptionPadding.Pkcs1 for linux/mac openssl </param>
        /// <param name="isPemKey">set key is pem format,default is false</param>
        /// <returns>encrypted string</returns>
        public static string RSAEncrypt(string publicKey, string srcString, RSAEncryptionPadding padding, bool isPemKey = false)
        {
            RSA rsa;
            if (isPemKey)
            {
                rsa = RsaProvider.FromPem(publicKey);
            }
            else
            {
                rsa = RSA.Create();
                rsa.FromJsonString(publicKey);
            }

            using (rsa)
            {
                #region uft-8
                
                var maxLength = GetMaxRsaEncryptLength(rsa, padding);

                var rawBytes = Encoding.UTF8.GetBytes(srcString);

                if (rawBytes.Length > maxLength)
                {
                    throw new OutofMaxlengthException($"'{srcString}' is out of max encrypt length {maxLength}", maxLength, rsa.KeySize, padding);
                }

                byte[] encryptBytes = rsa.Encrypt(rawBytes, padding);
                
                return encryptBytes.ToHexString();
                
                #endregion

                #region base 64
                /*
                var maxLength = GetMaxRsaEncryptLength(rsa, padding);

                var rawBytesBase64 = Convert.FromBase64String(srcString);

                if (rawBytesBase64.Length > maxLength)
                {
                    throw new OutofMaxlengthException($"'{srcString}' is out of max encrypt length {maxLength}", maxLength, rsa.KeySize, padding);
                }

                byte[] encryptBytesBase64 = rsa.Encrypt(rawBytesBase64, padding);

                return encryptBytesBase64.ToString();
                */
                #endregion


                
            }
        }

        /// <summary>
        /// RSA decrypt
        /// </summary>
        /// <param name="privateKey">private key</param>
        /// <param name="srcString">encrypted string</param>
        /// <returns>Decrypted string</returns>
        public static string RSADecrypt(string privateKey, string srcString)
        {
            string decryptStr = RSADecrypt(privateKey, srcString, RSAEncryptionPadding.OaepSHA512);
            return decryptStr;
        }

        /// <summary>
        /// RSA decrypt with pem key
        /// </summary>
        /// <param name="privateKey">pem private key</param>
        /// <param name="scrString">src string</param>
        /// <returns></returns>
        public static string RSADecryptWithPem(string privateKey, string srcString)
        {
            string decryptStr = RSADecrypt(privateKey, srcString, RSAEncryptionPadding.Pkcs1, true);
            return decryptStr;
        }

        /// <summary>
        /// RSA encrypt 
        /// </summary>
        /// <param name="publicKey">public key</param>
        /// <param name="srcString">src string</param>
        /// <param name="padding">rsa encryptPadding <see cref="RSAEncryptionPadding"/> RSAEncryptionPadding.Pkcs1 for linux/mac openssl </param>
        /// <param name="isPemKey">set key is pem format,default is false</param>
        /// <returns>encrypted string</returns>
        public static string RSADecrypt(string privateKey, string srcString, RSAEncryptionPadding padding, bool isPemKey = false)
        {
            RSA rsa;
            if (isPemKey)
            {
                rsa = RsaProvider.FromPem(privateKey);
            }
            else
            {
                rsa = RSA.Create();
                rsa.FromJsonString(privateKey);
            }

            using (rsa)
            {
                byte[] srcBytes = srcString.ToBytes();
                byte[] decryptBytes = rsa.Decrypt(srcBytes, padding);
                return Encoding.UTF8.GetString(decryptBytes);
            }
        }

        /// <summary>
        /// RSA from json string
        /// </summary>
        /// <param name="rsaKey">rsa json string</param>
        /// <returns></returns>
        public static RSA RSAFromString(string rsaKey)
        {
            RSA rsa = RSA.Create();

            rsa.FromJsonString(rsaKey);
            return rsa;
        }

        /// <summary>
        /// Create an RSA key 
        /// </summary>
        /// <param name="keySizeInBits">the default size is 2048</param>
        /// <returns></returns>
        public static RSAKey CreateRsaKey(RsaSize rsaSize = RsaSize.R2048)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = (int)rsaSize;

                string publicKey = rsa.ToJsonString(false);
                string privateKey = rsa.ToJsonString(true);

                return new RSAKey()
                {
                    PublicKey = publicKey,
                    PrivateKey = privateKey,
                    Exponent = rsa.ExportParameters(false).Exponent.ToHexString(),
                    Modulus = rsa.ExportParameters(false).Modulus.ToHexString()
                };
            }
        }

        /// <summary>
        /// Create an RSA key 
        /// </summary>
        /// <param name="rsa">rsa</param>
        /// <returns></returns>
        public static RSAKey CreateRsaKey(RSA rsa)
        {
            string publicKey = rsa.ToJsonString(false);
            string privateKey = rsa.ToJsonString(true);

            return new RSAKey()
            {
                PublicKey = publicKey,
                PrivateKey = privateKey,
                Exponent = rsa.ExportParameters(false).Exponent.ToHexString(),
                Modulus = rsa.ExportParameters(false).Modulus.ToHexString()
            };
        }

        /// <summary>
        /// Get rsa encrypt max length 
        /// </summary>
        /// <param name="rsa">Rsa instance </param>
        /// <param name="padding"><see cref="RSAEncryptionPadding"/></param>
        /// <returns></returns>
        private static int GetMaxRsaEncryptLength(RSA rsa, RSAEncryptionPadding padding)
        {
            var offset = 0;
            if (padding.Mode == RSAEncryptionPaddingMode.Pkcs1)
            {
                offset = 11;
            }
            else
            {
                if (padding.Equals(RSAEncryptionPadding.OaepSHA1))
                {
                    offset = 42;
                }

                if (padding.Equals(RSAEncryptionPadding.OaepSHA256))
                {
                    offset = 66;
                }

                if (padding.Equals(RSAEncryptionPadding.OaepSHA384))
                {
                    offset = 98;
                }

                if (padding.Equals(RSAEncryptionPadding.OaepSHA512))
                {
                    offset = 130;
                }
            }
            var keySize = rsa.KeySize;
            var maxLength = keySize / 8 - offset;
            return maxLength;
        }

        #endregion

    }
}
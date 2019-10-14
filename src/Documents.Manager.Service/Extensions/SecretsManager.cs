using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Documents.Manager.Business.Exceptions;

namespace Documents.Manager.Service.Extensions
{
    /// <summary>
    /// Secrets manager.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SecretsManager
    {
        #region :: Methods ::

        /// <summary>
        /// Gets the secret.
        /// </summary>
        /// <returns>The secret.</returns>
        public static string GetSecret()
        {
            string secretName = Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_NAME");
            string region = Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_REGION");

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_VERSION_STAGE") // VersionStage defaults to AWSCURRENT if unspecified.
            };

            // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
            // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
            // We rethrow the exception by default.

            try
            {
                GetSecretValueResponse response = client.GetSecretValueAsync(request).Result;

                // Decrypts secret using the associated KMS CMK.
                // Depending on whether the secret is a string or binary, one of these fields will be populated.
                if (response.SecretString != null)
                {
                    return response.SecretString;
                }

                string decodedBinarySecret;

                using (var memoryStream = response.SecretBinary)
                {
                    StreamReader reader = new StreamReader(memoryStream);
                    decodedBinarySecret = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                }

                return decodedBinarySecret;
            }
            catch(Exception ex)
            {
                throw new SecretsManagerException(ex.Message, ex);
            }
        }

        #endregion
    }
}
using Newtonsoft.Json;

namespace VaultSharp.Backends.Secret.Models.AWS
{
    /// <summary>
    /// Represents the AWS credentials.
    /// </summary>
    public class AWSCredentials
    {
        /// <summary>
        /// Gets or sets the access key.
        /// </summary>
        /// <value>
        /// The access key.
        /// </value>
        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        /// <value>
        /// The secret key.
        /// </value>
        [JsonProperty("secret_key")]
        public string SecretKey { get; set; }
    }
}
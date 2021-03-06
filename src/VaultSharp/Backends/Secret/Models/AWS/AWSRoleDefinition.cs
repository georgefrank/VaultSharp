using Newtonsoft.Json;

namespace VaultSharp.Backends.Secret.Models.AWS
{
    /// <summary>
    /// Represents the AWS role definition with an IAM policy.
    /// </summary>
    public class AWSRoleDefinition
    {
        /// <summary>
        /// Gets or sets the IAM policy as a JSON string.
        /// </summary>
        /// <value>
        /// The policy.
        /// </value>
        [JsonProperty("policy")]
        public string Policy { get; set; }
    }
}
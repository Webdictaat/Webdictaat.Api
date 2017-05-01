using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Webdictaat.Api.Services
{
    public interface ISecretService
    {
        string GetAssignmentToken(string email, int assignmentId, string secret);
    }

    public class SecretService : ISecretService
    {
         private SHA1 _sha1;

        public SecretService()
        {
            _sha1 = System.Security.Cryptography.SHA1.Create();
        }

        public string GetAssignmentToken(string userId, int assignmentId, string secret)
        {
            string toBeHashed = userId + assignmentId + secret;
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(toBeHashed);
            byte[] hash = _sha1.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

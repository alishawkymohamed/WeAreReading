namespace Helpers.Contracts
{
    public interface IEncryptionService
    {
        string EncryptString(string plain_text, string password, string salt = null);
        string DecryptString(string encrypted_value, string password, string salt = null);
        string GetSha256Hash(string input);
    }
}

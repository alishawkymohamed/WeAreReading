namespace Models.HelperModels
{
    public class ConnectionStrings
    {
        public string MainConnectionString { get; set; }
    }

    public class LocalizationSettings
    {
        public bool LoadLocalizationOnStart { get; set; }
        public string LocalizationRelativePath { get; set; }
    }

    public class SwaggerToTypeScriptSettings
    {
        public string SourceDocumentAbsoluteUrl { get; set; }
        public string OutputDocumentRelativePath { get; set; }
    }

    public class BearerTokensSettings
    {
        public string Key { set; get; }
        public string Issuer { set; get; }
        public string Audience { set; get; }
        public int AccessTokenExpirationMinutes { set; get; }
        public int RefreshTokenExpirationMinutes { set; get; }
        public bool AllowMultipleLoginsFromTheSameUser { set; get; }
        public bool AllowSignoutAllUserActiveClients { set; get; }
    }

    public class FileSettings
    {
        public string RelativeDirectory { get; set; }
    }

    public class EncryptionSettings
    {
        public int Iterations { get; set; }
        public int BlockSize { get; set; }
        public int KeySize { get; set; }
        public string Salt { get; set; }
        public string SecretPassword { get; set; }
        public string IV { get; set; }
    }

    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public LocalizationSettings LocalizationSettings { get; set; }
        public SwaggerToTypeScriptSettings SwaggerToTypeScriptSettings { get; set; }
        public FileSettings FileSettings { get; set; }
        public EncryptionSettings EncryptionSettings { get; set; }
        public BearerTokensSettings BearerTokensSettings { get; set; }
    }
}

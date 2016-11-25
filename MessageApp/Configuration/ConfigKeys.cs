
namespace MessageApp.Configuration
{
    public class ConfigKeys
    {
        private const string _productToken = "ProductToken";
        private const string _baseAddress = "BaseAddress";
        private const string _postUri = "PostUri";

        public static string ProductToken { get { return _productToken; } }
        public static string BaseAddress { get { return _baseAddress; } }
        public static string PostUri { get { return _postUri; } }
    }
}

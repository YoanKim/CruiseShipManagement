namespace CSMUI.Controllers
{
    public class ApiAuthenticationClient : HttpClient
    {
        public ApiAuthenticationClient() : base()
        {
            base.DefaultRequestHeaders.Add("public-authentication-key", "484ccbb3-988f-4f1a-a527-dcd256969bee");
        }
    }
}

using System.Text.Json.Serialization;

namespace TasksApi.Responses
{
    public class TokenResponse: BaseResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
       
    }
}

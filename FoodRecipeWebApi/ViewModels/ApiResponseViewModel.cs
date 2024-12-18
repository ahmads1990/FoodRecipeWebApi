namespace FoodRecipeWebApi.ViewModels
{
    public class ApiResponseViewModel<Entity>
    {
        public Entity Data { get; set; }
        public int? StatusCode { get; set; }
        public object Message { get; set; }
        public ApiResponseViewModel(int? statusCode, object message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }
        public ApiResponseViewModel(int? statusCode, Entity data, object message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
            Data = data;
        }

        private static string GetDefaultMessageForStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                201 => "Resource created",
                204 => "Resource deleted",
                400 => "Bad Request",
                401 => "You are not Authorized",
                403 => "You are forbidden",
                404 => "Resource Not Found",
                405 => "Method Not Allowed",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}

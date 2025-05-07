using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidarHeadersAttribute : ActionFilterAttribute
{
    private readonly string _communityIdHeader = "x-community-id";
    private readonly string _corsHeader = "Access-Control-Allow-Origin";
    private readonly string _privateKeyComunidad = "2b2463d9f3b093b61be6ce0adbdcc4a0f7e56776502d173a4cf4bb0a8f5d0e79";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var headers = context.HttpContext.Request.Headers;
        var communityId = headers[_communityIdHeader].ToString();
        var cors = headers[_corsHeader].ToString();

        if (string.IsNullOrEmpty(communityId) || communityId != _privateKeyComunidad || string.IsNullOrEmpty(cors))
        {
            context.Result = new JsonResult(new result { })
            {
                StatusCode = 200
            };
        }

        base.OnActionExecuting(context);
    }
}
public class result
{
    public List<string> Result { get; set; }
    public int Id { get; set; }

}
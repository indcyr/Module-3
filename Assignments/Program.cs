using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);


GetAllUsers(client);
GetSingleUser(client);
CreateUsers(client);
UpdateAllUsers(client);
DeleteAllUsers(client); 

static void GetAllUsers(RestClient client)
{
    var getUserRequest = new RestRequest("posts", Method.Get);
   // getUserRequest.AddQueryParameter("page", "2");//Adding query parameter

    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("GET response: \n" + getUserResponse.Content);
}
static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts/5", Method.Get);
    //getUserRequest.AddQueryParameter("page", "2");//Adding query parameter

    var getUserResponse = client.Execute(getUserRequest);

    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse JSON REsponse content
        JObject? userJson = JObject.Parse(getUserResponse?.Content);


        string? userId = userJson["id"].ToString();
        string? title = userJson["title"].ToString();

        Console.WriteLine($"User :  {userId} {title}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}
static void CreateUsers(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json"); //Adding Header
    createUserRequest.AddJsonBody(new
    {
        id = "1",
        title = "Hello"
    });

    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Create user response");
    Console.WriteLine(createUserResponse.Content);

}

static void UpdateAllUsers(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/2", Method.Put);

    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new
    {
        id = "1",
        title = "Updated Hello"
    });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update User response");
    Console.WriteLine(updateUserResponse.Content);
}

static void DeleteAllUsers(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("DELETE User Response");
    Console.WriteLine(deleteUserResponse.Content);
}

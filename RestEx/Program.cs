using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);

/*var getUserRequest = new RestRequest("users/2", Method.Get);
var getUserResponse = client.Execute(getUserRequest);
Console.WriteLine("GET response: \n" + getUserResponse.Content);

var createUserRequest = new RestRequest("users", Method.Post);

createUserRequest.AddParameter("name", "John Doe");
createUserRequest.AddParameter("job", "Software Developer");

var createUserResponse = client.Execute(createUserRequest);
Console.WriteLine("POST Create user response");
Console.WriteLine(createUserResponse.Content);

var updateUserRequest = new RestRequest("users/2", Method.Put);
updateUserRequest.AddParameter("name", "Updated John Doe");
updateUserRequest.AddParameter("job", "Senior Software Developer");

var updateUserResponse = client.Execute(updateUserRequest);
Console.WriteLine("PUT Update User response");
Console.WriteLine(updateUserResponse.Content);

var deleteUserRequest = new RestRequest("user/2", Method.Delete);
var deleteUserResponse = client.Execute(deleteUserRequest);
Console.WriteLine("DELETE User Response");
Console.WriteLine(deleteUserResponse.Content);*/






/*var getUserRequest = new RestRequest("users", Method.Get);
getUserRequest.AddQueryParameter("page", "2");//Adding query parameter

var getUserResponse = client.Execute(getUserRequest);
Console.WriteLine("GET response: \n" + getUserResponse.Content);

var createUserRequest = new RestRequest("users", Method.Post);
createUserRequest.AddHeader("Content-Type", "application/json"); //Adding Header
createUserRequest.AddJsonBody(new
{
    name = "John Doe",
    job = "Software Developer"
});

var createUserResponse = client.Execute(createUserRequest);
Console.WriteLine("POST Create user response");
Console.WriteLine(createUserResponse.Content);

var updateUserRequest = new RestRequest("users/2", Method.Put);

updateUserRequest.AddHeader("Content-Type", "application/json");
updateUserRequest.AddJsonBody(new
{
    name = "John Doe",
    job = "Software Developer"
});

var updateUserResponse = client.Execute(updateUserRequest);
Console.WriteLine("PUT Update User response");
Console.WriteLine(updateUserResponse.Content);

var deleteUserRequest = new RestRequest("user/2", Method.Delete);
var deleteUserResponse = client.Execute(deleteUserRequest);
Console.WriteLine("DELETE User Response");
Console.WriteLine(deleteUserResponse.Content);*/



/*var getUserRequest = new RestRequest("users/5", Method.Get);
//getUserRequest.AddQueryParameter("page", "2");//Adding query parameter

var getUserResponse = client.Execute(getUserRequest);

if(getUserResponse.StatusCode==System.Net.HttpStatusCode.OK)
{
    //Parse JSON REsponse content
    JObject? userJson = JObject.Parse(getUserResponse?.Content);


    string? userName = userJson["data"]["first_name"].ToString();
    string? userLastName = userJson["data"]["last_name"].ToString();

    Console.WriteLine($"User Name:  {userName} {userLastName}");
}
else
{
    Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
}


var createUserRequest = new RestRequest("users", Method.Post);
createUserRequest.AddHeader("Content-Type", "application/json"); //Adding Header
createUserRequest.AddJsonBody(new
{
    name = "John Doe",
    job = "Software Developer"
});

var createUserResponse = client.Execute(createUserRequest);
Console.WriteLine("POST Create user response");
Console.WriteLine(createUserResponse.Content);

var updateUserRequest = new RestRequest("users/2", Method.Put);

updateUserRequest.AddHeader("Content-Type", "application/json");
updateUserRequest.AddJsonBody(new
{
    name = "John Doe",
    job = "Software Developer"
});

var updateUserResponse = client.Execute(updateUserRequest);
Console.WriteLine("PUT Update User response");
Console.WriteLine(updateUserResponse.Content);

var deleteUserRequest = new RestRequest("user/2", Method.Delete);
var deleteUserResponse = client.Execute(deleteUserRequest);
Console.WriteLine("DELETE User Response");
Console.WriteLine(deleteUserResponse.Content);*/


GetAllUsers(client);
GetSingleUser(client);
CreateUsers(client);
UpdateAllUsers(client);
DeleteAllUsers(client);

static void GetAllUsers(RestClient client)
{
    var getUserRequest = new RestRequest("users", Method.Get);
    getUserRequest.AddQueryParameter("page", "2");//Adding query parameter

    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("GET response: \n" + getUserResponse.Content);
}
static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("users/5", Method.Get);
    //getUserRequest.AddQueryParameter("page", "2");//Adding query parameter

    var getUserResponse = client.Execute(getUserRequest);

    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse JSON REsponse content
        JObject? userJson = JObject.Parse(getUserResponse?.Content);


        string? userName = userJson["data"]["first_name"].ToString();
        string? userLastName = userJson["data"]["last_name"].ToString();

        Console.WriteLine($"User Name:  {userName} {userLastName}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}
static void CreateUsers(RestClient client)
{
    var createUserRequest = new RestRequest("users", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json"); //Adding Header
    createUserRequest.AddJsonBody(new
    {
        name = "John Doe",
        job = "Software Developer"
    });

    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Create user response");
    Console.WriteLine(createUserResponse.Content);

}

static void UpdateAllUsers(RestClient client)
{
    var updateUserRequest = new RestRequest("users/2", Method.Put);

    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new
    {
        name = "John Doe",
        job = "Software Developer"
    });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update User response");
    Console.WriteLine(updateUserResponse.Content);
}

static void DeleteAllUsers(RestClient client)
{
    var deleteUserRequest = new RestRequest("user/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("DELETE User Response");
    Console.WriteLine(deleteUserResponse.Content);
}

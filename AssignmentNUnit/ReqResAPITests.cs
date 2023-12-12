using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNUnit
{
    [TestFixture]
    internal class ReqResAPITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [Order(0)]
        public void GetSingleUser()
        {
            var request = new RestRequest("posts/2", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userdata = JsonConvert.DeserializeObject<UserData>(response.Content);
            

            Assert.NotNull(userdata);
            Assert.That(userdata.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(userdata.Title);
        }
        [Test]
        [Order(1)]
        public void CreateUser()
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { title = "Hello", body = "HIII" });
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            var post = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(post);

        }
        [Test]
        [Order(2)]
        public void UpdateUser()
        {
            var request = new RestRequest("posts/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { title = "Updated Hello", body = "Updated HIII" });
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
        }
        [Test]
        [Order(3)]
        public void DeleteUser()
        {
            var request = new RestRequest("posts/2", Method.Delete);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
        [Test]
        [Order(4)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("posts/200", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}

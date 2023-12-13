using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using RestExNUnit.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNUnit
{
    [TestFixture]
    public class ReqResTests : CoreCodes
    {
        [Test]
        [Order(2)]
        [TestCase(2)]
        public void GetSingleUser(int usrid)
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("Get Single User Test Started");

            var request = new RestRequest("users/" + usrid, Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var userdata = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                UserData? user = userdata?.Data;

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.That(user.Id, Is.EqualTo(2));
                Log.Information("User ID matches with fetch");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Get Single user test passed all Asserts");

                test.Pass("Get single user test passed all Asserts");
            
            }
            catch(AssertionException)
            {
                test.Fail("Get Single User test failed");
            }
        }
        [Test]
        [Order(1)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("Create User test started");
            var request = new RestRequest("users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Create user test passed all asserts");

                test.Pass("Create User test passed all Asserts");
            }
            catch(AssertionException)
            {
                test.Fail("Create User test failed");
            }

            
            }
        [Test]
        [Order(3)]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update User");
            Log.Information("Update User test started");
            var request = new RestRequest("users/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "Updated John Doe", job = "Senior Software Developer" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("Updated user and returned");
                Log.Information("Update user test passed all Asserts ");

                test.Pass("Update user test passed all asserts");
            }
            catch
            {
                test.Fail("Update user test failed");
            }
            
        }
        [Test]
        [Order(4)]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete User");
            Log.Information("Delete user test started");

            var request = new RestRequest("users/2", Method.Delete);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information("User Deleted");
                Log.Information("delete user test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Delete user test failed");
            }

        }
        [Test]
        [Order(5)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("Get Non Existing User");
            Log.Information("Non Existing user test started");

            var request = new RestRequest("users/999", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("NonExisting user test passed all asserts");
                test.Pass("NonExisting user test passed all Asserts");
            }
            catch(AssertionException)
            {
                test.Fail("NonExisting user test failed");
            }
        }

    }
    
}


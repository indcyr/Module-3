using CaseStudy.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    [TestFixture]
    internal class BookingTest : CoreCodes
    {
        private string baseUrl = "https://restful-booker.herokuapp.com/";
        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }
        [Test, Order(1)]
        public void CreateToken()
        {
            test = extent.CreateTest("Create Token");
            Log.Information("CreateToken Test started");

            var req = new RestRequest("auth", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {res.Content}");
                Log.Information("Create Token test passed all asserts");

                test.Pass("Create Token test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Create Token test passed all asserts");
            }
        }
        [Test, Order(2)]
        public void GetBookingId()
        {
            test = extent.CreateTest("Get Booking Id");
            Log.Information("GetBookingId Test started");

            var req = new RestRequest("booking", Method.Get);
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {res.Content}");
                Log.Information("GetBookingId Token test passed all asserts");

                test.Pass("GetBookingId test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("GetBookingId test passed all asserts");
            }
        }
        [Test, Order(3)]
        public void GetBooking()
        {
            test = extent.CreateTest("Get Booking");
            Log.Information("Get Booking Test started");

            var req = new RestRequest("booking/13", Method.Get);
            req.AddHeader("Accept", "application/json");
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {res.Content}");

                var user = JsonConvert.DeserializeObject<Booking>(res.Content);
                Assert.NotNull(user);

                Log.Information("Get Booking test passed all asserts");
                test.Pass("Get Booking test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Get Booking test passed all asserts");
            }
        }
        [Test, Order(4)]
        public void CreateBooking()
        {
            test = extent.CreateTest("Create Booking");
            Log.Information("CreateBooking Test started");

            var req = new RestRequest("booking", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Accept", "application/json");
            req.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01",
                },
                additionalneeds = "Breakfast"
            });
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {res.Content}");

                var user = JsonConvert.DeserializeObject<Booking>(res.Content);
                Assert.NotNull(user);

                Log.Information("Create Booking test passed all asserts");
                test.Pass("Create Booking test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Create Booking test passed all asserts");
            }
        }
        [Test, Order(5)]
        public void UpdateBooking()
        {
            test = extent.CreateTest("Update Booking");
            Log.Information("UpdateBooking Test started");

            var req = new RestRequest("auth", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            var res = client.Execute(req);
            var token = JsonConvert.DeserializeObject<Booking>(res.Content);
            var reqst = new RestRequest("booking/13", Method.Put);
            reqst.AddHeader("Content-Type", "application/json");
            reqst.AddHeader("Accept", "application/json");
            reqst.AddHeader("Cookie", "token=" + token?.Token);
            reqst.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01",
                },
                additionalneeds = "Breakfast"
            });
            var response = client.Execute(reqst);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {res.Content}");

                var user = JsonConvert.DeserializeObject<Booking>(res.Content);
                Assert.NotNull(user);
                Log.Information("Update Booking test passed all asserts");
                test.Pass("Update Booking test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Update Booking test passed all asserts");
            }
        }
        [Test, Order(6)]
        public void DeleteBooking()
        {
            test = extent.CreateTest("Delete Booking");
            Log.Information("DeleteBooking Test started");

            var req = new RestRequest("auth", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            var res = client.Execute(req);
            var token = JsonConvert.DeserializeObject<Booking>(res.Content);
            var reqst = new RestRequest("booking/1", Method.Delete);
            reqst.AddHeader("Content-Type", "application/json");
            reqst.AddHeader("Cookie", "token=" + token?.Token);
            var response = client.Execute(reqst);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("Booking Deleted");
                Log.Information("Delete Booking test passed all asserts");

                test.Pass("Delete Booking test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Delete Booking test passed all asserts");
            }
        }
    }
}

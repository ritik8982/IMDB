﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;
using Xunit;

namespace DemoApp.Test.StepFiles
{
    public class BaseSteps
    {
        protected WebApplicationFactory<TestStartup> Factory;
        protected HttpClient Client { get; set; }
        protected HttpResponseMessage Response { get; set; }

        public BaseSteps(WebApplicationFactory<TestStartup> baseFactory)
        {
            Factory = baseFactory;
        }

        [Given(@"I am a client")]
        public void ImClient()
        {
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }

        [When(@"I make GET Request '(.*)'")]
        public virtual async Task MakeGet(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative); //http://localhost/api/actors
            Response = await Client.GetAsync(uri);
        }
        [When(@"I make GET Request '([^']*)' and provide id as (.*)")]
        public void WhenIMakeGETRequestAndProvideIdAs(string p0, int p1)
        {
            throw new PendingStepException();
        }


        [Then(@"response code must be '(.*)'")]
        public void ResponseCompare(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode) statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

        [Then(@"response data must look like '(.*)'")]
        public void CompareResponse(string p0)
        {
            var responseData = Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Assert.Equal(p0, responseData);
        }

        [When(@"I make PUT Request '(.*)' with the following Data with the following Data '(.*)'")]
        public virtual async Task MakePut(string resourceEndPoint,
            string putDataJson)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(putDataJson, Encoding.UTF8, "application/json");
            Response = await Client.PutAsync(postRelativeUri, content);
        }

        [When(@"I am making a post request to '(.*)' with the following Data '(.*)'")]
        public virtual async Task MakePost(string resourceEndPoint,
            string postDataJson)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
            Response = await Client.PostAsync(postRelativeUri, content);
        }

        [When(@"I make Delete Request '(.*)'")]
        public virtual async Task MakeDelete(string resourceEndPoint)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            Response = await Client.DeleteAsync(postRelativeUri);
        }
    }
}

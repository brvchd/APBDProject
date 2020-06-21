using AdvertAPI;
using AdvertAPI.DTOs.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.IntergrationTests
{
    public class GetCampaignsTest
    {
        /*
        private readonly HttpClient _client;
        public GetCampaignsTest(WebApplicationFactory<Startup> appFactory) //fixture contstructor issue
        {
            _client = appFactory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task GET_CAMPAIGNS_SUCCESS()
        {
            var login = "\"Login:\" \"mylogin2\", \"Password\": \"qwerty123\"";
            var body = new StringContent(login, Encoding.UTF8, "application/json");
            var httpResponseLogin = await _client.PostAsync("/api/clients/login", body);

            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(await httpResponseLogin.Content.ReadAsStringAsync());
            var token = loginResponse.AccessToken;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponseGetCampaigns = await _client.GetAsync("api/clients");
            var campaignsResponse = JsonConvert.DeserializeObject<GetCampaignsResponse>(await httpResponseGetCampaigns.Content.ReadAsStringAsync());

            httpResponseGetCampaigns.StatusCode.Should().Be(HttpStatusCode.OK);
            campaignsResponse.Should().NotBeNull();
        */


        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MerchantManagement.Tests
{
    public class MerchantApiTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;

        public MerchantApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
                    mockHttpMessageHandler.Protected()
                        .Setup<Task<HttpResponseMessage>>(
                            "SendAsync",
                            ItExpr.IsAny<HttpRequestMessage>(),
                            ItExpr.IsAny<CancellationToken>())
                        .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                        {
                            if (request.RequestUri!.ToString().Contains("InvalidCountry"))
                            {
                                return new HttpResponseMessage(HttpStatusCode.NotFound);
                            }

                            return new HttpResponseMessage
                            {
                                StatusCode = HttpStatusCode.OK,
                                Content = new StringContent("[{\"name\":{\"common\":\"Nigeria\"}}]")
                            };
                        });

                    var mockHttpClient = new HttpClient(mockHttpMessageHandler.Object)
                    {
                        BaseAddress = new Uri("https://restcountries.com/")
                    };

                    services.RemoveAll<IHttpClientFactory>();
                    var mockFactory = new Mock<IHttpClientFactory>();
                    mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);
                    services.AddSingleton(mockFactory.Object);
                });
            });
        }

        [Fact]
        public async Task CreateMerchant_Returns_Created_When_Country_Is_Valid()
        {
            var client = _factory.CreateClient();

            var merchant = new
            {
                BusinessName = "Test Business",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Status = "Active",
                Country = "Nigeria"
            };

            var content = new StringContent(JsonSerializer.Serialize(merchant), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/merchants", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateMerchant_Returns_BadRequest_When_Country_Is_Invalid()
        {
            var client = _factory.CreateClient();

            var merchant = new
            {
                BusinessName = "Test Business",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Status = "Active",
                Country = "InvalidCountry"
            };

            var content = new StringContent(JsonSerializer.Serialize(merchant), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/merchants", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateMerchant_Returns_ValidationErrors_When_Data_Is_Invalid()
        {
            var client = _factory.CreateClient();

            var merchant = new
            {
                BusinessName = "",
                Email = "invalid-email",
                PhoneNumber = "",
                Status = "UnknownStatus",
                Country = "Nigeria"
            };

            var content = new StringContent(JsonSerializer.Serialize(merchant), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/merchants", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("BusinessName", responseContent);
            Assert.Contains("Email", responseContent);
        }

        [Fact]
        public async Task GetAllMerchants_Returns_Ok()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/merchants");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task UpdateMerchant_Returns_NoContent()
        {
            var client = _factory.CreateClient();

            var merchant = new
            {
                BusinessName = "Test Business",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Status = "Active",
                Country = "Nigeria"
            };

            var createContent = new StringContent(JsonSerializer.Serialize(merchant), Encoding.UTF8, "application/json");
            var createResponse = await client.PostAsync("/api/merchants", createContent);
            var createdMerchant = JsonSerializer.Deserialize<JsonElement>(await createResponse.Content.ReadAsStringAsync());
            var merchantId = createdMerchant.GetProperty("id").GetGuid();

            var updatedMerchant = new
            {
                Id = merchantId,
                BusinessName = "Updated Business",
                Email = "updated@example.com",
                PhoneNumber = "987654321",
                Status = "Inactive",
                Country = "Nigeria"
            };

            var updateContent = new StringContent(JsonSerializer.Serialize(updatedMerchant), Encoding.UTF8, "application/json");
            var updateResponse = await client.PutAsync($"/api/merchants/{merchantId}", updateContent);

            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteMerchant_Returns_NoContent()
        {
            var client = _factory.CreateClient();

            var merchant = new
            {
                BusinessName = "Test Business",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Status = "Active",
                Country = "Nigeria"
            };

            var createContent = new StringContent(JsonSerializer.Serialize(merchant), Encoding.UTF8, "application/json");
            var createResponse = await client.PostAsync("/api/merchants", createContent);
            var createdMerchant = JsonSerializer.Deserialize<JsonElement>(await createResponse.Content.ReadAsStringAsync());
            var merchantId = createdMerchant.GetProperty("id").GetGuid();

            var deleteResponse = await client.DeleteAsync($"/api/merchants/{merchantId}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }

}
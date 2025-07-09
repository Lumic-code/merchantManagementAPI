using FluentValidation;
using MediatR;
using MerchantManagement.API.Requests;
using MerchantManagement.App.Merchants.Commands.Create;
using MerchantManagement.App.Merchants.Commands.Delete;
using MerchantManagement.App.Merchants.Commands.Update;
using MerchantManagement.App.Merchants.Queries.Get;
using MerchantManagement.App.Merchants.Queries.List;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace MerchantManagement.API.Endpoints
{
    public static class MerchantEndpoints
    {

        public static void MapMerchantEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/merchants", async (CreateMerchantCommand command, IMediator mediator, IValidator<CreateMerchantCommand> validator, IHttpClientFactory httpClientFactory, IMemoryCache cache, ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("MerchantEndpoints");
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var cacheKey = $"country_{command.Country.ToLower()}";
                if (!cache.TryGetValue(cacheKey, out bool isCountryValid))
                {
                    try
                    {
                        var httpClient = httpClientFactory.CreateClient();
                        var response = await httpClient.GetAsync($"https://restcountries.com/v3.1/name/{Uri.EscapeDataString(command.Country)}");

                        isCountryValid = response.IsSuccessStatusCode;

                        cache.Set(cacheKey, isCountryValid, TimeSpan.FromHours(12));
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Country API call failed.");
                        return Results.StatusCode(500);
                    }
                }

                if (!isCountryValid)
                    return Results.BadRequest(new { error = "Invalid country provided." });

                var merchant = await mediator.Send(command);
                return Results.Created($"/api/merchants/{merchant.Id}", merchant);
            });


            app.MapGet("/api/merchants/{id}", async (Guid id, IMediator mediator) =>
            {
                var merchant = await mediator.Send(new GetMerchantByIdQuery { Id = id });
                return merchant is not null ? Results.Ok(merchant) : Results.NotFound();
            });

            app.MapGet("/api/merchants", async (IMediator mediator) =>
            {
                var merchants = await mediator.Send(new GetAllMerchantsQuery());
                return Results.Ok(merchants);
            });

            app.MapPut("/api/merchants/{id}", async (Guid id, UpdateMerchantRequest request, IMediator mediator, IValidator< UpdateMerchantRequest> validator) =>
            {
               

                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                try
                {
                    var command = new UpdateMerchantCommand
                    {
                        Id = id,
                        BusinessName = request.BusinessName,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Status = request.Status,
                        Country = request.Country
                    };

                    await mediator.Send(command);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            });

            app.MapDelete("/api/merchants/{id}", async (Guid id, IMediator mediator) =>
            {
                try
                {
                    await mediator.Send(new DeleteMerchantCommand { Id = id });
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            });
        }
    }
}

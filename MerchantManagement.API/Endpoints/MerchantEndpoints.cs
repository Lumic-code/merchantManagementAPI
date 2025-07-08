using FluentValidation;
using MediatR;
using MerchantManagement.App.Merchants.Commands.Create;
using MerchantManagement.App.Merchants.Commands.Delete;
using MerchantManagement.App.Merchants.Commands.Update;
using MerchantManagement.App.Merchants.Queries.Get;
using MerchantManagement.App.Merchants.Queries.List;
using System.Net.Http;

namespace MerchantManagement.API.Endpoints
{
    public static class MerchantEndpoints
    {

        public static void MapMerchantEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/merchants", async (CreateMerchantCommand command, IMediator mediator, IValidator<CreateMerchantCommand> validator, IHttpClientFactory httpClientFactory) =>
            {
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var httpClient = httpClientFactory.CreateClient();
                var countryApiUrl = $"https://restcountries.com/v3.1/name/{Uri.EscapeDataString(command.Country)}";

                try
                {
                    using var response = await httpClient.GetAsync(countryApiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        return Results.BadRequest(new { error = "Invalid country provided." });
                    }
                }
                catch
                {
                    return Results.StatusCode(500);
                }

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

            app.MapPut("/api/merchants/{id}", async (Guid id, UpdateMerchantCommand command, IMediator mediator) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Mismatched Merchant ID.");

                await mediator.Send(command);
                return Results.NoContent();
            });

            app.MapDelete("/api/merchants/{id}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteMerchantCommand { Id = id });
                return Results.NoContent();
            });
        }
    }
}

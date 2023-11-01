using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Api.Endpoints.Mappers.Offers;
using MyShop.Api.Endpoints.Requests.Offers;
using MyShop.Api.Endpoints.Responses.Offers;
using MyShop.Application.Commands.Offers;
using MyShop.Application.Queries.Offers;
using MyShop.Common.Contracts;
using MyShop.Domain.ViewModels;

namespace MyShop.Api.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all offers
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Offer/all
        ///     { }
        /// </remarks>
        /// <response code="200">
        /// The real status code can be found in the output<br></br>
        /// =================================<br></br>
        /// Sample success response:<br></br>
        /// {<br></br>
        ///   "StatusCode": 200,<br></br>
        ///   "ErrorCode": "",<br></br>
        ///   "ErrorMessage": "",<br></br>
        ///   "Result": <br></br>
        ///   [{<br></br>
        ///     "productId": 64,<br></br>
        ///     "productName": "string",<br></br>
        ///     "productBrand": "string",<br></br>
        ///     "productSize": "string",<br></br>
        ///     "quantity": 1,<br></br>
        ///     "price": 1<br></br>
        ///   }]<br></br>
        /// }<br></br>
        /// </response>
        [HttpGet("all", Name = "All")]
        [ProducesResponseType(typeof(HttpResponseBase<IEnumerable<OfferVm>>), 200)]
        public async Task<HttpResponseBase<IEnumerable<OfferVm>>> All(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AllOffersQuery(), cancellationToken);

            return response.ToAllContract().ToHttpResponse();
        }

        /// <summary>
        /// Add an offer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Offer/add
        ///     {
        ///        "productName": "myproductname",
        ///        "productBrand": "myproductbrand",
        ///        "productSize": "myproductsize",
        ///        "quantity": 10,
        ///        "price": 19.99
        ///     }
        /// </remarks>
        /// <response code="200">
        /// The real status code can be found in the output<br></br>
        /// =================================<br></br>
        /// Sample success response:<br></br>
        /// {<br></br>
        ///   "StatusCode": 200,<br></br>
        ///   "ErrorCode": "",<br></br>
        ///   "ErrorMessage": "",<br></br>
        ///   "Result": <br></br>
        ///   {<br></br>
        ///     "productId": 64,<br></br>
        ///     "productName": "string",<br></br>
        ///     "productBrand": "string",<br></br>
        ///     "productSize": "string",<br></br>
        ///     "quantity": 1,<br></br>
        ///     "price": 1<br></br>
        ///   }<br></br>
        /// }<br></br>
        /// =================================<br></br>
        /// Sample error response:<br></br>
        /// {<br></br>
        ///   "StatusCode": 400,<br></br>
        ///   "ErrorCode": "InvalidArgumentsException",<br></br>
        ///   "ErrorMessage": "'Price' must be greater than '0'.;'Quantity' must be greater than '0'.",<br></br>
        ///   "Result": null<br></br>
        /// }<br></br>
        /// </response>
        [HttpPost("add", Name = "Add")]
        [ProducesResponseType(typeof(HttpResponseBase<OfferAddResponse>), 200)]
        public async Task<HttpResponseBase<OfferAddResponse>> Add(OfferAddRequest request, CancellationToken cancellationToken)
        {
            var query = new AddOffersCommand(request.ToDomain());

            var response = await _mediator.Send(query, cancellationToken);

            return response.ToAddContract().ToHttpResponse();
        }

        /// <summary>
        /// Add an offer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Offer/update
        ///     {
        ///        "productId": 1,
        ///        "productName": "myproductname",
        ///        "productBrand": "myproductbrand",
        ///        "productSize": "myproductsize",
        ///        "quantity": 10,
        ///        "price": 19.99
        ///     }
        /// </remarks>
        /// <response code="200">
        /// The real status code can be found in the output<br></br>
        /// =================================<br></br>
        /// Sample success response:<br></br>
        /// {<br></br>
        ///   "StatusCode": 200,<br></br>
        ///   "ErrorCode": "",<br></br>
        ///   "ErrorMessage": "",<br></br>
        ///   "Result": <br></br>
        ///   {<br></br>
        ///     "productId": 64,<br></br>
        ///     "productName": "string",<br></br>
        ///     "productBrand": "string",<br></br>
        ///     "productSize": "string",<br></br>
        ///     "quantity": 1,<br></br>
        ///     "price": 1<br></br>
        ///   }<br></br>
        /// }<br></br>
        /// =================================<br></br>
        /// Sample error response:<br></br>
        /// {<br></br>
        ///   "StatusCode": 400,<br></br>
        ///   "ErrorCode": "InvalidArgumentsException",<br></br>
        ///   "ErrorMessage": "'Price' must be greater than '0'.;'Quantity' must be greater than '0'.",<br></br>
        ///   "Result": null<br></br>
        /// }<br></br>
        /// </response>
        [HttpPut("update", Name = "Update")]
        [ProducesResponseType(typeof(HttpResponseBase<OfferUpdateResponse>), 200)]
        public async Task<HttpResponseBase<OfferUpdateResponse>> Update(OfferUpdateRequest request, CancellationToken cancellationToken)
        {
            var query = new UpdateOffersCommand(request.ToDomain());

            var response = await _mediator.Send(query, cancellationToken);

            return response.ToUpdateContract().ToHttpResponse();
        }
    }
}
using Application.Product.Commands.BuyProduct;
using Application.Product.Commands.CreateProduct;
using Application.Product.Commands.DeleteProduct;
using Application.Product.Commands.UpdateProduct;
using Application.Product.DTOs;
using Application.Product.Queries;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    // [Authorize]
    public class ProductController : BaseController
    {
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<ProductDto>>> Get()
        {
            return Ok(await Mediator.Send(new GetProductListQuery()));
        }

        [HttpPost]
        // [Authorize(Roles = Roles.ADMINISTRATOR)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Unit>> Create(CreateProductCommand command)
        {
            var value = await Mediator.Send(command);

            return base.Ok(value);
        }

        [HttpPost]
        // [Authorize(Roles = Roles.CLIENT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Unit>> Purchase(PurchaseProductCommand command)
        {
            var value = await Mediator.Send(command);

            return base.Ok(value);
        }

        [HttpPut]
        // [Authorize(Roles = Roles.ADMINISTRATOR)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Unit>> Update(UpdateProductCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = Roles.ADMINISTRATOR)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            await Mediator.Send(new DeleteProductCommand { ProductId = Guid.Parse(id) });

            return NoContent();
        }
    }

}

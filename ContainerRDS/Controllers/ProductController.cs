using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using DataAccess.Services;
using AutoMapper;
using DataAccess.JsonModels;
using DataAccess.Static;
namespace ContainerRDS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IProductService service;
        private readonly IMapper mapper;

        public ProductController(ILogger<ProductController> logger,
            IProductService service, IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<OkObjectResult> GetProducts()
        {
            var products = await service.GetAll();
            if (products.Count > 0)
            {
                var model = new ProductListJsonModel(products);
                return Ok(model);
            }
            return Ok(new ProductListJsonModel(false, "Product list is empty"));
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetProductById(int id)
        {
            if (SDHelper.IsValueNotNull(id.ToString()))
            {
                var user = await service.Find(s => s.Id == id);
                if (user != null)
                    return Ok(mapper.Map<Product, ProductJsonModel>(user));
                return Ok(new ProductJsonModel() { Error = "There is no product with this Id", IsSuccess = false });
            }
            return Ok(new ProductJsonModel("Id field is empty", false));
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<OkObjectResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return Ok(new ProductJsonModel("Bad request", false));
            }

            await service.Update(product);

            return Ok(product);
        }

        // POST: api/Product
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<OkObjectResult> PostProduct(Product product)
        {
            await service.Create(product);
            return Ok(product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<OkObjectResult> DeleteProduct(int id)
        {
            var product = await service.Find(s => s.Id == id);
            if (product == null)
            {
                Ok(new ProductJsonModel("Not found", false));
            }

            await service.Delete(product);
            return Ok(product);
        }
    }
}

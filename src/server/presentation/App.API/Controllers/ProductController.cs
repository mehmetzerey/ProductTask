using App.Application.ModelView.Ticket;
using App.Application.Repositories.Products;
using App.Domain.Enums;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using App.Application.ModelView.Product;
using App.Domain.Models;
using System.Diagnostics;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IWriteProductRepository _writeProductRepository;
        private readonly IReadProductRepository _readProductRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IWriteProductRepository writeProductRepository, IReadProductRepository readProductRepository, IWebHostEnvironment environment)
        {
            _writeProductRepository = writeProductRepository;
            _readProductRepository = readProductRepository;
            _environment = environment;
        }
        // GET: ProductController
        [HttpGet("GetAll")]
        public IEnumerable<GetProductViewModel> GetAll()
        {
            return _readProductRepository.GetAll();
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public GetProductViewModel Get(int id)
        {
            var x = _readProductRepository.GetById(id);

            return new GetProductViewModel
            {
                Code = x.Code,
                Created = x.CreatedDate,
                Id = x.Id,
                ImageURL = x.ImageURL,
                Name = x.Name,
                Price = x.Price
            };
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CreateProductViewModel model)
        {
            var response = new ServiceResponse();
            var fileUrl = string.Empty;
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Boş alanları doldurun";
                return BadRequest(response);
            }
            else
            {
                if (model.FileLink != null)
                {
                    if (model.FileLink.Length / 1024 < 2048)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString();
                        string NewString = model.FileLink.ContentType.Replace("File/", ".");
                        if (NewString == "image/png")
                        {
                            NewString = ".png";
                        }
                        if (NewString == "image/jpeg")
                        {
                            NewString = ".jpeg";
                        }
                        if (NewString == "image/jpg")
                        {
                            NewString = ".jpg";
                        }
                        var Upload = Path.Combine(_environment.ContentRootPath, "MyFiles\\", uniqueFileName + NewString);
                        var path = "MyFiles";
                        if (!Directory.Exists(path))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(path);
                            Debug.WriteLine(Directory.GetCreationTime(path));
                        }
                        // Delete the directory.
                        // di.Delete();
                        fileUrl = uniqueFileName + NewString;
                        model.FileLink.CopyTo(new FileStream(Upload, FileMode.Create));
                    }

                }
                await _writeProductRepository.Create(new Product {
                    Code = model.Code,
                    CreatedDate = DateTime.Now,
                    ImageURL = fileUrl,
                    Name = model.Name,
                    Price = model.Price
                });

                response.IsSuccess = true;

                return Ok(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm]PutProductViewModel model)
        {
            var response = new ServiceResponse();
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Boş alanları doldurun";
                return BadRequest(response);
            }
            else
            {
                var getProduct = _readProductRepository.GetById(model.Id);
                var fileUrl = getProduct.ImageURL;

                if (getProduct != null)
                {
                    if (model.FileLink != null && model.FileLink.Length > 0)
                    {
                        if (model.FileLink.Length / 1024 < 2048)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString();
                            string NewString = model.FileLink.ContentType.Replace("File/", ".");
                            if (NewString == "image/png")
                            {
                                NewString = ".png";
                            }
                            if (NewString == "image/jpeg")
                            {
                                NewString = ".jpeg";
                            }
                            if (NewString == "image/jpg")
                            {
                                NewString = ".jpg";
                            }
                            var Upload = Path.Combine(_environment.ContentRootPath, "MyFiles\\", uniqueFileName + NewString);
                            var path = "MyFiles";
                            if (!Directory.Exists(path))
                            {
                                DirectoryInfo di = Directory.CreateDirectory(path);
                                Debug.WriteLine(Directory.GetCreationTime(path));
                            }
                            // Delete the directory.
                            // di.Delete();
                            fileUrl = uniqueFileName + NewString;
                            model.FileLink.CopyTo(new FileStream(Upload, FileMode.Create));
                        }

                    }
                    getProduct.ImageURL = fileUrl;
                    getProduct.Name = model.Name;
                    getProduct.Price = model.Price;
                    getProduct.Code = model.Code;
                    await _writeProductRepository.UpdateProduct(getProduct);
                    response.IsSuccess = true;
                    return Ok(response);
                }
                response.IsSuccess = false;
                return BadRequest(response);

            }
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var getProduct = _readProductRepository.GetById(id);
            if (getProduct != null)
            {
                await _writeProductRepository.DeleteProduct(getProduct);
            }
        }
    }
}

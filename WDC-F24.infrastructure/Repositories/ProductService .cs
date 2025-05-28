using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDC_F24.infrastructure.Data;
using WDC_F24.Application;
using WDC_F24.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WDC_F24.Application.Interfaces;
using WDC_F24.Application.DTOs.Responses;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WDC_F24.Application.DTOs.Requests;
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;


namespace WDC_F24.infrastructure.Repositories
{
    public class Productervice : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IRabbitMQPublisher _publisher;



        public Productervice(AppDbContext context, IRabbitMQPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
            
        }

        public async Task<GeneralResponse> GetAllAsync()
        {
            var ErrorMsg = "";
            try
            {

                var product = await _context.Products.ToListAsync();

                try
                {
                    _publisher.Publish("products-received", new
                    {
                        product = product,
                        Message = $"Products was received successfully"
                    }); 
                }
                catch (Exception pubEx)
                {
                    ErrorMsg = " , " + pubEx.Message + "publish Failed";
                }
                return GeneralResponse.Ok("products received successfully"+ ErrorMsg, product );


            }
                catch (DbUpdateException dbEx)
                {
                    return GeneralResponse.BadRequest($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                }
                catch (Exception ex)
                {
                    return GeneralResponse.BadRequest($"Unexpected error: {ex.Message}");
                }
            }

        public async Task<GeneralResponse> GetByIdAsync(Guid id)
        {
            try
            {
                var ErrorMsg = "";
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return GeneralResponse.BadRequest("id is required");
                }
                var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();


                try
                {
                    _publisher.Publish("product-received", new
                    {
                        product = product,
                        Message = $"Product {product.Id} was received successfully"
                    });
                }
                catch (Exception pubEx)
                {
                    ErrorMsg = " , " + pubEx.Message + "publish Failed";
                }
                return GeneralResponse.Ok("product received successfully"+ ErrorMsg, product );
            }

            catch (DbUpdateException dbEx)
            {
                return GeneralResponse.BadRequest($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return GeneralResponse.BadRequest($"Unexpected error: {ex.Message}");
            }
        }
        public async Task<GeneralResponse> DeleteAsync(Guid id)
        {
            var ErrorMsg = "";
            try
            {

                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return GeneralResponse.BadRequest("id is required");
                }
                var product = await _context.Products.Where(x=>x.Id == id ).FirstOrDefaultAsync();


                    _context.Products.Remove(product!);
                await _context.SaveChangesAsync();
                try
                {
                    _publisher.Publish("product-deleted", new
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Message = $"Product {product.Id} was deleted successfully"
                    });
                }
                catch (Exception pubEx)
                {
                    ErrorMsg = " , " + pubEx.Message + "publish Failed";
                }

                return GeneralResponse.Ok("product deleted successfully" + ErrorMsg);
            }
            catch (DbUpdateException dbEx)
            {
                return GeneralResponse.BadRequest($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return GeneralResponse.BadRequest($"Unexpected error: {ex.Message}");
            }
        } 
        public async Task<GeneralResponse> AddAsync(AddProudctRequestDto product)
        {
            var ErrorMsg = "";
            try
            {
                var exsistProduct = await _context.Products.Where(x => x.Name == product.Name).FirstOrDefaultAsync();
                if (exsistProduct != null)
                {
                    return GeneralResponse.BadRequest(product.Name + " already exist");
                }
                var addProduct = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                };


                await _context.Products.AddAsync(addProduct!);
                await _context.SaveChangesAsync();

                try
                {
                    _publisher.Publish("product-created", new
                    {
                        Id = addProduct.Id, 
                        Name = addProduct.Name,
                        Price = addProduct.Price,
                        Message = $"Product {addProduct.Id} was created successfully"
                    });
                }
                catch (Exception pubEx)
                {
                    ErrorMsg = " , " + pubEx.Message + "publish Failed";
                }

                return GeneralResponse.Ok("product added successfully" + ErrorMsg , addProduct);
            }
            catch (DbUpdateException dbEx)
            {
                return GeneralResponse.BadRequest($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return GeneralResponse.BadRequest($"Unexpected error: {ex.Message}");
            }

        }

        public async Task<GeneralResponse> UpdateAsync(UpdateProductRequestDto product )
        {
            var ErrorMsg = "";
            try
            {

           
                    var exsistProduct = await _context.Products.Where(x => x.Name == product.Name).FirstOrDefaultAsync();
                if (exsistProduct != null)
                {
                    return GeneralResponse.BadRequest(product.Name + "already exist");
                }

                var GetProduct = await _context.Products.Where(x => x.Id == product.Productid).FirstOrDefaultAsync();

                if (GetProduct == null)
                {
                    return GeneralResponse.BadRequest("product not available");
                }
                GetProduct.Name = product.Name ?? GetProduct.Name;
                GetProduct.Price = product.Price > 0 ? product.Price : GetProduct.Price;
                GetProduct.UpdatedAt = DateTime.UtcNow;


                _context.Products.Update(GetProduct);
                await _context.SaveChangesAsync();
                try
                {
                    _publisher.Publish("product-updated", new
                    {
                        Id = GetProduct.Id,
                        Name = GetProduct.Name,
                        Price = GetProduct.Price,
                        Message = $"Product {GetProduct.Id} was updated successfully"
                    });
                }
                catch (Exception pubEx)
                {
                    ErrorMsg = ", "+pubEx.Message + "publish Failed";
                }

                return GeneralResponse.Ok("product updated successfully " + ErrorMsg);

            }
            catch (DbUpdateException dbEx)
            {
                return GeneralResponse.BadRequest($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return GeneralResponse.BadRequest($"Unexpected error: {ex.Message}");
            }
        }



    }
}

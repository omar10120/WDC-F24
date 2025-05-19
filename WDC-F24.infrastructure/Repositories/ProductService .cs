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


namespace WDC_F24.infrastructure.Repositories
{
    public class Productervice : IProductService
    {
        private readonly AppDbContext _context;


        public Productervice(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> get()
        {

            var product = await _context.Product.ToListAsync();
            return GeneralResponse.Ok("Done");
        }


        //public async Task<IEnumerable<Product>> GetAllAsync()
        //{
        //    return await _context.Product.ToListAsync();
        //}

        //public async Task<Product> GetByIdAsync(int id)
        //{
        //    return await _context.Product.FindAsync(id);
        //}

        //public async Task<Product> AddAsync(Product product)
        //{
        //    _context.Product.Add(product);
        //    await _context.SaveChangesAsync();
        //    return product;
        //}
    }
}

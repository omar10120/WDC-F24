using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDC_F24.Application.DTOs.Responses;

namespace WDC_F24.Application.Interfaces
{
    public interface IProductService
    {
        //Task<IEnumerable<Product>> GetAllAsync();
        //Task<Product> GetByIdAsync(int id);
        //Task<Product> AddAsync(Product product);

        Task<GeneralResponse> get();


    }
}

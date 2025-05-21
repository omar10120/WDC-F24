using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDC_F24.Application.DTOs.Requests;
using WDC_F24.Application.DTOs.Responses;

namespace WDC_F24.Application.Interfaces
{
    public interface IProductService
    {
        Task<GeneralResponse> GetAllAsync();
        Task<GeneralResponse> GetByIdAsync(Guid id);
        Task<GeneralResponse> DeleteAsync(Guid id);
        Task<GeneralResponse> AddAsync(AddProudctRequestDto product);
        Task<GeneralResponse> UpdateAsync(AddProudctRequestDto product , Guid id);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WDC_F24.Application.DTOs.Requests;
using WDC_F24.Application.DTOs.Responses;
using WDC_F24.Application.Interfaces;
using WDC_F24.Domain.Entities;
using WDC_F24.UtilityServices;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductsController(IProductService productService, IHttpContextAccessor httpContextAccessor)
    {
        _productService = productService;
        _httpContextAccessor = httpContextAccessor;
        
    }


    [HttpGet("GetProduct")]
    //[ApiExplorerSettings(GroupName = "UserApp")]
    public async Task<IActionResult> Get()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var Result = await _productService.GetAllAsync();
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var Result = await _productService.GetByIdAsync(id);
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var Result = await _productService.DeleteAsync(id);
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
   
    [HttpPost("AddProduct")]
    public async Task<IActionResult> Add(AddProudctRequestDto product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var Result = await _productService.AddAsync(product);
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }  
    [HttpPatch("UpdateProduct")]
    public async Task<IActionResult> Update(Guid id,AddProudctRequestDto product )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var Result = await _productService.UpdateAsync(product , id);
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}

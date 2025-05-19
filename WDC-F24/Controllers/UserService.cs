using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> get()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var Result = await _productService.get();
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }





}

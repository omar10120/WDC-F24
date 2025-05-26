using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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


    [HttpGet]
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
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userName = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        var UserIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            if (!Guid.TryParse(UserIdentifier, out var userid))
                return Unauthorized("Invalid token");

            var Result = await _productService.DeleteAsync(id);
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [Authorize]
    [HttpPost("AddProduct")]
    public async Task<IActionResult> Add(AddProudctRequestDto product)
    {
        
        var UserIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
         
            if (string.IsNullOrEmpty(UserIdentifier) || !Guid.TryParse(UserIdentifier, out var userId))
                return Unauthorized("Invalid token or user ID");
            var Result = await _productService.AddAsync(product);
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [Authorize]
    [HttpPatch("UpdateProduct")]
    public async Task<IActionResult> Update(UpdateProductRequestDto product )
    {
        var UserIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            if (string.IsNullOrEmpty(UserIdentifier) || !Guid.TryParse(UserIdentifier, out var userId))
                return Unauthorized("Invalid token or user ID");
            var Result = await _productService.UpdateAsync(product);
            var Response = Result.ToActionResult();
            return Response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}

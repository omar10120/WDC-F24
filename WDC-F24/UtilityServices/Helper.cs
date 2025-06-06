﻿using Microsoft.AspNetCore.Mvc;
using WDC_F24.Application.DTOs.Responses;

namespace WDC_F24.UtilityServices
{
    public static class Helper
    {
        public static IActionResult ToActionResult(this GeneralResponse response)

        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}

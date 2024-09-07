using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solution.dotnet_ids.Models;

namespace Solution.dotnet_ids.Services
{
    public class WrapperService
    {
        //> Delegate
        public static async Task<IActionResult> ControllerWrapper(Func<Task<IActionResult>> action, ApiResponse _response, ControllerBase controller)
        {
            try
            {

                return await action(); 

            }
            catch (Exception ex)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSucess = false;
                _response.ErrorMessages.Add(ex.Message);
                return controller.BadRequest(_response);
            }



        }

    }
}
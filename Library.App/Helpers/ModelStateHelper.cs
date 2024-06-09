using Microsoft.AspNetCore.Mvc;

namespace Library.App.Helpers;

public static class ModelStateHelper
{
    public static void HandleModelStateErrors(Controller controller)
    {
        var errorMessages = controller.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        controller.TempData["ErrorMessage"] = string.Join("<br />", errorMessages);
    }
}
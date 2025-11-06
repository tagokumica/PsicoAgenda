using Domain.Notifiers;
using Microsoft.AspNetCore.Mvc;

namespace PsicoAgendaAPI.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (!_notifier.HasNotifications())
                return Ok(result);

            var errors = _notifier.GetNotifications()
                .Select(n => new { field = n.Key, message = n.Message });

            return UnprocessableEntity(new { errors });
        }
    }
}

using Domain.Notifiers;
using Microsoft.AspNetCore.Mvc;

namespace PsicoAgendaAPI.Controllers
{
    [ApiController]
    public abstract class NotifierController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected NotifierController(INotifier notifier)
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

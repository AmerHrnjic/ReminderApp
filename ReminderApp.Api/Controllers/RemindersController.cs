using Microsoft.AspNetCore.Mvc;
using ReminderApp.Application.Services.Interfaces;
using ReminderApp.Common.Dtos.Api;
using ReminderApp.Common.Enums;

namespace ReminderApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemindersController(
        IReminderService reminderService
    ) : ControllerBase
    {
        [HttpGet(Name = "reminders")]
        public async Task<ActionResult<IList<ReminderResponseDto>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok((await reminderService.GetAllRemindersAsync(cancellationToken))
                .Select(r => new ReminderResponseDto(r.Id.ToString(), r.Message, r.SendAt.ToString(), r.Status.ToString(), r.Email))
                .ToList());
        }

        [HttpPost(Name = "reminders")]
        public async Task<ActionResult<IList<CreateReminderResponseDto>>> CreateReminder(CreateReminderRequestDto createReminderDto, CancellationToken cancellationToken)
        {
            var result = await reminderService.CreateReminderAsync(new Domain.Entities.Reminder()
            {
                Message = createReminderDto.Message,
                SendAt = createReminderDto.SendAt,
                Status = ReminderStatus.Scheduled,
                Email = createReminderDto.Email
            }, cancellationToken);

            return Ok(new CreateReminderResponseDto(result.Id.ToString(), result.SendAt.ToString(), result.Status.ToString()));
        }
    }
}

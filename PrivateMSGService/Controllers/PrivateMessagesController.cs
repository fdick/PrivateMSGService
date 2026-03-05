using Microsoft.AspNetCore.Mvc;
using PrivateMSGService.API.Contracts;
using PrivateMSGService.Core.Abstractions;
using PrivateMSGService.Core.Models;

namespace PrivateMSGService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrivateMessagesController : ControllerBase
    {
        private readonly IPrivateMessagesService _privateMessagesService;

        public PrivateMessagesController(IPrivateMessagesService privateMessagesService)
        {
            this._privateMessagesService = privateMessagesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PrivateMessageResponse>>> GetAll()
        {
            var msges = await _privateMessagesService.GetAllAsync();

            var results = msges.Select(x => new PrivateMessageResponse(
                x.Item1.ID,
                x.Item1.FromUserID,
                x.Item1.ToUserID,
                x.Item1.Message,
                x.Item1.SentTime
                ));

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult <Guid>> CreateMessage([FromBody] PrivateMessageRequest request)
        {
            var (msg, error) = PrivateMessage.Create(
                Guid.NewGuid(),
                request.fromUserID,
                request.toUserID,
                request.message,
                DateTime.UtcNow
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }


            var msgID = await _privateMessagesService.CreatePrivateMsgAsync(msg);

            if (msgID == Guid.Empty)
            {
                return BadRequest("Stupid request\n" + request);
            }

            return Ok(msgID);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteMessage(Guid messageID)
        {
            var guid = await _privateMessagesService.DeletePrivateMsgAsync(messageID);

            return guid;
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateMessage(Guid messageID, [FromBody] PrivateMessageRequest updateData)
        {
            var guid = await _privateMessagesService.UpdatePrivateMsgAsync(
                messageID,
                updateData.toUserID,
                updateData.fromUserID,
                updateData.message,
                DateTime.UtcNow
                );

            return guid;
        }


    }
}

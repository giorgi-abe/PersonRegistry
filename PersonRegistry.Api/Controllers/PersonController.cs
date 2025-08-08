using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonRegistry.Api.Models.Person.Requests;
using PersonRegistry.Application.Features.Person.Commands.AddPerson;
using PersonRegistry.Application.Features.Person.Commands.AddPersonRelation;
using PersonRegistry.Application.Features.Person.Commands.DeletePerson;
using PersonRegistry.Application.Features.Person.Commands.RemovePersonRelation;
using PersonRegistry.Application.Features.Person.Commands.RemovePhoneNumber;
using PersonRegistry.Application.Features.Person.Commands.UpdatePersonBasicInfo;
using PersonRegistry.Application.Features.Person.Queries.GetPersonById;
using PersonRegistry.Application.Features.Person.Queries.SearchPeople;

namespace PersonRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(ISender sender,IMapper mapper) : ControllerBase
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        // POST: api/person
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddPersonRequest request)
        {
            var command = _mapper.Map<AddPersonCommand>(request);
            var result = await _sender.Send(command);
            return Ok(result);
        }

        // PUT: api/person/{id}
        [HttpPut]
        public async Task<IActionResult> Update( [FromBody] UpdatePersonRequest request)
        {

            var command = _mapper.Map<UpdatePersonBasicInfoCommand>(request);
            await _sender.Send(command);
            return NoContent();
        }

        // DELETE: api/person/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sender.Send(new DeletePersonCommand(id));
            return NoContent();
        }

        // POST: api/person/{id}/relations
        [HttpPost("relations")]
        public async Task<IActionResult> AddRelation([FromBody] AddPersonRelationRequest request)
        {

            var command = _mapper.Map<AddPersonRelationCommand>(request);
            await _sender.Send(command);
            return Ok();
        }

        // DELETE: api/person/{id}/relations
        [HttpDelete("{id:guid}/relations")]
        public async Task<IActionResult> RemoveRelation(Guid id, [FromQuery] Guid relatedPersonId)
        {
            await _sender.Send(new RemovePersonRelationCommand(id, relatedPersonId));
            return NoContent();
        }

        // GET: api/person/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetPersonByIdQuery(id));
            return Ok(result);
        }

        // GET: api/person/search
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchPeopleRequestModel model)
        {
            var query = _mapper.Map<SearchPeopleQuery>(model);
            var result = await _sender.Send(query);
            return Ok(result);
        }

        // POST: api/person/{id}/phonenumbers
        [HttpPost("phonenumbers")]
        public async Task<IActionResult> AddPhoneNumber([FromBody] AddPhoneNumberRequest request)
        {

            var command = _mapper.Map<AddPersonRelationCommand>(request);
            await _sender.Send(command);
            return Ok();
        }

        // DELETE: api/person/{id}/phonenumbers
        [HttpDelete("{id:guid}/phonenumbers")]
        public async Task<IActionResult> RemovePhoneNumber(Guid id, [FromQuery] Guid phoneNumberId)
        {
            await _sender.Send(new RemovePhoneNumberCommand(id, phoneNumberId));
            return NoContent();
        }
    }
}

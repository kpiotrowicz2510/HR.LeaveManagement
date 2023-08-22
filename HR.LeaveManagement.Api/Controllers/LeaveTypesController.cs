using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : Controller
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<List<LeaveTypeDto>> GetAll()
        {
            return await _mediator.Send(new GetLeaveTypesQuery());
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(LeaveTypeDetailsDto))]
        [ProducesResponseType(404)]
        public async Task<LeaveTypeDetailsDto> GetById(int id)
        {
            return await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
        }
        
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LeaveTypeDetailsDto))]
        [ProducesResponseType(400, Type = typeof(List<string>))]
        public async Task<ActionResult> Create(CreateLeaveTypeCommand leaveType)
        {
            var result = await _mediator.Send(leaveType);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }
        
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateLeaveTypeCommand leaveType)
        {
            await _mediator.Send(leaveType);
            return NoContent();
        }
        
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(DeleteLeaveTypeCommand leaveType)
        {
            await _mediator.Send(leaveType);
            return NoContent();
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SnippetApi.Data.Repository.Interfaces;
using SnippetApi.Models;
using SnippetApi.Models.Dtos;
using System.Text.RegularExpressions;

namespace SnippetApi.Controllers
{
    [ApiController]
    [Route("snippet/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GroupsController(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReadGroupDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult Groups()
        {
            var groups = unitOfWork.GroupRepo.GetAll().ToList();

            if (groups == null) { return NotFound(); }

            return Ok(mapper.Map<IEnumerable<ReadGroupDto>>(groups));
        }

        [HttpGet("{groupId:int}/commands")]
        [ProducesResponseType(typeof(IEnumerable<ReadCommadDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult Commands(int groupId)
        {
            var commands = unitOfWork.CommandRepo.GetAll(groupId);

            if (commands == null) { return NotFound(); }

            return Ok(mapper.Map<IEnumerable<ReadCommadDto>>(commands));
        }

        [HttpGet("{groupId:int}/commands/{commandId:int}")]
        [ProducesResponseType(typeof(ReadCommadDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Commands(int groupId, int commandId)
        {
            var group = await unitOfWork.GroupRepo.FindAsync(new object[] { groupId });
            var command = await unitOfWork.CommandRepo.FindAsync(new object[] { commandId });

            if (group == null || command == null) { return NotFound(); }

            return Ok(mapper.Map<ReadCommadDto>(command));
        }

        [HttpPost]
        [ProducesResponseType(typeof(WriteGroupDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Groups(WriteGroupDto groupDto)
        {
            try
            {
                await unitOfWork.GroupRepo.AddAsync(mapper.Map<SnippetApi.Models.Group>(groupDto));
                await unitOfWork.SaveChanges();

                return Created(HttpContext.Request.Path, groupDto);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("{groupId:int}/commands")]
        [ProducesResponseType(typeof(WriteCommandDto), 201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Commands(int groupId, WriteCommandDto commandDto)
        {
            try
            {
                var group = await unitOfWork.GroupRepo.FindAsync(new object[] { groupId });

                if(group == null) { return NotFound(); }

                var newCommand = mapper.Map<Command>(commandDto);
                newCommand.GroupId = groupId;

                await unitOfWork.CommandRepo.AddAsync(newCommand);
                await unitOfWork.SaveChanges();

                return Created(HttpContext.Request.Path, commandDto);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("{groupId:int}/commands/search")]
        [ProducesResponseType(typeof(ReadCommadDto), 201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SearchCommand(string commandName)
        {
            char.ToUpper(commandName[0]);
            var command = await unitOfWork.CommandRepo.FindByName(commandName);

            if(command == null) { return NotFound(); }

            return Created(HttpContext.Request.Path, mapper.Map<ReadCommadDto>(command));
        }

        [HttpPut("{groupId:int}/commands/{commandId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Commands(int groupId, int commandId, WriteCommandDto commandDto)
        {
            var group = await unitOfWork.GroupRepo.FindAsync(new object[] { groupId });
            var command = await unitOfWork.CommandRepo.FindAsync(new object[] { commandId });

            if (group == null || command == null) { return NotFound(); }

            mapper.Map(commandDto, command); //it's being tracked by the context
            unitOfWork.CommandRepo.Update(command); //for good practice i.e if in the feature automapper has changed
            await unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{groupId:int}/commands/{commandId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Commands(int groupId, int commandId, JsonPatchDocument<UpdateCommandDto> jsonPatch)
        {
            var group = await unitOfWork.GroupRepo.FindAsync(new object[] { groupId });
            var command = await unitOfWork.CommandRepo.FindAsync(new object[] { commandId });

            if (group == null || command == null) { return NotFound(); }

            var oldCommand = mapper.Map<UpdateCommandDto>(command);
            jsonPatch.ApplyTo(oldCommand, ModelState);

            if (!TryValidateModel(oldCommand)) { return ValidationProblem(ModelState); }

            mapper.Map(oldCommand, command); //it's being tracked by the context
            unitOfWork.CommandRepo.Update(command); //for good practice i.e if in the feature automapper has changed
            await unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{groupId:int}/commands/{commandId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveCommand(int groupId, int commandId)
        {
            var group = await unitOfWork.GroupRepo.FindAsync(new object[] { groupId });
            var command = await unitOfWork.CommandRepo.FindAsync(new object[] { commandId });

            if (group == null || command == null) { return NotFound(); }

            unitOfWork.CommandRepo.Remove(command);
            await unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}

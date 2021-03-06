﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitstop.ContractManagementAPI.Commands;
using Pitstop.ContractManagementAPI.DataAccess;
using Pitstop.ContractManagementAPI.Events;
using Pitstop.ContractManagementAPI.Model;
using Pitstop.Infrastructure.Messaging;
using System.Threading.Tasks;

namespace Pitstop.ContractManagementAPI.Controllers
{
    [Route("/api/[controller]")]
    public class RatesController : Controller
    {
        IMessagePublisher _messagePublisher;
        ContractManagementDBContext _dbContext;

        public RatesController(ContractManagementDBContext dbContext, IMessagePublisher messagePublisher)
        {
            _dbContext = dbContext;
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _dbContext.Rates.ToListAsync());
        }

        [HttpGet]
        [Route("{rateId}", Name = "GetByRateId")]
        public async Task<IActionResult> GetByRateId(int rateId)
        {
            var rate = await _dbContext.Rates.FirstOrDefaultAsync(c => c.RateId == rateId);
            if (rate == null)
            {
                return NotFound();
            }
            return Ok(rate);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRate command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // insert customer
                    Rate rate = Mapper.Map<Rate>(command);
                    _dbContext.Rates.Add(rate);
                    await _dbContext.SaveChangesAsync();

                    // send event
                    RateRegistered e = Mapper.Map<RateRegistered>(command);
                    await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                    // return result
                    return CreatedAtRoute("GetByRateId", new { rateId = rate.RateId }, rate);
                }
                return BadRequest();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator." + " -" + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

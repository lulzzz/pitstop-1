﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Models;
using Pitstop.ViewModels;
using AutoMapper;
using Polly;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Commands;
using WebApp.RESTClients;
using Pitstop.Application.VehicleManagement.Model;

namespace PitStop.Controllers
{
    public class VehicleOwnerManagementController : Controller
    {
        private IVehicleManagementAPI _vehicleManagementAPI;
        private readonly ILogger _logger;
        private ResiliencyHelper _resiliencyHelper;

        public VehicleOwnerManagementController(IVehicleManagementAPI vehicleManagementAPI, 
            ILogger<VehicleOwnerManagementController> logger)
        {
            _vehicleManagementAPI = vehicleManagementAPI;
            _logger = logger;
            _resiliencyHelper = new ResiliencyHelper(_logger);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new VehicleOwnerManagementViewModel
                {
                    Owners = await _vehicleManagementAPI.GetOwners()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Offline", new VehicleOwnerManagementOfflineViewModel(ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                Owner owner = await _vehicleManagementAPI.GetOwnerById(id);

                var model = new VehicleOwnerManagementDetailsViewModel
                {
                    Owner = owner
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Offline", new VehicleOwnerManagementOfflineViewModel(ex.Message));
            }
        }

        [HttpGet]
        public IActionResult New()
        {
                var model = new VehicleOwnerManagementNewViewModel
                {
                    Owner = new Owner()
                };
                return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] VehicleOwnerManagementNewViewModel inputModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    RegisterOwner cmd = Mapper.Map<RegisterOwner>(inputModel.Owner);
                    await _vehicleManagementAPI.RegisterOwner(cmd);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View("Offline", new VehicleOwnerManagementOfflineViewModel(ex.Message));
                }
            }
            else
            {
                return View("New", inputModel);
            }
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}

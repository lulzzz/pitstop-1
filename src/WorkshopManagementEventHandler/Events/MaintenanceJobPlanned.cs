﻿using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pitstop.WorkshopManagementEventHandler.Events
{
    public class MaintenanceJobPlanned : Event
    {
        public readonly Guid JobId;
        public readonly DateTime StartTime;
        public readonly DateTime EndTime;
        public readonly (string Id, string Nombre, string Telefono) CustomerInfo;
        public readonly (string Matricula, string Marca, string Modelo) VehicleInfo;
        public readonly string Description;

        public MaintenanceJobPlanned(Guid messageId, Guid jobId, DateTime startTime, DateTime endTime,
            (string Id, string Nombre, string Telefono) customerInfo,
            (string Matricula, string Marca, string Modelo) vehicleInfo,
            string description) : base(messageId)
        {
            JobId = jobId;
            StartTime = startTime;
            EndTime = endTime;
            CustomerInfo = customerInfo;
            VehicleInfo = vehicleInfo;
            Description = description;
        }
    }
}

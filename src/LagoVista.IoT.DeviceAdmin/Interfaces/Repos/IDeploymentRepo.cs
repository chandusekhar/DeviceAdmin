﻿using LagoVista.IoT.DeviceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.DeviceAdmin.Interfaces.Repos
{
    public interface IDeploymentRepo
    {
        Task AddDeploymentAsync(Deployment deployment);
        Task<Deployment> GetDeploymentAsync(string id, bool populateChildren = false);
        Task<IEnumerable<Deployment>> GetDeploymentsForOrgsAsync(string orgId);
        Task UpdateDeploymentAsync(Deployment deployment);     
    }
}

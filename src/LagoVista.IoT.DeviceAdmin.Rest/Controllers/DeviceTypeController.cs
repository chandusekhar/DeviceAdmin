﻿using LagoVista.Core;
using LagoVista.Core.Models;
using LagoVista.Core.Models.UIMetaData;
using LagoVista.Core.Validation;
using LagoVista.IoT.DeviceAdmin.Interfaces.Managers;
using LagoVista.IoT.DeviceAdmin.Models;
using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Web.Common.Controllers;
using LagoVista.UserAdmin.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LagoVista.IoT.DeviceAdmin.Rest.Controllers
{
    [Authorize]
    public class DeviceTypeController : LagoVistaBaseController
    {
        IDeviceTypeManager _deviceTypeManager;

        public DeviceTypeController(UserManager<AppUser> userManager, IAdminLogger logger, IDeviceTypeManager deviceTypeManager) : base(userManager, logger)
        {
            _deviceTypeManager = deviceTypeManager;
        }

        /// <summary>
        /// Device Type - Add
        /// </summary>
        /// <param name="deviceType"></param>
        [HttpPost("/api/devicetype")]
        public Task<InvokeResult> AddDeviceTypeAsync([FromBody] DeviceType deviceType)
        {
            return _deviceTypeManager.AddDeviceTypeAsync(deviceType, OrgEntityHeader, UserEntityHeader);
        }

        /// <summary>
        /// Device Type - Update
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        [HttpPut("/api/devicetype")]
        public Task<InvokeResult> UpdateDeviceTypeAsync([FromBody] DeviceType deviceType)
        {
            SetUpdatedProperties(deviceType);
            return _deviceTypeManager.UpdateDeviceTypeAsync(deviceType, OrgEntityHeader, UserEntityHeader);
        }

        /// <summary>
        /// Device Type- Get for Current Org
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/devicetypes")]
        public async Task<ListResponse<DeviceTypeSummary>> GetDeviceTypesForOrg()
        {
            var hostSummaries = await _deviceTypeManager.GetDeviceTypesForOrgsAsync(OrgEntityHeader.Id, UserEntityHeader);
            return ListResponse<DeviceTypeSummary>.Create(hostSummaries);
        }

        /// <summary>
        /// Device Type - In Use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/devicetype/{id}/inuse")]
        public Task<DependentObjectCheckResult> InUseCheck(String id)
        {
            return _deviceTypeManager.CheckDeviceTypeInUseAsync(id, OrgEntityHeader, UserEntityHeader);
        }

        /// <summary>
        /// Device Type - Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/devicetype/{id}")]
        public async Task<DetailResponse<DeviceType>> GetDeviceType(String id)
        {
            var deviceType = await _deviceTypeManager.GetDeviceTypeAsync(id, OrgEntityHeader, UserEntityHeader);

            return DetailResponse<DeviceType>.Create(deviceType);
        }

        /// <summary>
        /// Device Type - Key In Use
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/devicetype/{key}/keyinuse")]
        public Task<bool> GetDeviceTypeKeyInUseAsync(String key)
        {
            return _deviceTypeManager.QueryDeviceTypeKeyInUseAsync(key, CurrentOrgId);
        }

        /// <summary>
        /// Device Type - Delete
        /// </summary>
        /// <returns></returns>
        [HttpDelete("/api/devicetype/{id}")]
        public Task<InvokeResult> DeleteDeviceTypeAsync(string id)
        {
            return _deviceTypeManager.DeleteDeviceTypeAsync(id, OrgEntityHeader, UserEntityHeader);
        }

        /// <summary>
        ///  Device Type - Create New
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/devicetype/factory")]
        public DetailResponse<DeviceType> CreateDeviceType()
        {
            var response = DetailResponse<DeviceType>.Create();
            response.Model.Id = Guid.NewGuid().ToId();
            SetAuditProperties(response.Model);
            SetOwnedProperties(response.Model);
            return response;
        }

        /// <summary>
        ///  Device Type BOM Item - Create New
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/devicetype/bomitem/factory")]
        public DetailResponse<BOMItem> CreateBOMItem()
        {
            var response = DetailResponse<BOMItem>.Create();
            response.Model.Id = Guid.NewGuid().ToId();
            return response;
        }

        /// <summary>
        ///  Device Type Resource - Create New
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/mediaresource/factory")]
        public DetailResponse<MediaResource> CreateDeviceResource()
        {
            var response = DetailResponse<MediaResource>.Create();
            response.Model.Id = Guid.NewGuid().ToId();
            return response;
        }

        [HttpPost("/api/media/resources/{id}")]
        public async Task<InvokeResult<MediaResource>> UploadMediaAsync(string id, IFormFile file)
        {
            using (var strm = file.OpenReadStream())
            {
                return await _deviceTypeManager.AddResourceMediaAsync(id, strm, file.ContentType, OrgEntityHeader, UserEntityHeader);
            }
        }

        [HttpGet("/api/devicetype/{devicetypeid}/resources/{id}")]
        public async Task<IActionResult> DownloadMedia(string deviceTypeId, string id)
        {
            var response = await _deviceTypeManager.GetResourceMediaAsync(deviceTypeId, id, OrgEntityHeader, UserEntityHeader);

            var ms = new MemoryStream(response.ImageBytes);
            return new FileStreamResult(ms, response.ContentType);
        }

        [HttpGet("/api/devicetype/{devicetypeid}/bom/{bomitemid}/resources/{id}")]
        public async Task<IActionResult> DownloadMedia(string deviceTypeId, string bomitemid, string id)
        {
            var response = await _deviceTypeManager.GetBomResourceMediaAsync(deviceTypeId, id, bomitemid, OrgEntityHeader, UserEntityHeader);

            var ms = new MemoryStream(response.ImageBytes);
            return new FileStreamResult(ms, response.ContentType);
        }
    }
}

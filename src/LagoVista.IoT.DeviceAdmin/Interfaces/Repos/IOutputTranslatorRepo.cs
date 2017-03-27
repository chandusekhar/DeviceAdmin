﻿using LagoVista.IoT.DeviceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.DeviceAdmin.Interfaces.Repos
{
    public interface IOutputTranslatorRepo
    {
        Task AddOutputTranslatorConfigurationAsync(OutputTranslatorConfiguration outputTranslator);
        Task<OutputTranslatorConfiguration> GetOutputTranslatorConfigurationAsync(string id);
        Task<IEnumerable<PipelineModuleConfigurationSummary>> GetOutputTranslatorConfigurationsForOrgsAsync(string orgId);
        Task UpdateOutputTranslatorConfigurationAsync(OutputTranslatorConfiguration outputTranslator);
        Task DeleteOutputTranslatorConfigurationAsync(string id);
        Task<bool> QueryKeyInUseAsync(String key, String orgId);
    }
}

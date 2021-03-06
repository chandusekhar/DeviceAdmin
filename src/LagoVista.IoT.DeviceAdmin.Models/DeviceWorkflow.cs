﻿using LagoVista.Core.Attributes;
using LagoVista.Core.Interfaces;
using LagoVista.Core.Validation;
using LagoVista.IoT.DeviceAdmin.Resources;
using System;
using System.Collections.Generic;
using LagoVista.Core.Models;
using System.Linq;
using LagoVista.IoT.DeviceAdmin.Interfaces;
using LagoVista.IoT.DeviceAdmin.Models.Resources;

namespace LagoVista.IoT.DeviceAdmin.Models
{
    [EntityDescription(DeviceAdminDomain.DeviceAdmin, Resources.DeviceLibraryResources.Names.DeviceWorkflow_Title, Resources.DeviceLibraryResources.Names.DeviceWorkflow_Help, 
        Resources.DeviceLibraryResources.Names.DeviceWorkflow_Description, EntityDescriptionAttribute.EntityTypes.SimpleModel, typeof(DeviceLibraryResources))]
    public class DeviceWorkflow : IoTModelBase, IOwnedEntity, IValidateable, IKeyedEntity, INoSQLEntity, IPipelineModuleConfiguration
    {
        public DeviceWorkflow()
        {
            InputCommands = new List<InputCommand>();
            Attributes = new List<Attribute>();
            StateMachines = new List<StateMachine>();
            Inputs = new List<WorkflowInput>();
            OutputCommands = new List<OutputCommand>();
            BusinessRules = new List<BusinessRule>();
            Timers = new List<Timer>();
            Pages = new List<Page>();
            Environment = LagoVista.IoT.DeviceAdmin.Models.Environment.GetDefault().ToEntityHeader();
            ConfigurationVersion = 0.1;
        }
        public String DatabaseName { get; set; }

        public String EntityType { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_ConfigVersion, FieldType: FieldTypes.Decimal, IsRequired: true, ResourceType: typeof(DeviceLibraryResources))]
        public double ConfigurationVersion { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.Common_Key, HelpResource: Resources.DeviceLibraryResources.Names.Common_Key_Help, FieldType: FieldTypes.Key, RegExValidationMessageResource: Resources.DeviceLibraryResources.Names.Common_Key_Validation, ResourceType: typeof(DeviceLibraryResources), IsRequired: true)]
        public String Key { get; set; }

        public EntityHeader Environment { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.Common_IsPublic, FieldType: FieldTypes.Bool, ResourceType: typeof(DeviceLibraryResources))]
        public bool IsPublic { get; set; }
        public EntityHeader OwnerOrganization { get; set; }
        public EntityHeader OwnerUser { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_PrehandlerScript, WaterMark: Resources.DeviceLibraryResources.Names.DeviceWorkflow_PrehandlerScript_Watermark, HelpResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_PrehandlerScript_Help, FieldType: FieldTypes.NodeScript, ResourceType: typeof(DeviceLibraryResources))]
        public string PreHandlerScript { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_PosthandlerScript, WaterMark: Resources.DeviceLibraryResources.Names.DeviceWorkflow_PosthandlerScript_Watermark, HelpResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_PosthandlerScript_Help, FieldType: FieldTypes.NodeScript, ResourceType: typeof(DeviceLibraryResources))]
        public string PostHandlerScript { get; set; }


        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_Settings, HelpResource: DeviceLibraryResources.Names.DeviceWorkflow_Settings_Help, ResourceType: typeof(DeviceLibraryResources))]
        public List<Models.CustomField> Settings { get; set; }


        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_Attributes, HelpResource: DeviceLibraryResources.Names.DeviceWorkflow_Attributes_Help, ResourceType: typeof(DeviceLibraryResources))]
        public List<Models.Attribute> Attributes { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_InputCommands, HelpResource: DeviceLibraryResources.Names.DeviceWorkflow_InputCommands_Help, ResourceType: typeof(DeviceLibraryResources))]
        public List<Models.InputCommand> InputCommands { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_OutputCommands, ResourceType: typeof(DeviceLibraryResources))]
        public List<OutputCommand> OutputCommands { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_Inputs, ResourceType: typeof(DeviceLibraryResources))]
        public List<WorkflowInput> Inputs { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_Timer, ResourceType: typeof(DeviceLibraryResources))]
        public List<Timer> Timers { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_Pages, HelpResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_Pages_Help, ResourceType: typeof(DeviceLibraryResources), FieldType: FieldTypes.ChildList)]
        public List<Page> Pages { get; set; }


        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.DeviceWorkflow_BusinessRules, HelpResource:Resources.DeviceLibraryResources.Names.DeviceWorkflow_BusinessRules_Help, ResourceType: typeof(DeviceLibraryResources), FieldType: FieldTypes.ChildList)]
        public List<BusinessRule> BusinessRules { get; set; }

        public EntityHeader ToEntityHeader()
        {
            return new EntityHeader()
            {
                Id = Id,
                Text = Name,
            };
        }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.StateMachines, ResourceType: typeof(DeviceLibraryResources))]
        public List<StateMachine> StateMachines
        {
            get; set;
        }

        public DeviceWorkflowSummary CreateSummary()
        {
            return new DeviceWorkflowSummary()
            {
                Id = Id,
                IsPublic = IsPublic,
                Key = Key,
                Name = Name,
                Description = Description
            };
        }

        [CustomValidator]
        public void Validate(ValidationResult result)
        {
            if (Timers == null) Timers = new List<Timer>();


            if (Inputs.Select(param => param.Key).Distinct().Count() != Inputs.Count()) result.AddUserError("Duplicate Keys found on Inputs.");
            if (Attributes.Select(param => param.Key).Distinct().Count() != Attributes.Count()) result.AddUserError("Duplicate Keys found on Attributes.");
            if (InputCommands.Select(param => param.Key).Distinct().Count() != InputCommands.Count()) result.AddUserError("Duplicate Keys found on Input Commands.");
            if (StateMachines.Select(param => param.Key).Distinct().Count() != StateMachines.Count()) result.AddUserError("Duplicate Keys found on State Machines.");            
            if (OutputCommands.Select(param => param.Key).Distinct().Count() != OutputCommands.Count()) result.AddUserError("Duplicate Keys found on Output Commands.");
            if (Timers.Select(param => param.Key).Distinct().Count() != Timers.Count()) result.AddUserError("Duplicate Keys found on Timers.");

            foreach (var input in Inputs) input.Validate(this, result);
            foreach (var attribute in Attributes) attribute.Validate(this, result);
            foreach (var inputCommand in InputCommands) inputCommand.Validate(this, result);
            foreach (var stateMachine in StateMachines) stateMachine.Validate(this, result);
            foreach (var outputCommand in OutputCommands) outputCommand.Validate(this, result);
            foreach (var timer in Timers) timer.Validate(this, result);
        }
    }

    [EntityDescription(DeviceAdminDomain.DeviceAdmin, Resources.DeviceLibraryResources.Names.DeviceConfiguration_Title, Resources.DeviceLibraryResources.Names.DeviceConfiguration_Help, Resources.DeviceLibraryResources.Names.DeviceConfiguration_Description, EntityDescriptionAttribute.EntityTypes.Summary, typeof(DeviceLibraryResources))]
    public class DeviceWorkflowSummary : SummaryData, ISummaryData
    {

    }
}
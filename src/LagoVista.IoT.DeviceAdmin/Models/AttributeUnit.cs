﻿using LagoVista.Core.Attributes;
using LagoVista.Core.Interfaces;
using LagoVista.IoT.DeviceAdmin.Resources;
using Newtonsoft.Json;
using System;

namespace LagoVista.IoT.DeviceAdmin.Models
{
    [EntityDescription(DeviceAdminDomain.DeviceAdmin, DeviceLibraryResources.Names.AttributeUnit_Title, Resources.DeviceLibraryResources.Names.AttributeUnit_Help, DeviceLibraryResources.Names.AttributeUnit_Description, EntityDescriptionAttribute.EntityTypes.SimpleModel,typeof(DeviceLibraryResources))]
    public class AttributeUnit : IKeyedEntity, INamedEntity
    {
        [JsonProperty("id")]
        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.Common_UniqueId, IsUserEditable: false, ResourceType: typeof(DeviceLibraryResources), IsRequired: true)]
        public String Id { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.Common_Name, ResourceType: typeof(DeviceLibraryResources), IsRequired: true)]
        public String Name { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.Common_Key, HelpResource: Resources.DeviceLibraryResources.Names.Common_Key_Help, FieldType: FieldTypes.Key, RegExValidationMessageResource: Resources.DeviceLibraryResources.Names.Common_Key_Validation, ResourceType: typeof(DeviceLibraryResources), IsRequired: true)]
        public String Key { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.AttributeUnit_Abbreviation, IsRequired: true, MaxLength: 6, ResourceType: typeof(DeviceLibraryResources))]
        public String Abbreviation { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.Common_Description, ResourceType: typeof(DeviceLibraryResources))]
        public String Description { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.AttributeUnit_NumberDecimal, IsRequired: true, FieldType: FieldTypes.Integer, ResourceType: typeof(DeviceLibraryResources))]
        public int NumberDecimalPoints { get; set; }


        // Look at for running the scripts https://github.com/sebastienros/jint
        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.AttributeUnit_ConversionScript, FieldType:FieldTypes.NodeScript, HelpResource: Resources.DeviceLibraryResources.Names.AttributeUnit_ConversionScript_Help, ResourceType: typeof(DeviceLibraryResources))]
        public String ConversionScript { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.AttributeUnit_IsDefault, HelpResource: Resources.DeviceLibraryResources.Names.AttributeUnit_ConversionScript_Help, ResourceType: typeof(DeviceLibraryResources))]
        public String IsDefault { get; set; }
    }

}

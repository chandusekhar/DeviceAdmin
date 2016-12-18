﻿using LagoVista.Core.Attributes;
using LagoVista.Core.Interfaces;
using LagoVista.Core.Models;
using LagoVista.IoT.DeviceAdmin.Resources;
using System;

namespace LagoVista.IoT.DeviceAdmin.Models
{
    /// <summary>
    /// An attribute category is used to define a specific type of attribute so it can
    /// be used across different devices.  An example of this is would On/Off
    /// 
    /// From a programming perspective you can think of an Attribute Category as
    /// an interface of which properties the attribute impleements.
    /// 
    /// The Attribute itself can have up to one attribute category
    /// attached to it.
    /// </summary>
    [EntityDescription(Name:"Standard Attribute", Domain:DeviceAdminDomain.DeviceAdmin, Description:"A Standard Attribute is an Attribute that is common to many Device Configurations and can be used to include devices that implement a set of Standard Attributes into a device group.")]
    public class StandardAttribute : DeviceModelBase, IOwnedEntity, IKeyedEntity
    {
        public bool IsPublic { get; set; }
        public EntityHeader OwnerOrganization { get; set; }
        public EntityHeader OwnerUser { get; set; }

        public AttributeUnitSet UnitSet { get; set; }

        [FormField(LabelResource: Resources.DeviceLibraryResources.Names.Common_Key, HelpResource: Resources.DeviceLibraryResources.Names.Common_Key_Help, FieldType: FieldTypes.Key, RegExValidationMessageResource: Resources.DeviceLibraryResources.Names.Common_Key_Validation, ResourceType: typeof(DeviceLibraryResources), IsRequired: true)]
        public String Key { get; set; }
    }
}

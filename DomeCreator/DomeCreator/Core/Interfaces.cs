using System;

namespace DomeCreator.Core
{
    public interface IGroupable
    {
        Guid Id { get; }
        Guid ParentId { get; set; }
        bool IsGroup { get; set; }
        string GroupName { get; set; }
    }

    // Common interface for items that can be selected
    // on the DesignerCanvas; used by DesignerItem and Connection
    public interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
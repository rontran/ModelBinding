using System;

namespace ModelBinding.Models.Interfaces
{
    public interface IControl
    {
        Guid Id { get; set; }

        string Label { get; set; }

        string ControlType { get; }
    }
}

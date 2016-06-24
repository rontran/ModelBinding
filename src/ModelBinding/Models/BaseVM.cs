using System;
using ModelBinding.Models.Interfaces;

namespace ModelBinding.Models
{
    public abstract class BaseVM : IControl {
        public BaseVM() {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Label { get; set; }

        public abstract string ControlType { get; }
    }
}

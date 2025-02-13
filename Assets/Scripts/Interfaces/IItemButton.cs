using System;
using UI.Grid;

namespace Interfaces
{
    public interface IItemButton
    {
        public event Action<IItemButton> Click;
        public event Action<IItemButton> ButtonDisabled;

        public UIItemView ItemView { get; }
        public string Type { get; }
    }
}
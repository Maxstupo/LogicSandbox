namespace Maxstupo.LogicSandbox.Utility.Interaction {

    /// <summary>
    /// An optional interface used by the <see cref="Selector{T}"/> class to provide selection notifications.
    /// </summary>
    public interface ISelectable {

        /// <summary>
        /// Invoked when the item is selected. Selection state not monitored, method can be invoked repeatly.
        /// </summary>
        void OnSelected();

        /// <summary>
        /// Invoked when the item is deselected. Selection state not monitored, method can be invoked repeatly.
        /// </summary>
        void OnDeselected();

    }

}
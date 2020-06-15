namespace Maxstupo.LogicSandbox.Utility.Interaction {

    /// <summary>
    /// An optional interface used by the <see cref="Selector{T}"/> class to provide selection notifications.
    /// </summary>
    public interface ISelectable {

        void OnSelected();

        void OnDeselected();

    }

}
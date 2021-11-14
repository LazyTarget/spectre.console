namespace Spectre.Console
{
    /// <summary>
    /// Represent a manually created prompt item.
    /// </summary>
    public interface IManuallyCreatedItem
    {
        /// <summary>
        /// Gets a value indicating whether or not this item is manually inputted.
        /// </summary>
        bool IsManuallyCreated { get; }
    }
}

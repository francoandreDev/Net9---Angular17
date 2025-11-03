using System.Text.Json.Nodes;

namespace MiniApp.Models.Json
{
    /// <summary>
    /// Defines a contract for JSON-based entities that can be validated and serialized.
    /// </summary>
    public interface IJsonEntity
    {
        /// <summary>
        /// Gets the unique identifier of the entity.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Determines whether the current entity is valid based on custom validation rules.
        /// </summary>
        /// <returns><c>true</c> if the entity is valid; otherwise, <c>false</c>.</returns>
        bool IsValid();

        /// <summary>
        /// Converts the entity into a JSON representation.
        /// </summary>
        /// <returns>A <see cref="JsonObject"/> representing the entity.</returns>
        JsonObject ToJson();
    }
}

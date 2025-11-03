using System.Text.Json.Nodes;

namespace MiniApp.Models.Json
{
    /// <summary>
    /// Represents a basic JSON entity with an <c>Id</c> and <c>Name</c>,
    /// providing validation and JSON serialization capabilities.
    /// </summary>
    /// <param name="Id">The unique identifier of the entity.</param>
    /// <param name="Name">The display name of the entity.</param>
    public record JsonEntity(int Id, string Name) : IJsonEntity
    {
        /// <summary>
        /// Determines whether the current entity is valid.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <see cref="Id"/> is greater than zero and
        /// <see cref="Name"/> is not null, empty, or whitespace; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return Id > 0 && !string.IsNullOrWhiteSpace(Name);
        }

        /// <summary>
        /// Creates a new <see cref="JsonEntity"/> instance from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="json">The JSON object to deserialize.</param>
        /// <returns>
        /// A new <see cref="JsonEntity"/> instance if deserialization succeeds; otherwise, <c>null</c>.
        /// </returns>
        /// <remarks>
        /// The method expects the JSON object to contain <c>id</c> and <c>name</c> properties.
        /// </remarks>
        public static JsonEntity? FromJson(JsonObject? json)
        {
            if (json is null) return null;

            int? id = json["id"]?.GetValue<int>();
            string? name = json["name"]?.GetValue<string>();

            if (id is null || name is null) return null;

            return new JsonEntity(id.Value, name);
        }

        /// <summary>
        /// Converts the current entity to its JSON representation.
        /// </summary>
        /// <returns>
        /// A <see cref="JsonObject"/> containing the <c>id</c> and <c>name</c> properties of the entity.
        /// </returns>
        public JsonObject ToJson()
        {
            return new JsonObject
            {
                ["id"] = Id,
                ["name"] = Name
            };
        }
    }
}

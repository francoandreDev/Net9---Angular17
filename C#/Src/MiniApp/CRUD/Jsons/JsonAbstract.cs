using System.Text.Json.Nodes;

namespace MiniApp.CRUD.Jsons
{
    /// <summary>
    /// Defines the abstract base class for JSON-based CRUD operations.
    /// </summary>
    public abstract class JsonAbstract
    {
        /// <summary>
        /// Retrieves all JSON elements stored in memory.
        /// </summary>
        /// <returns>
        /// A <see cref="JsonArray"/> containing all the elements.
        /// </returns>
        public abstract JsonArray GetAll();

        /// <summary>
        /// Adds a new JSON object to the internal data collection.
        /// </summary>
        /// <param name="newData">
        /// The <see cref="JsonObject"/> instance to be added.
        /// Must not be <c>null</c>.
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="newData"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="newData"/> does not contain an 'id' property.</exception>
        /// <exception cref="JsonException">Thrown if the provided JSON data is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown if an element with the same <c>id</c> already exists.</exception>
        public abstract void Add(JsonObject newData);

        /// <summary>
        /// Searches for a JSON object by its <c>id</c> property.
        /// </summary>
        /// <param name="id">
        /// The unique integer identifier to search for.
        /// </param>
        /// <returns>
        /// The matching <see cref="JsonObject"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public abstract JsonObject? SearchById(int id);

        /// <summary>
        /// Updates an existing JSON object by its <c>id</c>.
        /// </summary>
        /// <param name="id">
        /// The integer identifier of the object to update.
        /// </param>
        /// <param name="newData">
        /// The <see cref="JsonObject"/> containing the updated key-value pairs.
        /// The <c>id</c> property (if present) will be ignored.
        /// </param>
        /// <returns>
        /// <c>true</c> if the object was successfully updated; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool UpdateById(int id, JsonObject newData);

        /// <summary>
        /// Deletes a JSON object by its <c>id</c> property.
        /// </summary>
        /// <param name="id">
        /// The unique integer identifier of the object to delete.
        /// </param>
        /// <returns>
        /// <c>true</c> if the object was deleted; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool DeleteById(int id);

        /// <summary>
        /// Safely extracts an integer ID from a JsonObject.
        /// </summary>
        /// <param name="obj">The JsonObject containing the "id" property.</param>
        /// <param name="id">The parsed integer ID (if found).</param>
        /// <returns>
        /// True if the "id" exists and is a valid integer; otherwise, false.
        /// </returns>
        protected abstract bool TryGetId(JsonObject obj, out int id);

        /// <summary>
        /// Finds a JsonObject in the current dataset by its integer "id".
        /// </summary>
        /// <param name="id">The ID to search for.</param>
        /// <returns>The JsonObject if found; otherwise, null.</returns>
        protected abstract JsonObject? FindById(int id);

        /// <summary>
        /// Validates and normalizes a <see cref="JsonObject"/> to ensure it contains
        /// a valid and properly formatted data.
        /// </summary>
        /// <param name="data">
        /// The <see cref="JsonObject"/> to validate and normalize.
        /// Might contain a numeric <c>id</c> property.
        /// </param>
        /// <param name="normalized">
        /// When this method returns, contains a normalized version of <paramref name="data"/>
        /// if validation succeeded; otherwise, an empty <see cref="JsonObject"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the input JSON object is valid and normalized successfully;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        protected virtual bool TryValidateAndNormalize(JsonObject data, out JsonObject normalized)
        {
            normalized = [];

            if (data is null)
                return false;

            // Allow updates without 'id' field, only if it is given by parameter
            if (data["id"] is JsonValue idValue && int.TryParse(idValue.ToString(), out int id))
                normalized["id"] = id;

            foreach (var pair in data)
            {
                if (pair.Key.Equals("id", StringComparison.OrdinalIgnoreCase)) continue;
                if (pair.Value is null) continue;

                normalized[pair.Key] = pair.Value.DeepClone();
            }

            // If the object has no util properties is not valid
            return normalized.Count > 0;
        }
    }
}

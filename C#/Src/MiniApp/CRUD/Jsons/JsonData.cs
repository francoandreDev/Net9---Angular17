using System.Text.Json;
using System.Text.Json.Nodes;
using System.Globalization;

namespace MiniApp.CRUD.Jsons
{
    /// <summary>
    /// Provides a concrete implementation of <see cref="JsonAbstract"/>
    /// for managing in-memory JSON data using <see cref="JsonArray"/> and <see cref="JsonObject"/>.
    /// </summary>
    public class JsonData : JsonAbstract
    {
        /// <summary>
        /// Internal JSON array that stores all JSON objects.
        /// </summary>
        private readonly JsonArray _jsonData;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonData"/> class
        /// with an empty <see cref="JsonArray"/>.
        /// </summary>
        public JsonData()
        {
            _jsonData = []; // Equivalent to new JsonArray(), just shorter syntax
        }

        /// <summary>
        /// Gets all JSON objects stored in memory.
        /// </summary>
        /// <returns>
        /// A <see cref="JsonArray"/> containing all stored JSON objects.
        /// </returns>
        public override JsonArray GetAll()
        {
            return _jsonData;
        }

        /// <summary>
        /// Adds a new JSON object to the in-memory collection.
        /// </summary>
        /// <param name="newData">The <see cref="JsonObject"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="newData"/> is null.</exception>
        /// <exception cref="JsonException">Thrown if the provided JSON data is invalid.</exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if an element with the same <c>id</c> already exists.
        /// </exception>
        public override void Add(JsonObject newData)
        {
            ArgumentNullException.ThrowIfNull(newData);

            // 🚨 Check explicitly for "id" before normalization
            if (newData["id"] is not JsonValue)
                throw new InvalidOperationException("The JSON object must contain an 'id' property.");

            if (!TryValidateAndNormalize(newData, out JsonObject normalized))
                throw new JsonException("The JSON data is invalid.");

            if (normalized["id"] is not JsonValue idValue)
                throw new InvalidOperationException("The normalized JSON object must contain an 'id' property.");

            int id = idValue.GetValue<int>();

            if (FindById(id) is not null)
                throw new InvalidOperationException($"An element with id={id} already exists.");

            _jsonData.Add(normalized);
        }

        /// <summary>
        /// Searches for a JSON object by its <c>id</c>.
        /// </summary>
        /// <param name="id">The identifier of the JSON object to search for.</param>
        /// <returns>
        /// The <see cref="JsonObject"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public override JsonObject? SearchById(int id)
        {
            return FindById(id);
        }

        /// <summary>
        /// Updates the properties of an existing JSON object with the given <c>id</c>.
        /// </summary>
        /// <param name="id">The identifier of the object to update.</param>
        /// <param name="newData">The new data to merge into the existing object.</param>
        /// <returns>
        /// <c>true</c> if the object was found and updated; otherwise, <c>false</c>.
        /// </returns>
        public override bool UpdateById(int id, JsonObject newData)
        {
            if (!TryValidateAndNormalize(newData, out JsonObject normalized))
                return false;

            JsonObject? current = FindById(id);
            if (current is null)
                return false;

            bool updated = false;

            foreach (var pair in normalized)
            {
                string key = pair.Key;
                JsonNode? value = pair.Value;

                // Skip 'id' field or null values
                if (string.Equals(key, "id", StringComparison.OrdinalIgnoreCase) || value is null)
                    continue;

                JsonNode clonedValue = value.DeepClone();

                if (!JsonNode.DeepEquals(current[key], clonedValue))
                {
                    current[key] = clonedValue;
                    updated = true;
                }
            }

            return updated;
        }

        /// <summary>
        /// Deletes the JSON object with the specified <c>id</c>.
        /// </summary>
        /// <param name="id">The identifier of the object to delete.</param>
        /// <returns>
        /// <c>true</c> if the object was found and removed; otherwise, <c>false</c>.
        /// </returns>
        public override bool DeleteById(int id)
        {
            JsonObject? target = FindById(id);

            if (target is null)
                return false;

            _jsonData.Remove(target);
            return true;
        }

        /// <summary>
        /// Finds a JSON object in the collection by its <c>id</c>.
        /// </summary>
        /// <param name="id">The identifier to search for.</param>
        /// <returns>
        /// The <see cref="JsonObject"/> if found; otherwise, <c>null</c>.
        /// </returns>
        protected override JsonObject? FindById(int id)
        {
            foreach (JsonNode? element in _jsonData)
            {
                if (element is not JsonObject obj)
                    continue;

                if (!TryGetId(obj, out int currentId))
                    continue;

                if (currentId == id)
                    return obj;
            }

            return null;
        }

        /// <summary>
        /// Attempts to extract an integer <c>id</c> property from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="obj">The JSON object from which to extract the ID.</param>
        /// <param name="id">When this method returns, contains the extracted ID if successful; otherwise, 0.</param>
        /// <returns>
        /// <c>true</c> if an <c>id</c> property was successfully parsed; otherwise, <c>false</c>.
        /// </returns>
        protected override bool TryGetId(JsonObject obj, out int id)
        {
            id = 0;

            if (obj is null || obj["id"] is not JsonValue idValue)
                return false;

            string? idText = idValue.ToString()?.Trim();
            if (string.IsNullOrEmpty(idText))
                return false;

            return int.TryParse(
                idText,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out id
            );
        }
    }
}

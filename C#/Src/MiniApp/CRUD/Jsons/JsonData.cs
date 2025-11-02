using System.Text.Json;
using System.Text.Json.Nodes;

namespace MiniApp.CRUD.Jsons
{
    public class JsonData : JsonAbstract
    {
        private readonly JsonArray _jsonData; //? Storage Variable

        //? Constructor
        public JsonData()
        {
            _jsonData = []; //? Same as new JsonArray(); but simplier.
        }
        public override JsonArray GetAll()
        {
            return _jsonData;
        }

        public override void Add(JsonObject newData)
        {
            ArgumentNullException.ThrowIfNull(newData);

            if (!TryValidateAndNormalize(newData, out JsonObject normalized))
                throw new JsonException("The JSON data is invalid.");

            int id = normalized["id"]!.GetValue<int>();

            if (FindById(id) is not null)
                throw new InvalidOperationException($"An element with id={id} already exists.");

            _jsonData.Add(normalized);
        }

        public override JsonObject? SearchById(int id)
        {
            return FindById(id);
        }

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

                if (string.Equals(key, "id", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (value is null)
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

        public override bool DeleteById(int id)
        {
            JsonObject? target = FindById(id);

            if (target is null) return false;

            _jsonData.Remove(target);
            return true;
        }

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
        protected override bool TryGetId(JsonObject obj, out int id)
        {
            id = 0;

            //? Validate existence
            if (obj is null || obj["id"] is not JsonValue idValue)
                return false;

            //? Convert to string, trimming spaces
            string? idText = idValue.ToString()?.Trim();
            if (string.IsNullOrEmpty(idText))
                return false;

            //? Parse using invariant culture
            return int.TryParse(
                idText,
                System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture,
                out id
            );
        }
    }
}
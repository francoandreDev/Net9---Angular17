using System.Text.Json.Nodes;

namespace MiniApp.Models.Json
{
    public record JsonEntity(int Id, string Name) : IJsonEntity
    {
        public bool IsValid()
        {
            return Id > 0 && !string.IsNullOrWhiteSpace(Name);
        }

        //? Factory method
        public static JsonEntity? FromJson(JsonObject json)
        {
            if (json is null) return null;

            int? id = json["id"]?.GetValue<int>();
            string? name = json["name"]?.GetValue<string>();

            if (id is null || name is null) return null;

            return new JsonEntity(id.Value, name);
        }

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

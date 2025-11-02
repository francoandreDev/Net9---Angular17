using System.Text.Json.Nodes;

namespace MiniApp.Models.Json
{
    public interface IJsonEntity
    {
        int Id { get; }
        string Name { get; }
        bool IsValid();
        JsonObject ToJson();
    }
}
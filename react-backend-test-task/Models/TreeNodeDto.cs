using Newtonsoft.Json;

namespace react_backend_test_task.Models;

[Serializable]
public class TreeNodeDto
{
    [JsonProperty("id", Required = Required.Always)]
    public Guid Id { get; set; }

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; }

    [JsonProperty("children", Required = Required.Always)]
    public ICollection<TreeNodeDto> Children { get; set; }
}
using Newtonsoft.Json;

namespace react_backend_test_task.Models;

[Serializable]
public class CreateTreeNodeRequest
{
    [JsonProperty("treeId", Required = Required.Always)]
    public Guid TreeId { get; set; }

    [JsonProperty("parentNodeId", Required = Required.Always)]
    public Guid ParentNodeId { get; set; }

    [JsonProperty("nodeName", Required = Required.Always)]
    public string NodeName { get; set; }
}
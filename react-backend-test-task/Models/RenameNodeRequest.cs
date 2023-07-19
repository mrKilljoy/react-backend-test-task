using Newtonsoft.Json;

namespace react_backend_test_task.Models;

[Serializable]
public class RenameNodeRequest
{
    [JsonProperty("treeId", Required = Required.Always)]
    public Guid TreeId { get; set; }
    
    [JsonProperty("nodeId", Required = Required.Always)]
    public Guid NodeId { get; set; }

    [JsonProperty("newNodeName", Required = Required.Always)]
    public string NewNodeName { get; set; }
}
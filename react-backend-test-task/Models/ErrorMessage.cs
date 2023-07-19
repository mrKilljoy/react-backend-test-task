using Newtonsoft.Json;

namespace react_backend_test_task.Models;

[Serializable]
public class ErrorMessage
{
    [Serializable]
    public class ErrorData
    {
        [JsonProperty("message", Required = Required.Always)]
        public string Message { get; set; }
    }
    
    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; set; }

    [JsonProperty("id", Required = Required.Always)]
    public Guid Id { get; set; }

    [JsonProperty("data", Required = Required.Always)]
    public ErrorData Data { get; set; }

    /// <inheritdoc />
    public override string ToString() => JsonConvert.SerializeObject(this);
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class FlowDataDTO
{
    [JsonProperty("flowData")]
    public string FlowData { get; set; } // A string JSON que precisa ser desserializada
}

public class FlowDataContentDTO
{
    [JsonProperty("nodes")]
    public List<NodeDTO> Nodes { get; set; }

    [JsonProperty("edges")]
    public List<EdgeDTO> Edges { get; set; }

    [JsonProperty("viewport")]
    public ViewportDTO Viewport { get; set; }
}

public class NodeDTO
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("position")]
    public PositionDTO Position { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("data")]
    public NodeDataDTO Data { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("selected")]
    public bool Selected { get; set; }

    [JsonProperty("positionAbsolute")]
    public PositionDTO PositionAbsolute { get; set; }

    [JsonProperty("dragging")]
    public bool Dragging { get; set; }
}

public class PositionDTO
{
    [JsonProperty("x")]
    public double X { get; set; }

    [JsonProperty("y")]
    public double Y { get; set; }
}

public class NodeDataDTO
{
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("baseClasses")]
    public List<string> BaseClasses { get; set; }

    [JsonProperty("inputs")]
    public InputsDTO Inputs { get; set; }

    [JsonProperty("filePath")]
    public string FilePath { get; set; }

    [JsonProperty("inputAnchors")]
    public List<InputAnchorDTO> InputAnchors { get; set; }

    [JsonProperty("inputParams")]
    public List<InputParamDTO> InputParams { get; set; }

    [JsonProperty("outputs")]
    public OutputsDTO Outputs { get; set; }

    [JsonProperty("outputAnchors")]
    public List<OutputAnchorDTO> OutputAnchors { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("selected")]
    public bool Selected { get; set; }
}

public class InputsDTO
{
    [JsonProperty("tools")]
    public string Tools { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("memory")]
    public string Memory { get; set; }

    [JsonProperty("systemMessage")]
    public string systemMessage { get; set; }

    [JsonProperty("inputModeration")]
    public string InputModeration { get; set; }

    [JsonProperty("maxIterations")]
    public string MaxIterations { get; set; }
}

public class InputAnchorDTO
{
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("list")]
    public bool List { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}

public class InputParamDTO
{
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("rows")]
    public int Rows { get; set; }

    [JsonProperty("default")]
    public string Default { get; set; }

    [JsonProperty("optional")]
    public bool Optional { get; set; }

    [JsonProperty("additionalParams")]
    public bool AdditionalParams { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}

public class OutputsDTO
{
    // Pode ser expandido conforme necessário
}

public class OutputAnchorDTO
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}

public class EdgeDTO
{
    [JsonProperty("source")]
    public string Source { get; set; }

    [JsonProperty("sourceHandle")]
    public string SourceHandle { get; set; }

    [JsonProperty("target")]
    public string Target { get; set; }

    [JsonProperty("targetHandle")]
    public string TargetHandle { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("selected")]
    public bool Selected { get; set; }
}

public class ViewportDTO
{
    [JsonProperty("x")]
    public double X { get; set; }

    [JsonProperty("y")]
    public double Y { get; set; }

    [JsonProperty("zoom")]
    public double Zoom { get; set; }
}
﻿using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

public class Platform : IMetadata
{
    public Dictionary<string, ICollection<string>>? Tags { get; set; }
    public required Guid Id { get; init; }
    public string Name { get; init; } = null!;
    [JsonIgnore] public string JsonFile => "platforms.json";
}

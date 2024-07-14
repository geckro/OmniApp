﻿using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     A series that the game is linked to.
/// </summary>
public class Series : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Logo { get; set; }
    [JsonIgnore] public string JsonFile => "series.json";
}
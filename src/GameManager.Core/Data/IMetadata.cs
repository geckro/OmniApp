﻿namespace GameManager.Core.Data;

public interface IMetadata
{
    string Name { get; }
    string? JsonFile { get; }
}

﻿using GameManager.UI.Helpers;

namespace GameManager.UI.Managers;

[Flags]
public enum RefreshOptions
{
    DataGrid = 0,
    FilterMenu = 1
}

public class RefreshManager
{
    private readonly GameTableHelper _gameTableHelper;

    public RefreshManager(GameTableHelper gameTableHelper)
    {
        _gameTableHelper = gameTableHelper;
    }

    public async Task RefreshControls(RefreshOptions options)
    {
        if (options.HasFlag(RefreshOptions.DataGrid))
        {
            await _gameTableHelper.RefreshGameTableAsync();
        }

        if (options.HasFlag(RefreshOptions.FilterMenu))
        {
        }
    }
}

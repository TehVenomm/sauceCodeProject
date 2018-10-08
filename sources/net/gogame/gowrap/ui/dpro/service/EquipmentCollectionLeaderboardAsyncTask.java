package net.gogame.gowrap.ui.dpro.service;

import net.gogame.gowrap.ui.dpro.model.leaderboard.EquipmentCollectionLeaderboardResponse;

public class EquipmentCollectionLeaderboardAsyncTask extends AbstractLeaderboardAsyncTask<EquipmentCollectionLeaderboardResponse> {
    public EquipmentCollectionLeaderboardAsyncTask() {
        this(DefaultEquipmentCollectionLeaderboardService.INSTANCE);
    }

    public EquipmentCollectionLeaderboardAsyncTask(EquipmentCollectionLeaderboardService equipmentCollectionLeaderboardService) {
        super(equipmentCollectionLeaderboardService);
    }
}

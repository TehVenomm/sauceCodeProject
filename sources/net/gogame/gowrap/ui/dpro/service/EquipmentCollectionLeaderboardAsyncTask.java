package net.gogame.gowrap.p019ui.dpro.service;

import net.gogame.gowrap.p019ui.dpro.model.leaderboard.EquipmentCollectionLeaderboardResponse;

/* renamed from: net.gogame.gowrap.ui.dpro.service.EquipmentCollectionLeaderboardAsyncTask */
public class EquipmentCollectionLeaderboardAsyncTask extends AbstractLeaderboardAsyncTask<EquipmentCollectionLeaderboardResponse> {
    public EquipmentCollectionLeaderboardAsyncTask() {
        this(DefaultEquipmentCollectionLeaderboardService.INSTANCE);
    }

    public EquipmentCollectionLeaderboardAsyncTask(EquipmentCollectionLeaderboardService equipmentCollectionLeaderboardService) {
        super(equipmentCollectionLeaderboardService);
    }
}

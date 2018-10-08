package net.gogame.gowrap.ui.dpro.service;

import net.gogame.gowrap.ui.dpro.model.leaderboard.PowerRatingLeaderboardResponse;

public class PowerRatingLeaderboardAsyncTask extends AbstractLeaderboardAsyncTask<PowerRatingLeaderboardResponse> {
    public PowerRatingLeaderboardAsyncTask() {
        this(DefaultPowerRatingLeaderboardService.INSTANCE);
    }

    public PowerRatingLeaderboardAsyncTask(PowerRatingLeaderboardService powerRatingLeaderboardService) {
        super(powerRatingLeaderboardService);
    }
}

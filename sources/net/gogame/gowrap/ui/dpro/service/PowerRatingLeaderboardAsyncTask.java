package net.gogame.gowrap.p019ui.dpro.service;

import net.gogame.gowrap.p019ui.dpro.model.leaderboard.PowerRatingLeaderboardResponse;

/* renamed from: net.gogame.gowrap.ui.dpro.service.PowerRatingLeaderboardAsyncTask */
public class PowerRatingLeaderboardAsyncTask extends AbstractLeaderboardAsyncTask<PowerRatingLeaderboardResponse> {
    public PowerRatingLeaderboardAsyncTask() {
        this(DefaultPowerRatingLeaderboardService.INSTANCE);
    }

    public PowerRatingLeaderboardAsyncTask(PowerRatingLeaderboardService powerRatingLeaderboardService) {
        super(powerRatingLeaderboardService);
    }
}

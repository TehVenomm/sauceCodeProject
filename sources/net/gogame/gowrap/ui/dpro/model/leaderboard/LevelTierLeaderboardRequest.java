package net.gogame.gowrap.p019ui.dpro.model.leaderboard;

import net.gogame.gowrap.p019ui.dpro.model.PageRequest;

/* renamed from: net.gogame.gowrap.ui.dpro.model.leaderboard.LevelTierLeaderboardRequest */
public class LevelTierLeaderboardRequest extends PageRequest implements LeaderboardRequest {
    private Integer levelTier;

    public LevelTierLeaderboardRequest() {
    }

    public LevelTierLeaderboardRequest(int i, int i2) {
        super(i, i2);
    }

    public LevelTierLeaderboardRequest(Integer num, int i, int i2) {
        super(i, i2);
        this.levelTier = num;
    }

    public Integer getLevelTier() {
        return this.levelTier;
    }

    public void setLevelTier(Integer num) {
        this.levelTier = num;
    }
}

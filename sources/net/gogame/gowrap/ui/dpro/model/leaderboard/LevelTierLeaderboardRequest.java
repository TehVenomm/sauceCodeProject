package net.gogame.gowrap.ui.dpro.model.leaderboard;

import net.gogame.gowrap.ui.dpro.model.PageRequest;

public class LevelTierLeaderboardRequest extends PageRequest implements LeaderboardRequest {
    private Integer levelTier;

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

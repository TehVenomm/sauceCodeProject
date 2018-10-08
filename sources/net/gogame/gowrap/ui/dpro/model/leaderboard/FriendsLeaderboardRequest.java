package net.gogame.gowrap.ui.dpro.model.leaderboard;

public class FriendsLeaderboardRequest implements LeaderboardRequest {
    private String hunterId;

    public FriendsLeaderboardRequest(String str) {
        this.hunterId = str;
    }

    public String getHunterId() {
        return this.hunterId;
    }

    public void setHunterId(String str) {
        this.hunterId = str;
    }
}

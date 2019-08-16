package net.gogame.gowrap.p019ui.dpro.model.leaderboard;

/* renamed from: net.gogame.gowrap.ui.dpro.model.leaderboard.FriendsLeaderboardRequest */
public class FriendsLeaderboardRequest implements LeaderboardRequest {
    private String hunterId;

    public FriendsLeaderboardRequest() {
    }

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

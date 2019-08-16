package net.gogame.gowrap.p019ui.dpro.model.leaderboard;

import android.util.JsonReader;
import java.io.IOException;

/* renamed from: net.gogame.gowrap.ui.dpro.model.leaderboard.PowerRatingLeaderboardResponse */
public class PowerRatingLeaderboardResponse extends AbstractLeaderboardResponse<PowerRatingLeaderboardEntry> {
    public PowerRatingLeaderboardResponse() {
    }

    public PowerRatingLeaderboardResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public PowerRatingLeaderboardEntry doParseEntry(JsonReader jsonReader) throws IOException {
        return new PowerRatingLeaderboardEntry(jsonReader);
    }
}

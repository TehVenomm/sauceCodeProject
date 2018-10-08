package net.gogame.gowrap.ui.dpro.model.leaderboard;

import android.util.JsonReader;
import java.io.IOException;

public class PowerRatingLeaderboardResponse extends AbstractLeaderboardResponse<PowerRatingLeaderboardEntry> {
    public PowerRatingLeaderboardResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected PowerRatingLeaderboardEntry doParseEntry(JsonReader jsonReader) throws IOException {
        return new PowerRatingLeaderboardEntry(jsonReader);
    }
}

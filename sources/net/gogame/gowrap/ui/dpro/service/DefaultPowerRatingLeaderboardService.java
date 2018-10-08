package net.gogame.gowrap.ui.dpro.service;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.ui.dpro.model.leaderboard.PowerRatingLeaderboardResponse;

public class DefaultPowerRatingLeaderboardService extends AbstractLeaderboardService<PowerRatingLeaderboardResponse> implements PowerRatingLeaderboardService {
    public static final DefaultPowerRatingLeaderboardService INSTANCE = new DefaultPowerRatingLeaderboardService();

    public DefaultPowerRatingLeaderboardService() {
        super("powerRating");
    }

    protected PowerRatingLeaderboardResponse parseResponse(JsonReader jsonReader) throws IOException {
        return new PowerRatingLeaderboardResponse(jsonReader);
    }
}

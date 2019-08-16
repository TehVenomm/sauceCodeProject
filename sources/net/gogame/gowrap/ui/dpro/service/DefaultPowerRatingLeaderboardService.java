package net.gogame.gowrap.p019ui.dpro.service;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.PowerRatingLeaderboardResponse;

/* renamed from: net.gogame.gowrap.ui.dpro.service.DefaultPowerRatingLeaderboardService */
public class DefaultPowerRatingLeaderboardService extends AbstractLeaderboardService<PowerRatingLeaderboardResponse> implements PowerRatingLeaderboardService {
    public static final DefaultPowerRatingLeaderboardService INSTANCE = new DefaultPowerRatingLeaderboardService();

    public DefaultPowerRatingLeaderboardService() {
        super("powerRating");
    }

    /* access modifiers changed from: protected */
    public PowerRatingLeaderboardResponse parseResponse(JsonReader jsonReader) throws IOException {
        return new PowerRatingLeaderboardResponse(jsonReader);
    }
}

package net.gogame.gowrap.ui.dpro.model.leaderboard;

import android.util.JsonReader;
import java.io.IOException;

public class EquipmentCollectionLeaderboardResponse extends AbstractLeaderboardResponse<EquipmentCollectionLeaderboardEntry> {
    public EquipmentCollectionLeaderboardResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected EquipmentCollectionLeaderboardEntry doParseEntry(JsonReader jsonReader) throws IOException {
        return new EquipmentCollectionLeaderboardEntry(jsonReader);
    }
}

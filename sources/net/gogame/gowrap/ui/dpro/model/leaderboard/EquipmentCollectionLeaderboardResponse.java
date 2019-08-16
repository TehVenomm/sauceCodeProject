package net.gogame.gowrap.p019ui.dpro.model.leaderboard;

import android.util.JsonReader;
import java.io.IOException;

/* renamed from: net.gogame.gowrap.ui.dpro.model.leaderboard.EquipmentCollectionLeaderboardResponse */
public class EquipmentCollectionLeaderboardResponse extends AbstractLeaderboardResponse<EquipmentCollectionLeaderboardEntry> {
    public EquipmentCollectionLeaderboardResponse() {
    }

    public EquipmentCollectionLeaderboardResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public EquipmentCollectionLeaderboardEntry doParseEntry(JsonReader jsonReader) throws IOException {
        return new EquipmentCollectionLeaderboardEntry(jsonReader);
    }
}

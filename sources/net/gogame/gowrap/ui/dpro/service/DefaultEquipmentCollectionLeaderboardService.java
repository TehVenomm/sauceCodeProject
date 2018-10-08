package net.gogame.gowrap.ui.dpro.service;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.ui.dpro.model.leaderboard.EquipmentCollectionLeaderboardResponse;

public class DefaultEquipmentCollectionLeaderboardService extends AbstractLeaderboardService<EquipmentCollectionLeaderboardResponse> implements EquipmentCollectionLeaderboardService {
    public static final DefaultEquipmentCollectionLeaderboardService INSTANCE = new DefaultEquipmentCollectionLeaderboardService();

    public DefaultEquipmentCollectionLeaderboardService() {
        super("equipmentCollection");
    }

    protected EquipmentCollectionLeaderboardResponse parseResponse(JsonReader jsonReader) throws IOException {
        return new EquipmentCollectionLeaderboardResponse(jsonReader);
    }
}

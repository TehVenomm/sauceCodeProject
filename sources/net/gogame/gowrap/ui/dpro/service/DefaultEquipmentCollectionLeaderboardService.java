package net.gogame.gowrap.p019ui.dpro.service;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.p019ui.dpro.model.leaderboard.EquipmentCollectionLeaderboardResponse;

/* renamed from: net.gogame.gowrap.ui.dpro.service.DefaultEquipmentCollectionLeaderboardService */
public class DefaultEquipmentCollectionLeaderboardService extends AbstractLeaderboardService<EquipmentCollectionLeaderboardResponse> implements EquipmentCollectionLeaderboardService {
    public static final DefaultEquipmentCollectionLeaderboardService INSTANCE = new DefaultEquipmentCollectionLeaderboardService();

    public DefaultEquipmentCollectionLeaderboardService() {
        super("equipmentCollection");
    }

    /* access modifiers changed from: protected */
    public EquipmentCollectionLeaderboardResponse parseResponse(JsonReader jsonReader) throws IOException {
        return new EquipmentCollectionLeaderboardResponse(jsonReader);
    }
}

package net.gogame.gowrap.ui.dpro.model.leaderboard;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class EquipmentCollectionLeaderboardEntry extends LeaderboardEntry {
    private static final String KEY_EQUIPMENT_COUNT = "equipmentCount";
    private static final String KEY_POINTS = "points";
    private Integer equipmentCount;
    private Long points;

    public EquipmentCollectionLeaderboardEntry(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_EQUIPMENT_COUNT)) {
            this.equipmentCount = JSONUtils.optInt(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_POINTS)) {
            return super.doParse(jsonReader, str);
        } else {
            this.points = JSONUtils.optLong(jsonReader);
            return true;
        }
    }

    public Long getValue() {
        return this.points;
    }

    public void setValue(Long l) {
        this.points = l;
    }

    public Integer getEquipmentCount() {
        return this.equipmentCount;
    }

    public void setEquipmentCount(Integer num) {
        this.equipmentCount = num;
    }

    public Long getPoints() {
        return this.points;
    }

    public void setPoints(Long l) {
        this.points = l;
    }
}

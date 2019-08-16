package net.gogame.gowrap.p019ui.dpro.model.leaderboard;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.dpro.model.leaderboard.PowerRatingLeaderboardEntry */
public class PowerRatingLeaderboardEntry extends LeaderboardEntry {
    private static final String KEY_POWER_RATING = "powerRating";
    private static final String KEY_WEAPON_NAME = "weaponName";
    private static final String KEY_WEAPON_TYPE = "weaponType";
    private Long powerRating;
    private String weaponName;
    private Integer weaponType;

    public PowerRatingLeaderboardEntry() {
    }

    public PowerRatingLeaderboardEntry(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_WEAPON_TYPE)) {
            this.weaponType = JSONUtils.optInt(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_WEAPON_NAME)) {
            this.weaponName = JSONUtils.optString(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_POWER_RATING)) {
            return super.doParse(jsonReader, str);
        } else {
            this.powerRating = JSONUtils.optLong(jsonReader);
            return true;
        }
    }

    public Long getValue() {
        return this.powerRating;
    }

    public void setValue(Long l) {
        this.powerRating = l;
    }

    public int getWeaponType() {
        return this.weaponType.intValue();
    }

    public void setWeaponType(int i) {
        this.weaponType = Integer.valueOf(i);
    }

    public String getWeaponName() {
        return this.weaponName;
    }

    public void setWeaponName(String str) {
        this.weaponName = str;
    }

    public long getPowerRating() {
        return this.powerRating.longValue();
    }

    public void setPowerRating(long j) {
        this.powerRating = Long.valueOf(j);
    }
}

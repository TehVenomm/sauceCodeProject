package net.gogame.gowrap.ui.dpro.model.leaderboard;

import android.util.JsonReader;
import java.io.IOException;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public abstract class LeaderboardEntry extends BaseJsonObject {
    private static final String KEY_HUNTER_ID = "hunterId";
    private static final String KEY_OFFSET = "offset";
    private static final String KEY_POSITION = "position";
    private static final String KEY_USER_ID = "userId";
    private static final String KEY_USER_LEVEL = "userLevel";
    private static final String KEY_USER_NAME = "userName";
    private static final String KEY_USER_TITLE = "userTitle";
    private static final String KEY_USER_TITLE_FRAME_ID = "userTitleFrameId";
    private String hunterId;
    private Long offset;
    private Long position;
    private Long userId;
    private Integer userLevel;
    private String userName;
    private String userTitle;
    private Long userTitleFrameId;

    public abstract Long getValue();

    public abstract void setValue(Long l);

    public LeaderboardEntry(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_OFFSET)) {
            this.offset = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_POSITION)) {
            this.position = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, "userId")) {
            this.userId = JSONUtils.optLong(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_HUNTER_ID)) {
            this.hunterId = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_USER_NAME)) {
            this.userName = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_USER_TITLE)) {
            this.userTitle = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_USER_TITLE_FRAME_ID)) {
            this.userTitleFrameId = JSONUtils.optLong(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, KEY_USER_LEVEL)) {
            return false;
        } else {
            this.userLevel = JSONUtils.optInt(jsonReader);
            return true;
        }
    }

    public Long getOffset() {
        return this.offset;
    }

    public void setOffset(Long l) {
        this.offset = l;
    }

    public Long getPosition() {
        return this.position;
    }

    public void setPosition(Long l) {
        this.position = l;
    }

    public Long getUserId() {
        return this.userId;
    }

    public void setUserId(Long l) {
        this.userId = l;
    }

    public String getHunterId() {
        return this.hunterId;
    }

    public void setHunterId(String str) {
        this.hunterId = str;
    }

    public String getUserName() {
        return this.userName;
    }

    public void setUserName(String str) {
        this.userName = str;
    }

    public String getUserTitle() {
        return this.userTitle;
    }

    public void setUserTitle(String str) {
        this.userTitle = str;
    }

    public Integer getUserLevel() {
        return this.userLevel;
    }

    public void setUserLevel(Integer num) {
        this.userLevel = num;
    }

    public Long getUserTitleFrameId() {
        return this.userTitleFrameId;
    }

    public void setUserTitleFrameId(Long l) {
        this.userTitleFrameId = l;
    }
}

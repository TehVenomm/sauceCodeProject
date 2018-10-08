package net.gogame.gowrap.ui.dpro.model.leaderboard;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.dpro.model.BaseResponse;

public abstract class AbstractLeaderboardResponse<T extends LeaderboardEntry> extends BaseResponse implements LeaderboardResponse<T> {
    private static final String KEY_DATE = "date";
    private static final String KEY_RECORDS = "records";
    private static final String KEY_TOTAL_RECORD_COUNT = "totalRecordCount";
    private String date;
    private List<T> records;
    private Long totalRecordCount;

    protected abstract T doParseEntry(JsonReader jsonReader) throws IOException;

    public AbstractLeaderboardResponse(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    protected boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, KEY_DATE)) {
            this.date = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_RECORDS)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.records = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        jsonReader.nextNull();
                        this.records.add(null);
                    } else {
                        this.records.add(doParseEntry(jsonReader));
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        } else if (!StringUtils.isEquals(str, KEY_TOTAL_RECORD_COUNT)) {
            return super.doParse(jsonReader, str);
        } else {
            this.totalRecordCount = JSONUtils.optLong(jsonReader);
            return true;
        }
    }

    public String getDate() {
        return this.date;
    }

    public void setDate(String str) {
        this.date = str;
    }

    public List<T> getRecords() {
        return this.records;
    }

    public void setRecords(List<T> list) {
        this.records = list;
    }

    public Long getTotalRecordCount() {
        return this.totalRecordCount;
    }

    public void setTotalRecordCount(Long l) {
        this.totalRecordCount = l;
    }
}

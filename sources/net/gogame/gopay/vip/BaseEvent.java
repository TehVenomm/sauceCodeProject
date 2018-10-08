package net.gogame.gopay.vip;

import org.json.JSONException;
import org.json.JSONObject;

public interface BaseEvent {
    JSONObject marshal() throws JSONException;

    void unmarshal(JSONObject jSONObject) throws JSONException;
}

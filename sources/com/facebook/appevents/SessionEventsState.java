package com.facebook.appevents;

import android.content.Context;
import android.os.Bundle;
import com.facebook.GraphRequest;
import com.facebook.internal.AppEventsLoggerUtility;
import com.facebook.internal.AppEventsLoggerUtility.GraphAPIActivityType;
import com.facebook.internal.AttributionIdentifiers;
import com.facebook.internal.Utility;
import java.util.ArrayList;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

class SessionEventsState {
    private final int MAX_ACCUMULATED_LOG_EVENTS = 1000;
    private List<AppEvent> accumulatedEvents = new ArrayList();
    private String anonymousAppDeviceGUID;
    private AttributionIdentifiers attributionIdentifiers;
    private List<AppEvent> inFlightEvents = new ArrayList();
    private int numSkippedEventsDueToFullBuffer;

    public SessionEventsState(AttributionIdentifiers attributionIdentifiers, String str) {
        this.attributionIdentifiers = attributionIdentifiers;
        this.anonymousAppDeviceGUID = str;
    }

    private byte[] getStringAsByteArray(String str) {
        byte[] bArr = null;
        try {
            bArr = str.getBytes("UTF-8");
        } catch (Exception e) {
            Utility.logd("Encoding exception: ", e);
        }
        return bArr;
    }

    private void populateRequest(GraphRequest graphRequest, Context context, int i, JSONArray jSONArray, boolean z) {
        JSONObject jSONObjectForGraphAPICall;
        try {
            jSONObjectForGraphAPICall = AppEventsLoggerUtility.getJSONObjectForGraphAPICall(GraphAPIActivityType.CUSTOM_APP_EVENTS, this.attributionIdentifiers, this.anonymousAppDeviceGUID, z, context);
            if (this.numSkippedEventsDueToFullBuffer > 0) {
                jSONObjectForGraphAPICall.put("num_skipped_events", i);
            }
        } catch (JSONException e) {
            jSONObjectForGraphAPICall = new JSONObject();
        }
        graphRequest.setGraphObject(jSONObjectForGraphAPICall);
        Bundle parameters = graphRequest.getParameters();
        if (parameters == null) {
            parameters = new Bundle();
        }
        String jSONArray2 = jSONArray.toString();
        if (jSONArray2 != null) {
            parameters.putByteArray("custom_events_file", getStringAsByteArray(jSONArray2));
            graphRequest.setTag(jSONArray2);
        }
        graphRequest.setParameters(parameters);
    }

    public void accumulatePersistedEvents(List<AppEvent> list) {
        synchronized (this) {
            this.accumulatedEvents.addAll(list);
        }
    }

    public void addEvent(AppEvent appEvent) {
        synchronized (this) {
            if (this.accumulatedEvents.size() + this.inFlightEvents.size() >= 1000) {
                this.numSkippedEventsDueToFullBuffer++;
            } else {
                this.accumulatedEvents.add(appEvent);
            }
        }
    }

    public void clearInFlightAndStats(boolean z) {
        synchronized (this) {
            if (z) {
                this.accumulatedEvents.addAll(this.inFlightEvents);
            }
            this.inFlightEvents.clear();
            this.numSkippedEventsDueToFullBuffer = 0;
        }
    }

    public int getAccumulatedEventCount() {
        int size;
        synchronized (this) {
            size = this.accumulatedEvents.size();
        }
        return size;
    }

    public List<AppEvent> getEventsToPersist() {
        List<AppEvent> list;
        synchronized (this) {
            list = this.accumulatedEvents;
            this.accumulatedEvents = new ArrayList();
        }
        return list;
    }

    public int populateRequest(GraphRequest graphRequest, Context context, boolean z, boolean z2) {
        synchronized (this) {
            int i = this.numSkippedEventsDueToFullBuffer;
            this.inFlightEvents.addAll(this.accumulatedEvents);
            this.accumulatedEvents.clear();
            JSONArray jSONArray = new JSONArray();
            for (AppEvent appEvent : this.inFlightEvents) {
                if (z || !appEvent.getIsImplicit()) {
                    jSONArray.put(appEvent.getJSONObject());
                }
            }
            if (jSONArray.length() == 0) {
                return 0;
            }
            populateRequest(graphRequest, context, i, jSONArray, z2);
            return jSONArray.length();
        }
    }
}

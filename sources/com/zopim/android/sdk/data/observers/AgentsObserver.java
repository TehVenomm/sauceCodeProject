package com.zopim.android.sdk.data.observers;

import android.util.Log;
import com.zopim.android.sdk.data.LivechatAgentsPath;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;
import java.util.Observable;
import java.util.Observer;

public abstract class AgentsObserver implements Observer {
    private static final String LOG_TAG = AgentsObserver.class.getSimpleName();

    public abstract void update(Map<String, Agent> map);

    public final void update(Observable observable, Object obj) {
        if (!(observable instanceof LivechatAgentsPath)) {
            Log.i(LOG_TAG, "Unexpected broadcast observable " + observable + " Observable should be of type " + LivechatAgentsPath.class);
        } else if (obj instanceof Map) {
            update((Map) obj);
        } else {
            Log.i(LOG_TAG, "Unexpected broadcast object " + obj + " Broadcast object should be of type " + Map.class);
        }
    }
}

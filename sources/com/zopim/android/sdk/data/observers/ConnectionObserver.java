package com.zopim.android.sdk.data.observers;

import android.util.Log;
import com.zopim.android.sdk.data.ConnectionPath;
import com.zopim.android.sdk.model.Connection;
import java.util.Observable;
import java.util.Observer;

public abstract class ConnectionObserver implements Observer {
    private static final String LOG_TAG = ConnectionObserver.class.getSimpleName();

    public abstract void update(Connection connection);

    public final void update(Observable observable, Object obj) {
        if (!(observable instanceof ConnectionPath)) {
            Log.i(LOG_TAG, "Unexpected broadcast observable " + observable + " Observable should be of type " + ConnectionPath.class);
        } else if (obj instanceof Connection) {
            update((Connection) obj);
        } else {
            Log.i(LOG_TAG, "Unexpected broadcast object " + obj + " Broadcast object should be of type " + Connection.class);
        }
    }
}

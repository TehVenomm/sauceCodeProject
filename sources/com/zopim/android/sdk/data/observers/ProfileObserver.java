package com.zopim.android.sdk.data.observers;

import android.util.Log;
import com.zopim.android.sdk.data.LivechatProfilePath;
import com.zopim.android.sdk.model.Profile;
import java.util.Observable;
import java.util.Observer;

public abstract class ProfileObserver implements Observer {
    private static final String LOG_TAG = ProfileObserver.class.getSimpleName();

    public abstract void update(Profile profile);

    public final void update(Observable observable, Object obj) {
        if (!(observable instanceof LivechatProfilePath)) {
            Log.i(LOG_TAG, "Unexpected broadcast observable " + observable + " Observable should be of type " + LivechatProfilePath.class);
        } else if (obj instanceof Profile) {
            update((Profile) obj);
        } else {
            Log.i(LOG_TAG, "Unexpected broadcast object " + obj + " Broadcast object should be of type " + Profile.class);
        }
    }
}

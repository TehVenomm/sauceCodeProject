package com.zopim.android.sdk.data.observers;

import android.util.Log;
import com.zopim.android.sdk.data.LivechatFormsPath;
import com.zopim.android.sdk.model.Forms;
import java.util.Observable;
import java.util.Observer;

public abstract class FormsObserver implements Observer {
    private static final String LOG_TAG = FormsObserver.class.getSimpleName();

    public abstract void update(Forms forms);

    public final void update(Observable observable, Object obj) {
        if (!(observable instanceof LivechatFormsPath)) {
            Log.i(LOG_TAG, "Unexpected broadcast observable " + observable + " Observable should be of type " + LivechatFormsPath.class);
        } else if (obj instanceof Forms) {
            update((Forms) obj);
        } else {
            Log.i(LOG_TAG, "Unexpected broadcast object " + obj + " Broadcast object should be of type " + Forms.class);
        }
    }
}

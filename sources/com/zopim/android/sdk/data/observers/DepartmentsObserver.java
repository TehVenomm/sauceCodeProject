package com.zopim.android.sdk.data.observers;

import android.util.Log;
import com.zopim.android.sdk.data.LivechatDepartmentsPath;
import com.zopim.android.sdk.model.Department;
import java.util.Map;
import java.util.Observable;
import java.util.Observer;

public abstract class DepartmentsObserver implements Observer {
    private static final String LOG_TAG = DepartmentsObserver.class.getSimpleName();

    public abstract void update(Map<String, Department> map);

    public final void update(Observable observable, Object obj) {
        if (!(observable instanceof LivechatDepartmentsPath)) {
            Log.i(LOG_TAG, "Unexpected broadcast observable " + observable + " Observable should be of type " + LivechatDepartmentsPath.class);
        } else if (obj instanceof Map) {
            update((Map) obj);
        } else {
            Log.i(LOG_TAG, "Unexpected broadcast object " + obj + " Broadcast object should be of type " + Map.class);
        }
    }
}

package com.github.droidfu;

import android.app.Application;
import android.content.Context;
import android.support.multidex.MultiDex;
import java.lang.ref.WeakReference;
import java.util.HashMap;

public class DroidFuApplication extends Application {
    private HashMap<String, WeakReference<Context>> contextObjects = new HashMap();

    protected void attachBaseContext(Context context) {
        super.attachBaseContext(context);
        MultiDex.install(this);
    }

    public Context getActiveContext(String str) {
        Context context;
        synchronized (this) {
            WeakReference weakReference = (WeakReference) this.contextObjects.get(str);
            if (weakReference == null) {
                context = null;
            } else {
                context = (Context) weakReference.get();
                if (context == null) {
                    this.contextObjects.remove(str);
                }
            }
        }
        return context;
    }

    public void onClose() {
    }

    public void resetActiveContext(String str) {
        synchronized (this) {
            this.contextObjects.remove(str);
        }
    }

    public void setActiveContext(String str, Context context) {
        synchronized (this) {
            this.contextObjects.put(str, new WeakReference(context));
        }
    }
}

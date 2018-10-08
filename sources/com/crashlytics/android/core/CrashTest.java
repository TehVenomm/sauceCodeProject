package com.crashlytics.android.core;

import android.os.AsyncTask;
import com.facebook.appevents.AppEventsConstants;
import io.fabric.sdk.android.Fabric;

public class CrashTest {
    private void privateMethodThatThrowsException(String str) {
        throw new RuntimeException(str);
    }

    public void crashAsyncTask(final long j) {
        new AsyncTask<Void, Void, Void>() {
            protected Void doInBackground(Void... voidArr) {
                try {
                    Thread.sleep(j);
                } catch (InterruptedException e) {
                }
                CrashTest.this.throwRuntimeException("Background thread crash");
                return null;
            }
        }.execute(new Void[]{(Void) null});
    }

    public void indexOutOfBounds() {
        Fabric.getLogger().mo4289d("Fabric", "Out of bounds value: " + new int[2][10]);
    }

    public int stackOverflow() {
        return stackOverflow() + ((int) Math.random());
    }

    public void throwFiveChainedExceptions() {
        try {
            privateMethodThatThrowsException(AppEventsConstants.EVENT_PARAM_VALUE_YES);
        } catch (Throwable e) {
            throw new RuntimeException("2", e);
        } catch (Throwable e2) {
            try {
                throw new RuntimeException("3", e2);
            } catch (Throwable e22) {
                try {
                    throw new RuntimeException("4", e22);
                } catch (Throwable e222) {
                    throw new RuntimeException("5", e222);
                }
            }
        }
    }

    public void throwRuntimeException(String str) {
        throw new RuntimeException(str);
    }
}

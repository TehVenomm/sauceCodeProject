package com.crashlytics.android.core;

import android.os.AsyncTask;
import com.facebook.appevents.AppEventsConstants;
import p017io.fabric.sdk.android.Fabric;

public class CrashTest {
    private void privateMethodThatThrowsException(String str) {
        throw new RuntimeException(str);
    }

    public void crashAsyncTask(final long j) {
        new AsyncTask<Void, Void, Void>() {
            /* access modifiers changed from: protected */
            public Void doInBackground(Void... voidArr) {
                try {
                    Thread.sleep(j);
                } catch (InterruptedException e) {
                }
                CrashTest.this.throwRuntimeException("Background thread crash");
                return null;
            }
        }.execute(new Void[]{null});
    }

    public void indexOutOfBounds() {
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Out of bounds value: " + new int[2][10]);
    }

    public int stackOverflow() {
        return stackOverflow() + ((int) Math.random());
    }

    public void throwFiveChainedExceptions() {
        try {
            privateMethodThatThrowsException(AppEventsConstants.EVENT_PARAM_VALUE_YES);
        } catch (Exception e) {
            throw new RuntimeException("2", e);
        } catch (Exception e2) {
            try {
                throw new RuntimeException("3", e2);
            } catch (Exception e3) {
                try {
                    throw new RuntimeException("4", e3);
                } catch (Exception e4) {
                    throw new RuntimeException("5", e4);
                }
            }
        }
    }

    public void throwRuntimeException(String str) {
        throw new RuntimeException(str);
    }
}

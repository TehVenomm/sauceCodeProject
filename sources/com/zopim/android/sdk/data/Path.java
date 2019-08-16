package com.zopim.android.sdk.data;

import android.util.Log;
import java.util.Observable;

public abstract class Path<T> extends Observable {
    protected static final boolean DEBUG = false;
    private static final String LOG_TAG = Path.class.getSimpleName();
    protected final Parser<T> PARSER = new Parser<>();
    protected T mData;

    public void broadcast() {
        broadcast(getData());
    }

    /* access modifiers changed from: protected */
    public void broadcast(T t) {
        if (countObservers() > 0) {
            setChanged();
            super.notifyObservers(t);
        }
    }

    /* access modifiers changed from: 0000 */
    public abstract void clear();

    /* access modifiers changed from: protected */
    public void finalize() {
        super.finalize();
    }

    public abstract T getData();

    /* access modifiers changed from: protected */
    public boolean isClearRequired(String str) {
        return str == null || str.equals("null");
    }

    @Deprecated
    public final void notifyObservers(Object obj) {
        try {
            broadcast(obj);
        } catch (ClassCastException e) {
            Log.w(LOG_TAG, "Parametrized object should be of specified type T. Will not notify observers.", e);
        }
    }

    /* access modifiers changed from: 0000 */
    public abstract void update(String str);
}

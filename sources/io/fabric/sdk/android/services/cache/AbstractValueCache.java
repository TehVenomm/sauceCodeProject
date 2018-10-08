package io.fabric.sdk.android.services.cache;

import android.content.Context;

public abstract class AbstractValueCache<T> implements ValueCache<T> {
    private final ValueCache<T> childCache;

    public AbstractValueCache() {
        this(null);
    }

    public AbstractValueCache(ValueCache<T> valueCache) {
        this.childCache = valueCache;
    }

    private void cache(Context context, T t) {
        if (t == null) {
            throw new NullPointerException();
        }
        cacheValue(context, t);
    }

    protected abstract void cacheValue(Context context, T t);

    protected abstract void doInvalidate(Context context);

    public final T get(Context context, ValueLoader<T> valueLoader) throws Exception {
        T cached;
        synchronized (this) {
            cached = getCached(context);
            if (cached == null) {
                cached = this.childCache != null ? this.childCache.get(context, valueLoader) : valueLoader.load(context);
                cache(context, cached);
            }
        }
        return cached;
    }

    protected abstract T getCached(Context context);

    public final void invalidate(Context context) {
        synchronized (this) {
            doInvalidate(context);
        }
    }
}

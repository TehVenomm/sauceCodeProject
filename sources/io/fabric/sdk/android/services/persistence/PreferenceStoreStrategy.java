package p017io.fabric.sdk.android.services.persistence;

import android.annotation.SuppressLint;

/* renamed from: io.fabric.sdk.android.services.persistence.PreferenceStoreStrategy */
public class PreferenceStoreStrategy<T> implements PersistenceStrategy<T> {
    private final String key;
    private final SerializationStrategy<T> serializer;
    private final PreferenceStore store;

    public PreferenceStoreStrategy(PreferenceStore preferenceStore, SerializationStrategy<T> serializationStrategy, String str) {
        this.store = preferenceStore;
        this.serializer = serializationStrategy;
        this.key = str;
    }

    @SuppressLint({"CommitPrefEdits"})
    public void clear() {
        this.store.edit().remove(this.key).commit();
    }

    public T restore() {
        return this.serializer.deserialize(this.store.get().getString(this.key, null));
    }

    @SuppressLint({"CommitPrefEdits"})
    public void save(T t) {
        this.store.save(this.store.edit().putString(this.key, this.serializer.serialize(t)));
    }
}

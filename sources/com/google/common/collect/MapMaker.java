package com.google.common.collect;

import com.google.common.base.FinalizableReferenceQueue;
import com.google.common.base.FinalizableSoftReference;
import com.google.common.base.FinalizableWeakReference;
import com.google.common.base.Function;
import com.google.common.collect.CustomConcurrentHashMap.ComputingStrategy;
import com.google.common.collect.CustomConcurrentHashMap.Internals;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.lang.ref.WeakReference;
import java.lang.reflect.Field;
import java.util.TimerTask;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;
import java.util.concurrent.TimeUnit;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public final class MapMaker {
    private static final ValueReference<Object, Object> COMPUTING = new C06391();
    private final Builder builder = new Builder();
    private long expirationNanos = 0;
    private Strength keyStrength = Strength.STRONG;
    private boolean useCustomMap;
    private Strength valueStrength = Strength.STRONG;

    private interface ValueReference<K, V> {
        ValueReference<K, V> copyFor(ReferenceEntry<K, V> referenceEntry);

        V get();

        V waitForValue() throws InterruptedException;
    }

    /* renamed from: com.google.common.collect.MapMaker$1 */
    class C06391 implements ValueReference<Object, Object> {
        C06391() {
        }

        public ValueReference<Object, Object> copyFor(ReferenceEntry<Object, Object> referenceEntry) {
            throw new AssertionError();
        }

        public Object get() {
            return null;
        }

        public Object waitForValue() {
            throw new AssertionError();
        }
    }

    private static class ComputationExceptionReference<K, V> implements ValueReference<K, V> {
        /* renamed from: t */
        final Throwable f371t;

        ComputationExceptionReference(Throwable th) {
            this.f371t = th;
        }

        public ValueReference<K, V> copyFor(ReferenceEntry<K, V> referenceEntry) {
            return this;
        }

        public V get() {
            return null;
        }

        public V waitForValue() {
            throw new AsynchronousComputationException(this.f371t);
        }
    }

    private interface ReferenceEntry<K, V> {
        int getHash();

        K getKey();

        ReferenceEntry<K, V> getNext();

        ValueReference<K, V> getValueReference();

        void setValueReference(ValueReference<K, V> valueReference);

        void valueReclaimed();
    }

    private static class SoftEntry<K, V> extends FinalizableSoftReference<K> implements ReferenceEntry<K, V> {
        final int hash;
        final Internals<K, V, ReferenceEntry<K, V>> internals;
        volatile ValueReference<K, V> valueReference = MapMaker.computing();

        SoftEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i) {
            super(k, QueueHolder.queue);
            this.internals = internals;
            this.hash = i;
        }

        public void finalizeReferent() {
            this.internals.removeEntry(this);
        }

        public int getHash() {
            return this.hash;
        }

        public K getKey() {
            return get();
        }

        public ReferenceEntry<K, V> getNext() {
            return null;
        }

        public ValueReference<K, V> getValueReference() {
            return this.valueReference;
        }

        public void setValueReference(ValueReference<K, V> valueReference) {
            this.valueReference = valueReference;
        }

        public void valueReclaimed() {
            this.internals.removeEntry(this, null);
        }
    }

    private static class LinkedSoftEntry<K, V> extends SoftEntry<K, V> {
        final ReferenceEntry<K, V> next;

        LinkedSoftEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i, ReferenceEntry<K, V> referenceEntry) {
            super(internals, k, i);
            this.next = referenceEntry;
        }

        public ReferenceEntry<K, V> getNext() {
            return this.next;
        }
    }

    private static class StrongEntry<K, V> implements ReferenceEntry<K, V> {
        final int hash;
        final Internals<K, V, ReferenceEntry<K, V>> internals;
        final K key;
        volatile ValueReference<K, V> valueReference = MapMaker.computing();

        StrongEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i) {
            this.internals = internals;
            this.key = k;
            this.hash = i;
        }

        public int getHash() {
            return this.hash;
        }

        public K getKey() {
            return this.key;
        }

        public ReferenceEntry<K, V> getNext() {
            return null;
        }

        public ValueReference<K, V> getValueReference() {
            return this.valueReference;
        }

        public void setValueReference(ValueReference<K, V> valueReference) {
            this.valueReference = valueReference;
        }

        public void valueReclaimed() {
            this.internals.removeEntry(this, null);
        }
    }

    private static class LinkedStrongEntry<K, V> extends StrongEntry<K, V> {
        final ReferenceEntry<K, V> next;

        LinkedStrongEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i, ReferenceEntry<K, V> referenceEntry) {
            super(internals, k, i);
            this.next = referenceEntry;
        }

        public ReferenceEntry<K, V> getNext() {
            return this.next;
        }
    }

    private static class WeakEntry<K, V> extends FinalizableWeakReference<K> implements ReferenceEntry<K, V> {
        final int hash;
        final Internals<K, V, ReferenceEntry<K, V>> internals;
        volatile ValueReference<K, V> valueReference = MapMaker.computing();

        WeakEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i) {
            super(k, QueueHolder.queue);
            this.internals = internals;
            this.hash = i;
        }

        public void finalizeReferent() {
            this.internals.removeEntry(this);
        }

        public int getHash() {
            return this.hash;
        }

        public K getKey() {
            return get();
        }

        public ReferenceEntry<K, V> getNext() {
            return null;
        }

        public ValueReference<K, V> getValueReference() {
            return this.valueReference;
        }

        public void setValueReference(ValueReference<K, V> valueReference) {
            this.valueReference = valueReference;
        }

        public void valueReclaimed() {
            this.internals.removeEntry(this, null);
        }
    }

    private static class LinkedWeakEntry<K, V> extends WeakEntry<K, V> {
        final ReferenceEntry<K, V> next;

        LinkedWeakEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i, ReferenceEntry<K, V> referenceEntry) {
            super(internals, k, i);
            this.next = referenceEntry;
        }

        public ReferenceEntry<K, V> getNext() {
            return this.next;
        }
    }

    private static class NullOutputExceptionReference<K, V> implements ValueReference<K, V> {
        final String message;

        NullOutputExceptionReference(String str) {
            this.message = str;
        }

        public ValueReference<K, V> copyFor(ReferenceEntry<K, V> referenceEntry) {
            return this;
        }

        public V get() {
            return null;
        }

        public V waitForValue() {
            throw new NullOutputException(this.message);
        }
    }

    private static class QueueHolder {
        static final FinalizableReferenceQueue queue = new FinalizableReferenceQueue();

        private QueueHolder() {
        }
    }

    private static class SoftValueReference<K, V> extends FinalizableSoftReference<V> implements ValueReference<K, V> {
        final ReferenceEntry<K, V> entry;

        SoftValueReference(V v, ReferenceEntry<K, V> referenceEntry) {
            super(v, QueueHolder.queue);
            this.entry = referenceEntry;
        }

        public ValueReference<K, V> copyFor(ReferenceEntry<K, V> referenceEntry) {
            return new SoftValueReference(get(), referenceEntry);
        }

        public void finalizeReferent() {
            this.entry.valueReclaimed();
        }

        public V waitForValue() {
            return get();
        }
    }

    private static class StrategyImpl<K, V> implements Serializable, ComputingStrategy<K, V, ReferenceEntry<K, V>> {
        private static final long serialVersionUID = 0;
        final long expirationNanos;
        Internals<K, V, ReferenceEntry<K, V>> internals;
        final Strength keyStrength;
        final ConcurrentMap<K, V> map;
        final Strength valueStrength;

        private static class Fields {
            static final Field expirationNanos = findField("expirationNanos");
            static final Field internals = findField("internals");
            static final Field keyStrength = findField("keyStrength");
            static final Field map = findField("map");
            static final Field valueStrength = findField("valueStrength");

            private Fields() {
            }

            static Field findField(String str) {
                try {
                    Field declaredField = StrategyImpl.class.getDeclaredField(str);
                    declaredField.setAccessible(true);
                    return declaredField;
                } catch (NoSuchFieldException e) {
                    throw new AssertionError(e);
                }
            }
        }

        private class FutureValueReference implements ValueReference<K, V> {
            final ReferenceEntry<K, V> newEntry;
            final ReferenceEntry<K, V> original;

            FutureValueReference(ReferenceEntry<K, V> referenceEntry, ReferenceEntry<K, V> referenceEntry2) {
                this.original = referenceEntry;
                this.newEntry = referenceEntry2;
            }

            public ValueReference<K, V> copyFor(ReferenceEntry<K, V> referenceEntry) {
                return new FutureValueReference(this.original, referenceEntry);
            }

            public V get() {
                try {
                    return this.original.getValueReference().get();
                } catch (Throwable th) {
                    removeEntry();
                }
            }

            void removeEntry() {
                StrategyImpl.this.internals.removeEntry(this.newEntry);
            }

            public V waitForValue() throws InterruptedException {
                try {
                    return StrategyImpl.this.waitForValue(this.original);
                } catch (Throwable th) {
                    removeEntry();
                }
            }
        }

        StrategyImpl(MapMaker mapMaker) {
            this.keyStrength = mapMaker.keyStrength;
            this.valueStrength = mapMaker.valueStrength;
            this.expirationNanos = mapMaker.expirationNanos;
            this.map = mapMaker.builder.buildMap(this);
        }

        StrategyImpl(MapMaker mapMaker, Function<? super K, ? extends V> function) {
            this.keyStrength = mapMaker.keyStrength;
            this.valueStrength = mapMaker.valueStrength;
            this.expirationNanos = mapMaker.expirationNanos;
            this.map = mapMaker.builder.buildComputingMap(this, function);
        }

        private void readObject(ObjectInputStream objectInputStream) throws IOException, ClassNotFoundException {
            try {
                Fields.keyStrength.set(this, objectInputStream.readObject());
                Fields.valueStrength.set(this, objectInputStream.readObject());
                Fields.expirationNanos.set(this, Long.valueOf(objectInputStream.readLong()));
                Fields.internals.set(this, objectInputStream.readObject());
                Fields.map.set(this, objectInputStream.readObject());
            } catch (IllegalAccessException e) {
                throw new AssertionError(e);
            }
        }

        private void writeObject(ObjectOutputStream objectOutputStream) throws IOException {
            objectOutputStream.writeObject(this.keyStrength);
            objectOutputStream.writeObject(this.valueStrength);
            objectOutputStream.writeLong(this.expirationNanos);
            objectOutputStream.writeObject(this.internals);
            objectOutputStream.writeObject(this.map);
        }

        public V compute(K k, ReferenceEntry<K, V> referenceEntry, Function<? super K, ? extends V> function) {
            try {
                Object apply = function.apply(k);
                if (apply == null) {
                    String str = function + " returned null for key " + k + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER;
                    setValueReference(referenceEntry, new NullOutputExceptionReference(str));
                    throw new NullOutputException(str);
                }
                setValue((ReferenceEntry) referenceEntry, apply);
                return apply;
            } catch (ComputationException e) {
                setValueReference(referenceEntry, new ComputationExceptionReference(e.getCause()));
                throw e;
            } catch (Throwable th) {
                setValueReference(referenceEntry, new ComputationExceptionReference(th));
                ComputationException computationException = new ComputationException(th);
            }
        }

        public ReferenceEntry<K, V> copyEntry(K k, ReferenceEntry<K, V> referenceEntry, ReferenceEntry<K, V> referenceEntry2) {
            ValueReference valueReference = referenceEntry.getValueReference();
            if (valueReference == MapMaker.COMPUTING) {
                ReferenceEntry<K, V> newEntry = newEntry((Object) k, referenceEntry.getHash(), (ReferenceEntry) referenceEntry2);
                newEntry.setValueReference(new FutureValueReference(referenceEntry, newEntry));
                return newEntry;
            }
            newEntry = newEntry((Object) k, referenceEntry.getHash(), (ReferenceEntry) referenceEntry2);
            newEntry.setValueReference(valueReference.copyFor(newEntry));
            return newEntry;
        }

        public boolean equalKeys(K k, Object obj) {
            return this.keyStrength.equal(k, obj);
        }

        public boolean equalValues(V v, Object obj) {
            return this.valueStrength.equal(v, obj);
        }

        public int getHash(ReferenceEntry<K, V> referenceEntry) {
            return referenceEntry.getHash();
        }

        public K getKey(ReferenceEntry<K, V> referenceEntry) {
            return referenceEntry.getKey();
        }

        public ReferenceEntry<K, V> getNext(ReferenceEntry<K, V> referenceEntry) {
            return referenceEntry.getNext();
        }

        public V getValue(ReferenceEntry<K, V> referenceEntry) {
            return referenceEntry.getValueReference().get();
        }

        public int hashKey(Object obj) {
            return this.keyStrength.hash(obj);
        }

        public ReferenceEntry<K, V> newEntry(K k, int i, ReferenceEntry<K, V> referenceEntry) {
            return this.keyStrength.newEntry(this.internals, k, i, referenceEntry);
        }

        void scheduleRemoval(K k, V v) {
            final WeakReference weakReference = new WeakReference(k);
            final WeakReference weakReference2 = new WeakReference(v);
            ExpirationTimer.instance.schedule(new TimerTask() {
                public void run() {
                    Object obj = weakReference.get();
                    if (obj != null) {
                        StrategyImpl.this.map.remove(obj, weakReference2.get());
                    }
                }
            }, TimeUnit.NANOSECONDS.toMillis(this.expirationNanos));
        }

        public void setInternals(Internals<K, V, ReferenceEntry<K, V>> internals) {
            this.internals = internals;
        }

        public void setValue(ReferenceEntry<K, V> referenceEntry, V v) {
            setValueReference(referenceEntry, this.valueStrength.referenceValue(referenceEntry, v));
            if (this.expirationNanos > 0) {
                scheduleRemoval(referenceEntry.getKey(), v);
            }
        }

        void setValueReference(ReferenceEntry<K, V> referenceEntry, ValueReference<K, V> valueReference) {
            Object obj = referenceEntry.getValueReference() == MapMaker.COMPUTING ? 1 : null;
            referenceEntry.setValueReference(valueReference);
            if (obj != null) {
                synchronized (referenceEntry) {
                    referenceEntry.notifyAll();
                }
            }
        }

        public V waitForValue(ReferenceEntry<K, V> referenceEntry) throws InterruptedException {
            ValueReference valueReference = referenceEntry.getValueReference();
            if (valueReference == MapMaker.COMPUTING) {
                synchronized (referenceEntry) {
                    while (true) {
                        valueReference = referenceEntry.getValueReference();
                        if (valueReference != MapMaker.COMPUTING) {
                            break;
                        }
                        referenceEntry.wait();
                    }
                }
            }
            return valueReference.waitForValue();
        }
    }

    private enum Strength {
        WEAK {
            <K, V> ReferenceEntry<K, V> copyEntry(K k, ReferenceEntry<K, V> referenceEntry, ReferenceEntry<K, V> referenceEntry2) {
                WeakEntry weakEntry = (WeakEntry) referenceEntry;
                return referenceEntry2 == null ? new WeakEntry(weakEntry.internals, k, weakEntry.hash) : new LinkedWeakEntry(weakEntry.internals, k, weakEntry.hash, referenceEntry2);
            }

            boolean equal(Object obj, Object obj2) {
                return obj == obj2;
            }

            int hash(Object obj) {
                return System.identityHashCode(obj);
            }

            <K, V> ReferenceEntry<K, V> newEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i, ReferenceEntry<K, V> referenceEntry) {
                return referenceEntry == null ? new WeakEntry(internals, k, i) : new LinkedWeakEntry(internals, k, i, referenceEntry);
            }

            <K, V> ValueReference<K, V> referenceValue(ReferenceEntry<K, V> referenceEntry, V v) {
                return new WeakValueReference(v, referenceEntry);
            }
        },
        SOFT {
            <K, V> ReferenceEntry<K, V> copyEntry(K k, ReferenceEntry<K, V> referenceEntry, ReferenceEntry<K, V> referenceEntry2) {
                SoftEntry softEntry = (SoftEntry) referenceEntry;
                return referenceEntry2 == null ? new SoftEntry(softEntry.internals, k, softEntry.hash) : new LinkedSoftEntry(softEntry.internals, k, softEntry.hash, referenceEntry2);
            }

            boolean equal(Object obj, Object obj2) {
                return obj == obj2;
            }

            int hash(Object obj) {
                return System.identityHashCode(obj);
            }

            <K, V> ReferenceEntry<K, V> newEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i, ReferenceEntry<K, V> referenceEntry) {
                return referenceEntry == null ? new SoftEntry(internals, k, i) : new LinkedSoftEntry(internals, k, i, referenceEntry);
            }

            <K, V> ValueReference<K, V> referenceValue(ReferenceEntry<K, V> referenceEntry, V v) {
                return new SoftValueReference(v, referenceEntry);
            }
        },
        STRONG {
            <K, V> ReferenceEntry<K, V> copyEntry(K k, ReferenceEntry<K, V> referenceEntry, ReferenceEntry<K, V> referenceEntry2) {
                StrongEntry strongEntry = (StrongEntry) referenceEntry;
                return referenceEntry2 == null ? new StrongEntry(strongEntry.internals, k, strongEntry.hash) : new LinkedStrongEntry(strongEntry.internals, k, strongEntry.hash, referenceEntry2);
            }

            boolean equal(Object obj, Object obj2) {
                return obj.equals(obj2);
            }

            int hash(Object obj) {
                return obj.hashCode();
            }

            <K, V> ReferenceEntry<K, V> newEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i, ReferenceEntry<K, V> referenceEntry) {
                return referenceEntry == null ? new StrongEntry(internals, k, i) : new LinkedStrongEntry(internals, k, i, referenceEntry);
            }

            <K, V> ValueReference<K, V> referenceValue(ReferenceEntry<K, V> referenceEntry, V v) {
                return new StrongValueReference(v);
            }
        };

        abstract <K, V> ReferenceEntry<K, V> copyEntry(K k, ReferenceEntry<K, V> referenceEntry, ReferenceEntry<K, V> referenceEntry2);

        abstract boolean equal(Object obj, Object obj2);

        abstract int hash(Object obj);

        abstract <K, V> ReferenceEntry<K, V> newEntry(Internals<K, V, ReferenceEntry<K, V>> internals, K k, int i, ReferenceEntry<K, V> referenceEntry);

        abstract <K, V> ValueReference<K, V> referenceValue(ReferenceEntry<K, V> referenceEntry, V v);
    }

    private static class StrongValueReference<K, V> implements ValueReference<K, V> {
        final V referent;

        StrongValueReference(V v) {
            this.referent = v;
        }

        public ValueReference<K, V> copyFor(ReferenceEntry<K, V> referenceEntry) {
            return this;
        }

        public V get() {
            return this.referent;
        }

        public V waitForValue() {
            return get();
        }
    }

    private static class WeakValueReference<K, V> extends FinalizableWeakReference<V> implements ValueReference<K, V> {
        final ReferenceEntry<K, V> entry;

        WeakValueReference(V v, ReferenceEntry<K, V> referenceEntry) {
            super(v, QueueHolder.queue);
            this.entry = referenceEntry;
        }

        public ValueReference<K, V> copyFor(ReferenceEntry<K, V> referenceEntry) {
            return new WeakValueReference(get(), referenceEntry);
        }

        public void finalizeReferent() {
            this.entry.valueReclaimed();
        }

        public V waitForValue() {
            return get();
        }
    }

    private static <K, V> ValueReference<K, V> computing() {
        return COMPUTING;
    }

    private MapMaker setKeyStrength(Strength strength) {
        if (this.keyStrength != Strength.STRONG) {
            throw new IllegalStateException("Key strength was already set to " + this.keyStrength + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
        }
        this.keyStrength = strength;
        this.useCustomMap = true;
        return this;
    }

    private MapMaker setValueStrength(Strength strength) {
        if (this.valueStrength != Strength.STRONG) {
            throw new IllegalStateException("Value strength was already set to " + this.valueStrength + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
        }
        this.valueStrength = strength;
        this.useCustomMap = true;
        return this;
    }

    public MapMaker concurrencyLevel(int i) {
        this.builder.concurrencyLevel(i);
        return this;
    }

    public MapMaker expiration(long j, TimeUnit timeUnit) {
        if (this.expirationNanos != 0) {
            throw new IllegalStateException("expiration time of " + this.expirationNanos + " ns was already set");
        } else if (j <= 0) {
            throw new IllegalArgumentException("invalid duration: " + j);
        } else {
            this.expirationNanos = timeUnit.toNanos(j);
            this.useCustomMap = true;
            return this;
        }
    }

    public MapMaker initialCapacity(int i) {
        this.builder.initialCapacity(i);
        return this;
    }

    public <K, V> ConcurrentMap<K, V> makeComputingMap(Function<? super K, ? extends V> function) {
        return new StrategyImpl(this, function).map;
    }

    public <K, V> ConcurrentMap<K, V> makeMap() {
        return this.useCustomMap ? new StrategyImpl(this).map : new ConcurrentHashMap(this.builder.getInitialCapacity(), 0.75f, this.builder.getConcurrencyLevel());
    }

    public MapMaker softKeys() {
        return setKeyStrength(Strength.SOFT);
    }

    public MapMaker softValues() {
        return setValueStrength(Strength.SOFT);
    }

    public MapMaker weakKeys() {
        return setKeyStrength(Strength.WEAK);
    }

    public MapMaker weakValues() {
        return setValueStrength(Strength.WEAK);
    }
}

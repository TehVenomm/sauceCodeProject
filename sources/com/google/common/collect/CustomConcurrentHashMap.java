package com.google.common.collect;

import com.google.common.base.Function;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.lang.reflect.Field;
import java.util.AbstractCollection;
import java.util.AbstractMap;
import java.util.AbstractSet;
import java.util.Collection;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.NoSuchElementException;
import java.util.Set;
import java.util.concurrent.ConcurrentMap;
import java.util.concurrent.atomic.AtomicReferenceArray;
import java.util.concurrent.locks.ReentrantLock;

final class CustomConcurrentHashMap {

    static final class Builder {
        private static final int DEFAULT_CONCURRENCY_LEVEL = 16;
        private static final int DEFAULT_INITIAL_CAPACITY = 16;
        private static final int UNSET_CONCURRENCY_LEVEL = -1;
        private static final int UNSET_INITIAL_CAPACITY = -1;
        int concurrencyLevel = -1;
        int initialCapacity = -1;

        Builder() {
        }

        public <K, V, E> ConcurrentMap<K, V> buildComputingMap(ComputingStrategy<K, V, E> computingStrategy, Function<? super K, ? extends V> function) {
            if (computingStrategy == null) {
                throw new NullPointerException("strategy");
            } else if (function != null) {
                return new ComputingImpl(computingStrategy, this, function);
            } else {
                throw new NullPointerException("computer");
            }
        }

        public <K, V, E> ConcurrentMap<K, V> buildMap(Strategy<K, V, E> strategy) {
            if (strategy != null) {
                return new Impl(strategy, this);
            }
            throw new NullPointerException("strategy");
        }

        public Builder concurrencyLevel(int i) {
            if (this.concurrencyLevel != -1) {
                throw new IllegalStateException("concurrency level was already set to " + this.concurrencyLevel);
            } else if (i <= 0) {
                throw new IllegalArgumentException();
            } else {
                this.concurrencyLevel = i;
                return this;
            }
        }

        /* access modifiers changed from: 0000 */
        public int getConcurrencyLevel() {
            if (this.concurrencyLevel == -1) {
                return 16;
            }
            return this.concurrencyLevel;
        }

        /* access modifiers changed from: 0000 */
        public int getInitialCapacity() {
            if (this.initialCapacity == -1) {
                return 16;
            }
            return this.initialCapacity;
        }

        public Builder initialCapacity(int i) {
            if (this.initialCapacity != -1) {
                throw new IllegalStateException("initial capacity was already set to " + this.initialCapacity);
            } else if (i < 0) {
                throw new IllegalArgumentException();
            } else {
                this.initialCapacity = i;
                return this;
            }
        }
    }

    static class ComputingImpl<K, V, E> extends Impl<K, V, E> {
        static final long serialVersionUID = 0;
        final Function<? super K, ? extends V> computer;
        final ComputingStrategy<K, V, E> computingStrategy;

        ComputingImpl(ComputingStrategy<K, V, E> computingStrategy2, Builder builder, Function<? super K, ? extends V> function) {
            super(computingStrategy2, builder);
            this.computingStrategy = computingStrategy2;
            this.computer = function;
        }

        public V get(Object obj) {
            V waitForValue;
            boolean z;
            if (obj == null) {
                throw new NullPointerException("key");
            }
            int hash = hash(obj);
            Segment segmentFor = segmentFor(hash);
            while (true) {
                Object entry = segmentFor.getEntry(obj, hash);
                if (entry == null) {
                    segmentFor.lock();
                    try {
                        entry = segmentFor.getEntry(obj, hash);
                        if (entry == null) {
                            int i = segmentFor.count;
                            if (i > segmentFor.threshold) {
                                segmentFor.expand();
                            }
                            AtomicReferenceArray<E> atomicReferenceArray = segmentFor.table;
                            int length = hash & (atomicReferenceArray.length() - 1);
                            Object obj2 = atomicReferenceArray.get(length);
                            segmentFor.modCount++;
                            entry = this.computingStrategy.newEntry(obj, hash, obj2);
                            atomicReferenceArray.set(length, entry);
                            segmentFor.count = i + 1;
                            z = true;
                        } else {
                            z = false;
                        }
                        if (z) {
                            try {
                                Object compute = this.computingStrategy.compute(obj, entry, this.computer);
                                if (compute != null) {
                                    return compute;
                                }
                                throw new NullPointerException("compute() returned null unexpectedly");
                            } catch (Throwable th) {
                                segmentFor.removeEntry(entry, hash);
                                throw th;
                            }
                        }
                    } finally {
                        segmentFor.unlock();
                    }
                }
                Object obj3 = entry;
                boolean z2 = false;
                while (true) {
                    try {
                        waitForValue = this.computingStrategy.waitForValue(obj3);
                        break;
                    } catch (InterruptedException e) {
                        z2 = true;
                    } catch (Throwable th2) {
                        if (z2) {
                            Thread.currentThread().interrupt();
                        }
                        throw th2;
                    }
                }
                if (waitForValue == null) {
                    segmentFor.removeEntry(obj3, hash);
                    if (z2) {
                        Thread.currentThread().interrupt();
                    }
                } else if (!z2) {
                    return waitForValue;
                } else {
                    Thread.currentThread().interrupt();
                    return waitForValue;
                }
            }
        }
    }

    public interface ComputingStrategy<K, V, E> extends Strategy<K, V, E> {
        V compute(K k, E e, Function<? super K, ? extends V> function);

        V waitForValue(E e) throws InterruptedException;
    }

    static class Impl<K, V, E> extends AbstractMap<K, V> implements ConcurrentMap<K, V>, Serializable {
        static final int MAXIMUM_CAPACITY = 1073741824;
        static final int MAX_SEGMENTS = 65536;
        static final int RETRIES_BEFORE_LOCK = 2;
        private static final long serialVersionUID = 1;
        Set<Entry<K, V>> entrySet;
        Set<K> keySet;
        final int segmentMask;
        final int segmentShift;
        final Segment[] segments;
        final Strategy<K, V, E> strategy;
        Collection<V> values;

        final class EntryIterator extends HashIterator implements Iterator<Entry<K, V>> {
            EntryIterator() {
                super();
            }

            public Entry<K, V> next() {
                return nextEntry();
            }
        }

        final class EntrySet extends AbstractSet<Entry<K, V>> {
            EntrySet() {
            }

            public void clear() {
                Impl.this.clear();
            }

            public boolean contains(Object obj) {
                if (!(obj instanceof Entry)) {
                    return false;
                }
                Entry entry = (Entry) obj;
                Object key = entry.getKey();
                if (key == null) {
                    return false;
                }
                Object obj2 = Impl.this.get(key);
                return obj2 != null && Impl.this.strategy.equalValues(obj2, entry.getValue());
            }

            public boolean isEmpty() {
                return Impl.this.isEmpty();
            }

            public Iterator<Entry<K, V>> iterator() {
                return new EntryIterator();
            }

            public boolean remove(Object obj) {
                if (!(obj instanceof Entry)) {
                    return false;
                }
                Entry entry = (Entry) obj;
                Object key = entry.getKey();
                return key != null && Impl.this.remove(key, entry.getValue());
            }

            public int size() {
                return Impl.this.size();
            }
        }

        static class Fields {
            static final Field segmentMask = findField("segmentMask");
            static final Field segmentShift = findField("segmentShift");
            static final Field segments = findField("segments");
            static final Field strategy = findField("strategy");

            Fields() {
            }

            static Field findField(String str) {
                try {
                    Field declaredField = Impl.class.getDeclaredField(str);
                    declaredField.setAccessible(true);
                    return declaredField;
                } catch (NoSuchFieldException e) {
                    throw new AssertionError(e);
                }
            }
        }

        abstract class HashIterator {
            AtomicReferenceArray<E> currentTable;
            WriteThroughEntry lastReturned;
            E nextEntry;
            WriteThroughEntry nextExternal;
            int nextSegmentIndex;
            int nextTableIndex = -1;

            HashIterator() {
                this.nextSegmentIndex = Impl.this.segments.length - 1;
                advance();
            }

            /* access modifiers changed from: 0000 */
            public final void advance() {
                this.nextExternal = null;
                if (!nextInChain() && !nextInTable()) {
                    while (this.nextSegmentIndex >= 0) {
                        Segment[] segmentArr = Impl.this.segments;
                        int i = this.nextSegmentIndex;
                        this.nextSegmentIndex = i - 1;
                        Segment segment = segmentArr[i];
                        if (segment.count != 0) {
                            this.currentTable = segment.table;
                            this.nextTableIndex = this.currentTable.length() - 1;
                            if (nextInTable()) {
                                return;
                            }
                        }
                    }
                }
            }

            /* access modifiers changed from: 0000 */
            public boolean advanceTo(E e) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                Object key = strategy.getKey(e);
                Object value = strategy.getValue(e);
                if (key == null || value == null) {
                    return false;
                }
                this.nextExternal = new WriteThroughEntry<>(key, value);
                return true;
            }

            public boolean hasMoreElements() {
                return hasNext();
            }

            public boolean hasNext() {
                return this.nextExternal != null;
            }

            /* access modifiers changed from: 0000 */
            public WriteThroughEntry nextEntry() {
                if (this.nextExternal == null) {
                    throw new NoSuchElementException();
                }
                this.lastReturned = this.nextExternal;
                advance();
                return this.lastReturned;
            }

            /* access modifiers changed from: 0000 */
            public boolean nextInChain() {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                if (this.nextEntry != null) {
                    this.nextEntry = strategy.getNext(this.nextEntry);
                    while (this.nextEntry != null) {
                        if (advanceTo(this.nextEntry)) {
                            return true;
                        }
                        this.nextEntry = strategy.getNext(this.nextEntry);
                    }
                }
                return false;
            }

            /* access modifiers changed from: 0000 */
            public boolean nextInTable() {
                while (this.nextTableIndex >= 0) {
                    AtomicReferenceArray<E> atomicReferenceArray = this.currentTable;
                    int i = this.nextTableIndex;
                    this.nextTableIndex = i - 1;
                    E e = atomicReferenceArray.get(i);
                    this.nextEntry = e;
                    if (e != null && (advanceTo(this.nextEntry) || nextInChain())) {
                        return true;
                    }
                }
                return false;
            }

            public void remove() {
                if (this.lastReturned == null) {
                    throw new IllegalStateException();
                }
                Impl.this.remove(this.lastReturned.getKey());
                this.lastReturned = null;
            }
        }

        class InternalsImpl implements Internals<K, V, E>, Serializable {
            static final long serialVersionUID = 0;

            InternalsImpl() {
            }

            public E getEntry(K k) {
                if (k == null) {
                    throw new NullPointerException("key");
                }
                int hash = Impl.this.hash(k);
                return Impl.this.segmentFor(hash).getEntry(k, hash);
            }

            public boolean removeEntry(E e) {
                if (e == null) {
                    throw new NullPointerException("entry");
                }
                int hash = Impl.this.strategy.getHash(e);
                return Impl.this.segmentFor(hash).removeEntry(e, hash);
            }

            public boolean removeEntry(E e, V v) {
                if (e == null) {
                    throw new NullPointerException("entry");
                }
                int hash = Impl.this.strategy.getHash(e);
                return Impl.this.segmentFor(hash).removeEntry(e, hash, v);
            }
        }

        final class KeyIterator extends HashIterator implements Iterator<K> {
            KeyIterator() {
                super();
            }

            public K next() {
                return super.nextEntry().getKey();
            }
        }

        final class KeySet extends AbstractSet<K> {
            KeySet() {
            }

            public void clear() {
                Impl.this.clear();
            }

            public boolean contains(Object obj) {
                return Impl.this.containsKey(obj);
            }

            public boolean isEmpty() {
                return Impl.this.isEmpty();
            }

            public Iterator<K> iterator() {
                return new KeyIterator();
            }

            public boolean remove(Object obj) {
                return Impl.this.remove(obj) != null;
            }

            public int size() {
                return Impl.this.size();
            }
        }

        final class Segment extends ReentrantLock {
            volatile int count;
            int modCount;
            volatile AtomicReferenceArray<E> table;
            int threshold;

            Segment(int i) {
                setTable(newEntryArray(i));
            }

            /* access modifiers changed from: 0000 */
            public void clear() {
                if (this.count != 0) {
                    lock();
                    try {
                        AtomicReferenceArray<E> atomicReferenceArray = this.table;
                        for (int i = 0; i < atomicReferenceArray.length(); i++) {
                            atomicReferenceArray.set(i, null);
                        }
                        this.modCount++;
                        this.count = 0;
                    } finally {
                        unlock();
                    }
                }
            }

            /* access modifiers changed from: 0000 */
            public boolean containsKey(Object obj, int i) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                if (this.count == 0) {
                    return false;
                }
                for (Object first = getFirst(i); first != null; first = strategy.getNext(first)) {
                    if (strategy.getHash(first) == i) {
                        Object key = strategy.getKey(first);
                        if (key != null && strategy.equalKeys(key, obj)) {
                            return strategy.getValue(first) != null;
                        }
                    }
                }
                return false;
            }

            /* access modifiers changed from: 0000 */
            public boolean containsValue(Object obj) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                if (this.count == 0) {
                    return false;
                }
                AtomicReferenceArray<E> atomicReferenceArray = this.table;
                int length = atomicReferenceArray.length();
                for (int i = 0; i < length; i++) {
                    for (Object obj2 = atomicReferenceArray.get(i); obj2 != null; obj2 = strategy.getNext(obj2)) {
                        Object value = strategy.getValue(obj2);
                        if (value != null && strategy.equalValues(value, obj)) {
                            return true;
                        }
                    }
                }
                return false;
            }

            /* access modifiers changed from: 0000 */
            public void expand() {
                AtomicReferenceArray<E> atomicReferenceArray = this.table;
                int length = atomicReferenceArray.length();
                if (length < Impl.MAXIMUM_CAPACITY) {
                    Strategy<K, V, E> strategy = Impl.this.strategy;
                    AtomicReferenceArray<E> newEntryArray = newEntryArray(length << 1);
                    this.threshold = (newEntryArray.length() * 3) / 4;
                    int length2 = newEntryArray.length() - 1;
                    for (int i = 0; i < length; i++) {
                        Object obj = atomicReferenceArray.get(i);
                        if (obj != null) {
                            Object next = strategy.getNext(obj);
                            int hash = strategy.getHash(obj) & length2;
                            if (next == null) {
                                newEntryArray.set(hash, obj);
                            } else {
                                Object obj2 = obj;
                                while (next != null) {
                                    int hash2 = strategy.getHash(next) & length2;
                                    if (hash2 != hash) {
                                        obj2 = next;
                                    } else {
                                        hash2 = hash;
                                    }
                                    next = strategy.getNext(next);
                                    hash = hash2;
                                }
                                newEntryArray.set(hash, obj2);
                                for (Object obj3 = obj; obj3 != obj2; obj3 = strategy.getNext(obj3)) {
                                    Object key = strategy.getKey(obj3);
                                    if (key != null) {
                                        int hash3 = strategy.getHash(obj3) & length2;
                                        newEntryArray.set(hash3, strategy.copyEntry(key, obj3, newEntryArray.get(hash3)));
                                    }
                                }
                            }
                        }
                    }
                    this.table = newEntryArray;
                }
            }

            /* access modifiers changed from: 0000 */
            public V get(Object obj, int i) {
                Object entry = getEntry(obj, i);
                if (entry == null) {
                    return null;
                }
                return Impl.this.strategy.getValue(entry);
            }

            public E getEntry(Object obj, int i) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                if (this.count != 0) {
                    for (E first = getFirst(i); first != null; first = strategy.getNext(first)) {
                        if (strategy.getHash(first) == i) {
                            Object key = strategy.getKey(first);
                            if (key != null && strategy.equalKeys(key, obj)) {
                                return first;
                            }
                        }
                    }
                }
                return null;
            }

            /* access modifiers changed from: 0000 */
            public E getFirst(int i) {
                AtomicReferenceArray<E> atomicReferenceArray = this.table;
                return atomicReferenceArray.get((atomicReferenceArray.length() - 1) & i);
            }

            /* access modifiers changed from: 0000 */
            public AtomicReferenceArray<E> newEntryArray(int i) {
                return new AtomicReferenceArray<>(i);
            }

            /* access modifiers changed from: 0000 */
            public V put(K k, int i, V v, boolean z) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                lock();
                int i2 = this.count;
                if (i2 > this.threshold) {
                    expand();
                }
                AtomicReferenceArray<E> atomicReferenceArray = this.table;
                int length = i & (atomicReferenceArray.length() - 1);
                Object obj = atomicReferenceArray.get(length);
                Object obj2 = obj;
                while (obj2 != null) {
                    Object key = strategy.getKey(obj2);
                    if (strategy.getHash(obj2) != i || key == null || !strategy.equalKeys(k, key)) {
                        try {
                            obj2 = strategy.getNext(obj2);
                        } catch (Throwable th) {
                            unlock();
                            throw th;
                        }
                    } else {
                        V value = strategy.getValue(obj2);
                        if (!z || value == null) {
                            strategy.setValue(obj2, v);
                            unlock();
                            return value;
                        }
                        unlock();
                        return value;
                    }
                }
                this.modCount++;
                Object newEntry = strategy.newEntry(k, i, obj);
                strategy.setValue(newEntry, v);
                atomicReferenceArray.set(length, newEntry);
                this.count = i2 + 1;
                unlock();
                return null;
            }

            /* access modifiers changed from: 0000 */
            public V remove(Object obj, int i) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                lock();
                try {
                    int i2 = this.count;
                    AtomicReferenceArray<E> atomicReferenceArray = this.table;
                    int length = i & (atomicReferenceArray.length() - 1);
                    Object obj2 = atomicReferenceArray.get(length);
                    Object obj3 = obj2;
                    while (obj3 != null) {
                        Object key = strategy.getKey(obj3);
                        if (strategy.getHash(obj3) != i || key == null || !strategy.equalKeys(key, obj)) {
                            obj3 = strategy.getNext(obj3);
                        } else {
                            V value = Impl.this.strategy.getValue(obj3);
                            this.modCount++;
                            Object next = strategy.getNext(obj3);
                            while (obj2 != obj3) {
                                Object key2 = strategy.getKey(obj2);
                                if (key2 != null) {
                                    next = strategy.copyEntry(key2, obj2, next);
                                }
                                obj2 = strategy.getNext(obj2);
                            }
                            atomicReferenceArray.set(length, next);
                            this.count = i2 - 1;
                            return value;
                        }
                    }
                    unlock();
                    return null;
                } finally {
                    unlock();
                }
            }

            /* JADX INFO: finally extract failed */
            /* access modifiers changed from: 0000 */
            public boolean remove(Object obj, int i, Object obj2) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                lock();
                try {
                    int i2 = this.count;
                    AtomicReferenceArray<E> atomicReferenceArray = this.table;
                    int length = i & (atomicReferenceArray.length() - 1);
                    Object obj3 = atomicReferenceArray.get(length);
                    Object obj4 = obj3;
                    while (obj4 != null) {
                        Object key = strategy.getKey(obj4);
                        if (strategy.getHash(obj4) != i || key == null || !strategy.equalKeys(key, obj)) {
                            obj4 = strategy.getNext(obj4);
                        } else {
                            Object value = Impl.this.strategy.getValue(obj4);
                            if (obj2 == value || !(obj2 == null || value == null || !strategy.equalValues(value, obj2))) {
                                this.modCount++;
                                Object next = strategy.getNext(obj4);
                                while (obj3 != obj4) {
                                    Object key2 = strategy.getKey(obj3);
                                    if (key2 != null) {
                                        next = strategy.copyEntry(key2, obj3, next);
                                    }
                                    obj3 = strategy.getNext(obj3);
                                }
                                atomicReferenceArray.set(length, next);
                                this.count = i2 - 1;
                                unlock();
                                return true;
                            }
                            unlock();
                            return false;
                        }
                    }
                    unlock();
                    return false;
                } catch (Throwable th) {
                    unlock();
                    throw th;
                }
            }

            /* JADX INFO: finally extract failed */
            public boolean removeEntry(E e, int i) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                lock();
                try {
                    int i2 = this.count;
                    AtomicReferenceArray<E> atomicReferenceArray = this.table;
                    int length = i & (atomicReferenceArray.length() - 1);
                    Object obj = atomicReferenceArray.get(length);
                    Object obj2 = obj;
                    while (obj2 != null) {
                        if (strategy.getHash(obj2) != i || !e.equals(obj2)) {
                            obj2 = strategy.getNext(obj2);
                        } else {
                            this.modCount++;
                            Object next = strategy.getNext(obj2);
                            while (obj != obj2) {
                                Object key = strategy.getKey(obj);
                                if (key != null) {
                                    next = strategy.copyEntry(key, obj, next);
                                }
                                obj = strategy.getNext(obj);
                            }
                            atomicReferenceArray.set(length, next);
                            this.count = i2 - 1;
                            unlock();
                            return true;
                        }
                    }
                    unlock();
                    return false;
                } catch (Throwable th) {
                    unlock();
                    throw th;
                }
            }

            /* JADX INFO: finally extract failed */
            public boolean removeEntry(E e, int i, V v) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                lock();
                try {
                    int i2 = this.count;
                    AtomicReferenceArray<E> atomicReferenceArray = this.table;
                    int length = i & (atomicReferenceArray.length() - 1);
                    Object obj = atomicReferenceArray.get(length);
                    Object obj2 = obj;
                    while (obj2 != null) {
                        if (strategy.getHash(obj2) != i || !e.equals(obj2)) {
                            obj2 = strategy.getNext(obj2);
                        } else {
                            V value = strategy.getValue(obj2);
                            if (value == v || (v != null && strategy.equalValues(value, v))) {
                                this.modCount++;
                                Object next = strategy.getNext(obj2);
                                while (obj != obj2) {
                                    Object key = strategy.getKey(obj);
                                    if (key != null) {
                                        next = strategy.copyEntry(key, obj, next);
                                    }
                                    obj = strategy.getNext(obj);
                                }
                                atomicReferenceArray.set(length, next);
                                this.count = i2 - 1;
                                unlock();
                                return true;
                            }
                            unlock();
                            return false;
                        }
                    }
                    unlock();
                    return false;
                } catch (Throwable th) {
                    unlock();
                    throw th;
                }
            }

            /* access modifiers changed from: 0000 */
            public V replace(K k, int i, V v) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                lock();
                try {
                    Object first = getFirst(i);
                    while (first != null) {
                        Object key = strategy.getKey(first);
                        if (strategy.getHash(first) != i || key == null || !strategy.equalKeys(k, key)) {
                            first = strategy.getNext(first);
                        } else {
                            Object value = strategy.getValue(first);
                            if (value == null) {
                                return null;
                            }
                            strategy.setValue(first, v);
                            unlock();
                            return value;
                        }
                    }
                    unlock();
                    return null;
                } finally {
                    unlock();
                }
            }

            /* JADX INFO: finally extract failed */
            /* access modifiers changed from: 0000 */
            public boolean replace(K k, int i, V v, V v2) {
                Strategy<K, V, E> strategy = Impl.this.strategy;
                lock();
                try {
                    for (Object first = getFirst(i); first != null; first = strategy.getNext(first)) {
                        Object key = strategy.getKey(first);
                        if (strategy.getHash(first) == i && key != null && strategy.equalKeys(k, key)) {
                            Object value = strategy.getValue(first);
                            if (value == null) {
                                unlock();
                                return false;
                            } else if (strategy.equalValues(value, v)) {
                                strategy.setValue(first, v2);
                                unlock();
                                return true;
                            }
                        }
                    }
                    unlock();
                    return false;
                } catch (Throwable th) {
                    unlock();
                    throw th;
                }
            }

            /* access modifiers changed from: 0000 */
            public void setTable(AtomicReferenceArray<E> atomicReferenceArray) {
                this.threshold = (atomicReferenceArray.length() * 3) / 4;
                this.table = atomicReferenceArray;
            }
        }

        final class ValueIterator extends HashIterator implements Iterator<V> {
            ValueIterator() {
                super();
            }

            public V next() {
                return super.nextEntry().getValue();
            }
        }

        final class Values extends AbstractCollection<V> {
            Values() {
            }

            public void clear() {
                Impl.this.clear();
            }

            public boolean contains(Object obj) {
                return Impl.this.containsValue(obj);
            }

            public boolean isEmpty() {
                return Impl.this.isEmpty();
            }

            public Iterator<V> iterator() {
                return new ValueIterator();
            }

            public int size() {
                return Impl.this.size();
            }
        }

        final class WriteThroughEntry extends AbstractMapEntry<K, V> {
            final K key;
            V value;

            WriteThroughEntry(K k, V v) {
                this.key = k;
                this.value = v;
            }

            public K getKey() {
                return this.key;
            }

            public V getValue() {
                return this.value;
            }

            public V setValue(V v) {
                if (v == null) {
                    throw new NullPointerException();
                }
                V put = Impl.this.put(getKey(), v);
                this.value = v;
                return put;
            }
        }

        Impl(Strategy<K, V, E> strategy2, Builder builder) {
            int i = 65536;
            int concurrencyLevel = builder.getConcurrencyLevel();
            int initialCapacity = builder.getInitialCapacity();
            if (concurrencyLevel <= 65536) {
                i = concurrencyLevel;
            }
            int i2 = 0;
            int i3 = 1;
            while (i3 < i) {
                i2++;
                i3 <<= 1;
            }
            this.segmentShift = 32 - i2;
            this.segmentMask = i3 - 1;
            this.segments = newSegmentArray(i3);
            int i4 = initialCapacity > MAXIMUM_CAPACITY ? MAXIMUM_CAPACITY : initialCapacity;
            int i5 = i4 / i3;
            int i6 = i5 * i3 < i4 ? i5 + 1 : i5;
            int i7 = 1;
            while (i7 < i6) {
                i7 <<= 1;
            }
            for (int i8 = 0; i8 < this.segments.length; i8++) {
                this.segments[i8] = new Segment<>(i7);
            }
            this.strategy = strategy2;
            strategy2.setInternals(new InternalsImpl());
        }

        private void readObject(ObjectInputStream objectInputStream) throws IOException, ClassNotFoundException {
            int i = 65536;
            try {
                int readInt = objectInputStream.readInt();
                int readInt2 = objectInputStream.readInt();
                Strategy strategy2 = (Strategy) objectInputStream.readObject();
                if (readInt2 <= 65536) {
                    i = readInt2;
                }
                int i2 = 0;
                int i3 = 1;
                while (i3 < i) {
                    i2++;
                    i3 <<= 1;
                }
                Fields.segmentShift.set(this, Integer.valueOf(32 - i2));
                Fields.segmentMask.set(this, Integer.valueOf(i3 - 1));
                Fields.segments.set(this, newSegmentArray(i3));
                int i4 = readInt > MAXIMUM_CAPACITY ? MAXIMUM_CAPACITY : readInt;
                int i5 = i4 / i3;
                if (i5 * i3 < i4) {
                    i5++;
                }
                int i6 = 1;
                while (i6 < i5) {
                    i6 <<= 1;
                }
                for (int i7 = 0; i7 < this.segments.length; i7++) {
                    this.segments[i7] = new Segment<>(i6);
                }
                Fields.strategy.set(this, strategy2);
                while (true) {
                    Object readObject = objectInputStream.readObject();
                    if (readObject != null) {
                        put(readObject, objectInputStream.readObject());
                    } else {
                        return;
                    }
                }
            } catch (IllegalAccessException e) {
                throw new AssertionError(e);
            }
        }

        private void writeObject(ObjectOutputStream objectOutputStream) throws IOException {
            objectOutputStream.writeInt(size());
            objectOutputStream.writeInt(this.segments.length);
            objectOutputStream.writeObject(this.strategy);
            for (Entry entry : entrySet()) {
                objectOutputStream.writeObject(entry.getKey());
                objectOutputStream.writeObject(entry.getValue());
            }
            objectOutputStream.writeObject(null);
        }

        public void clear() {
            for (Segment clear : this.segments) {
                clear.clear();
            }
        }

        public boolean containsKey(Object obj) {
            if (obj == null) {
                throw new NullPointerException("key");
            }
            int hash = hash(obj);
            return segmentFor(hash).containsKey(obj, hash);
        }

        public boolean containsValue(Object obj) {
            boolean z;
            int i;
            boolean z2 = 1;
            int i2 = 0;
            if (obj == null) {
                throw new NullPointerException("value");
            }
            Segment[] segmentArr = this.segments;
            int[] iArr = new int[segmentArr.length];
            for (int i3 = i2; i3 < 2; i3++) {
                int i4 = i2;
                for (int i5 = i2; i5 < segmentArr.length; i5++) {
                    int i6 = segmentArr[i5].count;
                    int i7 = segmentArr[i5].modCount;
                    iArr[i5] = i7;
                    i4 += i7;
                    if (segmentArr[i5].containsValue(obj)) {
                        return true;
                    }
                }
                if (i4 != 0) {
                    int i8 = i2;
                    while (true) {
                        if (i8 >= segmentArr.length) {
                            i = 1;
                            break;
                        }
                        int i9 = segmentArr[i8].count;
                        if (iArr[i8] != segmentArr[i8].modCount) {
                            i = i2;
                            break;
                        }
                        i8++;
                    }
                } else {
                    i = 1;
                }
                if (i != 0) {
                    return i2;
                }
            }
            int length = segmentArr.length;
            for (int i10 = i2; i10 < length; i10++) {
                segmentArr[i10].lock();
            }
            try {
                int length2 = segmentArr.length;
                int i11 = i2;
                while (true) {
                    if (i11 < length2) {
                        if (segmentArr[i11].containsValue(obj)) {
                            z = z2;
                            break;
                        }
                        i11++;
                    } else {
                        z = i2;
                        break;
                    }
                }
                return z;
            } finally {
                int length3 = segmentArr.length;
                while (i2 < length3) {
                    segmentArr[i2].unlock();
                    i2++;
                }
            }
        }

        public Set<Entry<K, V>> entrySet() {
            Set<Entry<K, V>> set = this.entrySet;
            if (set != null) {
                return set;
            }
            EntrySet entrySet2 = new EntrySet();
            this.entrySet = entrySet2;
            return entrySet2;
        }

        public V get(Object obj) {
            if (obj == null) {
                throw new NullPointerException("key");
            }
            int hash = hash(obj);
            return segmentFor(hash).get(obj, hash);
        }

        /* access modifiers changed from: 0000 */
        public int hash(Object obj) {
            return CustomConcurrentHashMap.rehash(this.strategy.hashKey(obj));
        }

        public boolean isEmpty() {
            Segment[] segmentArr = this.segments;
            int[] iArr = new int[segmentArr.length];
            int i = 0;
            for (int i2 = 0; i2 < segmentArr.length; i2++) {
                if (segmentArr[i2].count != 0) {
                    return false;
                }
                int i3 = segmentArr[i2].modCount;
                iArr[i2] = i3;
                i += i3;
            }
            if (i != 0) {
                for (int i4 = 0; i4 < segmentArr.length; i4++) {
                    if (segmentArr[i4].count != 0 || iArr[i4] != segmentArr[i4].modCount) {
                        return false;
                    }
                }
            }
            return true;
        }

        public Set<K> keySet() {
            Set<K> set = this.keySet;
            if (set != null) {
                return set;
            }
            KeySet keySet2 = new KeySet();
            this.keySet = keySet2;
            return keySet2;
        }

        /* access modifiers changed from: 0000 */
        public Segment[] newSegmentArray(int i) {
            throw new RuntimeException("d2j fail translate: java.lang.RuntimeException: can not merge L and I\n\tat com.googlecode.dex2jar.ir.TypeClass.merge(TypeClass.java:100)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeRef.updateTypeClass(TypeTransformer.java:243)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeAnalyze.copyTypes(TypeTransformer.java:478)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeAnalyze.fixTypes(TypeTransformer.java:335)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeAnalyze.analyze(TypeTransformer.java:305)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer.transform(TypeTransformer.java:44)\n\tat com.googlecode.d2j.dex.Dex2jar$2.optimize(Dex2jar.java:162)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertCode(Dex2Asm.java:434)\n\tat com.googlecode.d2j.dex.ExDex2Asm.convertCode(ExDex2Asm.java:42)\n\tat com.googlecode.d2j.dex.Dex2jar$2.convertCode(Dex2jar.java:129)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertMethod(Dex2Asm.java:529)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertClass(Dex2Asm.java:426)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertDex(Dex2Asm.java:442)\n\tat com.googlecode.d2j.dex.Dex2jar.doTranslate(Dex2jar.java:172)\n\tat com.googlecode.d2j.dex.Dex2jar.to(Dex2jar.java:272)\n\tat com.googlecode.dex2jar.tools.Dex2jarCmd.doCommandLine(Dex2jarCmd.java:109)\n\tat com.googlecode.dex2jar.tools.BaseCmd.doMain(BaseCmd.java:290)\n\tat com.googlecode.dex2jar.tools.Dex2jarCmd.main(Dex2jarCmd.java:33)\n");
        }

        public V put(K k, V v) {
            if (k == null) {
                throw new NullPointerException("key");
            } else if (v == null) {
                throw new NullPointerException("value");
            } else {
                int hash = hash(k);
                return segmentFor(hash).put(k, hash, v, false);
            }
        }

        public void putAll(Map<? extends K, ? extends V> map) {
            for (Entry entry : map.entrySet()) {
                put(entry.getKey(), entry.getValue());
            }
        }

        public V putIfAbsent(K k, V v) {
            if (k == null) {
                throw new NullPointerException("key");
            } else if (v == null) {
                throw new NullPointerException("value");
            } else {
                int hash = hash(k);
                return segmentFor(hash).put(k, hash, v, true);
            }
        }

        public V remove(Object obj) {
            if (obj == null) {
                throw new NullPointerException("key");
            }
            int hash = hash(obj);
            return segmentFor(hash).remove(obj, hash);
        }

        public boolean remove(Object obj, Object obj2) {
            if (obj == null) {
                throw new NullPointerException("key");
            }
            int hash = hash(obj);
            return segmentFor(hash).remove(obj, hash, obj2);
        }

        public V replace(K k, V v) {
            if (k == null) {
                throw new NullPointerException("key");
            } else if (v == null) {
                throw new NullPointerException("value");
            } else {
                int hash = hash(k);
                return segmentFor(hash).replace(k, hash, v);
            }
        }

        public boolean replace(K k, V v, V v2) {
            if (k == null) {
                throw new NullPointerException("key");
            } else if (v == null) {
                throw new NullPointerException("oldValue");
            } else if (v2 == null) {
                throw new NullPointerException("newValue");
            } else {
                int hash = hash(k);
                return segmentFor(hash).replace(k, hash, v, v2);
            }
        }

        /* access modifiers changed from: 0000 */
        public Segment segmentFor(int i) {
            return this.segments[(i >>> this.segmentShift) & this.segmentMask];
        }

        public int size() {
            long j;
            Segment[] segmentArr = this.segments;
            int[] iArr = new int[segmentArr.length];
            long j2 = 0;
            long j3 = 0;
            for (int i = 0; i < 2; i++) {
                j2 = 0;
                int i2 = 0;
                for (int i3 = 0; i3 < segmentArr.length; i3++) {
                    j2 += (long) segmentArr[i3].count;
                    int i4 = segmentArr[i3].modCount;
                    iArr[i3] = i4;
                    i2 += i4;
                }
                if (i2 != 0) {
                    j3 = 0;
                    int i5 = 0;
                    while (true) {
                        if (i5 >= segmentArr.length) {
                            break;
                        }
                        j3 += (long) segmentArr[i5].count;
                        if (iArr[i5] != segmentArr[i5].modCount) {
                            j3 = -1;
                            break;
                        }
                        i5++;
                    }
                } else {
                    j3 = 0;
                }
                if (j3 == j2) {
                    break;
                }
            }
            if (j3 != j) {
                for (Segment lock : segmentArr) {
                    lock.lock();
                }
                j = 0;
                for (Segment segment : segmentArr) {
                    j += (long) segment.count;
                }
                for (Segment unlock : segmentArr) {
                    unlock.unlock();
                }
            }
            if (j > 2147483647L) {
                return Integer.MAX_VALUE;
            }
            return (int) j;
        }

        public Collection<V> values() {
            Collection<V> collection = this.values;
            if (collection != null) {
                return collection;
            }
            Values values2 = new Values();
            this.values = values2;
            return values2;
        }
    }

    public interface Internals<K, V, E> {
        E getEntry(K k);

        boolean removeEntry(E e);

        boolean removeEntry(E e, V v);
    }

    static class SimpleInternalEntry<K, V> {
        final int hash;
        final K key;
        final SimpleInternalEntry<K, V> next;
        volatile V value;

        SimpleInternalEntry(K k, int i, V v, SimpleInternalEntry<K, V> simpleInternalEntry) {
            this.key = k;
            this.hash = i;
            this.value = v;
            this.next = simpleInternalEntry;
        }
    }

    static class SimpleStrategy<K, V> implements Strategy<K, V, SimpleInternalEntry<K, V>> {
        SimpleStrategy() {
        }

        public SimpleInternalEntry<K, V> copyEntry(K k, SimpleInternalEntry<K, V> simpleInternalEntry, SimpleInternalEntry<K, V> simpleInternalEntry2) {
            return new SimpleInternalEntry<>(k, simpleInternalEntry.hash, simpleInternalEntry.value, simpleInternalEntry2);
        }

        public boolean equalKeys(K k, Object obj) {
            return k.equals(obj);
        }

        public boolean equalValues(V v, Object obj) {
            return v.equals(obj);
        }

        public int getHash(SimpleInternalEntry<K, V> simpleInternalEntry) {
            return simpleInternalEntry.hash;
        }

        public K getKey(SimpleInternalEntry<K, V> simpleInternalEntry) {
            return simpleInternalEntry.key;
        }

        public SimpleInternalEntry<K, V> getNext(SimpleInternalEntry<K, V> simpleInternalEntry) {
            return simpleInternalEntry.next;
        }

        public V getValue(SimpleInternalEntry<K, V> simpleInternalEntry) {
            return simpleInternalEntry.value;
        }

        public int hashKey(Object obj) {
            return obj.hashCode();
        }

        public SimpleInternalEntry<K, V> newEntry(K k, int i, SimpleInternalEntry<K, V> simpleInternalEntry) {
            return new SimpleInternalEntry<>(k, i, null, simpleInternalEntry);
        }

        public void setInternals(Internals<K, V, SimpleInternalEntry<K, V>> internals) {
        }

        public void setValue(SimpleInternalEntry<K, V> simpleInternalEntry, V v) {
            simpleInternalEntry.value = v;
        }
    }

    public interface Strategy<K, V, E> {
        E copyEntry(K k, E e, E e2);

        boolean equalKeys(K k, Object obj);

        boolean equalValues(V v, Object obj);

        int getHash(E e);

        K getKey(E e);

        E getNext(E e);

        V getValue(E e);

        int hashKey(Object obj);

        E newEntry(K k, int i, E e);

        void setInternals(Internals<K, V, E> internals);

        void setValue(E e, V v);
    }

    private CustomConcurrentHashMap() {
    }

    /* access modifiers changed from: private */
    public static int rehash(int i) {
        int i2 = ((i << 15) ^ -12931) + i;
        int i3 = i2 ^ (i2 >>> 10);
        int i4 = i3 + (i3 << 3);
        int i5 = i4 ^ (i4 >>> 6);
        int i6 = i5 + (i5 << 2) + (i5 << 14);
        return i6 ^ (i6 >>> 16);
    }
}

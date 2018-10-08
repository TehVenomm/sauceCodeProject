package com.google.common.collect;

import com.google.common.base.Function;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
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

        int getConcurrencyLevel() {
            return this.concurrencyLevel == -1 ? 16 : this.concurrencyLevel;
        }

        int getInitialCapacity() {
            return this.initialCapacity == -1 ? 16 : this.initialCapacity;
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

            final void advance() {
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

            boolean advanceTo(E e) {
                Strategy strategy = Impl.this.strategy;
                Object key = strategy.getKey(e);
                Object value = strategy.getValue(e);
                if (key == null || value == null) {
                    return false;
                }
                this.nextExternal = new WriteThroughEntry(key, value);
                return true;
            }

            public boolean hasMoreElements() {
                return hasNext();
            }

            public boolean hasNext() {
                return this.nextExternal != null;
            }

            WriteThroughEntry nextEntry() {
                if (this.nextExternal == null) {
                    throw new NoSuchElementException();
                }
                this.lastReturned = this.nextExternal;
                advance();
                return this.lastReturned;
            }

            boolean nextInChain() {
                Strategy strategy = Impl.this.strategy;
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

            boolean nextInTable() {
                while (this.nextTableIndex >= 0) {
                    AtomicReferenceArray atomicReferenceArray = this.currentTable;
                    int i = this.nextTableIndex;
                    this.nextTableIndex = i - 1;
                    Object obj = atomicReferenceArray.get(i);
                    this.nextEntry = obj;
                    if (obj != null && (advanceTo(this.nextEntry) || nextInChain())) {
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
                key = Impl.this.get(key);
                return key != null && Impl.this.strategy.equalValues(key, entry.getValue());
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

            void clear() {
                if (this.count != 0) {
                    lock();
                    try {
                        AtomicReferenceArray atomicReferenceArray = this.table;
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

            boolean containsKey(Object obj, int i) {
                Strategy strategy = Impl.this.strategy;
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

            boolean containsValue(Object obj) {
                Strategy strategy = Impl.this.strategy;
                if (this.count == 0) {
                    return false;
                }
                AtomicReferenceArray atomicReferenceArray = this.table;
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

            void expand() {
                AtomicReferenceArray atomicReferenceArray = this.table;
                int length = atomicReferenceArray.length();
                if (length < Impl.MAXIMUM_CAPACITY) {
                    Strategy strategy = Impl.this.strategy;
                    AtomicReferenceArray newEntryArray = newEntryArray(length << 1);
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
                                for (next = obj; next != obj2; next = strategy.getNext(next)) {
                                    Object key = strategy.getKey(next);
                                    if (key != null) {
                                        hash = strategy.getHash(next) & length2;
                                        newEntryArray.set(hash, strategy.copyEntry(key, next, newEntryArray.get(hash)));
                                    }
                                }
                            }
                        }
                    }
                    this.table = newEntryArray;
                }
            }

            V get(Object obj, int i) {
                Object entry = getEntry(obj, i);
                return entry == null ? null : Impl.this.strategy.getValue(entry);
            }

            public E getEntry(Object obj, int i) {
                Strategy strategy = Impl.this.strategy;
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

            E getFirst(int i) {
                AtomicReferenceArray atomicReferenceArray = this.table;
                return atomicReferenceArray.get((atomicReferenceArray.length() - 1) & i);
            }

            AtomicReferenceArray<E> newEntryArray(int i) {
                return new AtomicReferenceArray(i);
            }

            V put(K k, int i, V v, boolean z) {
                Strategy strategy = Impl.this.strategy;
                lock();
                int i2 = this.count;
                if (i2 > this.threshold) {
                    expand();
                }
                AtomicReferenceArray atomicReferenceArray = this.table;
                int length = i & (atomicReferenceArray.length() - 1);
                Object obj = atomicReferenceArray.get(length);
                Object obj2 = obj;
                while (obj2 != null) {
                    try {
                        Object key = strategy.getKey(obj2);
                        if (strategy.getHash(obj2) == i && key != null && strategy.equalKeys(k, key)) {
                            V value = strategy.getValue(obj2);
                            if (z && value != null) {
                                return value;
                            }
                            strategy.setValue(obj2, v);
                            unlock();
                            return value;
                        }
                        obj2 = strategy.getNext(obj2);
                    } finally {
                        unlock();
                    }
                }
                this.modCount++;
                obj = strategy.newEntry(k, i, obj);
                strategy.setValue(obj, v);
                atomicReferenceArray.set(length, obj);
                this.count = i2 + 1;
                unlock();
                return null;
            }

            V remove(Object obj, int i) {
                Strategy strategy = Impl.this.strategy;
                lock();
                int i2 = this.count;
                AtomicReferenceArray atomicReferenceArray = this.table;
                int length = i & (atomicReferenceArray.length() - 1);
                Object obj2 = atomicReferenceArray.get(length);
                Object obj3 = obj2;
                while (obj3 != null) {
                    try {
                        Object key = strategy.getKey(obj3);
                        if (strategy.getHash(obj3) == i && key != null && strategy.equalKeys(key, obj)) {
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
                        obj3 = strategy.getNext(obj3);
                    } finally {
                        unlock();
                    }
                }
                unlock();
                return null;
            }

            boolean remove(Object obj, int i, Object obj2) {
                Strategy strategy = Impl.this.strategy;
                lock();
                int i2 = this.count;
                AtomicReferenceArray atomicReferenceArray = this.table;
                int length = i & (atomicReferenceArray.length() - 1);
                Object obj3 = atomicReferenceArray.get(length);
                Object obj4 = obj3;
                while (obj4 != null) {
                    Object key = strategy.getKey(obj4);
                    if (strategy.getHash(obj4) == i && key != null && strategy.equalKeys(key, obj)) {
                        key = Impl.this.strategy.getValue(obj4);
                        if (obj2 == key || !(obj2 == null || key == null || !strategy.equalValues(key, obj2))) {
                            this.modCount++;
                            Object next = strategy.getNext(obj4);
                            while (obj3 != obj4) {
                                key = strategy.getKey(obj3);
                                if (key != null) {
                                    next = strategy.copyEntry(key, obj3, next);
                                }
                                obj3 = strategy.getNext(obj3);
                            }
                            atomicReferenceArray.set(length, next);
                            this.count = i2 - 1;
                            return true;
                        }
                        unlock();
                        return false;
                    }
                    try {
                        obj4 = strategy.getNext(obj4);
                    } finally {
                        unlock();
                    }
                }
                unlock();
                return false;
            }

            public boolean removeEntry(E e, int i) {
                Strategy strategy = Impl.this.strategy;
                lock();
                int i2 = this.count;
                AtomicReferenceArray atomicReferenceArray = this.table;
                int length = i & (atomicReferenceArray.length() - 1);
                Object obj = atomicReferenceArray.get(length);
                Object obj2 = obj;
                while (obj2 != null) {
                    try {
                        if (strategy.getHash(obj2) == i && e.equals(obj2)) {
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
                            return true;
                        }
                        obj2 = strategy.getNext(obj2);
                    } finally {
                        unlock();
                    }
                }
                unlock();
                return false;
            }

            public boolean removeEntry(E e, int i, V v) {
                Strategy strategy = Impl.this.strategy;
                lock();
                int i2 = this.count;
                AtomicReferenceArray atomicReferenceArray = this.table;
                int length = i & (atomicReferenceArray.length() - 1);
                Object obj = atomicReferenceArray.get(length);
                Object obj2 = obj;
                while (obj2 != null) {
                    if (strategy.getHash(obj2) == i && e.equals(obj2)) {
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
                            return true;
                        }
                        unlock();
                        return false;
                    }
                    try {
                        obj2 = strategy.getNext(obj2);
                    } finally {
                        unlock();
                    }
                }
                unlock();
                return false;
            }

            V replace(K k, int i, V v) {
                Strategy strategy = Impl.this.strategy;
                lock();
                Object first = getFirst(i);
                while (first != null) {
                    try {
                        Object key = strategy.getKey(first);
                        if (strategy.getHash(first) == i && key != null && strategy.equalKeys(k, key)) {
                            V value = strategy.getValue(first);
                            if (value == null) {
                                return null;
                            }
                            strategy.setValue(first, v);
                            unlock();
                            return value;
                        }
                        first = strategy.getNext(first);
                    } finally {
                        unlock();
                    }
                }
                unlock();
                return null;
            }

            boolean replace(K k, int i, V v, V v2) {
                Strategy strategy = Impl.this.strategy;
                lock();
                Object first = getFirst(i);
                while (first != null) {
                    try {
                        Object key = strategy.getKey(first);
                        if (strategy.getHash(first) == i && key != null && strategy.equalKeys(k, key)) {
                            key = strategy.getValue(first);
                            if (key == null) {
                                unlock();
                                return false;
                            } else if (strategy.equalValues(key, v)) {
                                strategy.setValue(first, v2);
                                return true;
                            }
                        }
                        first = strategy.getNext(first);
                    } finally {
                        unlock();
                    }
                }
                unlock();
                return false;
            }

            void setTable(AtomicReferenceArray<E> atomicReferenceArray) {
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

        Impl(Strategy<K, V, E> strategy, Builder builder) {
            int i = 65536;
            int concurrencyLevel = builder.getConcurrencyLevel();
            int initialCapacity = builder.getInitialCapacity();
            if (concurrencyLevel <= 65536) {
                i = concurrencyLevel;
            }
            concurrencyLevel = 0;
            int i2 = 1;
            while (i2 < i) {
                concurrencyLevel++;
                i2 <<= 1;
            }
            this.segmentShift = 32 - concurrencyLevel;
            this.segmentMask = i2 - 1;
            this.segments = newSegmentArray(i2);
            i = initialCapacity > MAXIMUM_CAPACITY ? MAXIMUM_CAPACITY : initialCapacity;
            concurrencyLevel = i / i2;
            i = concurrencyLevel * i2 < i ? concurrencyLevel + 1 : concurrencyLevel;
            concurrencyLevel = 1;
            while (concurrencyLevel < i) {
                concurrencyLevel <<= 1;
            }
            for (i = 0; i < this.segments.length; i++) {
                this.segments[i] = new Segment(concurrencyLevel);
            }
            this.strategy = strategy;
            strategy.setInternals(new InternalsImpl());
        }

        private void readObject(ObjectInputStream objectInputStream) throws IOException, ClassNotFoundException {
            int i = 65536;
            try {
                int readInt = objectInputStream.readInt();
                int readInt2 = objectInputStream.readInt();
                Strategy strategy = (Strategy) objectInputStream.readObject();
                if (readInt2 <= 65536) {
                    i = readInt2;
                }
                readInt2 = 0;
                int i2 = 1;
                while (i2 < i) {
                    readInt2++;
                    i2 <<= 1;
                }
                Fields.segmentShift.set(this, Integer.valueOf(32 - readInt2));
                Fields.segmentMask.set(this, Integer.valueOf(i2 - 1));
                Fields.segments.set(this, newSegmentArray(i2));
                readInt2 = readInt > MAXIMUM_CAPACITY ? MAXIMUM_CAPACITY : readInt;
                i = readInt2 / i2;
                if (i * i2 < readInt2) {
                    i++;
                }
                readInt2 = 1;
                while (readInt2 < i) {
                    readInt2 <<= 1;
                }
                for (i = 0; i < this.segments.length; i++) {
                    this.segments[i] = new Segment(readInt2);
                }
                Fields.strategy.set(this, strategy);
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
            boolean z = true;
            int i = 0;
            if (obj == null) {
                throw new NullPointerException(Param.VALUE);
            }
            Segment[] segmentArr = this.segments;
            int[] iArr = new int[segmentArr.length];
            for (int i2 = 0; i2 < 2; i2++) {
                int i3;
                int i4 = 0;
                for (i3 = 0; i3 < segmentArr.length; i3++) {
                    int i5 = segmentArr[i3].count;
                    i5 = segmentArr[i3].modCount;
                    iArr[i3] = i5;
                    i4 += i5;
                    if (segmentArr[i3].containsValue(obj)) {
                        return true;
                    }
                }
                if (i4 != 0) {
                    for (i3 = 0; i3 < segmentArr.length; i3++) {
                        i4 = segmentArr[i3].count;
                        if (iArr[i3] != segmentArr[i3].modCount) {
                            i3 = 0;
                            break;
                        }
                    }
                    i3 = true;
                } else {
                    boolean z2 = true;
                }
                if (i3 != 0) {
                    return false;
                }
            }
            for (Segment lock : segmentArr) {
                lock.lock();
            }
            try {
                for (Segment lock2 : segmentArr) {
                    if (lock2.containsValue(obj)) {
                        break;
                    }
                }
                z = false;
                i3 = segmentArr.length;
                while (i < i3) {
                    segmentArr[i].unlock();
                    i++;
                }
                return z;
            } catch (Throwable th) {
                i3 = segmentArr.length;
                while (i < i3) {
                    segmentArr[i].unlock();
                    i++;
                }
            }
        }

        public Set<Entry<K, V>> entrySet() {
            Set<Entry<K, V>> set = this.entrySet;
            if (set != null) {
                return set;
            }
            set = new EntrySet();
            this.entrySet = set;
            return set;
        }

        public V get(Object obj) {
            if (obj == null) {
                throw new NullPointerException("key");
            }
            int hash = hash(obj);
            return segmentFor(hash).get(obj, hash);
        }

        int hash(Object obj) {
            return CustomConcurrentHashMap.rehash(this.strategy.hashKey(obj));
        }

        public boolean isEmpty() {
            int i;
            Segment[] segmentArr = this.segments;
            int[] iArr = new int[segmentArr.length];
            int i2 = 0;
            for (i = 0; i < segmentArr.length; i++) {
                if (segmentArr[i].count != 0) {
                    return false;
                }
                int i3 = segmentArr[i].modCount;
                iArr[i] = i3;
                i2 += i3;
            }
            if (i2 != 0) {
                i = 0;
                while (i < segmentArr.length) {
                    if (segmentArr[i].count != 0 || iArr[i] != segmentArr[i].modCount) {
                        return false;
                    }
                    i++;
                }
            }
            return true;
        }

        public Set<K> keySet() {
            Set<K> set = this.keySet;
            if (set != null) {
                return set;
            }
            set = new KeySet();
            this.keySet = set;
            return set;
        }

        Segment[] newSegmentArray(int i) {
            throw new RuntimeException("d2j fail translate: java.lang.RuntimeException: can not merge L and I\n\tat com.googlecode.dex2jar.ir.TypeClass.merge(TypeClass.java:100)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeRef.updateTypeClass(TypeTransformer.java:243)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeAnalyze.copyTypes(TypeTransformer.java:478)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeAnalyze.fixTypes(TypeTransformer.java:335)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer$TypeAnalyze.analyze(TypeTransformer.java:305)\n\tat com.googlecode.dex2jar.ir.ts.TypeTransformer.transform(TypeTransformer.java:44)\n\tat com.googlecode.d2j.dex.Dex2jar$2.optimize(Dex2jar.java:162)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertCode(Dex2Asm.java:434)\n\tat com.googlecode.d2j.dex.ExDex2Asm.convertCode(ExDex2Asm.java:42)\n\tat com.googlecode.d2j.dex.Dex2jar$2.convertCode(Dex2jar.java:129)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertMethod(Dex2Asm.java:529)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertClass(Dex2Asm.java:426)\n\tat com.googlecode.d2j.dex.Dex2Asm.convertDex(Dex2Asm.java:442)\n\tat com.googlecode.d2j.dex.Dex2jar.doTranslate(Dex2jar.java:172)\n\tat com.googlecode.d2j.dex.Dex2jar.to(Dex2jar.java:272)\n\tat com.googlecode.dex2jar.tools.Dex2jarCmd.doCommandLine(Dex2jarCmd.java:109)\n\tat com.googlecode.dex2jar.tools.BaseCmd.doMain(BaseCmd.java:290)\n\tat com.googlecode.dex2jar.tools.Dex2jarCmd.main(Dex2jarCmd.java:33)\n");
        }

        public V put(K k, V v) {
            if (k == null) {
                throw new NullPointerException("key");
            } else if (v == null) {
                throw new NullPointerException(Param.VALUE);
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
                throw new NullPointerException(Param.VALUE);
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
                throw new NullPointerException(Param.VALUE);
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

        Segment segmentFor(int i) {
            return this.segments[(i >>> this.segmentShift) & this.segmentMask];
        }

        public int size() {
            int i = 0;
            Segment[] segmentArr = this.segments;
            int[] iArr = new int[segmentArr.length];
            long j = 0;
            long j2 = 0;
            for (int i2 = 0; i2 < 2; i2++) {
                int i3;
                j = 0;
                int i4 = 0;
                for (i3 = 0; i3 < segmentArr.length; i3++) {
                    j += (long) segmentArr[i3].count;
                    int i5 = segmentArr[i3].modCount;
                    iArr[i3] = i5;
                    i4 += i5;
                }
                if (i4 != 0) {
                    j2 = 0;
                    for (i3 = 0; i3 < segmentArr.length; i3++) {
                        j2 += (long) segmentArr[i3].count;
                        if (iArr[i3] != segmentArr[i3].modCount) {
                            j2 = -1;
                            break;
                        }
                    }
                } else {
                    j2 = 0;
                }
                if (j2 == j) {
                    break;
                }
            }
            if (j2 != j) {
                for (Segment lock : segmentArr) {
                    lock.lock();
                }
                j = 0;
                for (Segment segment : segmentArr) {
                    j += (long) segment.count;
                }
                int length = segmentArr.length;
                while (i < length) {
                    segmentArr[i].unlock();
                    i++;
                }
            }
            return j > 2147483647L ? com.google.android.gms.nearby.messages.Strategy.TTL_SECONDS_INFINITE : (int) j;
        }

        public Collection<V> values() {
            Collection<V> collection = this.values;
            if (collection != null) {
                return collection;
            }
            collection = new Values();
            this.values = collection;
            return collection;
        }
    }

    static class ComputingImpl<K, V, E> extends Impl<K, V, E> {
        static final long serialVersionUID = 0;
        final Function<? super K, ? extends V> computer;
        final ComputingStrategy<K, V, E> computingStrategy;

        ComputingImpl(ComputingStrategy<K, V, E> computingStrategy, Builder builder, Function<? super K, ? extends V> function) {
            super(computingStrategy, builder);
            this.computingStrategy = computingStrategy;
            this.computer = function;
        }

        /* JADX WARNING: inconsistent code. */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        public V get(java.lang.Object r10) {
            /*
            r9 = this;
            r2 = 1;
            r3 = 0;
            if (r10 != 0) goto L_0x000c;
        L_0x0004:
            r0 = new java.lang.NullPointerException;
            r1 = "key";
            r0.<init>(r1);
            throw r0;
        L_0x000c:
            r5 = r9.hash(r10);
            r6 = r9.segmentFor(r5);
        L_0x0014:
            r0 = r6.getEntry(r10, r5);
            if (r0 != 0) goto L_0x006f;
        L_0x001a:
            r6.lock();
            r0 = r6.getEntry(r10, r5);	 Catch:{ all -> 0x006a }
            if (r0 != 0) goto L_0x00a0;
        L_0x0023:
            r1 = r6.count;	 Catch:{ all -> 0x006a }
            r0 = r6.threshold;	 Catch:{ all -> 0x006a }
            if (r1 <= r0) goto L_0x002c;
        L_0x0029:
            r6.expand();	 Catch:{ all -> 0x006a }
        L_0x002c:
            r4 = r6.table;	 Catch:{ all -> 0x006a }
            r0 = r4.length();	 Catch:{ all -> 0x006a }
            r0 = r0 + -1;
            r7 = r5 & r0;
            r0 = r4.get(r7);	 Catch:{ all -> 0x006a }
            r8 = r6.modCount;	 Catch:{ all -> 0x006a }
            r8 = r8 + 1;
            r6.modCount = r8;	 Catch:{ all -> 0x006a }
            r8 = r9.computingStrategy;	 Catch:{ all -> 0x006a }
            r0 = r8.newEntry(r10, r5, r0);	 Catch:{ all -> 0x006a }
            r4.set(r7, r0);	 Catch:{ all -> 0x006a }
            r1 = r1 + 1;
            r6.count = r1;	 Catch:{ all -> 0x006a }
            r1 = r2;
        L_0x004e:
            r6.unlock();
            if (r1 == 0) goto L_0x006f;
        L_0x0053:
            r1 = r9.computingStrategy;	 Catch:{ all -> 0x0065 }
            r2 = r9.computer;	 Catch:{ all -> 0x0065 }
            r1 = r1.compute(r10, r0, r2);	 Catch:{ all -> 0x0065 }
            if (r1 != 0) goto L_0x009e;
        L_0x005d:
            r1 = new java.lang.NullPointerException;	 Catch:{ all -> 0x0065 }
            r2 = "compute() returned null unexpectedly";
            r1.<init>(r2);	 Catch:{ all -> 0x0065 }
            throw r1;	 Catch:{ all -> 0x0065 }
        L_0x0065:
            r1 = move-exception;
            r6.removeEntry(r0, r5);
            throw r1;
        L_0x006a:
            r0 = move-exception;
            r6.unlock();
            throw r0;
        L_0x006f:
            r1 = r0;
            r4 = r3;
        L_0x0071:
            r0 = r9.computingStrategy;	 Catch:{ InterruptedException -> 0x0090, all -> 0x0093 }
            r0 = r0.waitForValue(r1);	 Catch:{ InterruptedException -> 0x0090, all -> 0x0093 }
            if (r0 != 0) goto L_0x0086;
        L_0x0079:
            r6.removeEntry(r1, r5);	 Catch:{ InterruptedException -> 0x0090, all -> 0x0093 }
            if (r4 == 0) goto L_0x0014;
        L_0x007e:
            r0 = java.lang.Thread.currentThread();
            r0.interrupt();
            goto L_0x0014;
        L_0x0086:
            if (r4 == 0) goto L_0x008f;
        L_0x0088:
            r1 = java.lang.Thread.currentThread();
            r1.interrupt();
        L_0x008f:
            return r0;
        L_0x0090:
            r0 = move-exception;
            r4 = r2;
            goto L_0x0071;
        L_0x0093:
            r0 = move-exception;
            if (r4 == 0) goto L_0x009d;
        L_0x0096:
            r1 = java.lang.Thread.currentThread();
            r1.interrupt();
        L_0x009d:
            throw r0;
        L_0x009e:
            r0 = r1;
            goto L_0x008f;
        L_0x00a0:
            r1 = r3;
            goto L_0x004e;
            */
            throw new UnsupportedOperationException("Method not decompiled: com.google.common.collect.CustomConcurrentHashMap.ComputingImpl.get(java.lang.Object):V");
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

    public interface ComputingStrategy<K, V, E> extends Strategy<K, V, E> {
        V compute(K k, E e, Function<? super K, ? extends V> function);

        V waitForValue(E e) throws InterruptedException;
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
            return new SimpleInternalEntry(k, simpleInternalEntry.hash, simpleInternalEntry.value, simpleInternalEntry2);
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
            return new SimpleInternalEntry(k, i, null, simpleInternalEntry);
        }

        public void setInternals(Internals<K, V, SimpleInternalEntry<K, V>> internals) {
        }

        public void setValue(SimpleInternalEntry<K, V> simpleInternalEntry, V v) {
            simpleInternalEntry.value = v;
        }
    }

    private CustomConcurrentHashMap() {
    }

    private static int rehash(int i) {
        int i2 = ((i << 15) ^ -12931) + i;
        i2 ^= i2 >>> 10;
        i2 += i2 << 3;
        i2 ^= i2 >>> 6;
        i2 += (i2 << 2) + (i2 << 14);
        return i2 ^ (i2 >>> 16);
    }
}

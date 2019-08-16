package com.google.gson.internal;

import java.io.ObjectStreamException;
import java.io.Serializable;
import java.util.AbstractMap;
import java.util.AbstractSet;
import java.util.Comparator;
import java.util.ConcurrentModificationException;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.Map.Entry;
import java.util.NoSuchElementException;
import java.util.Set;

public final class LinkedTreeMap<K, V> extends AbstractMap<K, V> implements Serializable {
    static final /* synthetic */ boolean $assertionsDisabled = (!LinkedTreeMap.class.desiredAssertionStatus());
    private static final Comparator<Comparable> NATURAL_ORDER = new Comparator<Comparable>() {
        public int compare(Comparable comparable, Comparable comparable2) {
            return comparable.compareTo(comparable2);
        }
    };
    Comparator<? super K> comparator;
    private EntrySet entrySet;
    final Node<K, V> header;
    private KeySet keySet;
    int modCount;
    Node<K, V> root;
    int size;

    class EntrySet extends AbstractSet<Entry<K, V>> {
        EntrySet() {
        }

        public void clear() {
            LinkedTreeMap.this.clear();
        }

        public boolean contains(Object obj) {
            return (obj instanceof Entry) && LinkedTreeMap.this.findByEntry((Entry) obj) != null;
        }

        public Iterator<Entry<K, V>> iterator() {
            return new LinkedTreeMapIterator<Entry<K, V>>() {
                {
                    LinkedTreeMap linkedTreeMap = LinkedTreeMap.this;
                }

                public Entry<K, V> next() {
                    return nextNode();
                }
            };
        }

        public boolean remove(Object obj) {
            if (obj instanceof Entry) {
                Node findByEntry = LinkedTreeMap.this.findByEntry((Entry) obj);
                if (findByEntry != null) {
                    LinkedTreeMap.this.removeInternal(findByEntry, true);
                    return true;
                }
            }
            return false;
        }

        public int size() {
            return LinkedTreeMap.this.size;
        }
    }

    final class KeySet extends AbstractSet<K> {
        KeySet() {
        }

        public void clear() {
            LinkedTreeMap.this.clear();
        }

        public boolean contains(Object obj) {
            return LinkedTreeMap.this.containsKey(obj);
        }

        public Iterator<K> iterator() {
            return new LinkedTreeMapIterator<K>() {
                {
                    LinkedTreeMap linkedTreeMap = LinkedTreeMap.this;
                }

                public K next() {
                    return nextNode().key;
                }
            };
        }

        public boolean remove(Object obj) {
            return LinkedTreeMap.this.removeInternalByKey(obj) != null;
        }

        public int size() {
            return LinkedTreeMap.this.size;
        }
    }

    private abstract class LinkedTreeMapIterator<T> implements Iterator<T> {
        int expectedModCount;
        Node<K, V> lastReturned;
        Node<K, V> next;

        private LinkedTreeMapIterator() {
            this.next = LinkedTreeMap.this.header.next;
            this.lastReturned = null;
            this.expectedModCount = LinkedTreeMap.this.modCount;
        }

        public final boolean hasNext() {
            return this.next != LinkedTreeMap.this.header;
        }

        /* access modifiers changed from: 0000 */
        public final Node<K, V> nextNode() {
            Node<K, V> node = this.next;
            if (node == LinkedTreeMap.this.header) {
                throw new NoSuchElementException();
            } else if (LinkedTreeMap.this.modCount != this.expectedModCount) {
                throw new ConcurrentModificationException();
            } else {
                this.next = node.next;
                this.lastReturned = node;
                return node;
            }
        }

        public final void remove() {
            if (this.lastReturned == null) {
                throw new IllegalStateException();
            }
            LinkedTreeMap.this.removeInternal(this.lastReturned, true);
            this.lastReturned = null;
            this.expectedModCount = LinkedTreeMap.this.modCount;
        }
    }

    static final class Node<K, V> implements Entry<K, V> {
        int height;
        final K key;
        Node<K, V> left;
        Node<K, V> next;
        Node<K, V> parent;
        Node<K, V> prev;
        Node<K, V> right;
        V value;

        Node() {
            this.key = null;
            this.prev = this;
            this.next = this;
        }

        Node(Node<K, V> node, K k, Node<K, V> node2, Node<K, V> node3) {
            this.parent = node;
            this.key = k;
            this.height = 1;
            this.next = node2;
            this.prev = node3;
            node3.next = this;
            node2.prev = this;
        }

        public boolean equals(Object obj) {
            if (!(obj instanceof Entry)) {
                return false;
            }
            Entry entry = (Entry) obj;
            if (this.key == null) {
                if (entry.getKey() != null) {
                    return false;
                }
            } else if (!this.key.equals(entry.getKey())) {
                return false;
            }
            if (this.value == null) {
                if (entry.getValue() != null) {
                    return false;
                }
            } else if (!this.value.equals(entry.getValue())) {
                return false;
            }
            return true;
        }

        public Node<K, V> first() {
            for (Node<K, V> node = this.left; node != null; node = node.left) {
                this = node;
            }
            return this;
        }

        public K getKey() {
            return this.key;
        }

        public V getValue() {
            return this.value;
        }

        public int hashCode() {
            int i = 0;
            int hashCode = this.key == null ? 0 : this.key.hashCode();
            if (this.value != null) {
                i = this.value.hashCode();
            }
            return hashCode ^ i;
        }

        public Node<K, V> last() {
            for (Node<K, V> node = this.right; node != null; node = node.right) {
                this = node;
            }
            return this;
        }

        public V setValue(V v) {
            V v2 = this.value;
            this.value = v;
            return v2;
        }

        public String toString() {
            return this.key + "=" + this.value;
        }
    }

    public LinkedTreeMap() {
        this(NATURAL_ORDER);
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=java.util.Comparator<? super K>, code=java.util.Comparator, for r2v0, types: [java.util.Comparator<? super K>, java.util.Comparator] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public LinkedTreeMap(java.util.Comparator r2) {
        /*
            r1 = this;
            r0 = 0
            r1.<init>()
            r1.size = r0
            r1.modCount = r0
            com.google.gson.internal.LinkedTreeMap$Node r0 = new com.google.gson.internal.LinkedTreeMap$Node
            r0.<init>()
            r1.header = r0
            if (r2 == 0) goto L_0x0014
        L_0x0011:
            r1.comparator = r2
            return
        L_0x0014:
            java.util.Comparator<java.lang.Comparable> r2 = NATURAL_ORDER
            goto L_0x0011
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.gson.internal.LinkedTreeMap.<init>(java.util.Comparator):void");
    }

    private boolean equal(Object obj, Object obj2) {
        return obj == obj2 || (obj != null && obj.equals(obj2));
    }

    private void rebalance(Node<K, V> node, boolean z) {
        while (node != null) {
            Node<K, V> node2 = node.left;
            Node<K, V> node3 = node.right;
            int i = node2 != null ? node2.height : 0;
            int i2 = node3 != null ? node3.height : 0;
            int i3 = i - i2;
            if (i3 == -2) {
                Node<K, V> node4 = node3.left;
                Node<K, V> node5 = node3.right;
                int i4 = (node4 != null ? node4.height : 0) - (node5 != null ? node5.height : 0);
                if (i4 == -1 || (i4 == 0 && !z)) {
                    rotateLeft(node);
                } else if ($assertionsDisabled || i4 == 1) {
                    rotateRight(node3);
                    rotateLeft(node);
                } else {
                    throw new AssertionError();
                }
                if (z) {
                    return;
                }
            } else if (i3 == 2) {
                Node<K, V> node6 = node2.left;
                Node<K, V> node7 = node2.right;
                int i5 = (node6 != null ? node6.height : 0) - (node7 != null ? node7.height : 0);
                if (i5 == 1 || (i5 == 0 && !z)) {
                    rotateRight(node);
                } else if ($assertionsDisabled || i5 == -1) {
                    rotateLeft(node2);
                    rotateRight(node);
                } else {
                    throw new AssertionError();
                }
                if (z) {
                    return;
                }
            } else if (i3 == 0) {
                node.height = i + 1;
                if (z) {
                    return;
                }
            } else if ($assertionsDisabled || i3 == -1 || i3 == 1) {
                node.height = Math.max(i, i2) + 1;
                if (!z) {
                    return;
                }
            } else {
                throw new AssertionError();
            }
            node = node.parent;
        }
    }

    private void replaceInParent(Node<K, V> node, Node<K, V> node2) {
        Node<K, V> node3 = node.parent;
        node.parent = null;
        if (node2 != null) {
            node2.parent = node3;
        }
        if (node3 == null) {
            this.root = node2;
        } else if (node3.left == node) {
            node3.left = node2;
        } else if ($assertionsDisabled || node3.right == node) {
            node3.right = node2;
        } else {
            throw new AssertionError();
        }
    }

    private void rotateLeft(Node<K, V> node) {
        int i = 0;
        Node<K, V> node2 = node.left;
        Node<K, V> node3 = node.right;
        Node<K, V> node4 = node3.left;
        Node<K, V> node5 = node3.right;
        node.right = node4;
        if (node4 != null) {
            node4.parent = node;
        }
        replaceInParent(node, node3);
        node3.left = node;
        node.parent = node3;
        node.height = Math.max(node2 != null ? node2.height : 0, node4 != null ? node4.height : 0) + 1;
        int i2 = node.height;
        if (node5 != null) {
            i = node5.height;
        }
        node3.height = Math.max(i2, i) + 1;
    }

    private void rotateRight(Node<K, V> node) {
        int i = 0;
        Node<K, V> node2 = node.left;
        Node<K, V> node3 = node.right;
        Node<K, V> node4 = node2.left;
        Node<K, V> node5 = node2.right;
        node.left = node5;
        if (node5 != null) {
            node5.parent = node;
        }
        replaceInParent(node, node2);
        node2.right = node;
        node.parent = node2;
        node.height = Math.max(node3 != null ? node3.height : 0, node5 != null ? node5.height : 0) + 1;
        int i2 = node.height;
        if (node4 != null) {
            i = node4.height;
        }
        node2.height = Math.max(i2, i) + 1;
    }

    private Object writeReplace() throws ObjectStreamException {
        return new LinkedHashMap(this);
    }

    public void clear() {
        this.root = null;
        this.size = 0;
        this.modCount++;
        Node<K, V> node = this.header;
        node.prev = node;
        node.next = node;
    }

    public boolean containsKey(Object obj) {
        return findByObject(obj) != null;
    }

    public Set<Entry<K, V>> entrySet() {
        EntrySet entrySet2 = this.entrySet;
        if (entrySet2 != null) {
            return entrySet2;
        }
        EntrySet entrySet3 = new EntrySet<>();
        this.entrySet = entrySet3;
        return entrySet3;
    }

    /* access modifiers changed from: 0000 */
    public Node<K, V> find(K k, boolean z) {
        int i;
        Node<K, V> node;
        Comparator<? super K> comparator2 = this.comparator;
        Node<K, V> node2 = this.root;
        if (node2 != null) {
            Comparable comparable = comparator2 == NATURAL_ORDER ? (Comparable) k : null;
            while (true) {
                int compare = comparable != null ? comparable.compareTo(node2.key) : comparator2.compare(k, node2.key);
                if (compare == 0) {
                    return node2;
                }
                Node<K, V> node3 = compare < 0 ? node2.left : node2.right;
                if (node3 == null) {
                    i = compare;
                    break;
                }
                node2 = node3;
            }
        } else {
            i = 0;
        }
        if (!z) {
            return null;
        }
        Node<K, V> node4 = this.header;
        if (node2 != null) {
            node = new Node<>(node2, k, node4, node4.prev);
            if (i < 0) {
                node2.left = node;
            } else {
                node2.right = node;
            }
            rebalance(node2, true);
        } else if (comparator2 != NATURAL_ORDER || (k instanceof Comparable)) {
            node = new Node<>(node2, k, node4, node4.prev);
            this.root = node;
        } else {
            throw new ClassCastException(k.getClass().getName() + " is not Comparable");
        }
        this.size++;
        this.modCount++;
        return node;
    }

    /* access modifiers changed from: 0000 */
    public Node<K, V> findByEntry(Entry<?, ?> entry) {
        Node<K, V> findByObject = findByObject(entry.getKey());
        if (findByObject != null && equal(findByObject.value, entry.getValue())) {
            return findByObject;
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public Node<K, V> findByObject(Object obj) {
        Node<K, V> node = null;
        if (obj == null) {
            return node;
        }
        try {
            return find(obj, false);
        } catch (ClassCastException e) {
            return node;
        }
    }

    public V get(Object obj) {
        Node findByObject = findByObject(obj);
        if (findByObject != null) {
            return findByObject.value;
        }
        return null;
    }

    public Set<K> keySet() {
        KeySet keySet2 = this.keySet;
        if (keySet2 != null) {
            return keySet2;
        }
        KeySet keySet3 = new KeySet<>();
        this.keySet = keySet3;
        return keySet3;
    }

    public V put(K k, V v) {
        if (k == null) {
            throw new NullPointerException("key == null");
        }
        Node find = find(k, true);
        V v2 = find.value;
        find.value = v;
        return v2;
    }

    public V remove(Object obj) {
        Node removeInternalByKey = removeInternalByKey(obj);
        if (removeInternalByKey != null) {
            return removeInternalByKey.value;
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public void removeInternal(Node<K, V> node, boolean z) {
        int i;
        int i2 = 0;
        if (z) {
            node.prev.next = node.next;
            node.next.prev = node.prev;
        }
        Node<K, V> node2 = node.left;
        Node<K, V> node3 = node.right;
        Node<K, V> node4 = node.parent;
        if (node2 == null || node3 == null) {
            if (node2 != null) {
                replaceInParent(node, node2);
                node.left = null;
            } else if (node3 != null) {
                replaceInParent(node, node3);
                node.right = null;
            } else {
                replaceInParent(node, null);
            }
            rebalance(node4, false);
            this.size--;
            this.modCount++;
            return;
        }
        Node<K, V> first = node2.height > node3.height ? node2.last() : node3.first();
        removeInternal(first, false);
        Node<K, V> node5 = node.left;
        if (node5 != null) {
            i = node5.height;
            first.left = node5;
            node5.parent = first;
            node.left = null;
        } else {
            i = 0;
        }
        Node<K, V> node6 = node.right;
        if (node6 != null) {
            i2 = node6.height;
            first.right = node6;
            node6.parent = first;
            node.right = null;
        }
        first.height = Math.max(i, i2) + 1;
        replaceInParent(node, first);
    }

    /* access modifiers changed from: 0000 */
    public Node<K, V> removeInternalByKey(Object obj) {
        Node<K, V> findByObject = findByObject(obj);
        if (findByObject != null) {
            removeInternal(findByObject, true);
        }
        return findByObject;
    }

    public int size() {
        return this.size;
    }
}

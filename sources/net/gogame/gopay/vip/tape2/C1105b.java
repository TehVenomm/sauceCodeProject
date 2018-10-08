package net.gogame.gopay.vip.tape2;

import java.io.File;
import java.io.IOException;
import java.util.ConcurrentModificationException;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.NoSuchElementException;

/* renamed from: net.gogame.gopay.vip.tape2.b */
final class C1105b<T> extends ObjectQueue<T> {
    /* renamed from: a */
    final LinkedList<T> f1332a = new LinkedList();
    /* renamed from: b */
    int f1333b = 0;
    /* renamed from: c */
    private boolean f1334c;

    /* renamed from: net.gogame.gopay.vip.tape2.b$a */
    private final class C1104a implements Iterator<T> {
        /* renamed from: a */
        int f1329a = 0;
        /* renamed from: b */
        int f1330b = this.f1331c.f1333b;
        /* renamed from: c */
        final /* synthetic */ C1105b f1331c;

        C1104a(C1105b c1105b) {
            this.f1331c = c1105b;
        }

        public boolean hasNext() {
            m994a();
            return this.f1329a != this.f1331c.size();
        }

        public T next() {
            if (this.f1331c.f1334c) {
                throw new IllegalStateException("closed");
            }
            m994a();
            if (this.f1329a >= this.f1331c.size()) {
                throw new NoSuchElementException();
            }
            LinkedList linkedList = this.f1331c.f1332a;
            int i = this.f1329a;
            this.f1329a = i + 1;
            return linkedList.get(i);
        }

        public void remove() {
            if (this.f1331c.f1334c) {
                throw new IllegalStateException("closed");
            }
            m994a();
            if (this.f1331c.size() == 0) {
                throw new NoSuchElementException();
            } else if (this.f1329a != 1) {
                throw new UnsupportedOperationException("Removal is only permitted from the head.");
            } else {
                try {
                    this.f1331c.remove();
                    this.f1330b = this.f1331c.f1333b;
                    this.f1329a--;
                } catch (Throwable e) {
                    throw new RuntimeException("todo: throw a proper error", e);
                }
            }
        }

        /* renamed from: a */
        private void m994a() {
            if (this.f1331c.f1333b != this.f1330b) {
                throw new ConcurrentModificationException();
            }
        }
    }

    C1105b() {
    }

    public File file() {
        return null;
    }

    public void add(T t) throws IOException {
        if (this.f1334c) {
            throw new IOException("closed");
        }
        this.f1333b++;
        this.f1332a.add(t);
    }

    public T peek() throws IOException {
        if (!this.f1334c) {
            return this.f1332a.peek();
        }
        throw new IOException("closed");
    }

    public int size() {
        return this.f1332a.size();
    }

    public void remove(int i) throws IOException {
        if (this.f1334c) {
            throw new IOException("closed");
        }
        this.f1333b++;
        for (int i2 = 0; i2 < i; i2++) {
            this.f1332a.remove();
        }
    }

    public Iterator<T> iterator() {
        return new C1104a(this);
    }

    public void close() throws IOException {
        this.f1334c = true;
    }
}

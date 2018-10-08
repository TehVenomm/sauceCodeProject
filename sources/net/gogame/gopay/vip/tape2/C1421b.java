package net.gogame.gopay.vip.tape2;

import java.io.File;
import java.io.IOException;
import java.util.ConcurrentModificationException;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.NoSuchElementException;

/* renamed from: net.gogame.gopay.vip.tape2.b */
final class C1421b<T> extends ObjectQueue<T> {
    /* renamed from: a */
    final LinkedList<T> f3720a = new LinkedList();
    /* renamed from: b */
    int f3721b = 0;
    /* renamed from: c */
    private boolean f3722c;

    /* renamed from: net.gogame.gopay.vip.tape2.b$a */
    private final class C1420a implements Iterator<T> {
        /* renamed from: a */
        int f3717a = 0;
        /* renamed from: b */
        int f3718b = this.f3719c.f3721b;
        /* renamed from: c */
        final /* synthetic */ C1421b f3719c;

        C1420a(C1421b c1421b) {
            this.f3719c = c1421b;
        }

        public boolean hasNext() {
            m4019a();
            return this.f3717a != this.f3719c.size();
        }

        public T next() {
            if (this.f3719c.f3722c) {
                throw new IllegalStateException("closed");
            }
            m4019a();
            if (this.f3717a >= this.f3719c.size()) {
                throw new NoSuchElementException();
            }
            LinkedList linkedList = this.f3719c.f3720a;
            int i = this.f3717a;
            this.f3717a = i + 1;
            return linkedList.get(i);
        }

        public void remove() {
            if (this.f3719c.f3722c) {
                throw new IllegalStateException("closed");
            }
            m4019a();
            if (this.f3719c.size() == 0) {
                throw new NoSuchElementException();
            } else if (this.f3717a != 1) {
                throw new UnsupportedOperationException("Removal is only permitted from the head.");
            } else {
                try {
                    this.f3719c.remove();
                    this.f3718b = this.f3719c.f3721b;
                    this.f3717a--;
                } catch (Throwable e) {
                    throw new RuntimeException("todo: throw a proper error", e);
                }
            }
        }

        /* renamed from: a */
        private void m4019a() {
            if (this.f3719c.f3721b != this.f3718b) {
                throw new ConcurrentModificationException();
            }
        }
    }

    C1421b() {
    }

    public File file() {
        return null;
    }

    public void add(T t) throws IOException {
        if (this.f3722c) {
            throw new IOException("closed");
        }
        this.f3721b++;
        this.f3720a.add(t);
    }

    public T peek() throws IOException {
        if (!this.f3722c) {
            return this.f3720a.peek();
        }
        throw new IOException("closed");
    }

    public int size() {
        return this.f3720a.size();
    }

    public void remove(int i) throws IOException {
        if (this.f3722c) {
            throw new IOException("closed");
        }
        this.f3721b++;
        for (int i2 = 0; i2 < i; i2++) {
            this.f3720a.remove();
        }
    }

    public Iterator<T> iterator() {
        return new C1420a(this);
    }

    public void close() throws IOException {
        this.f3722c = true;
    }
}

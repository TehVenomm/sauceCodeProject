package net.gogame.gopay.vip.tape2;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.util.Iterator;
import java.util.List;
import net.gogame.gopay.vip.tape2.ObjectQueue.Converter;

/* renamed from: net.gogame.gopay.vip.tape2.a */
final class C1419a<T> extends ObjectQueue<T> {
    /* renamed from: a */
    final Converter<T> f3713a;
    /* renamed from: b */
    private final QueueFile f3714b;
    /* renamed from: c */
    private final C1417a f3715c = new C1417a();
    /* renamed from: d */
    private final File f3716d;

    /* renamed from: net.gogame.gopay.vip.tape2.a$a */
    private static class C1417a extends ByteArrayOutputStream {
        /* renamed from: a */
        public byte[] m4018a() {
            return this.buf;
        }
    }

    /* renamed from: net.gogame.gopay.vip.tape2.a$b */
    private final class C1418b implements Iterator<T> {
        /* renamed from: a */
        final Iterator<byte[]> f3711a;
        /* renamed from: b */
        final /* synthetic */ C1419a f3712b;

        C1418b(C1419a c1419a, Iterator<byte[]> it) {
            this.f3712b = c1419a;
            this.f3711a = it;
        }

        public boolean hasNext() {
            return this.f3711a.hasNext();
        }

        public T next() {
            try {
                return this.f3712b.f3713a.from((byte[]) this.f3711a.next());
            } catch (Throwable e) {
                throw new RuntimeException("todo: throw a proper error", e);
            }
        }

        public void remove() {
            this.f3711a.remove();
        }
    }

    C1419a(File file, Converter<T> converter) throws IOException {
        this.f3716d = file;
        this.f3713a = converter;
        this.f3714b = new QueueFile(file);
    }

    public File file() {
        return this.f3716d;
    }

    public int size() {
        return this.f3714b.size();
    }

    public void add(T t) throws IOException {
        this.f3715c.reset();
        this.f3713a.toStream(t, this.f3715c);
        this.f3714b.add(this.f3715c.m4018a(), 0, this.f3715c.size());
    }

    public T peek() throws IOException {
        byte[] peek = this.f3714b.peek();
        if (peek == null) {
            return null;
        }
        return this.f3713a.from(peek);
    }

    public List<T> asList() throws IOException {
        return peek(size());
    }

    public void remove(int i) throws IOException {
        this.f3714b.remove(i);
    }

    public void close() throws IOException {
        this.f3714b.close();
    }

    public Iterator<T> iterator() {
        return new C1418b(this, this.f3714b.iterator());
    }
}

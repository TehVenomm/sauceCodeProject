package net.gogame.gopay.vip.tape2;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.util.Iterator;
import java.util.List;
import net.gogame.gopay.vip.tape2.ObjectQueue.Converter;

/* renamed from: net.gogame.gopay.vip.tape2.a */
final class C1103a<T> extends ObjectQueue<T> {
    /* renamed from: a */
    final Converter<T> f1325a;
    /* renamed from: b */
    private final QueueFile f1326b;
    /* renamed from: c */
    private final C1101a f1327c = new C1101a();
    /* renamed from: d */
    private final File f1328d;

    /* renamed from: net.gogame.gopay.vip.tape2.a$a */
    private static class C1101a extends ByteArrayOutputStream {
        /* renamed from: a */
        public byte[] m993a() {
            return this.buf;
        }
    }

    /* renamed from: net.gogame.gopay.vip.tape2.a$b */
    private final class C1102b implements Iterator<T> {
        /* renamed from: a */
        final Iterator<byte[]> f1323a;
        /* renamed from: b */
        final /* synthetic */ C1103a f1324b;

        C1102b(C1103a c1103a, Iterator<byte[]> it) {
            this.f1324b = c1103a;
            this.f1323a = it;
        }

        public boolean hasNext() {
            return this.f1323a.hasNext();
        }

        public T next() {
            try {
                return this.f1324b.f1325a.from((byte[]) this.f1323a.next());
            } catch (Throwable e) {
                throw new RuntimeException("todo: throw a proper error", e);
            }
        }

        public void remove() {
            this.f1323a.remove();
        }
    }

    C1103a(File file, Converter<T> converter) throws IOException {
        this.f1328d = file;
        this.f1325a = converter;
        this.f1326b = new QueueFile(file);
    }

    public File file() {
        return this.f1328d;
    }

    public int size() {
        return this.f1326b.size();
    }

    public void add(T t) throws IOException {
        this.f1327c.reset();
        this.f1325a.toStream(t, this.f1327c);
        this.f1326b.add(this.f1327c.m993a(), 0, this.f1327c.size());
    }

    public T peek() throws IOException {
        byte[] peek = this.f1326b.peek();
        if (peek == null) {
            return null;
        }
        return this.f1325a.from(peek);
    }

    public List<T> asList() throws IOException {
        return peek(size());
    }

    public void remove(int i) throws IOException {
        this.f1326b.remove(i);
    }

    public void close() throws IOException {
        this.f1326b.close();
    }

    public Iterator<T> iterator() {
        return new C1102b(this, this.f1326b.iterator());
    }
}

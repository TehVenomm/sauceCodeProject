package net.gogame.gopay.vip.tape2;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.util.Iterator;
import java.util.List;
import net.gogame.gopay.vip.tape2.ObjectQueue.Converter;

/* renamed from: net.gogame.gopay.vip.tape2.a */
final class C1669a<T> extends ObjectQueue<T> {

    /* renamed from: a */
    final Converter<T> f1392a;

    /* renamed from: b */
    private final QueueFile f1393b;

    /* renamed from: c */
    private final C1670a f1394c = new C1670a();

    /* renamed from: d */
    private final File f1395d;

    /* renamed from: net.gogame.gopay.vip.tape2.a$a */
    private static class C1670a extends ByteArrayOutputStream {
        /* renamed from: a */
        public byte[] mo22809a() {
            return this.buf;
        }
    }

    /* renamed from: net.gogame.gopay.vip.tape2.a$b */
    private final class C1671b implements Iterator<T> {

        /* renamed from: a */
        final Iterator<byte[]> f1396a;

        C1671b(Iterator<byte[]> it) {
            this.f1396a = it;
        }

        public boolean hasNext() {
            return this.f1396a.hasNext();
        }

        public T next() {
            try {
                return C1669a.this.f1392a.from((byte[]) this.f1396a.next());
            } catch (IOException e) {
                throw new RuntimeException("todo: throw a proper error", e);
            }
        }

        public void remove() {
            this.f1396a.remove();
        }
    }

    C1669a(File file, Converter<T> converter) throws IOException {
        this.f1395d = file;
        this.f1392a = converter;
        this.f1393b = new QueueFile(file);
    }

    public File file() {
        return this.f1395d;
    }

    public int size() {
        return this.f1393b.size();
    }

    public void add(T t) throws IOException {
        this.f1394c.reset();
        this.f1392a.toStream(t, this.f1394c);
        this.f1393b.add(this.f1394c.mo22809a(), 0, this.f1394c.size());
    }

    public T peek() throws IOException {
        byte[] peek = this.f1393b.peek();
        if (peek == null) {
            return null;
        }
        return this.f1392a.from(peek);
    }

    public List<T> asList() throws IOException {
        return peek(size());
    }

    public void remove(int i) throws IOException {
        this.f1393b.remove(i);
    }

    public void close() throws IOException {
        this.f1393b.close();
    }

    public Iterator<T> iterator() {
        return new C1671b(this.f1393b.iterator());
    }
}

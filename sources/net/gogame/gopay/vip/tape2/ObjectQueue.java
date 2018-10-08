package net.gogame.gopay.vip.tape2;

import java.io.Closeable;
import java.io.File;
import java.io.IOException;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

public abstract class ObjectQueue<T> implements Closeable, Iterable<T> {

    public interface Converter<T> {
        T from(byte[] bArr) throws IOException;

        void toStream(T t, OutputStream outputStream) throws IOException;
    }

    public abstract void add(T t) throws IOException;

    public abstract File file();

    public abstract T peek() throws IOException;

    public abstract void remove(int i) throws IOException;

    public abstract int size();

    public static <T> ObjectQueue<T> create(File file, Converter<T> converter) throws IOException {
        return new C1103a(file, converter);
    }

    public static <T> ObjectQueue<T> createInMemory() {
        return new C1105b();
    }

    public boolean isEmpty() {
        return size() == 0;
    }

    public List<T> peek(int i) throws IOException {
        int min = Math.min(i, size());
        List arrayList = new ArrayList(min);
        Iterator it = iterator();
        for (int i2 = 0; i2 < min; i2++) {
            arrayList.add(it.next());
        }
        return Collections.unmodifiableList(arrayList);
    }

    public List<T> asList() throws IOException {
        return peek(size());
    }

    public void remove() throws IOException {
        remove(1);
    }

    public void clear() throws IOException {
        remove(size());
    }
}

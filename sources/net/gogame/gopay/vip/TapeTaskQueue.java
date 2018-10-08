package net.gogame.gopay.vip;

import android.util.Log;
import java.io.File;
import java.io.IOException;
import net.gogame.gopay.vip.TaskQueue.Listener;
import net.gogame.gopay.vip.tape2.ObjectQueue;
import net.gogame.gopay.vip.tape2.ObjectQueue.Converter;

public abstract class TapeTaskQueue<T> extends AbstractTaskQueue<T> {
    /* renamed from: a */
    private final ObjectQueue<T> f1265a;

    public TapeTaskQueue(File file, Converter<T> converter, Listener listener) throws IOException {
        super(listener);
        this.f1265a = ObjectQueue.create(file, converter);
    }

    public synchronized void add(T t) {
        try {
            this.f1265a.add(t);
        } catch (Throwable e) {
            Log.e("goPay", "Exception", e);
        }
    }

    public synchronized T peek() {
        T peek;
        try {
            peek = this.f1265a.peek();
        } catch (Throwable e) {
            Log.e("goPay", "Exception", e);
            peek = null;
        }
        return peek;
    }

    public synchronized void remove() {
        try {
            this.f1265a.remove();
        } catch (Throwable e) {
            Log.e("goPay", "Exception", e);
        }
    }
}

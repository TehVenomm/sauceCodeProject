package net.gogame.gopay.vip;

import android.util.Log;
import java.io.File;
import java.io.IOException;
import net.gogame.gopay.vip.TaskQueue.Listener;
import net.gogame.gopay.vip.tape2.ObjectQueue;
import net.gogame.gopay.vip.tape2.ObjectQueue.Converter;

public abstract class TapeTaskQueue<T> extends AbstractTaskQueue<T> {
    /* renamed from: a */
    private final ObjectQueue<T> f3653a;

    public TapeTaskQueue(File file, Converter<T> converter, Listener listener) throws IOException {
        super(listener);
        this.f3653a = ObjectQueue.create(file, converter);
    }

    public synchronized void add(T t) {
        try {
            this.f3653a.add(t);
        } catch (Throwable e) {
            Log.e("goPay", "Exception", e);
        }
    }

    public synchronized T peek() {
        T peek;
        try {
            peek = this.f3653a.peek();
        } catch (Throwable e) {
            Log.e("goPay", "Exception", e);
            peek = null;
        }
        return peek;
    }

    public synchronized void remove() {
        try {
            this.f3653a.remove();
        } catch (Throwable e) {
            Log.e("goPay", "Exception", e);
        }
    }
}

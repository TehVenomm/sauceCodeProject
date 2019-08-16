package net.gogame.gopay.vip;

import android.util.Log;
import java.io.File;
import java.io.IOException;
import net.gogame.gopay.vip.TaskQueue.Listener;
import net.gogame.gopay.vip.tape2.ObjectQueue;
import net.gogame.gopay.vip.tape2.ObjectQueue.Converter;

public abstract class TapeTaskQueue<T> extends AbstractTaskQueue<T> {

    /* renamed from: a */
    private final ObjectQueue<T> f1352a;

    public TapeTaskQueue(File file, Converter<T> converter, Listener listener) throws IOException {
        super(listener);
        this.f1352a = ObjectQueue.create(file, converter);
    }

    public synchronized void add(T t) {
        try {
            this.f1352a.add(t);
        } catch (Exception e) {
            Log.e("goPay", "Exception", e);
        }
        return;
    }

    public synchronized T peek() {
        T t;
        try {
            t = this.f1352a.peek();
        } catch (Exception e) {
            Log.e("goPay", "Exception", e);
            t = null;
        }
        return t;
    }

    public synchronized void remove() {
        try {
            this.f1352a.remove();
        } catch (Exception e) {
            Log.e("goPay", "Exception", e);
        }
        return;
    }
}

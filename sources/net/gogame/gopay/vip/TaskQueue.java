package net.gogame.gopay.vip;

public interface TaskQueue<T> {

    public interface Listener<T> {
        boolean onTask(T t);
    }

    void add(T t);

    void start();
}

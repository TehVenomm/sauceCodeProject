package io.fabric.sdk.android.services.concurrency;

import java.lang.reflect.Array;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Queue;
import java.util.concurrent.PriorityBlockingQueue;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.ReentrantLock;

public class DependencyPriorityBlockingQueue<E extends Dependency & Task & PriorityProvider> extends PriorityBlockingQueue<E> {
    static final int PEEK = 1;
    static final int POLL = 2;
    static final int POLL_WITH_TIMEOUT = 3;
    static final int TAKE = 0;
    final Queue<E> blockedQueue = new LinkedList();
    private final ReentrantLock lock = new ReentrantLock();

    boolean canProcess(E e) {
        return e.areDependenciesMet();
    }

    public void clear() {
        try {
            this.lock.lock();
            this.blockedQueue.clear();
            super.clear();
        } finally {
            this.lock.unlock();
        }
    }

    <T> T[] concatenate(T[] tArr, T[] tArr2) {
        int length = tArr.length;
        int length2 = tArr2.length;
        Object[] objArr = (Object[]) Array.newInstance(tArr.getClass().getComponentType(), length + length2);
        System.arraycopy(tArr, 0, objArr, 0, length);
        System.arraycopy(tArr2, 0, objArr, length, length2);
        return objArr;
    }

    public boolean contains(Object obj) {
        try {
            this.lock.lock();
            boolean z = super.contains(obj) || this.blockedQueue.contains(obj);
            this.lock.unlock();
            return z;
        } catch (Throwable th) {
            this.lock.unlock();
        }
    }

    public int drainTo(Collection<? super E> collection) {
        int size;
        try {
            this.lock.lock();
            int drainTo = super.drainTo(collection);
            size = this.blockedQueue.size();
            while (!this.blockedQueue.isEmpty()) {
                collection.add(this.blockedQueue.poll());
            }
            return drainTo + size;
        } finally {
            size = this.lock;
            size.unlock();
        }
    }

    public int drainTo(Collection<? super E> collection, int i) {
        try {
            this.lock.lock();
            int drainTo = super.drainTo(collection, i);
            while (!this.blockedQueue.isEmpty() && drainTo <= i) {
                collection.add(this.blockedQueue.poll());
                drainTo++;
            }
            this.lock.unlock();
            return drainTo;
        } catch (Throwable th) {
            this.lock.unlock();
        }
    }

    E get(int i, Long l, TimeUnit timeUnit) throws InterruptedException {
        E performOperation;
        while (true) {
            performOperation = performOperation(i, l, timeUnit);
            if (performOperation == null || canProcess(performOperation)) {
                return performOperation;
            }
            offerBlockedResult(i, performOperation);
        }
        return performOperation;
    }

    boolean offerBlockedResult(int i, E e) {
        try {
            this.lock.lock();
            if (i == 1) {
                super.remove(e);
            }
            boolean offer = this.blockedQueue.offer(e);
            return offer;
        } finally {
            this.lock.unlock();
        }
    }

    public E peek() {
        E e = null;
        try {
            e = get(1, null, null);
        } catch (InterruptedException e2) {
        }
        return e;
    }

    E performOperation(int i, Long l, TimeUnit timeUnit) throws InterruptedException {
        switch (i) {
            case 0:
                return (Dependency) super.take();
            case 1:
                return (Dependency) super.peek();
            case 2:
                return (Dependency) super.poll();
            case 3:
                return (Dependency) super.poll(l.longValue(), timeUnit);
            default:
                return null;
        }
    }

    public E poll() {
        E e = null;
        try {
            e = get(2, null, null);
        } catch (InterruptedException e2) {
        }
        return e;
    }

    public E poll(long j, TimeUnit timeUnit) throws InterruptedException {
        return get(3, Long.valueOf(j), timeUnit);
    }

    public void recycleBlockedQueue() {
        try {
            this.lock.lock();
            Iterator it = this.blockedQueue.iterator();
            while (it.hasNext()) {
                Dependency dependency = (Dependency) it.next();
                if (canProcess(dependency)) {
                    super.offer(dependency);
                    it.remove();
                }
            }
        } finally {
            this.lock.unlock();
        }
    }

    public boolean remove(Object obj) {
        try {
            this.lock.lock();
            boolean z = super.remove(obj) || this.blockedQueue.remove(obj);
            this.lock.unlock();
            return z;
        } catch (Throwable th) {
            this.lock.unlock();
        }
    }

    public boolean removeAll(Collection<?> collection) {
        int removeAll;
        try {
            this.lock.lock();
            int removeAll2 = super.removeAll(collection);
            removeAll = this.blockedQueue.removeAll(collection);
            return removeAll2 | removeAll;
        } finally {
            removeAll = this.lock;
            removeAll.unlock();
        }
    }

    public int size() {
        int size;
        try {
            this.lock.lock();
            int size2 = this.blockedQueue.size();
            size = super.size();
            return size2 + size;
        } finally {
            size = this.lock;
            size.unlock();
        }
    }

    public E take() throws InterruptedException {
        return get(0, null, null);
    }

    public Object[] toArray() {
        try {
            this.lock.lock();
            Object[] concatenate = concatenate(super.toArray(), this.blockedQueue.toArray());
            return concatenate;
        } finally {
            this.lock.unlock();
        }
    }

    public <T> T[] toArray(T[] tArr) {
        try {
            this.lock.lock();
            T[] concatenate = concatenate(super.toArray(tArr), this.blockedQueue.toArray(tArr));
            return concatenate;
        } finally {
            this.lock.unlock();
        }
    }
}

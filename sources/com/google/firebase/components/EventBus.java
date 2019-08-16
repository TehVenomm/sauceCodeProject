package com.google.firebase.components;

import android.support.annotation.GuardedBy;
import com.google.android.gms.common.internal.Preconditions;
import com.google.firebase.events.Event;
import com.google.firebase.events.EventHandler;
import com.google.firebase.events.Publisher;
import com.google.firebase.events.Subscriber;
import java.util.ArrayDeque;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executor;

class EventBus implements Subscriber, Publisher {
    private final Executor defaultExecutor;
    @GuardedBy("this")
    private final Map<Class<?>, ConcurrentHashMap<EventHandler<Object>, Executor>> handlerMap = new HashMap();
    @GuardedBy("this")
    private Queue<Event<?>> pendingEvents = new ArrayDeque();

    EventBus(Executor executor) {
        this.defaultExecutor = executor;
    }

    private Set<Entry<EventHandler<Object>, Executor>> getHandlers(Event<?> event) {
        Set<Entry<EventHandler<Object>, Executor>> entrySet;
        synchronized (this) {
            Map map = (Map) this.handlerMap.get(event.getType());
            entrySet = map == null ? Collections.emptySet() : map.entrySet();
        }
        return entrySet;
    }

    /* access modifiers changed from: 0000 */
    public void enablePublishingAndFlushPending() {
        Queue<Event<?>> queue = null;
        synchronized (this) {
            if (this.pendingEvents != null) {
                queue = this.pendingEvents;
                this.pendingEvents = null;
            }
        }
        if (queue != null) {
            for (Event publish : queue) {
                publish(publish);
            }
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:10:0x001c, code lost:
        if (r2.hasNext() == false) goto L_0x000e;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:11:0x001e, code lost:
        r0 = (java.util.Map.Entry) r2.next();
        ((java.util.concurrent.Executor) r0.getValue()).execute(com.google.firebase.components.EventBus$$Lambda$1.lambdaFactory$(r0, r4));
     */
    /* JADX WARNING: Code restructure failed: missing block: B:21:?, code lost:
        return;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:8:0x0010, code lost:
        r2 = getHandlers(r4).iterator();
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void publish(com.google.firebase.events.Event<?> r4) {
        /*
            r3 = this;
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r4)
            monitor-enter(r3)
            java.util.Queue<com.google.firebase.events.Event<?>> r0 = r3.pendingEvents     // Catch:{ all -> 0x0032 }
            if (r0 == 0) goto L_0x000f
            java.util.Queue<com.google.firebase.events.Event<?>> r0 = r3.pendingEvents     // Catch:{ all -> 0x0032 }
            r0.add(r4)     // Catch:{ all -> 0x0032 }
            monitor-exit(r3)     // Catch:{ all -> 0x0032 }
        L_0x000e:
            return
        L_0x000f:
            monitor-exit(r3)     // Catch:{ all -> 0x0032 }
            java.util.Set r0 = r3.getHandlers(r4)
            java.util.Iterator r2 = r0.iterator()
        L_0x0018:
            boolean r0 = r2.hasNext()
            if (r0 == 0) goto L_0x000e
            java.lang.Object r0 = r2.next()
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0
            java.lang.Object r1 = r0.getValue()
            java.util.concurrent.Executor r1 = (java.util.concurrent.Executor) r1
            java.lang.Runnable r0 = com.google.firebase.components.EventBus$$Lambda$1.lambdaFactory$(r0, r4)
            r1.execute(r0)
            goto L_0x0018
        L_0x0032:
            r0 = move-exception
            monitor-exit(r3)     // Catch:{ all -> 0x0032 }
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.components.EventBus.publish(com.google.firebase.events.Event):void");
    }

    public <T> void subscribe(Class<T> cls, EventHandler<? super T> eventHandler) {
        subscribe(cls, this.defaultExecutor, eventHandler);
    }

    public <T> void subscribe(Class<T> cls, Executor executor, EventHandler<? super T> eventHandler) {
        synchronized (this) {
            Preconditions.checkNotNull(cls);
            Preconditions.checkNotNull(eventHandler);
            Preconditions.checkNotNull(executor);
            if (!this.handlerMap.containsKey(cls)) {
                this.handlerMap.put(cls, new ConcurrentHashMap());
            }
            ((ConcurrentHashMap) this.handlerMap.get(cls)).put(eventHandler, executor);
        }
    }

    public <T> void unsubscribe(Class<T> cls, EventHandler<? super T> eventHandler) {
        synchronized (this) {
            Preconditions.checkNotNull(cls);
            Preconditions.checkNotNull(eventHandler);
            if (this.handlerMap.containsKey(cls)) {
                ConcurrentHashMap concurrentHashMap = (ConcurrentHashMap) this.handlerMap.get(cls);
                concurrentHashMap.remove(eventHandler);
                if (concurrentHashMap.isEmpty()) {
                    this.handlerMap.remove(cls);
                }
            }
        }
    }
}

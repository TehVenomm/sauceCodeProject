package com.google.firebase.components;

import com.google.android.gms.common.internal.Preconditions;
import com.google.firebase.events.Publisher;
import com.google.firebase.events.Subscriber;
import com.google.firebase.inject.Provider;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.concurrent.Executor;

public class ComponentRuntime extends AbstractComponentContainer {
    private static final Provider<Set<Object>> EMPTY_PROVIDER = ComponentRuntime$$Lambda$5.lambdaFactory$();
    private final Map<Component<?>, Lazy<?>> components = new HashMap();
    private final EventBus eventBus;
    private final Map<Class<?>, Lazy<?>> lazyInstanceMap = new HashMap();
    private final Map<Class<?>, Lazy<Set<?>>> lazySetMap = new HashMap();

    public ComponentRuntime(Executor executor, Iterable<ComponentRegistrar> iterable, Component<?>... componentArr) {
        this.eventBus = new EventBus(executor);
        ArrayList<Component> arrayList = new ArrayList<>();
        arrayList.add(Component.m441of(this.eventBus, EventBus.class, Subscriber.class, Publisher.class));
        for (ComponentRegistrar components2 : iterable) {
            arrayList.addAll(components2.getComponents());
        }
        Collections.addAll(arrayList, componentArr);
        CycleDetector.detect(arrayList);
        for (Component component : arrayList) {
            this.components.put(component, new Lazy(ComponentRuntime$$Lambda$1.lambdaFactory$(this, component)));
        }
        processInstanceComponents();
        processSetComponents();
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=java.util.Set, code=java.util.Set<com.google.firebase.components.Lazy>, for r3v0, types: [java.util.Set, java.util.Set<com.google.firebase.components.Lazy>] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static /* synthetic */ java.util.Set lambda$processSetComponents$1(java.util.Set<com.google.firebase.components.Lazy> r3) {
        /*
            java.util.HashSet r1 = new java.util.HashSet
            r1.<init>()
            java.util.Iterator r2 = r3.iterator()
        L_0x0009:
            boolean r0 = r2.hasNext()
            if (r0 == 0) goto L_0x001d
            java.lang.Object r0 = r2.next()
            com.google.firebase.components.Lazy r0 = (com.google.firebase.components.Lazy) r0
            java.lang.Object r0 = r0.get()
            r1.add(r0)
            goto L_0x0009
        L_0x001d:
            java.util.Set r0 = java.util.Collections.unmodifiableSet(r1)
            return r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.components.ComponentRuntime.lambda$processSetComponents$1(java.util.Set):java.util.Set");
    }

    private void processInstanceComponents() {
        for (Entry entry : this.components.entrySet()) {
            Component component = (Component) entry.getKey();
            if (component.isValue()) {
                Lazy lazy = (Lazy) entry.getValue();
                for (Class put : component.getProvidedInterfaces()) {
                    this.lazyInstanceMap.put(put, lazy);
                }
            }
        }
        validateDependencies();
    }

    private void processSetComponents() {
        HashMap hashMap = new HashMap();
        for (Entry entry : this.components.entrySet()) {
            Component component = (Component) entry.getKey();
            if (!component.isValue()) {
                Lazy lazy = (Lazy) entry.getValue();
                for (Class cls : component.getProvidedInterfaces()) {
                    if (!hashMap.containsKey(cls)) {
                        hashMap.put(cls, new HashSet());
                    }
                    ((Set) hashMap.get(cls)).add(lazy);
                }
            }
        }
        for (Entry entry2 : hashMap.entrySet()) {
            this.lazySetMap.put((Class) entry2.getKey(), new Lazy(ComponentRuntime$$Lambda$4.lambdaFactory$((Set) entry2.getValue())));
        }
    }

    private void validateDependencies() {
        for (Component component : this.components.keySet()) {
            Iterator it = component.getDependencies().iterator();
            while (true) {
                if (it.hasNext()) {
                    Dependency dependency = (Dependency) it.next();
                    if (dependency.isRequired() && !this.lazyInstanceMap.containsKey(dependency.getInterface())) {
                        throw new MissingDependencyException(String.format("Unsatisfied dependency for component %s: %s", new Object[]{component, dependency.getInterface()}));
                    }
                }
            }
        }
    }

    public /* bridge */ /* synthetic */ Object get(Class cls) {
        return super.get(cls);
    }

    public <T> Provider<T> getProvider(Class<T> cls) {
        Preconditions.checkNotNull(cls, "Null interface requested.");
        return (Provider) this.lazyInstanceMap.get(cls);
    }

    public void initializeEagerComponents(boolean z) {
        for (Entry entry : this.components.entrySet()) {
            Component component = (Component) entry.getKey();
            Lazy lazy = (Lazy) entry.getValue();
            if (component.isAlwaysEager() || (component.isEagerInDefaultApp() && z)) {
                lazy.get();
            }
        }
        this.eventBus.enablePublishingAndFlushPending();
    }

    public /* bridge */ /* synthetic */ Set setOf(Class cls) {
        return super.setOf(cls);
    }

    public <T> Provider<Set<T>> setOfProvider(Class<T> cls) {
        Lazy lazy = (Lazy) this.lazySetMap.get(cls);
        return lazy != null ? lazy : EMPTY_PROVIDER;
    }
}

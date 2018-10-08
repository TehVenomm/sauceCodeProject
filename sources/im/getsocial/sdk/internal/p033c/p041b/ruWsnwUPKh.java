package im.getsocial.sdk.internal.p033c.p041b;

import java.util.EnumSet;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.internal.c.b.ruWsnwUPKh */
public class ruWsnwUPKh {
    /* renamed from: a */
    protected final Map<Class<? extends KluUZYuxme>, KluUZYuxme> f1250a = new HashMap();
    /* renamed from: b */
    protected EnumSet<jMsobIMeui> f1251b;
    /* renamed from: c */
    private final Object f1252c = new Object();

    public ruWsnwUPKh(EnumSet<jMsobIMeui> enumSet) {
        this.f1251b = enumSet;
    }

    /* renamed from: a */
    public final <T extends KluUZYuxme> T m1218a(Class<T> cls) {
        KluUZYuxme kluUZYuxme;
        Object obj = null;
        synchronized (this.f1252c) {
            Object obj2 = (KluUZYuxme) this.f1250a.get(cls);
            if (obj2 == null) {
                try {
                    if (cls.getConstructors().length == 0) {
                        throw new RuntimeException("Can't instantiate [" + cls + "]. It must not have a private constructor.");
                    } else if (cls.getConstructors().length > 1) {
                        throw new RuntimeException("Can't instantiate [" + cls + "]. It must not have multiple constructors");
                    } else if (cls.getConstructors()[0].getParameterTypes().length > 0) {
                        throw new RuntimeException("Can't instantiate [" + cls + "]. Constructor must have no arguments.");
                    } else {
                        obj2 = (KluUZYuxme) cls.newInstance();
                        if (!this.f1251b.contains(jMsobIMeui.APP)) {
                            obj = 1;
                        }
                        if (obj == null && obj2.mo4351a() != jMsobIMeui.APP) {
                            throw new RuntimeException("Can not get '" + cls.getName() + "' from this RepositoryPool. Scope '" + obj2.mo4351a() + "' not initialised.");
                        } else if (this.f1251b.contains(obj2.mo4351a())) {
                            this.f1250a.put(cls, obj2);
                        } else {
                            throw new RuntimeException("Can not get '" + cls.getName() + "' from this RepositoryPool. Valid scopes are " + this.f1251b.toString());
                        }
                    }
                } catch (Throwable e) {
                    throw new RuntimeException("Can't instantiate [" + cls + "]. Repository can have only one constructor without parameters.", e);
                } catch (Throwable e2) {
                    throw new RuntimeException("IllegalAccessException on [" + cls + "]", e2);
                }
            }
            kluUZYuxme = (KluUZYuxme) cls.cast(obj2);
        }
        return kluUZYuxme;
    }

    /* renamed from: a */
    final void m1219a(ruWsnwUPKh ruwsnwupkh) {
        synchronized (this.f1252c) {
            EnumSet enumSet = ruwsnwupkh.f1251b;
            if (enumSet.contains(jMsobIMeui.APP)) {
                throw new RuntimeException("Can not commitRepositoryTransaction with Application scope.");
            }
            Iterator it = this.f1250a.keySet().iterator();
            while (it.hasNext()) {
                if (enumSet.contains(((KluUZYuxme) this.f1250a.get(it.next())).mo4351a())) {
                    it.remove();
                }
            }
            for (Entry entry : ruwsnwupkh.f1250a.entrySet()) {
                this.f1250a.put(entry.getKey(), entry.getValue());
            }
        }
    }
}

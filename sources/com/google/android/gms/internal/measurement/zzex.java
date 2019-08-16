package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzel;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.ServiceConfigurationError;
import java.util.ServiceLoader;
import java.util.logging.Level;
import java.util.logging.Logger;

abstract class zzex<T extends zzel> {
    private static final Logger logger = Logger.getLogger(zzee.class.getName());
    private static String zzahv = "com.google.protobuf.BlazeGeneratedExtensionRegistryLiteLoader";

    zzex() {
    }

    static <T extends zzel> T zzc(Class<T> cls) {
        String format;
        ClassLoader classLoader = zzex.class.getClassLoader();
        if (cls.equals(zzel.class)) {
            format = zzahv;
        } else if (!cls.getPackage().equals(zzex.class.getPackage())) {
            throw new IllegalArgumentException(cls.getName());
        } else {
            format = String.format("%s.BlazeGenerated%sLoader", new Object[]{cls.getPackage().getName(), cls.getSimpleName()});
        }
        try {
            return (zzel) cls.cast(((zzex) Class.forName(format, true, classLoader).getConstructor(new Class[0]).newInstance(new Object[0])).zzub());
        } catch (NoSuchMethodException e) {
            throw new IllegalStateException(e);
        } catch (InstantiationException e2) {
            throw new IllegalStateException(e2);
        } catch (IllegalAccessException e3) {
            throw new IllegalStateException(e3);
        } catch (InvocationTargetException e4) {
            throw new IllegalStateException(e4);
        } catch (ClassNotFoundException e5) {
            Iterator it = ServiceLoader.load(zzex.class, classLoader).iterator();
            ArrayList arrayList = new ArrayList();
            while (it.hasNext()) {
                try {
                    arrayList.add(cls.cast(((zzex) it.next()).zzub()));
                } catch (ServiceConfigurationError e6) {
                    Logger logger2 = logger;
                    Level level = Level.SEVERE;
                    String valueOf = String.valueOf(cls.getSimpleName());
                    logger2.logp(level, "com.google.protobuf.GeneratedExtensionRegistryLoader", "load", valueOf.length() != 0 ? "Unable to load ".concat(valueOf) : new String("Unable to load "), e6);
                }
            }
            if (arrayList.size() == 1) {
                return (zzel) arrayList.get(0);
            }
            if (arrayList.size() == 0) {
                return null;
            }
            try {
                return (zzel) cls.getMethod("combine", new Class[]{Collection.class}).invoke(null, new Object[]{arrayList});
            } catch (NoSuchMethodException e7) {
                throw new IllegalStateException(e7);
            } catch (IllegalAccessException e8) {
                throw new IllegalStateException(e8);
            } catch (InvocationTargetException e9) {
                throw new IllegalStateException(e9);
            }
        }
    }

    /* access modifiers changed from: protected */
    public abstract T zzub();
}

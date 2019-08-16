package android.arch.lifecycle;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;
import java.util.Map;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

@RestrictTo({Scope.LIBRARY_GROUP})
class Lifecycling {
    private static Map<Class, Constructor<? extends GenericLifecycleObserver>> sCallbackCache = new HashMap();
    private static Constructor<? extends GenericLifecycleObserver> sREFLECTIVE;

    static {
        try {
            sREFLECTIVE = ReflectiveGenericLifecycleObserver.class.getDeclaredConstructor(new Class[]{Object.class});
        } catch (NoSuchMethodException e) {
        }
    }

    Lifecycling() {
    }

    static String getAdapterName(String str) {
        return str.replace(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER, EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR) + "_LifecycleAdapter";
    }

    @NonNull
    static GenericLifecycleObserver getCallback(Object obj) {
        if (obj instanceof GenericLifecycleObserver) {
            return (GenericLifecycleObserver) obj;
        }
        try {
            Class cls = obj.getClass();
            Constructor constructor = (Constructor) sCallbackCache.get(cls);
            if (constructor != null) {
                return (GenericLifecycleObserver) constructor.newInstance(new Object[]{obj});
            }
            Constructor<? extends GenericLifecycleObserver> generatedAdapterConstructor = getGeneratedAdapterConstructor(cls);
            if (generatedAdapterConstructor == null) {
                generatedAdapterConstructor = sREFLECTIVE;
            } else if (!generatedAdapterConstructor.isAccessible()) {
                generatedAdapterConstructor.setAccessible(true);
            }
            sCallbackCache.put(cls, generatedAdapterConstructor);
            return (GenericLifecycleObserver) generatedAdapterConstructor.newInstance(new Object[]{obj});
        } catch (IllegalAccessException e) {
            throw new RuntimeException(e);
        } catch (InstantiationException e2) {
            throw new RuntimeException(e2);
        } catch (InvocationTargetException e3) {
            throw new RuntimeException(e3);
        }
    }

    @Nullable
    private static Constructor<? extends GenericLifecycleObserver> getGeneratedAdapterConstructor(Class<?> cls) {
        Package packageR = cls.getPackage();
        String str = packageR != null ? packageR.getName() : "";
        String canonicalName = cls.getCanonicalName();
        if (canonicalName == null) {
            return null;
        }
        if (!str.isEmpty()) {
            canonicalName = canonicalName.substring(str.length() + 1);
        }
        String adapterName = getAdapterName(canonicalName);
        try {
            return Class.forName(str.isEmpty() ? adapterName : str + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + adapterName).getDeclaredConstructor(new Class[]{cls});
        } catch (ClassNotFoundException e) {
            Class superclass = cls.getSuperclass();
            if (superclass != null) {
                return getGeneratedAdapterConstructor(superclass);
            }
            return null;
        } catch (NoSuchMethodException e2) {
            throw new RuntimeException(e2);
        }
    }
}

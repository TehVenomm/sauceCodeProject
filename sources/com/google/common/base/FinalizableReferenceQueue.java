package com.google.common.base;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.lang.ref.ReferenceQueue;
import java.lang.reflect.Method;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.apache.commons.lang3.ClassUtils;

public class FinalizableReferenceQueue {
    private static final String FINALIZER_CLASS_NAME = "com.google.common.base.internal.Finalizer";
    /* access modifiers changed from: private */
    public static final Logger logger = Logger.getLogger(FinalizableReferenceQueue.class.getName());
    private static final Method startFinalizer = getStartFinalizer(loadFinalizer(new SystemLoader(), new DecoupledLoader(), new DirectLoader()));
    final ReferenceQueue<Object> queue;
    final boolean threadStarted;

    static class DecoupledLoader implements FinalizerLoader {
        private static final String LOADING_ERROR = "Could not load Finalizer in its own class loader. Loading Finalizer in the current class loader instead. As a result, you will not be able to garbage collect this class loader. To support reclaiming this class loader, either resolve the underlying issue, or move Google Collections to your system class path.";

        DecoupledLoader() {
        }

        /* access modifiers changed from: 0000 */
        public URL getBaseUrl() throws IOException {
            String sb = new StringBuilder(String.valueOf(FinalizableReferenceQueue.FINALIZER_CLASS_NAME.replace(ClassUtils.PACKAGE_SEPARATOR_CHAR, '/'))).append(".class").toString();
            URL resource = getClass().getClassLoader().getResource(sb);
            if (resource == null) {
                throw new FileNotFoundException(sb);
            }
            String url = resource.toString();
            if (url.endsWith(sb)) {
                return new URL(resource, url.substring(0, url.length() - sb.length()));
            }
            throw new IOException("Unsupported path style: " + url);
        }

        public Class<?> loadFinalizer() {
            try {
                return newLoader(getBaseUrl()).loadClass(FinalizableReferenceQueue.FINALIZER_CLASS_NAME);
            } catch (Exception e) {
                FinalizableReferenceQueue.logger.log(Level.WARNING, LOADING_ERROR, e);
                return null;
            }
        }

        /* access modifiers changed from: 0000 */
        public URLClassLoader newLoader(URL url) {
            return new URLClassLoader(new URL[]{url});
        }
    }

    static class DirectLoader implements FinalizerLoader {
        DirectLoader() {
        }

        public Class<?> loadFinalizer() {
            try {
                return Class.forName(FinalizableReferenceQueue.FINALIZER_CLASS_NAME);
            } catch (ClassNotFoundException e) {
                throw new AssertionError(e);
            }
        }
    }

    interface FinalizerLoader {
        Class<?> loadFinalizer();
    }

    static class SystemLoader implements FinalizerLoader {
        SystemLoader() {
        }

        public Class<?> loadFinalizer() {
            Class<?> cls = null;
            try {
                ClassLoader systemClassLoader = ClassLoader.getSystemClassLoader();
                if (systemClassLoader == null) {
                    return cls;
                }
                try {
                    return systemClassLoader.loadClass(FinalizableReferenceQueue.FINALIZER_CLASS_NAME);
                } catch (ClassNotFoundException e) {
                    return cls;
                }
            } catch (SecurityException e2) {
                FinalizableReferenceQueue.logger.info("Not allowed to access system class loader.");
                return cls;
            }
        }
    }

    public FinalizableReferenceQueue() {
        ReferenceQueue<Object> referenceQueue;
        boolean z = true;
        try {
            referenceQueue = (ReferenceQueue) startFinalizer.invoke(null, new Object[]{FinalizableReference.class, this});
        } catch (IllegalAccessException e) {
            throw new AssertionError(e);
        } catch (Throwable th) {
            logger.log(Level.INFO, "Failed to start reference finalizer thread. Reference cleanup will only occur when new references are created.", th);
            referenceQueue = new ReferenceQueue<>();
            z = false;
        }
        this.queue = referenceQueue;
        this.threadStarted = z;
    }

    static Method getStartFinalizer(Class<?> cls) {
        try {
            return cls.getMethod("startFinalizer", new Class[]{Class.class, Object.class});
        } catch (NoSuchMethodException e) {
            throw new AssertionError(e);
        }
    }

    private static Class<?> loadFinalizer(FinalizerLoader... finalizerLoaderArr) {
        for (FinalizerLoader loadFinalizer : finalizerLoaderArr) {
            Class<?> loadFinalizer2 = loadFinalizer.loadFinalizer();
            if (loadFinalizer2 != null) {
                return loadFinalizer2;
            }
        }
        throw new AssertionError();
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: CFG modification limit reached, blocks count: 112 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void cleanUp() {
        /*
            r4 = this;
            boolean r0 = r4.threadStarted
            if (r0 == 0) goto L_0x000d
        L_0x0004:
            return
        L_0x0005:
            r0.clear()
            com.google.common.base.FinalizableReference r0 = (com.google.common.base.FinalizableReference) r0     // Catch:{ Throwable -> 0x0016 }
            r0.finalizeReferent()     // Catch:{ Throwable -> 0x0016 }
        L_0x000d:
            java.lang.ref.ReferenceQueue<java.lang.Object> r0 = r4.queue
            java.lang.ref.Reference r0 = r0.poll()
            if (r0 != 0) goto L_0x0005
            goto L_0x0004
        L_0x0016:
            r0 = move-exception
            java.util.logging.Logger r1 = logger
            java.util.logging.Level r2 = java.util.logging.Level.SEVERE
            java.lang.String r3 = "Error cleaning up after reference."
            r1.log(r2, r3, r0)
            goto L_0x000d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.common.base.FinalizableReferenceQueue.cleanUp():void");
    }
}

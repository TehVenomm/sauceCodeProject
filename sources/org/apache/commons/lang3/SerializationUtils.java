package org.apache.commons.lang3;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.io.ObjectStreamClass;
import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

public class SerializationUtils {

    static class ClassLoaderAwareObjectInputStream extends ObjectInputStream {
        private static final Map<String, Class<?>> primitiveTypes = new HashMap();
        private final ClassLoader classLoader;

        public ClassLoaderAwareObjectInputStream(InputStream inputStream, ClassLoader classLoader2) throws IOException {
            super(inputStream);
            this.classLoader = classLoader2;
            primitiveTypes.put("byte", Byte.TYPE);
            primitiveTypes.put("short", Short.TYPE);
            primitiveTypes.put("int", Integer.TYPE);
            primitiveTypes.put("long", Long.TYPE);
            primitiveTypes.put("float", Float.TYPE);
            primitiveTypes.put("double", Double.TYPE);
            primitiveTypes.put("boolean", Boolean.TYPE);
            primitiveTypes.put("char", Character.TYPE);
            primitiveTypes.put("void", Void.TYPE);
        }

        /* access modifiers changed from: protected */
        public Class<?> resolveClass(ObjectStreamClass objectStreamClass) throws IOException, ClassNotFoundException {
            String name = objectStreamClass.getName();
            try {
                return Class.forName(name, false, this.classLoader);
            } catch (ClassNotFoundException e) {
                try {
                    return Class.forName(name, false, Thread.currentThread().getContextClassLoader());
                } catch (ClassNotFoundException e2) {
                    ClassNotFoundException classNotFoundException = e2;
                    Class<?> cls = (Class) primitiveTypes.get(name);
                    if (cls != null) {
                        return cls;
                    }
                    throw classNotFoundException;
                }
            }
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:22:0x003d A[SYNTHETIC, Splitter:B:22:0x003d] */
    /* JADX WARNING: Unknown top exception splitter block from list: {B:16:0x0031=Splitter:B:16:0x0031, B:27:0x0043=Splitter:B:27:0x0043} */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static <T extends java.io.Serializable> T clone(T r4) {
        /*
            r0 = 0
            if (r4 != 0) goto L_0x0004
        L_0x0003:
            return r0
        L_0x0004:
            byte[] r1 = serialize(r4)
            java.io.ByteArrayInputStream r3 = new java.io.ByteArrayInputStream
            r3.<init>(r1)
            org.apache.commons.lang3.SerializationUtils$ClassLoaderAwareObjectInputStream r2 = new org.apache.commons.lang3.SerializationUtils$ClassLoaderAwareObjectInputStream     // Catch:{ ClassNotFoundException -> 0x002f, IOException -> 0x0041, all -> 0x0054 }
            java.lang.Class r1 = r4.getClass()     // Catch:{ ClassNotFoundException -> 0x002f, IOException -> 0x0041, all -> 0x0054 }
            java.lang.ClassLoader r1 = r1.getClassLoader()     // Catch:{ ClassNotFoundException -> 0x002f, IOException -> 0x0041, all -> 0x0054 }
            r2.<init>(r3, r1)     // Catch:{ ClassNotFoundException -> 0x002f, IOException -> 0x0041, all -> 0x0054 }
            java.lang.Object r0 = r2.readObject()     // Catch:{ ClassNotFoundException -> 0x005a, IOException -> 0x0057 }
            java.io.Serializable r0 = (java.io.Serializable) r0     // Catch:{ ClassNotFoundException -> 0x005a, IOException -> 0x0057 }
            if (r2 == 0) goto L_0x0003
            r2.close()     // Catch:{ IOException -> 0x0026 }
            goto L_0x0003
        L_0x0026:
            r0 = move-exception
            org.apache.commons.lang3.SerializationException r1 = new org.apache.commons.lang3.SerializationException
            java.lang.String r2 = "IOException on closing cloned object data InputStream."
            r1.<init>(r2, r0)
            throw r1
        L_0x002f:
            r1 = move-exception
            r2 = r0
        L_0x0031:
            org.apache.commons.lang3.SerializationException r0 = new org.apache.commons.lang3.SerializationException     // Catch:{ all -> 0x0039 }
            java.lang.String r3 = "ClassNotFoundException while reading cloned object data"
            r0.<init>(r3, r1)     // Catch:{ all -> 0x0039 }
            throw r0     // Catch:{ all -> 0x0039 }
        L_0x0039:
            r0 = move-exception
            r1 = r0
        L_0x003b:
            if (r2 == 0) goto L_0x0040
            r2.close()     // Catch:{ IOException -> 0x004b }
        L_0x0040:
            throw r1
        L_0x0041:
            r1 = move-exception
            r2 = r0
        L_0x0043:
            org.apache.commons.lang3.SerializationException r0 = new org.apache.commons.lang3.SerializationException     // Catch:{ all -> 0x0039 }
            java.lang.String r3 = "IOException while reading cloned object data"
            r0.<init>(r3, r1)     // Catch:{ all -> 0x0039 }
            throw r0     // Catch:{ all -> 0x0039 }
        L_0x004b:
            r0 = move-exception
            org.apache.commons.lang3.SerializationException r1 = new org.apache.commons.lang3.SerializationException
            java.lang.String r2 = "IOException on closing cloned object data InputStream."
            r1.<init>(r2, r0)
            throw r1
        L_0x0054:
            r1 = move-exception
            r2 = r0
            goto L_0x003b
        L_0x0057:
            r0 = move-exception
            r1 = r0
            goto L_0x0043
        L_0x005a:
            r0 = move-exception
            r1 = r0
            goto L_0x0031
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.SerializationUtils.clone(java.io.Serializable):java.io.Serializable");
    }

    public static <T extends Serializable> T roundtrip(T t) {
        return (Serializable) deserialize(serialize(t));
    }

    /* JADX WARNING: Removed duplicated region for block: B:18:0x0024 A[SYNTHETIC, Splitter:B:18:0x0024] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static void serialize(java.io.Serializable r3, java.io.OutputStream r4) {
        /*
            if (r4 != 0) goto L_0x000a
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "The OutputStream must not be null"
            r0.<init>(r1)
            throw r0
        L_0x000a:
            r2 = 0
            java.io.ObjectOutputStream r1 = new java.io.ObjectOutputStream     // Catch:{ IOException -> 0x0019, all -> 0x002c }
            r1.<init>(r4)     // Catch:{ IOException -> 0x0019, all -> 0x002c }
            r1.writeObject(r3)     // Catch:{ IOException -> 0x002f }
            if (r1 == 0) goto L_0x0018
            r1.close()     // Catch:{ IOException -> 0x0028 }
        L_0x0018:
            return
        L_0x0019:
            r0 = move-exception
            r1 = r2
        L_0x001b:
            org.apache.commons.lang3.SerializationException r2 = new org.apache.commons.lang3.SerializationException     // Catch:{ all -> 0x0021 }
            r2.<init>(r0)     // Catch:{ all -> 0x0021 }
            throw r2     // Catch:{ all -> 0x0021 }
        L_0x0021:
            r0 = move-exception
        L_0x0022:
            if (r1 == 0) goto L_0x0027
            r1.close()     // Catch:{ IOException -> 0x002a }
        L_0x0027:
            throw r0
        L_0x0028:
            r0 = move-exception
            goto L_0x0018
        L_0x002a:
            r1 = move-exception
            goto L_0x0027
        L_0x002c:
            r0 = move-exception
            r1 = r2
            goto L_0x0022
        L_0x002f:
            r0 = move-exception
            goto L_0x001b
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.SerializationUtils.serialize(java.io.Serializable, java.io.OutputStream):void");
    }

    public static byte[] serialize(Serializable serializable) {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream(512);
        serialize(serializable, byteArrayOutputStream);
        return byteArrayOutputStream.toByteArray();
    }

    /* JADX WARNING: Removed duplicated region for block: B:20:0x0026 A[SYNTHETIC, Splitter:B:20:0x0026] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static <T> T deserialize(java.io.InputStream r3) {
        /*
            if (r3 != 0) goto L_0x000a
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "The InputStream must not be null"
            r0.<init>(r1)
            throw r0
        L_0x000a:
            r2 = 0
            java.io.ObjectInputStream r1 = new java.io.ObjectInputStream     // Catch:{ ClassCastException -> 0x001a, ClassNotFoundException -> 0x002a, IOException -> 0x0033 }
            r1.<init>(r3)     // Catch:{ ClassCastException -> 0x001a, ClassNotFoundException -> 0x002a, IOException -> 0x0033 }
            java.lang.Object r0 = r1.readObject()     // Catch:{ ClassCastException -> 0x0044, ClassNotFoundException -> 0x0041, IOException -> 0x003e }
            if (r1 == 0) goto L_0x0019
            r1.close()     // Catch:{ IOException -> 0x003a }
        L_0x0019:
            return r0
        L_0x001a:
            r0 = move-exception
            r1 = r2
        L_0x001c:
            org.apache.commons.lang3.SerializationException r2 = new org.apache.commons.lang3.SerializationException     // Catch:{ all -> 0x0022 }
            r2.<init>(r0)     // Catch:{ all -> 0x0022 }
            throw r2     // Catch:{ all -> 0x0022 }
        L_0x0022:
            r0 = move-exception
            r2 = r1
        L_0x0024:
            if (r2 == 0) goto L_0x0029
            r2.close()     // Catch:{ IOException -> 0x003c }
        L_0x0029:
            throw r0
        L_0x002a:
            r0 = move-exception
        L_0x002b:
            org.apache.commons.lang3.SerializationException r1 = new org.apache.commons.lang3.SerializationException     // Catch:{ all -> 0x0031 }
            r1.<init>(r0)     // Catch:{ all -> 0x0031 }
            throw r1     // Catch:{ all -> 0x0031 }
        L_0x0031:
            r0 = move-exception
            goto L_0x0024
        L_0x0033:
            r0 = move-exception
        L_0x0034:
            org.apache.commons.lang3.SerializationException r1 = new org.apache.commons.lang3.SerializationException     // Catch:{ all -> 0x0031 }
            r1.<init>(r0)     // Catch:{ all -> 0x0031 }
            throw r1     // Catch:{ all -> 0x0031 }
        L_0x003a:
            r1 = move-exception
            goto L_0x0019
        L_0x003c:
            r1 = move-exception
            goto L_0x0029
        L_0x003e:
            r0 = move-exception
            r2 = r1
            goto L_0x0034
        L_0x0041:
            r0 = move-exception
            r2 = r1
            goto L_0x002b
        L_0x0044:
            r0 = move-exception
            goto L_0x001c
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.SerializationUtils.deserialize(java.io.InputStream):java.lang.Object");
    }

    public static <T> T deserialize(byte[] bArr) {
        if (bArr != null) {
            return deserialize((InputStream) new ByteArrayInputStream(bArr));
        }
        throw new IllegalArgumentException("The byte[] must not be null");
    }
}

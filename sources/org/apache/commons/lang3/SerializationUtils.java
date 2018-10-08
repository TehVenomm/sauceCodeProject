package org.apache.commons.lang3;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.ObjectStreamClass;
import java.io.OutputStream;
import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

public class SerializationUtils {

    static class ClassLoaderAwareObjectInputStream extends ObjectInputStream {
        private static final Map<String, Class<?>> primitiveTypes = new HashMap();
        private final ClassLoader classLoader;

        public ClassLoaderAwareObjectInputStream(InputStream inputStream, ClassLoader classLoader) throws IOException {
            super(inputStream);
            this.classLoader = classLoader;
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

        protected Class<?> resolveClass(ObjectStreamClass objectStreamClass) throws IOException, ClassNotFoundException {
            Class<?> cls;
            String name = objectStreamClass.getName();
            try {
                cls = Class.forName(name, false, this.classLoader);
            } catch (ClassNotFoundException e) {
                try {
                    cls = Class.forName(name, false, Thread.currentThread().getContextClassLoader());
                } catch (ClassNotFoundException e2) {
                    ClassNotFoundException classNotFoundException = e2;
                    cls = (Class) primitiveTypes.get(name);
                    if (cls == null) {
                        throw classNotFoundException;
                    }
                }
            }
            return cls;
        }
    }

    public static <T extends Serializable> T clone(T t) {
        Throwable e;
        Throwable th;
        T t2 = null;
        if (t != null) {
            ClassLoaderAwareObjectInputStream classLoaderAwareObjectInputStream;
            try {
                classLoaderAwareObjectInputStream = new ClassLoaderAwareObjectInputStream(new ByteArrayInputStream(serialize(t)), t.getClass().getClassLoader());
                try {
                    Serializable serializable = (Serializable) classLoaderAwareObjectInputStream.readObject();
                    if (classLoaderAwareObjectInputStream != null) {
                        try {
                            classLoaderAwareObjectInputStream.close();
                        } catch (Throwable e2) {
                            throw new SerializationException("IOException on closing cloned object data InputStream.", e2);
                        }
                    }
                } catch (ClassNotFoundException e3) {
                    e2 = e3;
                    try {
                        throw new SerializationException("ClassNotFoundException while reading cloned object data", e2);
                    } catch (Throwable th2) {
                        e2 = th2;
                        if (classLoaderAwareObjectInputStream != null) {
                            try {
                                classLoaderAwareObjectInputStream.close();
                            } catch (Throwable e22) {
                                throw new SerializationException("IOException on closing cloned object data InputStream.", e22);
                            }
                        }
                        throw e22;
                    }
                } catch (IOException e4) {
                    e22 = e4;
                    throw new SerializationException("IOException while reading cloned object data", e22);
                }
            } catch (Throwable e5) {
                th = e5;
                classLoaderAwareObjectInputStream = null;
                e22 = th;
                throw new SerializationException("ClassNotFoundException while reading cloned object data", e22);
            } catch (Throwable e52) {
                th = e52;
                classLoaderAwareObjectInputStream = null;
                e22 = th;
                throw new SerializationException("IOException while reading cloned object data", e22);
            } catch (Throwable e522) {
                th = e522;
                classLoaderAwareObjectInputStream = null;
                e22 = th;
                if (classLoaderAwareObjectInputStream != null) {
                    classLoaderAwareObjectInputStream.close();
                }
                throw e22;
            }
        }
        return t2;
    }

    public static <T extends Serializable> T roundtrip(T t) {
        return (Serializable) deserialize(serialize(t));
    }

    public static void serialize(Serializable serializable, OutputStream outputStream) {
        Throwable e;
        if (outputStream == null) {
            throw new IllegalArgumentException("The OutputStream must not be null");
        }
        ObjectOutputStream objectOutputStream;
        try {
            objectOutputStream = new ObjectOutputStream(outputStream);
            try {
                objectOutputStream.writeObject(serializable);
                if (objectOutputStream != null) {
                    try {
                        objectOutputStream.close();
                    } catch (IOException e2) {
                    }
                }
            } catch (IOException e3) {
                e = e3;
                try {
                    throw new SerializationException(e);
                } catch (Throwable th) {
                    e = th;
                    if (objectOutputStream != null) {
                        try {
                            objectOutputStream.close();
                        } catch (IOException e4) {
                        }
                    }
                    throw e;
                }
            }
        } catch (IOException e5) {
            e = e5;
            objectOutputStream = null;
            throw new SerializationException(e);
        } catch (Throwable th2) {
            e = th2;
            objectOutputStream = null;
            if (objectOutputStream != null) {
                objectOutputStream.close();
            }
            throw e;
        }
    }

    public static byte[] serialize(Serializable serializable) {
        OutputStream byteArrayOutputStream = new ByteArrayOutputStream(512);
        serialize(serializable, byteArrayOutputStream);
        return byteArrayOutputStream.toByteArray();
    }

    public static <T> T deserialize(InputStream inputStream) {
        Throwable e;
        if (inputStream == null) {
            throw new IllegalArgumentException("The InputStream must not be null");
        }
        ObjectInputStream objectInputStream = null;
        ObjectInputStream objectInputStream2;
        try {
            objectInputStream2 = new ObjectInputStream(inputStream);
            try {
                T readObject = objectInputStream2.readObject();
                if (objectInputStream2 != null) {
                    try {
                        objectInputStream2.close();
                    } catch (IOException e2) {
                    }
                }
                return readObject;
            } catch (ClassCastException e3) {
                e = e3;
                try {
                    throw new SerializationException(e);
                } catch (Throwable th) {
                    e = th;
                    objectInputStream = objectInputStream2;
                    if (objectInputStream != null) {
                        try {
                            objectInputStream.close();
                        } catch (IOException e4) {
                        }
                    }
                    throw e;
                }
            } catch (ClassNotFoundException e5) {
                e = e5;
                objectInputStream = objectInputStream2;
                throw new SerializationException(e);
            } catch (IOException e6) {
                e = e6;
                objectInputStream = objectInputStream2;
                throw new SerializationException(e);
            }
        } catch (ClassCastException e7) {
            e = e7;
            objectInputStream2 = null;
            throw new SerializationException(e);
        } catch (ClassNotFoundException e8) {
            e = e8;
            throw new SerializationException(e);
        } catch (IOException e9) {
            e = e9;
            throw new SerializationException(e);
        } catch (Throwable th2) {
            e = th2;
            if (objectInputStream != null) {
                objectInputStream.close();
            }
            throw e;
        }
    }

    public static <T> T deserialize(byte[] bArr) {
        if (bArr != null) {
            return deserialize(new ByteArrayInputStream(bArr));
        }
        throw new IllegalArgumentException("The byte[] must not be null");
    }
}

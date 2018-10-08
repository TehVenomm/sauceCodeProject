package im.getsocial.sdk.ui.internal.p131d.p133b;

import im.getsocial.p015a.p016a.pdwpUtZXDT;
import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

/* renamed from: im.getsocial.sdk.ui.internal.d.b.upgqDBbsrL */
public final class upgqDBbsrL {
    /* renamed from: a */
    private static final Map<Class, jjbQypPegg> f2919a = new HashMap();

    private upgqDBbsrL() {
    }

    /* renamed from: a */
    public static <T> T m3218a(pdwpUtZXDT pdwputzxdt, Class<T> cls) {
        return new upgqDBbsrL().m3219a((Object) pdwputzxdt, (Class) cls);
    }

    /* renamed from: a */
    private Object m3219a(Object obj, Class cls) {
        if (cls == Object.class) {
            return obj;
        }
        Set hashSet = new HashSet();
        hashSet.add(Boolean.class);
        hashSet.add(Character.class);
        hashSet.add(Byte.class);
        hashSet.add(Short.class);
        hashSet.add(Integer.class);
        hashSet.add(Long.class);
        hashSet.add(Float.class);
        hashSet.add(Double.class);
        hashSet.add(Void.class);
        if (hashSet.contains(cls) || cls.isPrimitive()) {
            if (cls == Boolean.TYPE || cls == Boolean.class) {
                return obj == null ? Boolean.valueOf(false) : obj;
            } else {
                if (obj == null) {
                    return Integer.valueOf(0);
                }
                Number number = (Number) obj;
                return (cls == Integer.TYPE || cls == Integer.class) ? Integer.valueOf(number.intValue()) : (cls == Double.TYPE || cls == Double.class) ? Double.valueOf(number.doubleValue()) : (cls == Long.TYPE || cls == Long.class) ? Long.valueOf(number.longValue()) : (cls == Float.TYPE || cls == Float.class) ? Float.valueOf(number.floatValue()) : number;
            }
        } else if (cls == String.class) {
            return obj;
        } else {
            if (f2919a.containsKey(cls)) {
                return ((jjbQypPegg) f2919a.get(cls)).mo4726a(obj);
            }
            if (obj == null) {
                return null;
            }
            if (cls.isEnum()) {
                return Enum.valueOf(cls, (String) obj);
            }
            Object newInstance = cls.newInstance();
            m3221a(newInstance, (pdwpUtZXDT) obj);
            return newInstance;
        }
    }

    /* renamed from: a */
    public static <T> void m3220a(Class<T> cls, jjbQypPegg<T> jjbqyppegg) {
        f2919a.put(cls, jjbqyppegg);
    }

    /* renamed from: a */
    private void m3221a(Object obj, pdwpUtZXDT pdwputzxdt) {
        for (Field field : obj.getClass().getDeclaredFields()) {
            cjrhisSQCL cjrhissqcl = (cjrhisSQCL) field.getAnnotation(cjrhisSQCL.class);
            if (cjrhissqcl != null) {
                Object obj2 = pdwputzxdt.get(cjrhissqcl.m3216a());
                try {
                    Object a = m3219a(obj2, field.getType());
                    boolean isAccessible = field.isAccessible();
                    field.setAccessible(true);
                    try {
                        field.set(obj, a);
                        field.setAccessible(isAccessible);
                    } catch (Throwable e) {
                        throw new RuntimeException(e);
                    } catch (Throwable th) {
                        field.setAccessible(isAccessible);
                    }
                } catch (Throwable e2) {
                    throw new RuntimeException("Failed to parse " + field.getName() + " of " + obj.getClass() + " having: " + obj2, e2);
                }
            }
        }
    }
}

package im.getsocial.sdk.internal.p033c.p041b.p050a;

import im.getsocial.sdk.internal.p033c.p041b.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import java.lang.annotation.Annotation;
import java.lang.reflect.Constructor;

/* renamed from: im.getsocial.sdk.internal.c.b.a.upgqDBbsrL */
public final class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f1232a = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);

    /* renamed from: a */
    private static <T> T m1184a(Constructor<?> constructor, Object... objArr) {
        boolean isAccessible = constructor.isAccessible();
        constructor.setAccessible(true);
        try {
            T newInstance = constructor.newInstance(objArr);
            constructor.setAccessible(isAccessible);
            return newInstance;
        } catch (Throwable e) {
            f1232a.mo4389a(e);
            throw new RuntimeException("Can not instantiate [" + constructor.getDeclaringClass() + "] :" + e);
        } catch (Throwable th) {
            constructor.setAccessible(isAccessible);
        }
    }

    /* renamed from: a */
    private static <T> Constructor<?> m1185a(Class<T> cls) {
        try {
            return cls.getConstructor(new Class[0]);
        } catch (NoSuchMethodException e) {
            return null;
        }
    }

    /* renamed from: a */
    private static Object[] m1186a(Constructor<?> constructor, pdwpUtZXDT pdwputzxdt) {
        Class[] parameterTypes = constructor.getParameterTypes();
        Annotation[][] parameterAnnotations = constructor.getParameterAnnotations();
        Object[] objArr = new Object[parameterTypes.length];
        for (int i = 0; i < parameterTypes.length; i++) {
            KSZKMmRWhZ kSZKMmRWhZ = null;
            Annotation[] annotationArr = parameterAnnotations[i];
            int length = annotationArr.length;
            int i2 = 0;
            while (i2 < length) {
                Annotation annotation = annotationArr[i2];
                i2++;
                kSZKMmRWhZ = annotation instanceof KSZKMmRWhZ ? (KSZKMmRWhZ) annotation : kSZKMmRWhZ;
            }
            objArr[i] = pdwputzxdt.m1206a(parameterTypes[i], kSZKMmRWhZ);
        }
        return objArr;
    }

    /* renamed from: a */
    public final <T> T mo4378a(Class<T> cls, pdwpUtZXDT pdwputzxdt) {
        Constructor[] declaredConstructors = cls.getDeclaredConstructors();
        Constructor constructor = null;
        int length = declaredConstructors.length;
        int i = 0;
        while (i < length) {
            Constructor constructor2 = declaredConstructors[i];
            if (constructor2.getAnnotation(XdbacJlTDQ.class) == null) {
                constructor2 = constructor;
            } else if (constructor != null) {
                throw new RuntimeException("Can't instantiate [" + cls + "]. It must not have multiple injectable constructors");
            }
            i++;
            constructor = constructor2;
        }
        if (constructor == null) {
            constructor = upgqDBbsrL.m1185a(cls);
        }
        if (constructor != null) {
            return upgqDBbsrL.m1184a(constructor, upgqDBbsrL.m1186a(constructor, pdwputzxdt));
        }
        throw new RuntimeException("Can't instantiate [" + cls + "]. It must have exactly one injectable constructor.");
    }
}

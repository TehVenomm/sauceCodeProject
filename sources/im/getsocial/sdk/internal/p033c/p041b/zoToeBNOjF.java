package im.getsocial.sdk.internal.p033c.p041b;

import java.lang.reflect.Field;

/* renamed from: im.getsocial.sdk.internal.c.b.zoToeBNOjF */
public class zoToeBNOjF {
    /* renamed from: a */
    private final pdwpUtZXDT f1233a;

    public zoToeBNOjF(pdwpUtZXDT pdwputzxdt) {
        this.f1233a = pdwputzxdt;
    }

    /* renamed from: a */
    private void m1188a(Object obj, Class cls) {
        while (cls != Object.class) {
            for (Field field : cls.getDeclaredFields()) {
                if (field.getAnnotation(XdbacJlTDQ.class) != null) {
                    boolean z = field.getAnnotation(qZypgoeblR.class) != null;
                    boolean isAccessible = field.isAccessible();
                    field.setAccessible(true);
                    try {
                        Object a = this.f1233a.m1206a(field.getType(), (KSZKMmRWhZ) field.getAnnotation(KSZKMmRWhZ.class));
                        if (a != null) {
                            field.set(obj, a);
                        }
                        field.setAccessible(isAccessible);
                    } catch (RuntimeException e) {
                        if (z) {
                            field.setAccessible(isAccessible);
                        } else {
                            throw e;
                        }
                    } catch (Throwable e2) {
                        throw new RuntimeException(e2);
                    } catch (Throwable th) {
                        field.setAccessible(isAccessible);
                    }
                }
            }
            cls = cls.getSuperclass();
        }
    }

    /* renamed from: a */
    public void mo4379a(Object obj) {
        m1188a(obj, obj.getClass());
    }
}

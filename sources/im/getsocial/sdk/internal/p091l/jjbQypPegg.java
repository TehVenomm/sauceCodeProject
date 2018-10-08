package im.getsocial.sdk.internal.p091l;

import java.lang.reflect.Method;

/* renamed from: im.getsocial.sdk.internal.l.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static upgqDBbsrL m2092a() {
        int i = 0;
        Method[] declaredMethods = Class.forName("im.getsocial.sdk.ui.GetSocialUi").getDeclaredMethods();
        int length = declaredMethods.length;
        while (i < length) {
            Method method = declaredMethods[i];
            boolean isAccessible = method.isAccessible();
            method.setAccessible(true);
            try {
                if (upgqDBbsrL.class.isAssignableFrom(method.getReturnType())) {
                    upgqDBbsrL upgqdbbsrl = (upgqDBbsrL) method.invoke(null, new Object[0]);
                    method.setAccessible(isAccessible);
                    return upgqdbbsrl;
                }
                method.setAccessible(isAccessible);
                i++;
            } catch (Throwable th) {
                method.setAccessible(isAccessible);
                throw th;
            }
        }
        throw new ClassNotFoundException("No class for UiReflectionHelper");
    }
}

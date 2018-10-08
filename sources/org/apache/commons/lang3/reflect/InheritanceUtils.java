package org.apache.commons.lang3.reflect;

import org.apache.commons.lang3.BooleanUtils;

public class InheritanceUtils {
    public static int distance(Class<?> cls, Class<?> cls2) {
        if (cls == null || cls2 == null) {
            return -1;
        }
        if (cls.equals(cls2)) {
            return 0;
        }
        Class superclass = cls.getSuperclass();
        int toInteger = BooleanUtils.toInteger(cls2.equals(superclass));
        if (toInteger == 1) {
            return toInteger;
        }
        toInteger += distance(superclass, cls2);
        if (toInteger > 0) {
            return toInteger + 1;
        }
        return -1;
    }
}

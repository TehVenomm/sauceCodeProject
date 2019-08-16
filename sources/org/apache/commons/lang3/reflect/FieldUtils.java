package org.apache.commons.lang3.reflect;

import java.lang.annotation.Annotation;
import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.util.ArrayList;
import java.util.List;
import org.apache.commons.lang3.ClassUtils;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.Validate;

public class FieldUtils {
    public static Field getField(Class<?> cls, String str) {
        Field field = getField(cls, str, false);
        MemberUtils.setAccessibleWorkaround(field);
        return field;
    }

    public static Field getField(Class<?> cls, String str, boolean z) {
        Field field;
        Field field2;
        Validate.isTrue(cls != null, "The class must not be null", new Object[0]);
        Validate.isTrue(StringUtils.isNotBlank(str), "The field name must not be blank/empty", new Object[0]);
        Class<?> cls2 = cls;
        while (true) {
            if (cls2 != null) {
                try {
                    field = cls2.getDeclaredField(str);
                    if (Modifier.isPublic(field.getModifiers())) {
                        break;
                    } else if (z) {
                        field.setAccessible(true);
                        break;
                    } else {
                        cls2 = cls2.getSuperclass();
                    }
                } catch (NoSuchFieldException e) {
                }
            } else {
                field = null;
                for (Class field3 : ClassUtils.getAllInterfaces(cls)) {
                    try {
                        field2 = field3.getField(str);
                        Validate.isTrue(field == null, "Reference to field %s is ambiguous relative to %s; a matching field exists on two or more implemented interfaces.", str, cls);
                    } catch (NoSuchFieldException e2) {
                        field2 = field;
                    }
                    field = field2;
                }
            }
        }
        return field;
    }

    public static Field getDeclaredField(Class<?> cls, String str) {
        return getDeclaredField(cls, str, false);
    }

    public static Field getDeclaredField(Class<?> cls, String str, boolean z) {
        boolean z2 = true;
        if (cls == null) {
            z2 = false;
        }
        Validate.isTrue(z2, "The class must not be null", new Object[0]);
        Validate.isTrue(StringUtils.isNotBlank(str), "The field name must not be blank/empty", new Object[0]);
        try {
            Field declaredField = cls.getDeclaredField(str);
            if (MemberUtils.isAccessible(declaredField)) {
                return declaredField;
            }
            if (!z) {
                return null;
            }
            declaredField.setAccessible(true);
            return declaredField;
        } catch (NoSuchFieldException e) {
            return null;
        }
    }

    public static Field[] getAllFields(Class<?> cls) {
        List allFieldsList = getAllFieldsList(cls);
        return (Field[]) allFieldsList.toArray(new Field[allFieldsList.size()]);
    }

    public static List<Field> getAllFieldsList(Class<?> cls) {
        Validate.isTrue(cls != null, "The class must not be null", new Object[0]);
        ArrayList arrayList = new ArrayList();
        while (cls != null) {
            for (Field add : cls.getDeclaredFields()) {
                arrayList.add(add);
            }
            cls = cls.getSuperclass();
        }
        return arrayList;
    }

    public static Field[] getFieldsWithAnnotation(Class<?> cls, Class<? extends Annotation> cls2) {
        List fieldsListWithAnnotation = getFieldsListWithAnnotation(cls, cls2);
        return (Field[]) fieldsListWithAnnotation.toArray(new Field[fieldsListWithAnnotation.size()]);
    }

    public static List<Field> getFieldsListWithAnnotation(Class<?> cls, Class<? extends Annotation> cls2) {
        Validate.isTrue(cls2 != null, "The annotation class must not be null", new Object[0]);
        List<Field> allFieldsList = getAllFieldsList(cls);
        ArrayList arrayList = new ArrayList();
        for (Field field : allFieldsList) {
            if (field.getAnnotation(cls2) != null) {
                arrayList.add(field);
            }
        }
        return arrayList;
    }

    public static Object readStaticField(Field field) throws IllegalAccessException {
        return readStaticField(field, false);
    }

    public static Object readStaticField(Field field, boolean z) throws IllegalAccessException {
        boolean z2;
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "The field must not be null", new Object[0]);
        Validate.isTrue(Modifier.isStatic(field.getModifiers()), "The field '%s' is not static", field.getName());
        return readField(field, (Object) null, z);
    }

    public static Object readStaticField(Class<?> cls, String str) throws IllegalAccessException {
        return readStaticField(cls, str, false);
    }

    public static Object readStaticField(Class<?> cls, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Field field = getField(cls, str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate field '%s' on %s", str, cls);
        return readStaticField(field, false);
    }

    public static Object readDeclaredStaticField(Class<?> cls, String str) throws IllegalAccessException {
        return readDeclaredStaticField(cls, str, false);
    }

    public static Object readDeclaredStaticField(Class<?> cls, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Field declaredField = getDeclaredField(cls, str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", cls.getName(), str);
        return readStaticField(declaredField, false);
    }

    public static Object readField(Field field, Object obj) throws IllegalAccessException {
        return readField(field, obj, false);
    }

    public static Object readField(Field field, Object obj, boolean z) throws IllegalAccessException {
        Validate.isTrue(field != null, "The field must not be null", new Object[0]);
        if (!z || field.isAccessible()) {
            MemberUtils.setAccessibleWorkaround(field);
        } else {
            field.setAccessible(true);
        }
        return field.get(obj);
    }

    public static Object readField(Object obj, String str) throws IllegalAccessException {
        return readField(obj, str, false);
    }

    public static Object readField(Object obj, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Class cls = obj.getClass();
        Field field = getField(cls, str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate field %s on %s", str, cls);
        return readField(field, obj, false);
    }

    public static Object readDeclaredField(Object obj, String str) throws IllegalAccessException {
        return readDeclaredField(obj, str, false);
    }

    public static Object readDeclaredField(Object obj, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Class cls = obj.getClass();
        Field declaredField = getDeclaredField(cls, str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", cls, str);
        return readField(declaredField, obj, false);
    }

    public static void writeStaticField(Field field, Object obj) throws IllegalAccessException {
        writeStaticField(field, obj, false);
    }

    public static void writeStaticField(Field field, Object obj, boolean z) throws IllegalAccessException {
        boolean z2;
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "The field must not be null", new Object[0]);
        Validate.isTrue(Modifier.isStatic(field.getModifiers()), "The field %s.%s is not static", field.getDeclaringClass().getName(), field.getName());
        writeField(field, (Object) null, obj, z);
    }

    public static void writeStaticField(Class<?> cls, String str, Object obj) throws IllegalAccessException {
        writeStaticField(cls, str, obj, false);
    }

    public static void writeStaticField(Class<?> cls, String str, Object obj, boolean z) throws IllegalAccessException {
        boolean z2;
        Field field = getField(cls, str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate field %s on %s", str, cls);
        writeStaticField(field, obj, false);
    }

    public static void writeDeclaredStaticField(Class<?> cls, String str, Object obj) throws IllegalAccessException {
        writeDeclaredStaticField(cls, str, obj, false);
    }

    public static void writeDeclaredStaticField(Class<?> cls, String str, Object obj, boolean z) throws IllegalAccessException {
        boolean z2;
        Field declaredField = getDeclaredField(cls, str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", cls.getName(), str);
        writeField(declaredField, (Object) null, obj, false);
    }

    public static void writeField(Field field, Object obj, Object obj2) throws IllegalAccessException {
        writeField(field, obj, obj2, false);
    }

    public static void writeField(Field field, Object obj, Object obj2, boolean z) throws IllegalAccessException {
        Validate.isTrue(field != null, "The field must not be null", new Object[0]);
        if (!z || field.isAccessible()) {
            MemberUtils.setAccessibleWorkaround(field);
        } else {
            field.setAccessible(true);
        }
        field.set(obj, obj2);
    }

    public static void removeFinalModifier(Field field) {
        removeFinalModifier(field, true);
    }

    /* JADX WARNING: No exception handlers in catch block: Catch:{  } */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static void removeFinalModifier(java.lang.reflect.Field r5, boolean r6) {
        /*
            r1 = 1
            r2 = 0
            if (r5 == 0) goto L_0x003c
            r0 = r1
        L_0x0005:
            java.lang.String r3 = "The field must not be null"
            java.lang.Object[] r4 = new java.lang.Object[r2]
            org.apache.commons.lang3.Validate.isTrue(r0, r3, r4)
            int r0 = r5.getModifiers()     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
            boolean r0 = java.lang.reflect.Modifier.isFinal(r0)     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
            if (r0 == 0) goto L_0x003b
            java.lang.Class<java.lang.reflect.Field> r0 = java.lang.reflect.Field.class
            java.lang.String r3 = "modifiers"
            java.lang.reflect.Field r3 = r0.getDeclaredField(r3)     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
            if (r6 == 0) goto L_0x003e
            boolean r0 = r3.isAccessible()     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
            if (r0 != 0) goto L_0x003e
        L_0x0026:
            if (r1 == 0) goto L_0x002c
            r0 = 1
            r3.setAccessible(r0)     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
        L_0x002c:
            int r0 = r5.getModifiers()     // Catch:{ all -> 0x0040 }
            r0 = r0 & -17
            r3.setInt(r5, r0)     // Catch:{ all -> 0x0040 }
            if (r1 == 0) goto L_0x003b
            r0 = 0
            r3.setAccessible(r0)     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
        L_0x003b:
            return
        L_0x003c:
            r0 = r2
            goto L_0x0005
        L_0x003e:
            r1 = r2
            goto L_0x0026
        L_0x0040:
            r0 = move-exception
            if (r1 == 0) goto L_0x0047
            r1 = 0
            r3.setAccessible(r1)     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
        L_0x0047:
            throw r0     // Catch:{ NoSuchFieldException -> 0x0048, IllegalAccessException -> 0x004a }
        L_0x0048:
            r0 = move-exception
            goto L_0x003b
        L_0x004a:
            r0 = move-exception
            goto L_0x003b
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.reflect.FieldUtils.removeFinalModifier(java.lang.reflect.Field, boolean):void");
    }

    public static void writeField(Object obj, String str, Object obj2) throws IllegalAccessException {
        writeField(obj, str, obj2, false);
    }

    public static void writeField(Object obj, String str, Object obj2, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Class cls = obj.getClass();
        Field field = getField(cls, str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", cls.getName(), str);
        writeField(field, obj, obj2, false);
    }

    public static void writeDeclaredField(Object obj, String str, Object obj2) throws IllegalAccessException {
        writeDeclaredField(obj, str, obj2, false);
    }

    public static void writeDeclaredField(Object obj, String str, Object obj2, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Class cls = obj.getClass();
        Field declaredField = getDeclaredField(cls, str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", cls.getName(), str);
        writeField(declaredField, obj, obj2, false);
    }
}

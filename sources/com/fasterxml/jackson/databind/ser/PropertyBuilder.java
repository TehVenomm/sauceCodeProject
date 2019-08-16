package com.fasterxml.jackson.databind.ser;

import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.annotation.JsonInclude.Value;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.SerializationConfig;
import com.fasterxml.jackson.databind.annotation.JsonSerialize.Typing;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.util.Annotations;
import com.fasterxml.jackson.databind.util.ClassUtil;

public class PropertyBuilder {
    private static final Object NO_DEFAULT_MARKER = Boolean.FALSE;
    protected final AnnotationIntrospector _annotationIntrospector = this._config.getAnnotationIntrospector();
    protected final BeanDescription _beanDesc;
    protected final SerializationConfig _config;
    protected Object _defaultBean;
    protected final Value _defaultInclusion;

    public PropertyBuilder(SerializationConfig serializationConfig, BeanDescription beanDescription) {
        this._config = serializationConfig;
        this._beanDesc = beanDescription;
        this._defaultInclusion = beanDescription.findPropertyInclusion(serializationConfig.getDefaultPropertyInclusion());
    }

    public Annotations getClassAnnotations() {
        return this._beanDesc.getClassAnnotations();
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:22:0x00ab  */
    /* JADX WARNING: Removed duplicated region for block: B:25:0x00be  */
    /* JADX WARNING: Removed duplicated region for block: B:47:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.ser.BeanPropertyWriter buildWriter(com.fasterxml.jackson.databind.SerializerProvider r13, com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition r14, com.fasterxml.jackson.databind.JavaType r15, com.fasterxml.jackson.databind.JsonSerializer<?> r16, com.fasterxml.jackson.databind.jsontype.TypeSerializer r17, com.fasterxml.jackson.databind.jsontype.TypeSerializer r18, com.fasterxml.jackson.databind.introspect.AnnotatedMember r19, boolean r20) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r12 = this;
            r0 = r19
            r1 = r20
            com.fasterxml.jackson.databind.JavaType r2 = r12.findSerializationType(r0, r1, r15)
            if (r18 == 0) goto L_0x010d
            if (r2 != 0) goto L_0x000d
            r2 = r15
        L_0x000d:
            com.fasterxml.jackson.databind.JavaType r3 = r2.getContentType()
            if (r3 != 0) goto L_0x0050
            java.lang.IllegalStateException r3 = new java.lang.IllegalStateException
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            java.lang.String r5 = "Problem trying to create BeanPropertyWriter for property '"
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r5 = r14.getName()
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r5 = "' (of type "
            java.lang.StringBuilder r4 = r4.append(r5)
            com.fasterxml.jackson.databind.BeanDescription r5 = r12._beanDesc
            com.fasterxml.jackson.databind.JavaType r5 = r5.getType()
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r5 = "); serialization type "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.StringBuilder r2 = r4.append(r2)
            java.lang.String r4 = " has no content"
            java.lang.StringBuilder r2 = r2.append(r4)
            java.lang.String r2 = r2.toString()
            r3.<init>(r2)
            throw r3
        L_0x0050:
            r0 = r18
            com.fasterxml.jackson.databind.JavaType r9 = r2.withContentTypeHandler(r0)
            r9.getContentType()
        L_0x0059:
            r11 = 0
            r3 = 0
            com.fasterxml.jackson.annotation.JsonInclude$Value r2 = r12._defaultInclusion
            com.fasterxml.jackson.annotation.JsonInclude$Value r4 = r14.findInclusion()
            com.fasterxml.jackson.annotation.JsonInclude$Value r2 = r2.withOverrides(r4)
            com.fasterxml.jackson.annotation.JsonInclude$Include r2 = r2.getValueInclusion()
            com.fasterxml.jackson.annotation.JsonInclude$Include r4 = com.fasterxml.jackson.annotation.JsonInclude.Include.USE_DEFAULTS
            if (r2 != r4) goto L_0x006f
            com.fasterxml.jackson.annotation.JsonInclude$Include r2 = com.fasterxml.jackson.annotation.JsonInclude.Include.ALWAYS
        L_0x006f:
            int[] r4 = com.fasterxml.jackson.databind.ser.PropertyBuilder.C08871.$SwitchMap$com$fasterxml$jackson$annotation$JsonInclude$Include
            int r2 = r2.ordinal()
            r2 = r4[r2]
            switch(r2) {
                case 1: goto L_0x00c3;
                case 2: goto L_0x00f6;
                case 3: goto L_0x0101;
                case 4: goto L_0x0106;
                default: goto L_0x007a;
            }
        L_0x007a:
            r2 = r3
        L_0x007b:
            boolean r3 = r15.isContainerType()
            if (r3 == 0) goto L_0x0109
            com.fasterxml.jackson.databind.SerializationConfig r3 = r12._config
            com.fasterxml.jackson.databind.SerializationFeature r4 = com.fasterxml.jackson.databind.SerializationFeature.WRITE_EMPTY_JSON_ARRAYS
            boolean r3 = r3.isEnabled(r4)
            if (r3 != 0) goto L_0x0109
            java.lang.Object r11 = com.fasterxml.jackson.databind.ser.BeanPropertyWriter.MARKER_FOR_EMPTY
            r10 = r2
        L_0x008e:
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter r2 = new com.fasterxml.jackson.databind.ser.BeanPropertyWriter
            com.fasterxml.jackson.databind.BeanDescription r3 = r12._beanDesc
            com.fasterxml.jackson.databind.util.Annotations r5 = r3.getClassAnnotations()
            r3 = r14
            r4 = r19
            r6 = r15
            r7 = r16
            r8 = r17
            r2.<init>(r3, r4, r5, r6, r7, r8, r9, r10, r11)
            com.fasterxml.jackson.databind.AnnotationIntrospector r3 = r12._annotationIntrospector
            r0 = r19
            java.lang.Object r3 = r3.findNullSerializer(r0)
            if (r3 == 0) goto L_0x00b4
            r0 = r19
            com.fasterxml.jackson.databind.JsonSerializer r3 = r13.serializerInstance(r0, r3)
            r2.assignNullSerializer(r3)
        L_0x00b4:
            com.fasterxml.jackson.databind.AnnotationIntrospector r3 = r12._annotationIntrospector
            r0 = r19
            com.fasterxml.jackson.databind.util.NameTransformer r3 = r3.findUnwrappingNameTransformer(r0)
            if (r3 == 0) goto L_0x00c2
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter r2 = r2.unwrappingWriter(r3)
        L_0x00c2:
            return r2
        L_0x00c3:
            if (r9 != 0) goto L_0x00df
            r2 = r15
        L_0x00c6:
            com.fasterxml.jackson.annotation.JsonInclude$Value r4 = r12._defaultInclusion
            com.fasterxml.jackson.annotation.JsonInclude$Include r4 = r4.getValueInclusion()
            com.fasterxml.jackson.annotation.JsonInclude$Include r5 = com.fasterxml.jackson.annotation.JsonInclude.Include.NON_DEFAULT
            if (r4 != r5) goto L_0x00e1
            java.lang.String r4 = r14.getName()
            r0 = r19
            java.lang.Object r11 = r12.getPropertyDefaultValue(r4, r0, r2)
        L_0x00da:
            if (r11 != 0) goto L_0x00e6
            r2 = 1
            r10 = r2
            goto L_0x008e
        L_0x00df:
            r2 = r9
            goto L_0x00c6
        L_0x00e1:
            java.lang.Object r11 = r12.getDefaultValue(r2)
            goto L_0x00da
        L_0x00e6:
            java.lang.Class r2 = r11.getClass()
            boolean r2 = r2.isArray()
            if (r2 == 0) goto L_0x010b
            java.lang.Object r11 = com.fasterxml.jackson.databind.util.ArrayBuilders.getArrayComparator(r11)
            r10 = r3
            goto L_0x008e
        L_0x00f6:
            r2 = 1
            boolean r3 = r15.isReferenceType()
            if (r3 == 0) goto L_0x0109
            java.lang.Object r11 = com.fasterxml.jackson.databind.ser.BeanPropertyWriter.MARKER_FOR_EMPTY
            r10 = r2
            goto L_0x008e
        L_0x0101:
            r2 = 1
            java.lang.Object r11 = com.fasterxml.jackson.databind.ser.BeanPropertyWriter.MARKER_FOR_EMPTY
            r10 = r2
            goto L_0x008e
        L_0x0106:
            r2 = 1
            goto L_0x007b
        L_0x0109:
            r10 = r2
            goto L_0x008e
        L_0x010b:
            r10 = r3
            goto L_0x008e
        L_0x010d:
            r9 = r2
            goto L_0x0059
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.PropertyBuilder.buildWriter(com.fasterxml.jackson.databind.SerializerProvider, com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition, com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.databind.JsonSerializer, com.fasterxml.jackson.databind.jsontype.TypeSerializer, com.fasterxml.jackson.databind.jsontype.TypeSerializer, com.fasterxml.jackson.databind.introspect.AnnotatedMember, boolean):com.fasterxml.jackson.databind.ser.BeanPropertyWriter");
    }

    /* access modifiers changed from: protected */
    public JavaType findSerializationType(Annotated annotated, boolean z, JavaType javaType) throws JsonMappingException {
        boolean z2 = true;
        JavaType refineSerializationType = this._annotationIntrospector.refineSerializationType(this._config, annotated, javaType);
        if (refineSerializationType != javaType) {
            Class rawClass = refineSerializationType.getRawClass();
            Class rawClass2 = javaType.getRawClass();
            if (!rawClass.isAssignableFrom(rawClass2) && !rawClass2.isAssignableFrom(rawClass)) {
                throw new IllegalArgumentException("Illegal concrete-type annotation for method '" + annotated.getName() + "': class " + rawClass.getName() + " not a super-type of (declared) class " + rawClass2.getName());
            }
            javaType = refineSerializationType;
            z = true;
        }
        Typing findSerializationTyping = this._annotationIntrospector.findSerializationTyping(annotated);
        if (!(findSerializationTyping == null || findSerializationTyping == Typing.DEFAULT_TYPING)) {
            if (findSerializationTyping != Typing.STATIC) {
                z2 = false;
            }
            z = z2;
        }
        if (z) {
            return javaType.withStaticTyping();
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public Object getDefaultBean() {
        Object obj = this._defaultBean;
        if (obj == null) {
            obj = this._beanDesc.instantiateBean(this._config.canOverrideAccessModifiers());
            if (obj == null) {
                obj = NO_DEFAULT_MARKER;
            }
            this._defaultBean = obj;
        }
        if (obj == NO_DEFAULT_MARKER) {
            return null;
        }
        return this._defaultBean;
    }

    /* access modifiers changed from: protected */
    public Object getPropertyDefaultValue(String str, AnnotatedMember annotatedMember, JavaType javaType) {
        Object defaultBean = getDefaultBean();
        if (defaultBean == null) {
            return getDefaultValue(javaType);
        }
        try {
            return annotatedMember.getValue(defaultBean);
        } catch (Exception e) {
            return _throwWrapped(e, str, defaultBean);
        }
    }

    /* access modifiers changed from: protected */
    public Object getDefaultValue(JavaType javaType) {
        Class<String> rawClass = javaType.getRawClass();
        Class primitiveType = ClassUtil.primitiveType(rawClass);
        if (primitiveType != null) {
            return ClassUtil.defaultValue(primitiveType);
        }
        if (javaType.isContainerType() || javaType.isReferenceType()) {
            return Include.NON_EMPTY;
        }
        if (rawClass == String.class) {
            return "";
        }
        return null;
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Incorrect type for immutable var: ssa=java.lang.Exception, code=java.lang.Throwable, for r4v0, types: [java.lang.Throwable, java.lang.Exception] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public java.lang.Object _throwWrapped(java.lang.Throwable r4, java.lang.String r5, java.lang.Object r6) {
        /*
            r3 = this;
            r0 = r4
        L_0x0001:
            java.lang.Throwable r1 = r0.getCause()
            if (r1 == 0) goto L_0x000c
            java.lang.Throwable r0 = r0.getCause()
            goto L_0x0001
        L_0x000c:
            boolean r1 = r0 instanceof java.lang.Error
            if (r1 == 0) goto L_0x0013
            java.lang.Error r0 = (java.lang.Error) r0
            throw r0
        L_0x0013:
            boolean r1 = r0 instanceof java.lang.RuntimeException
            if (r1 == 0) goto L_0x001a
            java.lang.RuntimeException r0 = (java.lang.RuntimeException) r0
            throw r0
        L_0x001a:
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Failed to get property '"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r5)
            java.lang.String r2 = "' of default "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.Class r2 = r6.getClass()
            java.lang.String r2 = r2.getName()
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r2 = " instance"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.PropertyBuilder._throwWrapped(java.lang.Exception, java.lang.String, java.lang.Object):java.lang.Object");
    }
}

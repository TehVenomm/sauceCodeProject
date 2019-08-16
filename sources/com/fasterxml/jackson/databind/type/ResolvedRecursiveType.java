package com.fasterxml.jackson.databind.type;

import com.fasterxml.jackson.databind.JavaType;

public class ResolvedRecursiveType extends TypeBase {
    private static final long serialVersionUID = 1;
    protected JavaType _referencedType;

    public ResolvedRecursiveType(Class<?> cls, TypeBindings typeBindings) {
        super(cls, typeBindings, null, null, 0, null, null, false);
    }

    public void setReference(JavaType javaType) {
        if (this._referencedType != null) {
            throw new IllegalStateException("Trying to re-set self reference; old value = " + this._referencedType + ", new = " + javaType);
        }
        this._referencedType = javaType;
    }

    public JavaType getSelfReferencedType() {
        return this._referencedType;
    }

    public StringBuilder getGenericSignature(StringBuilder sb) {
        return this._referencedType.getGenericSignature(sb);
    }

    public StringBuilder getErasedSignature(StringBuilder sb) {
        return this._referencedType.getErasedSignature(sb);
    }

    public JavaType withContentType(JavaType javaType) {
        return this;
    }

    public JavaType withTypeHandler(Object obj) {
        return this;
    }

    public JavaType withContentTypeHandler(Object obj) {
        return this;
    }

    public JavaType withValueHandler(Object obj) {
        return this;
    }

    public JavaType withContentValueHandler(Object obj) {
        return this;
    }

    public JavaType withStaticTyping() {
        return this;
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public JavaType _narrow(Class<?> cls) {
        return this;
    }

    public JavaType refine(Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        return null;
    }

    public boolean isContainerType() {
        return false;
    }

    public String toString() {
        return "[resolved recursive type -> " + this._referencedType + ']';
    }

    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        return ((ResolvedRecursiveType) obj).getSelfReferencedType().equals(getSelfReferencedType());
    }
}

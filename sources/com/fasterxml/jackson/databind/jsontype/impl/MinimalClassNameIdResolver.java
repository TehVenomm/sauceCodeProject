package com.fasterxml.jackson.databind.jsontype.impl;

import com.fasterxml.jackson.annotation.JsonTypeInfo.C0862Id;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.type.TypeFactory;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public class MinimalClassNameIdResolver extends ClassNameIdResolver {
    protected final String _basePackageName;
    protected final String _basePackagePrefix;

    protected MinimalClassNameIdResolver(JavaType javaType, TypeFactory typeFactory) {
        super(javaType, typeFactory);
        String name = javaType.getRawClass().getName();
        int lastIndexOf = name.lastIndexOf(46);
        if (lastIndexOf < 0) {
            this._basePackageName = "";
            this._basePackagePrefix = AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER;
            return;
        }
        this._basePackagePrefix = name.substring(0, lastIndexOf + 1);
        this._basePackageName = name.substring(0, lastIndexOf);
    }

    public C0862Id getMechanism() {
        return C0862Id.MINIMAL_CLASS;
    }

    public String idFromValue(Object obj) {
        String name = obj.getClass().getName();
        if (name.startsWith(this._basePackagePrefix)) {
            return name.substring(this._basePackagePrefix.length() - 1);
        }
        return name;
    }

    /* access modifiers changed from: protected */
    public JavaType _typeFromId(String str, TypeFactory typeFactory) {
        if (str.startsWith(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER)) {
            StringBuilder sb = new StringBuilder(str.length() + this._basePackageName.length());
            if (this._basePackageName.length() == 0) {
                sb.append(str.substring(1));
            } else {
                sb.append(this._basePackageName).append(str);
            }
            str = sb.toString();
        }
        return super._typeFromId(str, typeFactory);
    }
}

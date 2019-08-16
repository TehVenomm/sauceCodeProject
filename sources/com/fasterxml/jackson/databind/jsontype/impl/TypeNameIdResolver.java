package com.fasterxml.jackson.databind.jsontype.impl;

import com.fasterxml.jackson.annotation.JsonTypeInfo.C0862Id;
import com.fasterxml.jackson.databind.DatabindContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.cfg.MapperConfig;
import com.fasterxml.jackson.databind.jsontype.NamedType;
import java.lang.reflect.Type;
import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.TreeSet;

public class TypeNameIdResolver extends TypeIdResolverBase {
    protected final MapperConfig<?> _config;
    protected final Map<String, JavaType> _idToType;
    protected final Map<String, String> _typeToId;

    protected TypeNameIdResolver(MapperConfig<?> mapperConfig, JavaType javaType, Map<String, String> map, Map<String, JavaType> map2) {
        super(javaType, mapperConfig.getTypeFactory());
        this._config = mapperConfig;
        this._typeToId = map;
        this._idToType = map2;
    }

    public static TypeNameIdResolver construct(MapperConfig<?> mapperConfig, JavaType javaType, Collection<NamedType> collection, boolean z, boolean z2) {
        Map map;
        Map map2;
        Map map3;
        if (z == z2) {
            throw new IllegalArgumentException();
        }
        if (z) {
            map = new HashMap();
        } else {
            map = null;
        }
        if (z2) {
            map2 = new HashMap();
            map3 = new TreeMap();
        } else {
            map2 = null;
            map3 = map;
        }
        if (collection != null) {
            for (NamedType namedType : collection) {
                Class type = namedType.getType();
                String _defaultTypeId = namedType.hasName() ? namedType.getName() : _defaultTypeId(type);
                if (z) {
                    map3.put(type.getName(), _defaultTypeId);
                }
                if (z2) {
                    JavaType javaType2 = (JavaType) map2.get(_defaultTypeId);
                    if (javaType2 == null || !type.isAssignableFrom(javaType2.getRawClass())) {
                        map2.put(_defaultTypeId, mapperConfig.constructType(type));
                    }
                }
            }
        }
        return new TypeNameIdResolver(mapperConfig, javaType, map3, map2);
    }

    public C0862Id getMechanism() {
        return C0862Id.NAME;
    }

    public String idFromValue(Object obj) {
        return idFromClass(obj.getClass());
    }

    /* access modifiers changed from: protected */
    public String idFromClass(Class<?> cls) {
        String str;
        if (cls == null) {
            return null;
        }
        Class rawClass = this._typeFactory.constructType((Type) cls).getRawClass();
        String name = rawClass.getName();
        synchronized (this._typeToId) {
            str = (String) this._typeToId.get(name);
            if (str == null) {
                if (this._config.isAnnotationProcessingEnabled()) {
                    str = this._config.getAnnotationIntrospector().findTypeName(this._config.introspectClassAnnotations(rawClass).getClassInfo());
                }
                if (str == null) {
                    str = _defaultTypeId(rawClass);
                }
                this._typeToId.put(name, str);
            }
        }
        return str;
    }

    public String idFromValueAndType(Object obj, Class<?> cls) {
        if (obj == null) {
            return idFromClass(cls);
        }
        return idFromValue(obj);
    }

    @Deprecated
    public JavaType typeFromId(String str) {
        return _typeFromId(str);
    }

    public JavaType typeFromId(DatabindContext databindContext, String str) {
        return _typeFromId(str);
    }

    /* access modifiers changed from: protected */
    public JavaType _typeFromId(String str) {
        return (JavaType) this._idToType.get(str);
    }

    public String getDescForKnownTypeIds() {
        return new TreeSet(this._idToType.keySet()).toString();
    }

    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append('[').append(getClass().getName());
        sb.append("; id-to-type=").append(this._idToType);
        sb.append(']');
        return sb.toString();
    }

    protected static String _defaultTypeId(Class<?> cls) {
        String name = cls.getName();
        int lastIndexOf = name.lastIndexOf(46);
        return lastIndexOf < 0 ? name : name.substring(lastIndexOf + 1);
    }
}

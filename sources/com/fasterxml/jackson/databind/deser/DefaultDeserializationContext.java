package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.annotation.ObjectIdGenerator;
import com.fasterxml.jackson.annotation.ObjectIdGenerator.IdKey;
import com.fasterxml.jackson.annotation.ObjectIdResolver;
import com.fasterxml.jackson.annotation.SimpleObjectIdResolver;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationConfig;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.InjectableValues;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonDeserializer.None;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.cfg.HandlerInstantiator;
import com.fasterxml.jackson.databind.deser.impl.ReadableObjectId;
import com.fasterxml.jackson.databind.deser.impl.ReadableObjectId.Referring;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.util.ClassUtil;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map.Entry;

public abstract class DefaultDeserializationContext extends DeserializationContext implements Serializable {
    private static final long serialVersionUID = 1;
    private List<ObjectIdResolver> _objectIdResolvers;
    protected transient LinkedHashMap<IdKey, ReadableObjectId> _objectIds;

    public static final class Impl extends DefaultDeserializationContext {
        private static final long serialVersionUID = 1;

        public Impl(DeserializerFactory deserializerFactory) {
            super(deserializerFactory, (DeserializerCache) null);
        }

        protected Impl(Impl impl, DeserializationConfig deserializationConfig, JsonParser jsonParser, InjectableValues injectableValues) {
            super(impl, deserializationConfig, jsonParser, injectableValues);
        }

        protected Impl(Impl impl) {
            super(impl);
        }

        protected Impl(Impl impl, DeserializerFactory deserializerFactory) {
            super((DefaultDeserializationContext) impl, deserializerFactory);
        }

        public DefaultDeserializationContext copy() {
            if (getClass() != Impl.class) {
                return DefaultDeserializationContext.super.copy();
            }
            return new Impl(this);
        }

        public DefaultDeserializationContext createInstance(DeserializationConfig deserializationConfig, JsonParser jsonParser, InjectableValues injectableValues) {
            return new Impl(this, deserializationConfig, jsonParser, injectableValues);
        }

        public DefaultDeserializationContext with(DeserializerFactory deserializerFactory) {
            return new Impl(this, deserializerFactory);
        }
    }

    public abstract DefaultDeserializationContext createInstance(DeserializationConfig deserializationConfig, JsonParser jsonParser, InjectableValues injectableValues);

    public abstract DefaultDeserializationContext with(DeserializerFactory deserializerFactory);

    protected DefaultDeserializationContext(DeserializerFactory deserializerFactory, DeserializerCache deserializerCache) {
        super(deserializerFactory, deserializerCache);
    }

    protected DefaultDeserializationContext(DefaultDeserializationContext defaultDeserializationContext, DeserializationConfig deserializationConfig, JsonParser jsonParser, InjectableValues injectableValues) {
        super(defaultDeserializationContext, deserializationConfig, jsonParser, injectableValues);
    }

    protected DefaultDeserializationContext(DefaultDeserializationContext defaultDeserializationContext, DeserializerFactory deserializerFactory) {
        super((DeserializationContext) defaultDeserializationContext, deserializerFactory);
    }

    protected DefaultDeserializationContext(DefaultDeserializationContext defaultDeserializationContext) {
        super((DeserializationContext) defaultDeserializationContext);
    }

    public DefaultDeserializationContext copy() {
        throw new IllegalStateException("DefaultDeserializationContext sub-class not overriding copy()");
    }

    public ReadableObjectId findObjectId(Object obj, ObjectIdGenerator<?> objectIdGenerator, ObjectIdResolver objectIdResolver) {
        ObjectIdResolver objectIdResolver2;
        if (obj == null) {
            return null;
        }
        IdKey key = objectIdGenerator.key(obj);
        if (this._objectIds == null) {
            this._objectIds = new LinkedHashMap<>();
        } else {
            ReadableObjectId readableObjectId = (ReadableObjectId) this._objectIds.get(key);
            if (readableObjectId != null) {
                return readableObjectId;
            }
        }
        if (this._objectIdResolvers != null) {
            Iterator it = this._objectIdResolvers.iterator();
            while (true) {
                if (!it.hasNext()) {
                    objectIdResolver2 = null;
                    break;
                }
                objectIdResolver2 = (ObjectIdResolver) it.next();
                if (objectIdResolver2.canUseFor(objectIdResolver)) {
                    break;
                }
            }
        } else {
            this._objectIdResolvers = new ArrayList(8);
            objectIdResolver2 = null;
        }
        if (objectIdResolver2 == null) {
            objectIdResolver2 = objectIdResolver.newForDeserialization(this);
            this._objectIdResolvers.add(objectIdResolver2);
        }
        ReadableObjectId createReadableObjectId = createReadableObjectId(key);
        createReadableObjectId.setResolver(objectIdResolver2);
        this._objectIds.put(key, createReadableObjectId);
        return createReadableObjectId;
    }

    /* access modifiers changed from: protected */
    public ReadableObjectId createReadableObjectId(IdKey idKey) {
        return new ReadableObjectId(idKey);
    }

    @Deprecated
    public ReadableObjectId findObjectId(Object obj, ObjectIdGenerator<?> objectIdGenerator) {
        return findObjectId(obj, objectIdGenerator, new SimpleObjectIdResolver());
    }

    public void checkUnresolvedObjectId() throws UnresolvedForwardReference {
        if (this._objectIds != null && isEnabled(DeserializationFeature.FAIL_ON_UNRESOLVED_OBJECT_IDS)) {
            UnresolvedForwardReference unresolvedForwardReference = null;
            for (Entry value : this._objectIds.entrySet()) {
                ReadableObjectId readableObjectId = (ReadableObjectId) value.getValue();
                if (readableObjectId.hasReferringProperties() && !tryToResolveUnresolvedObjectId(readableObjectId)) {
                    if (unresolvedForwardReference == null) {
                        unresolvedForwardReference = new UnresolvedForwardReference(getParser(), "Unresolved forward references for: ");
                    }
                    Object obj = readableObjectId.getKey().key;
                    Iterator referringProperties = readableObjectId.referringProperties();
                    while (referringProperties.hasNext()) {
                        Referring referring = (Referring) referringProperties.next();
                        unresolvedForwardReference.addUnresolvedId(obj, referring.getBeanType(), referring.getLocation());
                    }
                }
            }
            if (unresolvedForwardReference != null) {
                throw unresolvedForwardReference;
            }
        }
    }

    /* access modifiers changed from: protected */
    public boolean tryToResolveUnresolvedObjectId(ReadableObjectId readableObjectId) {
        return readableObjectId.tryToResolveUnresolved(this);
    }

    public JsonDeserializer<Object> deserializerInstance(Annotated annotated, Object obj) throws JsonMappingException {
        JsonDeserializer<Object> jsonDeserializer = null;
        if (obj != null) {
            if (obj instanceof JsonDeserializer) {
                jsonDeserializer = (JsonDeserializer) obj;
            } else if (!(obj instanceof Class)) {
                throw new IllegalStateException("AnnotationIntrospector returned deserializer definition of type " + obj.getClass().getName() + "; expected type JsonDeserializer or Class<JsonDeserializer> instead");
            } else {
                Class<None> cls = (Class) obj;
                if (cls != None.class && !ClassUtil.isBogusClass(cls)) {
                    if (!JsonDeserializer.class.isAssignableFrom(cls)) {
                        throw new IllegalStateException("AnnotationIntrospector returned Class " + cls.getName() + "; expected Class<JsonDeserializer>");
                    }
                    HandlerInstantiator handlerInstantiator = this._config.getHandlerInstantiator();
                    if (handlerInstantiator != null) {
                        jsonDeserializer = handlerInstantiator.deserializerInstance(this._config, annotated, cls);
                    }
                    if (jsonDeserializer == null) {
                        jsonDeserializer = (JsonDeserializer) ClassUtil.createInstance(cls, this._config.canOverrideAccessModifiers());
                    }
                }
            }
            if (jsonDeserializer instanceof ResolvableDeserializer) {
                ((ResolvableDeserializer) jsonDeserializer).resolve(this);
            }
        }
        return jsonDeserializer;
    }

    public final KeyDeserializer keyDeserializerInstance(Annotated annotated, Object obj) throws JsonMappingException {
        KeyDeserializer keyDeserializer = null;
        if (obj != null) {
            if (obj instanceof KeyDeserializer) {
                keyDeserializer = (KeyDeserializer) obj;
            } else if (!(obj instanceof Class)) {
                throw new IllegalStateException("AnnotationIntrospector returned key deserializer definition of type " + obj.getClass().getName() + "; expected type KeyDeserializer or Class<KeyDeserializer> instead");
            } else {
                Class<KeyDeserializer.None> cls = (Class) obj;
                if (cls != KeyDeserializer.None.class && !ClassUtil.isBogusClass(cls)) {
                    if (!KeyDeserializer.class.isAssignableFrom(cls)) {
                        throw new IllegalStateException("AnnotationIntrospector returned Class " + cls.getName() + "; expected Class<KeyDeserializer>");
                    }
                    HandlerInstantiator handlerInstantiator = this._config.getHandlerInstantiator();
                    if (handlerInstantiator != null) {
                        keyDeserializer = handlerInstantiator.keyDeserializerInstance(this._config, annotated, cls);
                    }
                    if (keyDeserializer == null) {
                        keyDeserializer = (KeyDeserializer) ClassUtil.createInstance(cls, this._config.canOverrideAccessModifiers());
                    }
                }
            }
            if (keyDeserializer instanceof ResolvableDeserializer) {
                ((ResolvableDeserializer) keyDeserializer).resolve(this);
            }
        }
        return keyDeserializer;
    }
}

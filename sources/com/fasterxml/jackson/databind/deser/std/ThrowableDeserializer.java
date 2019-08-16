package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.deser.BeanDeserializer;
import com.fasterxml.jackson.databind.deser.BeanDeserializerBase;
import com.fasterxml.jackson.databind.deser.SettableBeanProperty;
import com.fasterxml.jackson.databind.util.NameTransformer;
import java.io.IOException;

public class ThrowableDeserializer extends BeanDeserializer {
    protected static final String PROP_NAME_MESSAGE = "message";
    private static final long serialVersionUID = 1;

    public ThrowableDeserializer(BeanDeserializer beanDeserializer) {
        super(beanDeserializer);
        this._vanillaProcessing = false;
    }

    protected ThrowableDeserializer(BeanDeserializer beanDeserializer, NameTransformer nameTransformer) {
        super((BeanDeserializerBase) beanDeserializer, nameTransformer);
    }

    public JsonDeserializer<Object> unwrappingDeserializer(NameTransformer nameTransformer) {
        return getClass() != ThrowableDeserializer.class ? this : new ThrowableDeserializer(this, nameTransformer);
    }

    public Object deserializeFromObject(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        Object createUsingDefault;
        int i;
        if (this._propertyBasedCreator != null) {
            return _deserializeUsingPropertyBased(jsonParser, deserializationContext);
        }
        if (this._delegateDeserializer != null) {
            return this._valueInstantiator.createUsingDelegate(deserializationContext, this._delegateDeserializer.deserialize(jsonParser, deserializationContext));
        }
        if (this._beanType.isAbstract()) {
            throw JsonMappingException.from(jsonParser, "Can not instantiate abstract type " + this._beanType + " (need to add/enable type information?)");
        }
        boolean canCreateFromString = this._valueInstantiator.canCreateFromString();
        boolean canCreateUsingDefault = this._valueInstantiator.canCreateUsingDefault();
        if (canCreateFromString || canCreateUsingDefault) {
            int i2 = 0;
            Object[] objArr = null;
            Object obj = null;
            while (jsonParser.getCurrentToken() != JsonToken.END_OBJECT) {
                String currentName = jsonParser.getCurrentName();
                SettableBeanProperty find = this._beanProperties.find(currentName);
                jsonParser.nextToken();
                if (find == null) {
                    if ("message".equals(currentName) && canCreateFromString) {
                        obj = this._valueInstantiator.createFromString(deserializationContext, jsonParser.getText());
                        if (objArr != null) {
                            for (int i3 = 0; i3 < i2; i3 += 2) {
                                ((SettableBeanProperty) objArr[i3]).set(obj, objArr[i3 + 1]);
                            }
                            i = i2;
                            objArr = null;
                        }
                    } else if (this._ignorableProps != null && this._ignorableProps.contains(currentName)) {
                        jsonParser.skipChildren();
                        i = i2;
                    } else if (this._anySetter != null) {
                        this._anySetter.deserializeAndSet(jsonParser, deserializationContext, obj, currentName);
                        i = i2;
                    } else {
                        handleUnknownProperty(jsonParser, deserializationContext, obj, currentName);
                    }
                    i = i2;
                } else if (obj != null) {
                    find.deserializeAndSet(jsonParser, deserializationContext, obj);
                    i = i2;
                } else {
                    if (objArr == null) {
                        int size = this._beanProperties.size();
                        objArr = new Object[(size + size)];
                    }
                    int i4 = i2 + 1;
                    objArr[i2] = find;
                    i = i4 + 1;
                    objArr[i4] = find.deserialize(jsonParser, deserializationContext);
                }
                jsonParser.nextToken();
                i2 = i;
            }
            if (obj != null) {
                return obj;
            }
            if (canCreateFromString) {
                createUsingDefault = this._valueInstantiator.createFromString(deserializationContext, null);
            } else {
                createUsingDefault = this._valueInstantiator.createUsingDefault(deserializationContext);
            }
            if (objArr == null) {
                return createUsingDefault;
            }
            for (int i5 = 0; i5 < i2; i5 += 2) {
                ((SettableBeanProperty) objArr[i5]).set(createUsingDefault, objArr[i5 + 1]);
            }
            return createUsingDefault;
        }
        throw JsonMappingException.from(jsonParser, "Can not deserialize Throwable of type " + this._beanType + " without having a default contructor, a single-String-arg constructor; or explicit @JsonCreator");
    }
}

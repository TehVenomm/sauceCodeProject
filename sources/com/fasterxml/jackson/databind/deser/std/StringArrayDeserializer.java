package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.annotation.JsonFormat.Feature;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ObjectBuffer;
import java.io.IOException;

@JacksonStdImpl
public final class StringArrayDeserializer extends StdDeserializer<String[]> implements ContextualDeserializer {
    public static final StringArrayDeserializer instance = new StringArrayDeserializer();
    private static final long serialVersionUID = 2;
    protected JsonDeserializer<String> _elementDeserializer;
    protected final Boolean _unwrapSingle;

    public StringArrayDeserializer() {
        this(null, null);
    }

    protected StringArrayDeserializer(JsonDeserializer<?> jsonDeserializer, Boolean bool) {
        super(String[].class);
        this._elementDeserializer = jsonDeserializer;
        this._unwrapSingle = bool;
    }

    public JsonDeserializer<?> createContextual(DeserializationContext deserializationContext, BeanProperty beanProperty) throws JsonMappingException {
        JsonDeserializer<String> handleSecondaryContextualization;
        JsonDeserializer findConvertingContentDeserializer = findConvertingContentDeserializer(deserializationContext, beanProperty, this._elementDeserializer);
        JavaType constructType = deserializationContext.constructType(String.class);
        if (findConvertingContentDeserializer == null) {
            handleSecondaryContextualization = deserializationContext.findContextualValueDeserializer(constructType, beanProperty);
        } else {
            handleSecondaryContextualization = deserializationContext.handleSecondaryContextualization(findConvertingContentDeserializer, beanProperty, constructType);
        }
        Boolean findFormatFeature = findFormatFeature(deserializationContext, beanProperty, String[].class, Feature.ACCEPT_SINGLE_VALUE_AS_ARRAY);
        if (handleSecondaryContextualization != null && isDefaultDeserializer(handleSecondaryContextualization)) {
            handleSecondaryContextualization = null;
        }
        return (this._elementDeserializer == handleSecondaryContextualization && this._unwrapSingle == findFormatFeature) ? this : new StringArrayDeserializer(handleSecondaryContextualization, findFormatFeature);
    }

    /* JADX WARNING: Removed duplicated region for block: B:19:0x0044 A[Catch:{ Exception -> 0x004e }] */
    /* JADX WARNING: Removed duplicated region for block: B:24:0x0059  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public java.lang.String[] deserialize(com.fasterxml.jackson.core.JsonParser r8, com.fasterxml.jackson.databind.DeserializationContext r9) throws java.io.IOException {
        /*
            r7 = this;
            r3 = 0
            boolean r0 = r8.isExpectedStartArrayToken()
            if (r0 != 0) goto L_0x000c
            java.lang.String[] r0 = r7.handleNonArray(r8, r9)
        L_0x000b:
            return r0
        L_0x000c:
            com.fasterxml.jackson.databind.JsonDeserializer<java.lang.String> r0 = r7._elementDeserializer
            if (r0 == 0) goto L_0x0015
            java.lang.String[] r0 = r7._deserializeCustom(r8, r9)
            goto L_0x000b
        L_0x0015:
            com.fasterxml.jackson.databind.util.ObjectBuffer r5 = r9.leaseObjectBuffer()
            java.lang.Object[] r2 = r5.resetAndStart()
            r1 = r3
        L_0x001e:
            java.lang.String r0 = r8.nextTextValue()     // Catch:{ Exception -> 0x004e }
            if (r0 != 0) goto L_0x005b
            com.fasterxml.jackson.core.JsonToken r4 = r8.getCurrentToken()     // Catch:{ Exception -> 0x004e }
            com.fasterxml.jackson.core.JsonToken r6 = com.fasterxml.jackson.core.JsonToken.END_ARRAY     // Catch:{ Exception -> 0x004e }
            if (r4 != r6) goto L_0x0038
            java.lang.Class<java.lang.String> r0 = java.lang.String.class
            java.lang.Object[] r0 = r5.completeAndClearBuffer(r2, r1, r0)
            java.lang.String[] r0 = (java.lang.String[]) r0
            r9.returnObjectBuffer(r5)
            goto L_0x000b
        L_0x0038:
            com.fasterxml.jackson.core.JsonToken r6 = com.fasterxml.jackson.core.JsonToken.VALUE_NULL     // Catch:{ Exception -> 0x004e }
            if (r4 == r6) goto L_0x005b
            java.lang.String r0 = r7._parseString(r8, r9)     // Catch:{ Exception -> 0x004e }
            r4 = r0
        L_0x0041:
            int r0 = r2.length     // Catch:{ Exception -> 0x004e }
            if (r1 < r0) goto L_0x0059
            java.lang.Object[] r2 = r5.appendCompletedChunk(r2)     // Catch:{ Exception -> 0x004e }
            r0 = r3
        L_0x0049:
            int r1 = r0 + 1
            r2[r0] = r4     // Catch:{ Exception -> 0x004e }
            goto L_0x001e
        L_0x004e:
            r0 = move-exception
            int r3 = r5.bufferedSize()
            int r1 = r1 + r3
            com.fasterxml.jackson.databind.JsonMappingException r0 = com.fasterxml.jackson.databind.JsonMappingException.wrapWithPath(r0, r2, r1)
            throw r0
        L_0x0059:
            r0 = r1
            goto L_0x0049
        L_0x005b:
            r4 = r0
            goto L_0x0041
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.deser.std.StringArrayDeserializer.deserialize(com.fasterxml.jackson.core.JsonParser, com.fasterxml.jackson.databind.DeserializationContext):java.lang.String[]");
    }

    /* access modifiers changed from: protected */
    public final String[] _deserializeCustom(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        Object obj;
        int i;
        ObjectBuffer leaseObjectBuffer = deserializationContext.leaseObjectBuffer();
        Object[] resetAndStart = leaseObjectBuffer.resetAndStart();
        JsonDeserializer<String> jsonDeserializer = this._elementDeserializer;
        int i2 = 0;
        while (true) {
            try {
                if (jsonParser.nextTextValue() == null) {
                    JsonToken currentToken = jsonParser.getCurrentToken();
                    if (currentToken == JsonToken.END_ARRAY) {
                        String[] strArr = (String[]) leaseObjectBuffer.completeAndClearBuffer(resetAndStart, i2, String.class);
                        deserializationContext.returnObjectBuffer(leaseObjectBuffer);
                        return strArr;
                    }
                    obj = currentToken == JsonToken.VALUE_NULL ? (String) jsonDeserializer.getNullValue(deserializationContext) : (String) jsonDeserializer.deserialize(jsonParser, deserializationContext);
                } else {
                    obj = (String) jsonDeserializer.deserialize(jsonParser, deserializationContext);
                }
                if (i2 >= resetAndStart.length) {
                    i = 0;
                    resetAndStart = leaseObjectBuffer.appendCompletedChunk(resetAndStart);
                } else {
                    i = i2;
                }
                i2 = i + 1;
                resetAndStart[i] = obj;
            } catch (Exception e) {
                throw JsonMappingException.wrapWithPath((Throwable) e, (Object) String.class, i2);
            }
        }
    }

    public Object deserializeWithType(JsonParser jsonParser, DeserializationContext deserializationContext, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromArray(jsonParser, deserializationContext);
    }

    private final String[] handleNonArray(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        String str = null;
        if (this._unwrapSingle == Boolean.TRUE || (this._unwrapSingle == null && deserializationContext.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY))) {
            String[] strArr = new String[1];
            if (!jsonParser.hasToken(JsonToken.VALUE_NULL)) {
                str = _parseString(jsonParser, deserializationContext);
            }
            strArr[0] = str;
            return strArr;
        } else if (jsonParser.hasToken(JsonToken.VALUE_STRING) && deserializationContext.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jsonParser.getText().length() == 0) {
            return null;
        } else {
            throw deserializationContext.mappingException(this._valueClass);
        }
    }
}

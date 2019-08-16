package com.zopim.android.sdk.data;

import android.util.Log;
import com.fasterxml.jackson.annotation.JsonAutoDetect.Visibility;
import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.JsonFactory;
import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.SerializationFeature;
import java.io.IOException;

public class Parser<T> {
    private static final boolean DEBUG = false;
    private static final String LOG_TAG = Parser.class.getSimpleName();
    ObjectMapper mMapper = new ObjectMapper(new JsonFactory());

    Parser() {
        this.mMapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
        this.mMapper.configure(SerializationFeature.WRITE_NULL_MAP_VALUES, false);
        this.mMapper.setSerializationInclusion(Include.NON_NULL);
        this.mMapper.setVisibility(PropertyAccessor.ALL, Visibility.NONE);
        this.mMapper.setVisibility(PropertyAccessor.GETTER, Visibility.NONE);
        this.mMapper.setVisibility(PropertyAccessor.FIELD, Visibility.ANY);
    }

    /* access modifiers changed from: 0000 */
    public ObjectMapper getMapper() {
        return this.mMapper;
    }

    /* access modifiers changed from: 0000 */
    public T parse(JsonNode jsonNode, TypeReference<T> typeReference) {
        T t = null;
        if (jsonNode == null) {
            Log.i(LOG_TAG, "Can not a node that is null. Aborting.");
            return t;
        } else if (typeReference == null) {
            Log.i(LOG_TAG, "TypeReference is not set. This parse operation requires a type reference to be set. Aborting.");
            return t;
        } else {
            try {
                return parse(this.mMapper.writeValueAsString(jsonNode), typeReference);
            } catch (JsonProcessingException e) {
                Log.w(LOG_TAG, "Error writing node as string", e);
                return t;
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public T parse(String str, TypeReference<T> typeReference) {
        T t = null;
        if (str == null || str.isEmpty()) {
            Log.i(LOG_TAG, "Can not parse json message that is null or empty. Aborting.");
            return t;
        } else if (typeReference == null) {
            Log.i(LOG_TAG, "TypeReference is not set. This parse operation requires a type reference to be set. Aborting.");
            return t;
        } else {
            try {
                return this.mMapper.readValue(str, (TypeReference) typeReference);
            } catch (ClassCastException e) {
                Log.w(LOG_TAG, "Casting error while parsing message " + e.getMessage(), e);
                return t;
            } catch (JsonParseException e2) {
                Log.w(LOG_TAG, "Parsing error while parsing message " + str, e2);
                return t;
            } catch (JsonMappingException e3) {
                Log.w(LOG_TAG, "Mapping error while parsing message " + str, e3);
                return t;
            } catch (IOException e4) {
                Log.w(LOG_TAG, "IO error while parsing message " + str, e4);
                return t;
            } catch (Exception e5) {
                Log.w(LOG_TAG, "Unexpected error occurred", e5);
                return t;
            }
        }
    }
}

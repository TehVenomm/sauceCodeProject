package com.zopim.android.sdk.data;

import android.util.Log;
import com.fasterxml.jackson.annotation.JsonAutoDetect.Visibility;
import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.JsonFactory;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.SerializationFeature;

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

    ObjectMapper getMapper() {
        return this.mMapper;
    }

    T parse(JsonNode jsonNode, TypeReference<T> typeReference) {
        T t = null;
        if (jsonNode == null) {
            Log.i(LOG_TAG, "Can not a node that is null. Aborting.");
        } else if (typeReference == null) {
            Log.i(LOG_TAG, "TypeReference is not set. This parse operation requires a type reference to be set. Aborting.");
        } else {
            try {
                t = parse(this.mMapper.writeValueAsString(jsonNode), (TypeReference) typeReference);
            } catch (Throwable e) {
                Log.w(LOG_TAG, "Error writing node as string", e);
            }
        }
        return t;
    }

    T parse(String str, TypeReference<T> typeReference) {
        T t = null;
        if (str == null || str.isEmpty()) {
            Log.i(LOG_TAG, "Can not parse json message that is null or empty. Aborting.");
        } else if (typeReference == null) {
            Log.i(LOG_TAG, "TypeReference is not set. This parse operation requires a type reference to be set. Aborting.");
        } else {
            try {
                t = this.mMapper.readValue(str, (TypeReference) typeReference);
            } catch (Throwable e) {
                Log.w(LOG_TAG, "Casting error while parsing message " + e.getMessage(), e);
            } catch (Throwable e2) {
                Log.w(LOG_TAG, "Parsing error while parsing message " + str, e2);
            } catch (Throwable e22) {
                Log.w(LOG_TAG, "Mapping error while parsing message " + str, e22);
            } catch (Throwable e222) {
                Log.w(LOG_TAG, "IO error while parsing message " + str, e222);
            } catch (Throwable e2222) {
                Log.w(LOG_TAG, "Unexpected error occurred", e2222);
            }
        }
        return t;
    }
}

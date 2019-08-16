package com.fasterxml.jackson.databind;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.Version;
import com.fasterxml.jackson.core.Versioned;
import com.fasterxml.jackson.databind.ObjectWriter.Prefetch;
import com.fasterxml.jackson.databind.cfg.PackageVersion;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.DefaultSerializerProvider;
import com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap;
import com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap.SerializerAndMapResult;
import com.fasterxml.jackson.databind.ser.impl.TypeWrappedSerializer;
import java.io.Closeable;
import java.io.Flushable;
import java.io.IOException;

public class SequenceWriter implements Versioned, Closeable, Flushable {
    protected final boolean _cfgCloseCloseable = this._config.isEnabled(SerializationFeature.CLOSE_CLOSEABLE);
    protected final boolean _cfgFlush = this._config.isEnabled(SerializationFeature.FLUSH_AFTER_WRITE_VALUE);
    protected final boolean _closeGenerator;
    protected boolean _closed;
    protected final SerializationConfig _config;
    protected PropertySerializerMap _dynamicSerializers = PropertySerializerMap.emptyForRootValues();
    protected final JsonGenerator _generator;
    protected boolean _openArray;
    protected final DefaultSerializerProvider _provider;
    protected final JsonSerializer<Object> _rootSerializer;
    protected final TypeSerializer _typeSerializer;

    public SequenceWriter(DefaultSerializerProvider defaultSerializerProvider, JsonGenerator jsonGenerator, boolean z, Prefetch prefetch) throws IOException {
        this._provider = defaultSerializerProvider;
        this._generator = jsonGenerator;
        this._closeGenerator = z;
        this._rootSerializer = prefetch.getValueSerializer();
        this._typeSerializer = prefetch.getTypeSerializer();
        this._config = defaultSerializerProvider.getConfig();
    }

    public SequenceWriter init(boolean z) throws IOException {
        if (z) {
            this._generator.writeStartArray();
            this._openArray = true;
        }
        return this;
    }

    public Version version() {
        return PackageVersion.VERSION;
    }

    public SequenceWriter write(Object obj) throws IOException {
        if (obj == null) {
            this._provider.serializeValue(this._generator, null);
            return this;
        } else if (this._cfgCloseCloseable && (obj instanceof Closeable)) {
            return _writeCloseableValue(obj);
        } else {
            JsonSerializer<Object> jsonSerializer = this._rootSerializer;
            if (jsonSerializer == null) {
                Class cls = obj.getClass();
                jsonSerializer = this._dynamicSerializers.serializerFor(cls);
                if (jsonSerializer == null) {
                    jsonSerializer = _findAndAddDynamic(cls);
                }
            }
            this._provider.serializeValue(this._generator, obj, null, jsonSerializer);
            if (!this._cfgFlush) {
                return this;
            }
            this._generator.flush();
            return this;
        }
    }

    public SequenceWriter write(Object obj, JavaType javaType) throws IOException {
        if (obj == null) {
            this._provider.serializeValue(this._generator, null);
            return this;
        } else if (this._cfgCloseCloseable && (obj instanceof Closeable)) {
            return _writeCloseableValue(obj, javaType);
        } else {
            JsonSerializer serializerFor = this._dynamicSerializers.serializerFor(javaType.getRawClass());
            if (serializerFor == null) {
                serializerFor = _findAndAddDynamic(javaType);
            }
            this._provider.serializeValue(this._generator, obj, javaType, serializerFor);
            if (!this._cfgFlush) {
                return this;
            }
            this._generator.flush();
            return this;
        }
    }

    public SequenceWriter writeAll(Object[] objArr) throws IOException {
        for (Object write : objArr) {
            write(write);
        }
        return this;
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=C, code=C<java.lang.Object>, for r3v0, types: [C, C<java.lang.Object>, java.util.Collection] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public <C extends java.util.Collection<?>> com.fasterxml.jackson.databind.SequenceWriter writeAll(C<java.lang.Object> r3) throws java.io.IOException {
        /*
            r2 = this;
            java.util.Iterator r0 = r3.iterator()
        L_0x0004:
            boolean r1 = r0.hasNext()
            if (r1 == 0) goto L_0x0012
            java.lang.Object r1 = r0.next()
            r2.write(r1)
            goto L_0x0004
        L_0x0012:
            return r2
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.SequenceWriter.writeAll(java.util.Collection):com.fasterxml.jackson.databind.SequenceWriter");
    }

    public SequenceWriter writeAll(Iterable<?> iterable) throws IOException {
        for (Object write : iterable) {
            write(write);
        }
        return this;
    }

    public void flush() throws IOException {
        if (!this._closed) {
            this._generator.flush();
        }
    }

    public void close() throws IOException {
        if (!this._closed) {
            this._closed = true;
            if (this._openArray) {
                this._openArray = false;
                this._generator.writeEndArray();
            }
            if (this._closeGenerator) {
                this._generator.close();
            }
        }
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:20:0x0037 A[SYNTHETIC, Splitter:B:20:0x0037] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.SequenceWriter _writeCloseableValue(java.lang.Object r7) throws java.io.IOException {
        /*
            r6 = this;
            r2 = 0
            r0 = r7
            java.io.Closeable r0 = (java.io.Closeable) r0
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r1 = r6._rootSerializer     // Catch:{ all -> 0x0033 }
            if (r1 != 0) goto L_0x0018
            java.lang.Class r3 = r7.getClass()     // Catch:{ all -> 0x0033 }
            com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap r1 = r6._dynamicSerializers     // Catch:{ all -> 0x0033 }
            com.fasterxml.jackson.databind.JsonSerializer r1 = r1.serializerFor(r3)     // Catch:{ all -> 0x0033 }
            if (r1 != 0) goto L_0x0018
            com.fasterxml.jackson.databind.JsonSerializer r1 = r6._findAndAddDynamic(r3)     // Catch:{ all -> 0x0033 }
        L_0x0018:
            com.fasterxml.jackson.databind.ser.DefaultSerializerProvider r3 = r6._provider     // Catch:{ all -> 0x0033 }
            com.fasterxml.jackson.core.JsonGenerator r4 = r6._generator     // Catch:{ all -> 0x0033 }
            r5 = 0
            r3.serializeValue(r4, r7, r5, r1)     // Catch:{ all -> 0x0033 }
            boolean r1 = r6._cfgFlush     // Catch:{ all -> 0x0033 }
            if (r1 == 0) goto L_0x0029
            com.fasterxml.jackson.core.JsonGenerator r1 = r6._generator     // Catch:{ all -> 0x0033 }
            r1.flush()     // Catch:{ all -> 0x0033 }
        L_0x0029:
            r1 = 0
            r0.close()     // Catch:{ all -> 0x003f }
            if (r2 == 0) goto L_0x0032
            r1.close()     // Catch:{ IOException -> 0x003b }
        L_0x0032:
            return r6
        L_0x0033:
            r1 = move-exception
            r2 = r0
        L_0x0035:
            if (r2 == 0) goto L_0x003a
            r2.close()     // Catch:{ IOException -> 0x003d }
        L_0x003a:
            throw r1
        L_0x003b:
            r0 = move-exception
            goto L_0x0032
        L_0x003d:
            r0 = move-exception
            goto L_0x003a
        L_0x003f:
            r0 = move-exception
            r1 = r0
            goto L_0x0035
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.SequenceWriter._writeCloseableValue(java.lang.Object):com.fasterxml.jackson.databind.SequenceWriter");
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:18:0x0031 A[SYNTHETIC, Splitter:B:18:0x0031] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.SequenceWriter _writeCloseableValue(java.lang.Object r5, com.fasterxml.jackson.databind.JavaType r6) throws java.io.IOException {
        /*
            r4 = this;
            r0 = r5
            java.io.Closeable r0 = (java.io.Closeable) r0
            com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap r1 = r4._dynamicSerializers     // Catch:{ all -> 0x002d }
            java.lang.Class r2 = r6.getRawClass()     // Catch:{ all -> 0x002d }
            com.fasterxml.jackson.databind.JsonSerializer r1 = r1.serializerFor(r2)     // Catch:{ all -> 0x002d }
            if (r1 != 0) goto L_0x0013
            com.fasterxml.jackson.databind.JsonSerializer r1 = r4._findAndAddDynamic(r6)     // Catch:{ all -> 0x002d }
        L_0x0013:
            com.fasterxml.jackson.databind.ser.DefaultSerializerProvider r2 = r4._provider     // Catch:{ all -> 0x002d }
            com.fasterxml.jackson.core.JsonGenerator r3 = r4._generator     // Catch:{ all -> 0x002d }
            r2.serializeValue(r3, r5, r6, r1)     // Catch:{ all -> 0x002d }
            boolean r1 = r4._cfgFlush     // Catch:{ all -> 0x002d }
            if (r1 == 0) goto L_0x0023
            com.fasterxml.jackson.core.JsonGenerator r1 = r4._generator     // Catch:{ all -> 0x002d }
            r1.flush()     // Catch:{ all -> 0x002d }
        L_0x0023:
            r2 = 0
            r0.close()     // Catch:{ all -> 0x0039 }
            if (r2 == 0) goto L_0x002c
            r2.close()     // Catch:{ IOException -> 0x0035 }
        L_0x002c:
            return r4
        L_0x002d:
            r1 = move-exception
            r2 = r0
        L_0x002f:
            if (r2 == 0) goto L_0x0034
            r2.close()     // Catch:{ IOException -> 0x0037 }
        L_0x0034:
            throw r1
        L_0x0035:
            r0 = move-exception
            goto L_0x002c
        L_0x0037:
            r0 = move-exception
            goto L_0x0034
        L_0x0039:
            r0 = move-exception
            r1 = r0
            goto L_0x002f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.SequenceWriter._writeCloseableValue(java.lang.Object, com.fasterxml.jackson.databind.JavaType):com.fasterxml.jackson.databind.SequenceWriter");
    }

    private final JsonSerializer<Object> _findAndAddDynamic(Class<?> cls) throws JsonMappingException {
        SerializerAndMapResult addSerializer;
        if (this._typeSerializer == null) {
            addSerializer = this._dynamicSerializers.findAndAddRootValueSerializer(cls, (SerializerProvider) this._provider);
        } else {
            addSerializer = this._dynamicSerializers.addSerializer(cls, (JsonSerializer<Object>) new TypeWrappedSerializer<Object>(this._typeSerializer, this._provider.findValueSerializer(cls, (BeanProperty) null)));
        }
        this._dynamicSerializers = addSerializer.map;
        return addSerializer.serializer;
    }

    private final JsonSerializer<Object> _findAndAddDynamic(JavaType javaType) throws JsonMappingException {
        SerializerAndMapResult addSerializer;
        if (this._typeSerializer == null) {
            addSerializer = this._dynamicSerializers.findAndAddRootValueSerializer(javaType, (SerializerProvider) this._provider);
        } else {
            addSerializer = this._dynamicSerializers.addSerializer(javaType, (JsonSerializer<Object>) new TypeWrappedSerializer<Object>(this._typeSerializer, this._provider.findValueSerializer(javaType, (BeanProperty) null)));
        }
        this._dynamicSerializers = addSerializer.map;
        return addSerializer.serializer;
    }
}

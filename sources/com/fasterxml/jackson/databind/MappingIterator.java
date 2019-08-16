package com.fasterxml.jackson.databind;

import com.fasterxml.jackson.core.FormatSchema;
import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonStreamContext;
import com.fasterxml.jackson.core.JsonToken;
import java.io.Closeable;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.NoSuchElementException;

public class MappingIterator<T> implements Iterator<T>, Closeable {
    protected static final MappingIterator<?> EMPTY_ITERATOR = new MappingIterator<>(null, null, null, null, false, null);
    protected static final int STATE_CLOSED = 0;
    protected static final int STATE_HAS_VALUE = 3;
    protected static final int STATE_MAY_HAVE_VALUE = 2;
    protected static final int STATE_NEED_RESYNC = 1;
    protected final boolean _closeParser;
    protected final DeserializationContext _context;
    protected final JsonDeserializer<T> _deserializer;
    protected final JsonParser _parser;
    protected final JsonStreamContext _seqContext;
    protected int _state;
    protected final JavaType _type;
    protected final T _updatedValue;

    protected MappingIterator(JavaType javaType, JsonParser jsonParser, DeserializationContext deserializationContext, JsonDeserializer<?> jsonDeserializer, boolean z, Object obj) {
        this._type = javaType;
        this._parser = jsonParser;
        this._context = deserializationContext;
        this._deserializer = jsonDeserializer;
        this._closeParser = z;
        if (obj == null) {
            this._updatedValue = null;
        } else {
            this._updatedValue = obj;
        }
        if (jsonParser == null) {
            this._seqContext = null;
            this._state = 0;
            return;
        }
        JsonStreamContext parsingContext = jsonParser.getParsingContext();
        if (!z || !jsonParser.isExpectedStartArrayToken()) {
            JsonToken currentToken = jsonParser.getCurrentToken();
            if (currentToken == JsonToken.START_OBJECT || currentToken == JsonToken.START_ARRAY) {
                parsingContext = parsingContext.getParent();
            }
        } else {
            jsonParser.clearCurrentToken();
        }
        this._seqContext = parsingContext;
        this._state = 2;
    }

    protected static <T> MappingIterator<T> emptyIterator() {
        return EMPTY_ITERATOR;
    }

    public boolean hasNext() {
        try {
            return hasNextValue();
        } catch (JsonMappingException e) {
            return ((Boolean) _handleMappingException(e)).booleanValue();
        } catch (IOException e2) {
            return ((Boolean) _handleIOException(e2)).booleanValue();
        }
    }

    public T next() {
        try {
            return nextValue();
        } catch (JsonMappingException e) {
            throw new RuntimeJsonMappingException(e.getMessage(), e);
        } catch (IOException e2) {
            throw new RuntimeException(e2.getMessage(), e2);
        }
    }

    public void remove() {
        throw new UnsupportedOperationException();
    }

    public void close() throws IOException {
        if (this._state != 0) {
            this._state = 0;
            if (this._parser != null) {
                this._parser.close();
            }
        }
    }

    public boolean hasNextValue() throws IOException {
        switch (this._state) {
            case 0:
                return false;
            case 1:
                _resync();
                break;
            case 2:
                break;
            default:
                return true;
        }
        if (this._parser.getCurrentToken() == null) {
            JsonToken nextToken = this._parser.nextToken();
            if (nextToken == null || nextToken == JsonToken.END_ARRAY) {
                this._state = 0;
                if (!this._closeParser || this._parser == null) {
                    return false;
                }
                this._parser.close();
                return false;
            }
        }
        this._state = 3;
        return true;
    }

    public T nextValue() throws IOException {
        T t;
        switch (this._state) {
            case 0:
                return _throwNoSuchElement();
            case 1:
            case 2:
                if (!hasNextValue()) {
                    return _throwNoSuchElement();
                }
                break;
        }
        int i = 1;
        try {
            if (this._updatedValue == null) {
                t = this._deserializer.deserialize(this._parser, this._context);
            } else {
                this._deserializer.deserialize(this._parser, this._context, this._updatedValue);
                t = this._updatedValue;
            }
            i = 2;
            return t;
        } finally {
            this._state = i;
            this._parser.clearCurrentToken();
        }
    }

    public List<T> readAll() throws IOException {
        return readAll((L) new ArrayList());
    }

    public <L extends List<? super T>> L readAll(L l) throws IOException {
        while (hasNextValue()) {
            l.add(nextValue());
        }
        return l;
    }

    public <C extends Collection<? super T>> C readAll(C c) throws IOException {
        while (hasNextValue()) {
            c.add(nextValue());
        }
        return c;
    }

    public JsonParser getParser() {
        return this._parser;
    }

    public FormatSchema getParserSchema() {
        return this._parser.getSchema();
    }

    public JsonLocation getCurrentLocation() {
        return this._parser.getCurrentLocation();
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: CFG modification limit reached, blocks count: 121 */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x002e, code lost:
        if (r1 != null) goto L_0x0016;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void _resync() throws java.io.IOException {
        /*
            r3 = this;
            com.fasterxml.jackson.core.JsonParser r0 = r3._parser
            com.fasterxml.jackson.core.JsonStreamContext r1 = r0.getParsingContext()
            com.fasterxml.jackson.core.JsonStreamContext r2 = r3._seqContext
            if (r1 != r2) goto L_0x0016
        L_0x000a:
            return
        L_0x000b:
            com.fasterxml.jackson.core.JsonToken r2 = com.fasterxml.jackson.core.JsonToken.START_ARRAY
            if (r1 == r2) goto L_0x0013
            com.fasterxml.jackson.core.JsonToken r2 = com.fasterxml.jackson.core.JsonToken.START_OBJECT
            if (r1 != r2) goto L_0x002e
        L_0x0013:
            r0.skipChildren()
        L_0x0016:
            com.fasterxml.jackson.core.JsonToken r1 = r0.nextToken()
            com.fasterxml.jackson.core.JsonToken r2 = com.fasterxml.jackson.core.JsonToken.END_ARRAY
            if (r1 == r2) goto L_0x0022
            com.fasterxml.jackson.core.JsonToken r2 = com.fasterxml.jackson.core.JsonToken.END_OBJECT
            if (r1 != r2) goto L_0x000b
        L_0x0022:
            com.fasterxml.jackson.core.JsonStreamContext r1 = r0.getParsingContext()
            com.fasterxml.jackson.core.JsonStreamContext r2 = r3._seqContext
            if (r1 != r2) goto L_0x0016
            r0.clearCurrentToken()
            goto L_0x000a
        L_0x002e:
            if (r1 != 0) goto L_0x0016
            goto L_0x000a
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.MappingIterator._resync():void");
    }

    /* access modifiers changed from: protected */
    public <R> R _throwNoSuchElement() {
        throw new NoSuchElementException();
    }

    /* access modifiers changed from: protected */
    public <R> R _handleMappingException(JsonMappingException jsonMappingException) {
        throw new RuntimeJsonMappingException(jsonMappingException.getMessage(), jsonMappingException);
    }

    /* access modifiers changed from: protected */
    public <R> R _handleIOException(IOException iOException) {
        throw new RuntimeException(iOException.getMessage(), iOException);
    }
}

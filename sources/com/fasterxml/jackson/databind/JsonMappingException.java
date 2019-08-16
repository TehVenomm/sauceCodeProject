package com.fasterxml.jackson.databind;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.util.ClassUtil;
import java.io.Closeable;
import java.io.IOException;
import java.io.Serializable;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import org.apache.commons.lang3.ClassUtils;

public class JsonMappingException extends JsonProcessingException {
    static final int MAX_REFS_TO_LIST = 1000;
    private static final long serialVersionUID = 1;
    protected LinkedList<Reference> _path;
    protected transient Closeable _processor;

    public static class Reference implements Serializable {
        private static final long serialVersionUID = 1;
        protected String _asString;
        protected String _fieldName;
        protected Object _from;
        protected int _index = -1;

        protected Reference() {
        }

        public Reference(Object obj) {
            this._from = obj;
        }

        public Reference(Object obj, String str) {
            this._from = obj;
            if (str == null) {
                throw new NullPointerException("Can not pass null fieldName");
            }
            this._fieldName = str;
        }

        public Reference(Object obj, int i) {
            this._from = obj;
            this._index = i;
        }

        private Reference(Reference reference, String str, Object obj) {
            this._asString = str;
            this._from = obj;
            this._fieldName = reference._fieldName;
            this._index = reference._index;
        }

        public void setFrom(Object obj) {
            this._from = obj;
        }

        public void setFieldName(String str) {
            this._fieldName = str;
        }

        public void setIndex(int i) {
            this._index = i;
        }

        public Object getFrom() {
            return this._from;
        }

        public String getFieldName() {
            return this._fieldName;
        }

        public int getIndex() {
            return this._index;
        }

        public String toString() {
            Class cls;
            if (this._asString == null) {
                StringBuilder sb = new StringBuilder();
                if (this._from == null) {
                    sb.append("UNKNOWN");
                } else {
                    if (this._from instanceof Class) {
                        cls = (Class) this._from;
                    } else {
                        cls = this._from.getClass();
                    }
                    String packageName = ClassUtil.getPackageName(cls);
                    if (packageName != null) {
                        sb.append(packageName);
                        sb.append(ClassUtils.PACKAGE_SEPARATOR_CHAR);
                    }
                    sb.append(cls.getSimpleName());
                }
                sb.append('[');
                if (this._fieldName != null) {
                    sb.append('\"');
                    sb.append(this._fieldName);
                    sb.append('\"');
                } else if (this._index >= 0) {
                    sb.append(this._index);
                } else {
                    sb.append('?');
                }
                sb.append(']');
                this._asString = sb.toString();
            }
            return this._asString;
        }

        /* access modifiers changed from: 0000 */
        public Object writeReplace() {
            return new Reference(this, toString(), null);
        }
    }

    @Deprecated
    public JsonMappingException(String str) {
        super(str);
    }

    @Deprecated
    public JsonMappingException(String str, Throwable th) {
        super(str, th);
    }

    @Deprecated
    public JsonMappingException(String str, JsonLocation jsonLocation) {
        super(str, jsonLocation);
    }

    @Deprecated
    public JsonMappingException(String str, JsonLocation jsonLocation, Throwable th) {
        super(str, jsonLocation, th);
    }

    public JsonMappingException(Closeable closeable, String str) {
        super(str);
        this._processor = closeable;
        if (closeable instanceof JsonParser) {
            this._location = ((JsonParser) closeable).getTokenLocation();
        }
    }

    public JsonMappingException(Closeable closeable, String str, Throwable th) {
        super(str, th);
        this._processor = closeable;
        if (closeable instanceof JsonParser) {
            this._location = ((JsonParser) closeable).getTokenLocation();
        }
    }

    public JsonMappingException(Closeable closeable, String str, JsonLocation jsonLocation) {
        super(str, jsonLocation);
        this._processor = closeable;
    }

    public static JsonMappingException from(JsonParser jsonParser, String str) {
        return new JsonMappingException((Closeable) jsonParser, str);
    }

    public static JsonMappingException from(JsonParser jsonParser, String str, Throwable th) {
        return new JsonMappingException((Closeable) jsonParser, str, th);
    }

    public static JsonMappingException from(JsonGenerator jsonGenerator, String str) {
        return new JsonMappingException((Closeable) jsonGenerator, str, (Throwable) null);
    }

    public static JsonMappingException from(JsonGenerator jsonGenerator, String str, Throwable th) {
        return new JsonMappingException((Closeable) jsonGenerator, str, th);
    }

    public static JsonMappingException from(DeserializationContext deserializationContext, String str) {
        return new JsonMappingException((Closeable) deserializationContext.getParser(), str);
    }

    public static JsonMappingException from(DeserializationContext deserializationContext, String str, Throwable th) {
        return new JsonMappingException((Closeable) deserializationContext.getParser(), str, th);
    }

    public static JsonMappingException from(SerializerProvider serializerProvider, String str) {
        return new JsonMappingException((Closeable) null, str);
    }

    public static JsonMappingException from(SerializerProvider serializerProvider, String str, Throwable th) {
        return new JsonMappingException((Closeable) null, str, th);
    }

    public static JsonMappingException fromUnexpectedIOE(IOException iOException) {
        return new JsonMappingException((Closeable) null, String.format("Unexpected IOException (of type %s): %s", new Object[]{iOException.getClass().getName(), iOException.getMessage()}));
    }

    public static JsonMappingException wrapWithPath(Throwable th, Object obj, String str) {
        return wrapWithPath(th, new Reference(obj, str));
    }

    public static JsonMappingException wrapWithPath(Throwable th, Object obj, int i) {
        return wrapWithPath(th, new Reference(obj, i));
    }

    public static JsonMappingException wrapWithPath(Throwable th, Reference reference) {
        String str;
        Closeable closeable;
        JsonMappingException jsonMappingException;
        if (th instanceof JsonMappingException) {
            jsonMappingException = (JsonMappingException) th;
        } else {
            String message = th.getMessage();
            if (message == null || message.length() == 0) {
                str = "(was " + th.getClass().getName() + ")";
            } else {
                str = message;
            }
            if (th instanceof JsonProcessingException) {
                Object processor = ((JsonProcessingException) th).getProcessor();
                if (processor instanceof Closeable) {
                    closeable = (Closeable) processor;
                    jsonMappingException = new JsonMappingException(closeable, str, th);
                }
            }
            closeable = null;
            jsonMappingException = new JsonMappingException(closeable, str, th);
        }
        jsonMappingException.prependPath(reference);
        return jsonMappingException;
    }

    public List<Reference> getPath() {
        if (this._path == null) {
            return Collections.emptyList();
        }
        return Collections.unmodifiableList(this._path);
    }

    public String getPathReference() {
        return getPathReference(new StringBuilder()).toString();
    }

    public StringBuilder getPathReference(StringBuilder sb) {
        _appendPathDesc(sb);
        return sb;
    }

    public void prependPath(Object obj, String str) {
        prependPath(new Reference(obj, str));
    }

    public void prependPath(Object obj, int i) {
        prependPath(new Reference(obj, i));
    }

    public void prependPath(Reference reference) {
        if (this._path == null) {
            this._path = new LinkedList<>();
        }
        if (this._path.size() < 1000) {
            this._path.addFirst(reference);
        }
    }

    public String getLocalizedMessage() {
        return _buildMessage();
    }

    public String getMessage() {
        return _buildMessage();
    }

    /* access modifiers changed from: protected */
    public String _buildMessage() {
        String message = super.getMessage();
        if (this._path == null) {
            return message;
        }
        StringBuilder sb = message == null ? new StringBuilder() : new StringBuilder(message);
        sb.append(" (through reference chain: ");
        StringBuilder pathReference = getPathReference(sb);
        pathReference.append(')');
        return pathReference.toString();
    }

    public String toString() {
        return getClass().getName() + ": " + getMessage();
    }

    /* access modifiers changed from: protected */
    public void _appendPathDesc(StringBuilder sb) {
        if (this._path != null) {
            Iterator it = this._path.iterator();
            while (it.hasNext()) {
                sb.append(((Reference) it.next()).toString());
                if (it.hasNext()) {
                    sb.append("->");
                }
            }
        }
    }
}

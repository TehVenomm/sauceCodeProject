package com.google.gson.internal.bind;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;
import com.google.gson.TypeAdapter;
import com.google.gson.TypeAdapterFactory;
import com.google.gson.reflect.TypeToken;
import com.google.gson.stream.JsonReader;
import com.google.gson.stream.JsonToken;
import com.google.gson.stream.JsonWriter;
import java.io.IOException;
import java.sql.Date;
import java.text.DateFormat;
import java.text.SimpleDateFormat;

public final class SqlDateTypeAdapter extends TypeAdapter<Date> {
    public static final TypeAdapterFactory FACTORY = new C06991();
    private final DateFormat format = new SimpleDateFormat("MMM d, yyyy");

    /* renamed from: com.google.gson.internal.bind.SqlDateTypeAdapter$1 */
    static final class C06991 implements TypeAdapterFactory {
        C06991() {
        }

        public <T> TypeAdapter<T> create(Gson gson, TypeToken<T> typeToken) {
            return typeToken.getRawType() == Date.class ? new SqlDateTypeAdapter() : null;
        }
    }

    public Date read(JsonReader jsonReader) throws IOException {
        Date date;
        synchronized (this) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                date = null;
            } else {
                try {
                    date = new Date(this.format.parse(jsonReader.nextString()).getTime());
                } catch (Throwable e) {
                    throw new JsonSyntaxException(e);
                }
            }
        }
        return date;
    }

    public void write(JsonWriter jsonWriter, Date date) throws IOException {
        synchronized (this) {
            jsonWriter.value(date == null ? null : this.format.format(date));
        }
    }
}

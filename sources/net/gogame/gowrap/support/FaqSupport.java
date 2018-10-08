package net.gogame.gowrap.support;

import android.content.Context;
import android.util.JsonReader;
import android.util.JsonToken;
import android.util.Log;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.io.utils.IOUtils;
import net.gogame.gowrap.model.faq.Category;

public final class FaqSupport {
    private static final String KEY_FAQS = "faqs";

    private FaqSupport() {
    }

    public static Category getFaq(Context context, String str) {
        try {
            return parse(new File(context.getFilesDir(), "net/gogame/gowrap/faq.json.gz"), str);
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Error loading FAQ", e);
            return null;
        }
    }

    private static Category parse(File file, String str) throws IOException {
        InputStream newInputStream = IOUtils.newInputStream(file);
        try {
            Reader inputStreamReader = new InputStreamReader(newInputStream, "UTF-8");
            JsonReader jsonReader;
            Category parse;
            try {
                jsonReader = new JsonReader(inputStreamReader);
                parse = parse(jsonReader, str);
                JSONUtils.closeQuietly(jsonReader);
                IOUtils.closeQuietly(inputStreamReader);
                return parse;
            } catch (Throwable th) {
                IOUtils.closeQuietly(inputStreamReader);
            }
        } finally {
            IOUtils.closeQuietly(newInputStream);
        }
    }

    private static Category parse(JsonReader jsonReader, String str) throws IOException {
        if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
            Category category = null;
            jsonReader.beginObject();
            while (jsonReader.hasNext()) {
                if (StringUtils.isEquals(jsonReader.nextName(), KEY_FAQS)) {
                    category = parseFaqs(jsonReader, str);
                } else {
                    jsonReader.skipValue();
                }
            }
            jsonReader.endObject();
            return category;
        }
        throw new IllegalArgumentException("object expected");
    }

    private static Category parseFaqs(JsonReader jsonReader, String str) throws IOException {
        Category category = null;
        if (jsonReader.peek() == JsonToken.BEGIN_OBJECT) {
            jsonReader.beginObject();
            Category category2 = null;
            while (jsonReader.hasNext()) {
                String nextName = jsonReader.nextName();
                if (category2 != null) {
                    jsonReader.skipValue();
                } else if (StringUtils.isEquals(nextName, str)) {
                    category2 = new Category(jsonReader);
                } else if (StringUtils.isEquals(nextName, InternalConstants.DEFAULT_LOCALE)) {
                    category = new Category(jsonReader);
                } else {
                    jsonReader.skipValue();
                }
            }
            jsonReader.endObject();
            return category2 != null ? category2 : category;
        } else {
            throw new IllegalArgumentException("object expected");
        }
    }
}

package net.gogame.gowrap.model.news;

import android.util.JsonReader;
import android.util.JsonToken;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import net.gogame.gowrap.support.BaseJsonObject;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;

public class MarkupElement extends BaseJsonObject {
    private static final String KEY_CHILDREN = "children";
    private static final String KEY_LINK = "link";
    private static final String KEY_SRC = "src";
    private static final String KEY_STYLE = "style";
    private static final String KEY_TEXT = "text";
    private static final String KEY_TEXT_STYLES = "textStyles";
    private static final String KEY_TYPE = "type";
    private List<MarkupElement> children;
    private String link;
    private String src;
    private String style;
    private String text;
    private List<TextStyle> textStyles;
    private String type;

    public enum TextStyle {
        BOLD,
        ITALIC
    }

    public MarkupElement() {
    }

    public MarkupElement(JsonReader jsonReader) throws IOException {
        super(jsonReader);
    }

    /* access modifiers changed from: protected */
    public boolean doParse(JsonReader jsonReader, String str) throws IOException {
        if (StringUtils.isEquals(str, "type")) {
            this.type = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_CHILDREN)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.children = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        jsonReader.nextNull();
                        this.children.add(null);
                    } else {
                        this.children.add(new MarkupElement(jsonReader));
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        } else if (StringUtils.isEquals(str, KEY_TEXT)) {
            this.text = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_TEXT_STYLES)) {
            if (jsonReader.peek() == JsonToken.NULL) {
                jsonReader.nextNull();
                return true;
            } else if (jsonReader.peek() == JsonToken.BEGIN_ARRAY) {
                jsonReader.beginArray();
                this.textStyles = new ArrayList();
                while (jsonReader.hasNext()) {
                    if (jsonReader.peek() == JsonToken.NULL) {
                        jsonReader.nextNull();
                        this.textStyles.add(null);
                    } else if (jsonReader.peek() == JsonToken.STRING) {
                        String nextString = jsonReader.nextString();
                        if (StringUtils.isEquals(nextString, "bold")) {
                            this.textStyles.add(TextStyle.BOLD);
                        } else if (StringUtils.isEquals(nextString, "italic")) {
                            this.textStyles.add(TextStyle.ITALIC);
                        } else {
                            throw new IllegalArgumentException("unknown textStyle: " + nextString);
                        }
                    } else {
                        throw new IllegalArgumentException("string or null expected");
                    }
                }
                jsonReader.endArray();
                return true;
            } else {
                throw new IllegalArgumentException("array or null expected");
            }
        } else if (StringUtils.isEquals(str, "link")) {
            this.link = JSONUtils.optString(jsonReader);
            return true;
        } else if (StringUtils.isEquals(str, KEY_SRC)) {
            this.src = JSONUtils.optString(jsonReader);
            return true;
        } else if (!StringUtils.isEquals(str, "style")) {
            return false;
        } else {
            if (jsonReader.peek() == JsonToken.NUMBER) {
                this.style = String.valueOf(jsonReader.nextLong());
                return true;
            }
            this.style = JSONUtils.optString(jsonReader);
            return true;
        }
    }

    public String getType() {
        return this.type;
    }

    public void setType(String str) {
        this.type = str;
    }

    public List<MarkupElement> getChildren() {
        return this.children;
    }

    public void setChildren(List<MarkupElement> list) {
        this.children = list;
    }

    public String getText() {
        return this.text;
    }

    public void setText(String str) {
        this.text = str;
    }

    public List<TextStyle> getTextStyles() {
        return this.textStyles;
    }

    public void setTextStyles(List<TextStyle> list) {
        this.textStyles = list;
    }

    public String getLink() {
        return this.link;
    }

    public void setLink(String str) {
        this.link = str;
    }

    public String getSrc() {
        return this.src;
    }

    public void setSrc(String str) {
        this.src = str;
    }

    public String getStyle() {
        return this.style;
    }

    public void setStyle(String str) {
        this.style = str;
    }
}

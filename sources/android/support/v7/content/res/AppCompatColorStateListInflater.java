package android.support.p003v7.content.res;

import android.content.res.ColorStateList;
import android.content.res.Resources;
import android.content.res.Resources.Theme;
import android.content.res.TypedArray;
import android.graphics.Color;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.p000v4.graphics.ColorUtils;
import android.support.p003v7.appcompat.C0260R;
import android.util.AttributeSet;
import android.util.StateSet;
import android.util.Xml;
import java.io.IOException;
import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserException;

/* renamed from: android.support.v7.content.res.AppCompatColorStateListInflater */
final class AppCompatColorStateListInflater {
    private static final int DEFAULT_COLOR = -65536;

    private AppCompatColorStateListInflater() {
    }

    @NonNull
    public static ColorStateList createFromXml(@NonNull Resources resources, @NonNull XmlPullParser xmlPullParser, @Nullable Theme theme) throws XmlPullParserException, IOException {
        int next;
        AttributeSet asAttributeSet = Xml.asAttributeSet(xmlPullParser);
        do {
            next = xmlPullParser.next();
            if (next == 2) {
                break;
            }
        } while (next != 1);
        if (next == 2) {
            return createFromXmlInner(resources, xmlPullParser, asAttributeSet, theme);
        }
        throw new XmlPullParserException("No start tag found");
    }

    @NonNull
    private static ColorStateList createFromXmlInner(@NonNull Resources resources, @NonNull XmlPullParser xmlPullParser, @NonNull AttributeSet attributeSet, @Nullable Theme theme) throws XmlPullParserException, IOException {
        String name = xmlPullParser.getName();
        if (name.equals("selector")) {
            return inflate(resources, xmlPullParser, attributeSet, theme);
        }
        throw new XmlPullParserException(xmlPullParser.getPositionDescription() + ": invalid color state list tag " + name);
    }

    private static ColorStateList inflate(@NonNull Resources resources, @NonNull XmlPullParser xmlPullParser, @NonNull AttributeSet attributeSet, @Nullable Theme theme) throws XmlPullParserException, IOException {
        int i;
        int depth = xmlPullParser.getDepth() + 1;
        int[][] iArr = new int[20][];
        int i2 = 0;
        int[] iArr2 = new int[iArr.length];
        while (true) {
            int next = xmlPullParser.next();
            if (next == 1) {
                break;
            }
            int depth2 = xmlPullParser.getDepth();
            if (depth2 < depth && next == 3) {
                break;
            } else if (next == 2 && depth2 <= depth && xmlPullParser.getName().equals("item")) {
                TypedArray obtainAttributes = obtainAttributes(resources, theme, attributeSet, C0260R.styleable.ColorStateListItem);
                int color = obtainAttributes.getColor(C0260R.styleable.ColorStateListItem_android_color, -65281);
                float f = 1.0f;
                if (obtainAttributes.hasValue(C0260R.styleable.ColorStateListItem_android_alpha)) {
                    f = obtainAttributes.getFloat(C0260R.styleable.ColorStateListItem_android_alpha, 1.0f);
                } else if (obtainAttributes.hasValue(C0260R.styleable.ColorStateListItem_alpha)) {
                    f = obtainAttributes.getFloat(C0260R.styleable.ColorStateListItem_alpha, 1.0f);
                }
                obtainAttributes.recycle();
                int attributeCount = attributeSet.getAttributeCount();
                int[] iArr3 = new int[attributeCount];
                int i3 = 0;
                int i4 = 0;
                while (i4 < attributeCount) {
                    int attributeNameResource = attributeSet.getAttributeNameResource(i4);
                    if (attributeNameResource == 16843173 || attributeNameResource == 16843551 || attributeNameResource == C0260R.attr.alpha) {
                        i = i3;
                    } else {
                        int i5 = i3 + 1;
                        if (!attributeSet.getAttributeBooleanValue(i4, false)) {
                            attributeNameResource = -attributeNameResource;
                        }
                        iArr3[i3] = attributeNameResource;
                        i = i5;
                    }
                    i4++;
                    i3 = i;
                }
                int[] trimStateSet = StateSet.trimStateSet(iArr3, i3);
                int modulateColorAlpha = modulateColorAlpha(color, f);
                if (i2 == 0 || trimStateSet.length == 0) {
                }
                iArr2 = GrowingArrayUtils.append(iArr2, i2, modulateColorAlpha);
                i2++;
                iArr = (int[][]) GrowingArrayUtils.append((T[]) iArr, i2, trimStateSet);
            }
        }
        int[] iArr4 = new int[i2];
        int[][] iArr5 = new int[i2][];
        System.arraycopy(iArr2, 0, iArr4, 0, i2);
        System.arraycopy(iArr, 0, iArr5, 0, i2);
        return new ColorStateList(iArr5, iArr4);
    }

    private static int modulateColorAlpha(int i, float f) {
        return ColorUtils.setAlphaComponent(i, Math.round(((float) Color.alpha(i)) * f));
    }

    private static TypedArray obtainAttributes(Resources resources, Theme theme, AttributeSet attributeSet, int[] iArr) {
        return theme == null ? resources.obtainAttributes(attributeSet, iArr) : theme.obtainStyledAttributes(attributeSet, iArr, 0, 0);
    }
}

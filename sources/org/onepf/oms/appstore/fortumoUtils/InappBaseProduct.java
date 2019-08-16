package org.onepf.oms.appstore.fortumoUtils;

import android.text.TextUtils;
import java.util.Currency;
import java.util.HashMap;
import java.util.Locale;
import org.jetbrains.annotations.NotNull;

public class InappBaseProduct {
    public static final String PUBLISHED = "published";
    public static final String UNPUBLISHED = "unpublished";
    boolean autoFill;
    String baseDescription;
    float basePrice;
    String baseTitle;
    final HashMap<String, String> localeToDescriptionMap = new HashMap<>();
    final HashMap<String, Float> localeToPrice = new HashMap<>();
    final HashMap<String, String> localeToTitleMap = new HashMap<>();
    String productId;
    boolean published;

    public InappBaseProduct() {
    }

    public InappBaseProduct(@NotNull InappBaseProduct inappBaseProduct) {
        this.published = inappBaseProduct.published;
        this.productId = inappBaseProduct.productId;
        this.baseTitle = inappBaseProduct.baseTitle;
        this.baseDescription = inappBaseProduct.baseDescription;
        this.basePrice = inappBaseProduct.basePrice;
        this.localeToTitleMap.putAll(inappBaseProduct.localeToTitleMap);
        this.localeToDescriptionMap.putAll(inappBaseProduct.localeToDescriptionMap);
        this.localeToPrice.putAll(inappBaseProduct.localeToPrice);
    }

    public void addCountryPrice(String str, float f) {
        this.localeToPrice.put(str, Float.valueOf(f));
    }

    public void addDescriptionLocalization(String str, String str2) {
        this.localeToDescriptionMap.put(str, str2);
    }

    public void addTitleLocalization(String str, String str2) {
        this.localeToTitleMap.put(str, str2);
    }

    public String getBaseDescription() {
        return this.baseDescription;
    }

    public float getBasePrice() {
        return this.basePrice;
    }

    public String getBaseTitle() {
        return this.baseTitle;
    }

    public String getDescription() {
        return getDescriptionByLocale(Locale.getDefault().toString());
    }

    public String getDescriptionByLocale(String str) {
        String str2 = (String) this.localeToDescriptionMap.get(str);
        return !TextUtils.isEmpty(str2) ? str2 : this.baseDescription;
    }

    public float getPriceByCountryCode(String str) {
        Float f = (Float) this.localeToPrice.get(str);
        return f != null ? f.floatValue() : this.basePrice;
    }

    public String getPriceDetails() {
        Locale locale = Locale.getDefault();
        Float f = (Float) this.localeToPrice.get(locale.getCountry());
        return String.format("%.2f %s", new Object[]{Float.valueOf(f != null ? f.floatValue() : this.basePrice), f != null ? Currency.getInstance(locale).getSymbol() : Currency.getInstance(Locale.US).getSymbol()});
    }

    public String getProductId() {
        return this.productId;
    }

    public String getTitle() {
        return getTitleByLocale(Locale.getDefault().toString());
    }

    public String getTitleByLocale(String str) {
        String str2 = (String) this.localeToTitleMap.get(str);
        return !TextUtils.isEmpty(str2) ? str2 : this.baseTitle;
    }

    /* access modifiers changed from: protected */
    @NotNull
    public StringBuilder getValidateInfo() {
        StringBuilder sb = new StringBuilder();
        if (TextUtils.isEmpty(this.productId)) {
            sb.append("product id is empty");
        }
        if (TextUtils.isEmpty(this.baseTitle)) {
            if (sb.length() > 0) {
                sb.append(", ");
            }
            sb.append("base title is empty");
        }
        if (TextUtils.isEmpty(this.baseDescription)) {
            if (sb.length() > 0) {
                sb.append(", ");
            }
            sb.append("base description is empty");
        }
        if (this.basePrice == 0.0f) {
            if (sb.length() > 0) {
                sb.append(", ");
            }
            sb.append("base price is not defined");
        }
        return sb;
    }

    public boolean isAutoFill() {
        return this.autoFill;
    }

    public boolean isPublished() {
        return this.published;
    }

    public void setAutoFill(boolean z) {
        this.autoFill = z;
    }

    public void setBaseDescription(String str) {
        this.baseDescription = str;
    }

    public void setBasePrice(float f) {
        this.basePrice = f;
    }

    public void setBaseTitle(String str) {
        this.baseTitle = str;
    }

    public void setProductId(String str) {
        this.productId = str;
    }

    public void setPublished(@NotNull String str) {
        if (str.equals(PUBLISHED) || str.equals(UNPUBLISHED)) {
            this.published = str.equals(PUBLISHED);
            return;
        }
        throw new IllegalArgumentException("Wrong \"publish-state\" attr value " + str);
    }

    public String toString() {
        return "InappBaseProduct{published=" + this.published + ", productId='" + this.productId + '\'' + ", baseTitle='" + this.baseTitle + '\'' + ", localeToTitleMap=" + this.localeToTitleMap + ", baseDescription='" + this.baseDescription + '\'' + ", localeToDescriptionMap=" + this.localeToDescriptionMap + ", autoFill=" + this.autoFill + ", basePrice=" + this.basePrice + ", localeToPrice=" + this.localeToPrice + '}';
    }

    public void validateItem() {
        StringBuilder validateInfo = getValidateInfo();
        if (validateInfo.length() > 0) {
            throw new IllegalStateException("in-app product is not valid: " + validateInfo.toString());
        }
    }
}

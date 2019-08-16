package org.onepf.oms.appstore.fortumoUtils;

import android.text.TextUtils;
import org.jetbrains.annotations.NotNull;

public class InappSubscriptionProduct extends InappBaseProduct {
    public static final String ONE_MONTH = "oneMonth";
    public static final String ONE_YEAR = "oneYear";
    private String period;

    public InappSubscriptionProduct(@NotNull InappBaseProduct inappBaseProduct, String str) {
        super(inappBaseProduct);
        this.period = str;
    }

    public String getPeriod() {
        return this.period;
    }

    public void setPeriod(@NotNull String str) {
        if (str.equals(ONE_MONTH) || str.equals(ONE_YEAR)) {
            this.period = str;
            return;
        }
        throw new IllegalStateException("Wrong period value!");
    }

    @NotNull
    public String toString() {
        return "InappSubscriptionProduct{published=" + this.published + ", productId='" + this.productId + '\'' + ", baseTitle='" + this.baseTitle + '\'' + ", localeToTitleMap=" + this.localeToTitleMap + ", baseDescription='" + this.baseDescription + '\'' + ", localeToDescriptionMap=" + this.localeToDescriptionMap + ", autoFill=" + this.autoFill + ", basePrice=" + this.basePrice + ", localeToPrice=" + this.localeToPrice + ", period='" + this.period + '\'' + '}';
    }

    public void validateItem() {
        StringBuilder validateInfo = getValidateInfo();
        if (TextUtils.isEmpty(this.period) || (!this.period.equals(ONE_MONTH) && !this.period.equals(ONE_YEAR))) {
            if (validateInfo.length() > 0) {
                validateInfo.append(", ");
            }
            validateInfo.append("period is not valid");
        }
        if (validateInfo.length() > 0) {
            throw new IllegalStateException("subscription product is not valid: " + validateInfo);
        }
    }
}

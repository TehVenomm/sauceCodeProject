package com.google.android.apps.analytics;

public class Item {
    private final String itemCategory;
    private final long itemCount;
    private final String itemName;
    private final double itemPrice;
    private final String itemSKU;
    private final String orderId;

    public static class Builder {
        /* access modifiers changed from: private */
        public String itemCategory = null;
        /* access modifiers changed from: private */
        public final long itemCount;
        /* access modifiers changed from: private */
        public String itemName = null;
        /* access modifiers changed from: private */
        public final double itemPrice;
        /* access modifiers changed from: private */
        public final String itemSKU;
        /* access modifiers changed from: private */
        public final String orderId;

        public Builder(String str, String str2, double d, long j) {
            if (str == null || str.trim().length() == 0) {
                throw new IllegalArgumentException("orderId must not be empty or null");
            } else if (str2 == null || str2.trim().length() == 0) {
                throw new IllegalArgumentException("itemSKU must not be empty or null");
            } else {
                this.orderId = str;
                this.itemSKU = str2;
                this.itemPrice = d;
                this.itemCount = j;
            }
        }

        public Item build() {
            return new Item(this);
        }

        public Builder setItemCategory(String str) {
            this.itemCategory = str;
            return this;
        }

        public Builder setItemName(String str) {
            this.itemName = str;
            return this;
        }
    }

    private Item(Builder builder) {
        this.orderId = builder.orderId;
        this.itemSKU = builder.itemSKU;
        this.itemPrice = builder.itemPrice;
        this.itemCount = builder.itemCount;
        this.itemName = builder.itemName;
        this.itemCategory = builder.itemCategory;
    }

    /* access modifiers changed from: 0000 */
    public String getItemCategory() {
        return this.itemCategory;
    }

    /* access modifiers changed from: 0000 */
    public long getItemCount() {
        return this.itemCount;
    }

    /* access modifiers changed from: 0000 */
    public String getItemName() {
        return this.itemName;
    }

    /* access modifiers changed from: 0000 */
    public double getItemPrice() {
        return this.itemPrice;
    }

    /* access modifiers changed from: 0000 */
    public String getItemSKU() {
        return this.itemSKU;
    }

    /* access modifiers changed from: 0000 */
    public String getOrderId() {
        return this.orderId;
    }
}

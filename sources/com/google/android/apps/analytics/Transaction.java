package com.google.android.apps.analytics;

public class Transaction {
    private final String orderId;
    private final double shippingCost;
    private final String storeName;
    private final double totalCost;
    private final double totalTax;

    public static class Builder {
        /* access modifiers changed from: private */
        public final String orderId;
        /* access modifiers changed from: private */
        public double shippingCost = 0.0d;
        /* access modifiers changed from: private */
        public String storeName = null;
        /* access modifiers changed from: private */
        public final double totalCost;
        /* access modifiers changed from: private */
        public double totalTax = 0.0d;

        public Builder(String str, double d) {
            if (str == null || str.trim().length() == 0) {
                throw new IllegalArgumentException("orderId must not be empty or null");
            }
            this.orderId = str;
            this.totalCost = d;
        }

        public Transaction build() {
            return new Transaction(this);
        }

        public Builder setShippingCost(double d) {
            this.shippingCost = d;
            return this;
        }

        public Builder setStoreName(String str) {
            this.storeName = str;
            return this;
        }

        public Builder setTotalTax(double d) {
            this.totalTax = d;
            return this;
        }
    }

    private Transaction(Builder builder) {
        this.orderId = builder.orderId;
        this.totalCost = builder.totalCost;
        this.storeName = builder.storeName;
        this.totalTax = builder.totalTax;
        this.shippingCost = builder.shippingCost;
    }

    /* access modifiers changed from: 0000 */
    public String getOrderId() {
        return this.orderId;
    }

    /* access modifiers changed from: 0000 */
    public double getShippingCost() {
        return this.shippingCost;
    }

    /* access modifiers changed from: 0000 */
    public String getStoreName() {
        return this.storeName;
    }

    /* access modifiers changed from: 0000 */
    public double getTotalCost() {
        return this.totalCost;
    }

    /* access modifiers changed from: 0000 */
    public double getTotalTax() {
        return this.totalTax;
    }
}

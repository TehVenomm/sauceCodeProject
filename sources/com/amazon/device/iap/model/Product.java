package com.amazon.device.iap.model;

import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import com.amazon.device.iap.internal.model.ProductBuilder;
import com.amazon.device.iap.internal.util.C0408d;
import org.json.JSONException;
import org.json.JSONObject;

public final class Product implements Parcelable {
    public static final Creator<Product> CREATOR = new Creator<Product>() {
        public Product createFromParcel(Parcel parcel) {
            return new Product(parcel);
        }

        public Product[] newArray(int i) {
            return new Product[i];
        }
    };
    private static final String DESCRIPTION = "description";
    private static final String PRICE = "price";
    private static final String PRODUCT_TYPE = "productType";
    private static final String SKU = "sku";
    private static final String SMALL_ICON_URL = "smallIconUrl";
    private static final String TITLE = "title";
    private final String description;
    private final String price;
    private final ProductType productType;
    private final String sku;
    private final String smallIconUrl;
    private final String title;

    private Product(Parcel parcel) {
        this.sku = parcel.readString();
        this.productType = ProductType.valueOf(parcel.readString());
        this.description = parcel.readString();
        this.price = parcel.readString();
        this.smallIconUrl = parcel.readString();
        this.title = parcel.readString();
    }

    public Product(ProductBuilder productBuilder) {
        C0408d.m164a((Object) productBuilder.getSku(), SKU);
        C0408d.m164a((Object) productBuilder.getProductType(), PRODUCT_TYPE);
        C0408d.m164a((Object) productBuilder.getDescription(), "description");
        C0408d.m164a((Object) productBuilder.getTitle(), "title");
        C0408d.m164a((Object) productBuilder.getSmallIconUrl(), SMALL_ICON_URL);
        if (ProductType.SUBSCRIPTION != productBuilder.getProductType()) {
            C0408d.m164a((Object) productBuilder.getPrice(), "price");
        }
        this.sku = productBuilder.getSku();
        this.productType = productBuilder.getProductType();
        this.description = productBuilder.getDescription();
        this.price = productBuilder.getPrice();
        this.smallIconUrl = productBuilder.getSmallIconUrl();
        this.title = productBuilder.getTitle();
    }

    public int describeContents() {
        return 0;
    }

    public String getDescription() {
        return this.description;
    }

    public String getPrice() {
        return this.price;
    }

    public ProductType getProductType() {
        return this.productType;
    }

    public String getSku() {
        return this.sku;
    }

    public String getSmallIconUrl() {
        return this.smallIconUrl;
    }

    public String getTitle() {
        return this.title;
    }

    public JSONObject toJSON() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put(SKU, this.sku);
        jSONObject.put(PRODUCT_TYPE, this.productType);
        jSONObject.put("description", this.description);
        jSONObject.put("price", this.price);
        jSONObject.put(SMALL_ICON_URL, this.smallIconUrl);
        jSONObject.put("title", this.title);
        return jSONObject;
    }

    public String toString() {
        boolean z = false;
        try {
            return toJSON().toString(4);
        } catch (JSONException e) {
            return z;
        }
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(this.sku);
        parcel.writeString(this.productType.toString());
        parcel.writeString(this.description);
        parcel.writeString(this.price);
        parcel.writeString(this.smallIconUrl);
        parcel.writeString(this.title);
    }
}

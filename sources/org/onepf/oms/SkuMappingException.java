package org.onepf.oms;

import org.jetbrains.annotations.NotNull;

public class SkuMappingException extends IllegalArgumentException {
    public static final int REASON_SKU = 1;
    public static final int REASON_STORE_NAME = 2;
    public static final int REASON_STORE_SKU = 3;

    public SkuMappingException() {
        super("Error while map sku.");
    }

    public SkuMappingException(String str) {
        super(str);
    }

    @NotNull
    public static SkuMappingException newInstance(int i) {
        switch (i) {
            case 1:
                return new SkuMappingException("Sku can't be null or empty value.");
            case 2:
                return new SkuMappingException("Store name can't be null or empty value.");
            case 3:
                return new SkuMappingException("Store sku can't be null or empty value.");
            default:
                return new SkuMappingException();
        }
    }
}

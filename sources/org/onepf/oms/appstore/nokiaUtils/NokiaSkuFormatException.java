package org.onepf.oms.appstore.nokiaUtils;

import org.onepf.oms.SkuMappingException;

public class NokiaSkuFormatException extends SkuMappingException {
    public NokiaSkuFormatException() {
        super("Nokia Store SKU can contain only digits.");
    }
}

package org.onepf.oms.appstore.nokiaUtils;

import org.onepf.oms.appstore.googleUtils.IabResult;

public class NokiaResult extends IabResult {
    public static final int RESULT_NO_SIM = 9;

    public NokiaResult(int i, String str) {
        int i2 = i == 9 ? 6 : i;
        if (i == 9) {
            str = "No sim. " + str;
        }
        super(i2, str);
    }
}

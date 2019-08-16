package p017io.fabric.sdk.android.services.common;

/* renamed from: io.fabric.sdk.android.services.common.DeliveryMechanism */
public enum DeliveryMechanism {
    DEVELOPER(1),
    USER_SIDELOAD(2),
    TEST_DISTRIBUTION(3),
    APP_STORE(4);
    
    public static final String BETA_APP_PACKAGE_NAME = "io.crash.air";

    /* renamed from: id */
    private final int f1200id;

    private DeliveryMechanism(int i) {
        this.f1200id = i;
    }

    public static DeliveryMechanism determineFrom(String str) {
        return BETA_APP_PACKAGE_NAME.equals(str) ? TEST_DISTRIBUTION : str != null ? APP_STORE : DEVELOPER;
    }

    public int getId() {
        return this.f1200id;
    }

    public String toString() {
        return Integer.toString(this.f1200id);
    }
}

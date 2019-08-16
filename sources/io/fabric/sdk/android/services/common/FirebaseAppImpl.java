package p017io.fabric.sdk.android.services.common;

import android.content.Context;
import java.lang.reflect.Method;
import p017io.fabric.sdk.android.Fabric;

/* renamed from: io.fabric.sdk.android.services.common.FirebaseAppImpl */
final class FirebaseAppImpl implements FirebaseApp {
    private static final String FIREBASE_APP_CLASS = "com.google.firebase.FirebaseApp";
    private static final String GET_INSTANCE_METHOD = "getInstance";
    private static final String IS_DATA_COLLECTION_ENABLED_METHOD = "isDataCollectionDefaultEnabled";
    private final Object firebaseAppInstance;
    private final Method isDataCollectionDefaultEnabledMethod;

    private FirebaseAppImpl(Class cls, Object obj) throws NoSuchMethodException {
        this.firebaseAppInstance = obj;
        this.isDataCollectionDefaultEnabledMethod = cls.getDeclaredMethod(IS_DATA_COLLECTION_ENABLED_METHOD, new Class[0]);
    }

    public static FirebaseApp getInstance(Context context) {
        try {
            Class loadClass = context.getClassLoader().loadClass(FIREBASE_APP_CLASS);
            return new FirebaseAppImpl(loadClass, loadClass.getDeclaredMethod(GET_INSTANCE_METHOD, new Class[0]).invoke(loadClass, new Object[0]));
        } catch (ClassNotFoundException e) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Could not find class: com.google.firebase.FirebaseApp");
        } catch (NoSuchMethodException e2) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Could not find method: " + e2.getMessage());
        } catch (Exception e3) {
            Fabric.getLogger().mo20970d(Fabric.TAG, "Unexpected error loading FirebaseApp instance.", e3);
        }
        return null;
    }

    public boolean isDataCollectionDefaultEnabled() {
        try {
            return ((Boolean) this.isDataCollectionDefaultEnabledMethod.invoke(this.firebaseAppInstance, new Object[0])).booleanValue();
        } catch (Exception e) {
            Fabric.getLogger().mo20970d(Fabric.TAG, "Cannot check isDataCollectionDefaultEnabled on FirebaseApp.", e);
            return false;
        }
    }
}

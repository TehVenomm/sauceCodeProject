package org.onepf.oms.appstore;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.text.TextUtils;
import java.io.IOException;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.regex.Pattern;
import mp.MpUtils;
import mp.PaymentRequest.PaymentRequestBuilder;
import mp.PaymentResponse;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.appstore.fortumoUtils.InappBaseProduct;
import org.onepf.oms.appstore.fortumoUtils.InappsXMLParser;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.SkuDetails;
import org.onepf.oms.util.Logger;
import org.xmlpull.v1.XmlPullParserException;

public class FortumoBillingService implements AppstoreInAppBillingService {
    private static final String SHARED_PREFS_FORTUMO = "onepf_shared_prefs_fortumo";
    private int activityRequestCode;
    private Context context;
    @Nullable
    private String developerPayload;
    private Map<String, FortumoProduct> inappsMap;
    private boolean isNook;
    @Nullable
    private OnIabPurchaseFinishedListener purchaseFinishedListener;

    static class FortumoProduct extends InappBaseProduct {
        private FortumoDetails fortumoDetails;
        private String fortumoPrice;

        public FortumoProduct(@NotNull InappBaseProduct inappBaseProduct, FortumoDetails fortumoDetails2, String str) {
            super(inappBaseProduct);
            this.fortumoDetails = fortumoDetails2;
            this.fortumoPrice = str;
        }

        public String getFortumoPrice() {
            return this.fortumoPrice;
        }

        public String getInAppSecret() {
            return this.fortumoDetails.getServiceInAppSecret();
        }

        public String getNookInAppSecret() {
            return this.fortumoDetails.getNookInAppSecret();
        }

        public String getNookServiceId() {
            return this.fortumoDetails.getNookServiceId();
        }

        public String getServiceId() {
            return this.fortumoDetails.getServiceId();
        }

        public boolean isConsumable() {
            return this.fortumoDetails.isConsumable();
        }

        @NotNull
        public SkuDetails toSkuDetails(String str) {
            return new SkuDetails("inapp", getProductId(), getTitle(), str, getDescription());
        }

        @NotNull
        public String toString() {
            return "FortumoProduct{fortumoDetails=" + this.fortumoDetails + ", fortumoPrice='" + this.fortumoPrice + '\'' + '}';
        }
    }

    static class FortumoProductParser {
        private static final String CONSUMABLE_ATTR = "consumable";
        private static final String FORTUMO_PRODUCTS_TAG = "fortumo-products";
        private static final String ID_ATTR = "id";
        private static final String NOOK_SERVICE_ID_ATTR = "nook-service-id";
        private static final String NOOK_SERVICE_INAPP_SECRET_ATTR = "nook-service-inapp-secret";
        private static final String PRODUCT_TAG = "product";
        private static final String SERVICE_ID_ATTR = "service-id";
        private static final String SERVICE_INAPP_SECRET_ATTR = "service-inapp-secret";
        private static final Pattern skuPattern = Pattern.compile("([a-z]|[0-9]){1}[a-z0-9._]*");

        static class FortumoDetails {
            private boolean consumable;

            /* renamed from: id */
            private String f1458id;
            private String nookInAppSecret;
            private String nookServiceId;
            private String serviceId;
            private String serviceInAppSecret;

            public FortumoDetails(String str, boolean z, String str2, String str3, String str4, String str5) {
                this.f1458id = str;
                this.consumable = z;
                this.serviceId = str2;
                this.serviceInAppSecret = str3;
                this.nookServiceId = str4;
                this.nookInAppSecret = str5;
            }

            public String getId() {
                return this.f1458id;
            }

            public String getNookInAppSecret() {
                return this.nookInAppSecret;
            }

            public String getNookServiceId() {
                return this.nookServiceId;
            }

            public String getServiceId() {
                return this.serviceId;
            }

            public String getServiceInAppSecret() {
                return this.serviceInAppSecret;
            }

            public boolean isConsumable() {
                return this.consumable;
            }

            @NotNull
            public String toString() {
                return "FortumoDetails{id='" + this.f1458id + '\'' + ", serviceId='" + this.serviceId + '\'' + ", serviceInAppSecret='" + this.serviceInAppSecret + '\'' + ", nookServiceId='" + this.nookServiceId + '\'' + ", nookInAppSecret='" + this.nookInAppSecret + '\'' + ", consumable=" + this.consumable + '}';
            }
        }

        private FortumoProductParser() {
        }

        /* JADX WARNING: Code restructure failed: missing block: B:48:0x0125, code lost:
            r1 = r7;
         */
        @org.jetbrains.annotations.NotNull
        /* Code decompiled incorrectly, please refer to instructions dump. */
        static java.util.Map<java.lang.String, org.onepf.oms.appstore.FortumoBillingService.FortumoProductParser.FortumoDetails> parse(@org.jetbrains.annotations.NotNull android.content.Context r13, boolean r14) throws org.xmlpull.v1.XmlPullParserException, java.io.IOException {
            /*
                r9 = 0
                r8 = 1
                r10 = 0
                org.xmlpull.v1.XmlPullParserFactory r0 = org.xmlpull.v1.XmlPullParserFactory.newInstance()
                r0.setNamespaceAware(r8)
                org.xmlpull.v1.XmlPullParser r11 = r0.newPullParser()
                android.content.res.AssetManager r0 = r13.getAssets()
                java.lang.String r1 = "fortumo_inapps_details.xml"
                java.io.InputStream r0 = r0.open(r1)
                r11.setInput(r0, r10)
                java.util.HashMap r12 = new java.util.HashMap
                r12.<init>()
                int r1 = r11.getEventType()
                r0 = r10
                r2 = r1
                r7 = r9
            L_0x0027:
                if (r2 == r8) goto L_0x0124
                java.lang.String r1 = r11.getName()
                switch(r2) {
                    case 2: goto L_0x0037;
                    case 3: goto L_0x0104;
                    default: goto L_0x0030;
                }
            L_0x0030:
                r1 = r7
            L_0x0031:
                int r2 = r11.next()
                r7 = r1
                goto L_0x0027
            L_0x0037:
                java.lang.String r2 = "fortumo-products"
                boolean r2 = r1.equals(r2)
                if (r2 == 0) goto L_0x0041
                r1 = r8
                goto L_0x0031
            L_0x0041:
                java.lang.String r2 = "product"
                boolean r1 = r1.equalsIgnoreCase(r2)
                if (r1 == 0) goto L_0x0125
                if (r7 != 0) goto L_0x0062
                java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
                java.lang.String r1 = "%s is not inside %s"
                r2 = 2
                java.lang.Object[] r2 = new java.lang.Object[r2]
                java.lang.String r3 = "product"
                r2[r9] = r3
                java.lang.String r3 = "fortumo-products"
                r2[r8] = r3
                java.lang.String r1 = java.lang.String.format(r1, r2)
                r0.<init>(r1)
                throw r0
            L_0x0062:
                java.lang.String r0 = "id"
                java.lang.String r1 = r11.getAttributeValue(r10, r0)
                java.util.regex.Pattern r0 = skuPattern
                java.util.regex.Matcher r0 = r0.matcher(r1)
                boolean r0 = r0.matches()
                if (r0 != 0) goto L_0x0084
                java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
                java.lang.String r2 = "Wrong SKU: %s. SKU must match \"([a-z]|[0-9]){1}[a-z0-9._]*\"."
                java.lang.Object[] r3 = new java.lang.Object[r8]
                r3[r9] = r1
                java.lang.String r1 = java.lang.String.format(r2, r3)
                r0.<init>(r1)
                throw r0
            L_0x0084:
                java.lang.String r0 = "service-id"
                java.lang.String r3 = r11.getAttributeValue(r10, r0)
                java.lang.String r0 = "service-inapp-secret"
                java.lang.String r4 = r11.getAttributeValue(r10, r0)
                boolean r0 = serviceInfoIsComplete(r3, r4)
                if (r0 != 0) goto L_0x00a6
                java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
                java.lang.String r2 = "%s: service data is NOT complete"
                java.lang.Object[] r3 = new java.lang.Object[r8]
                r3[r9] = r1
                java.lang.String r1 = java.lang.String.format(r2, r3)
                r0.<init>(r1)
                throw r0
            L_0x00a6:
                java.lang.String r0 = "nook-service-id"
                java.lang.String r5 = r11.getAttributeValue(r10, r0)
                java.lang.String r0 = "nook-service-inapp-secret"
                java.lang.String r6 = r11.getAttributeValue(r10, r0)
                boolean r0 = serviceInfoIsComplete(r5, r6)
                if (r0 != 0) goto L_0x00c8
                java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
                java.lang.String r2 = "%s: service data is NOT complete"
                java.lang.Object[] r3 = new java.lang.Object[r8]
                r3[r9] = r1
                java.lang.String r1 = java.lang.String.format(r2, r3)
                r0.<init>(r1)
                throw r0
            L_0x00c8:
                if (r14 == 0) goto L_0x00de
                boolean r0 = android.text.TextUtils.isEmpty(r5)
                if (r0 != 0) goto L_0x00d6
                boolean r0 = android.text.TextUtils.isEmpty(r6)
                if (r0 == 0) goto L_0x00f2
            L_0x00d6:
                java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
                java.lang.String r1 = "fortumo nook-service-id attribute and nook-service-inapp-secret values must be non-empty!"
                r0.<init>(r1)
                throw r0
            L_0x00de:
                boolean r0 = android.text.TextUtils.isEmpty(r3)
                if (r0 != 0) goto L_0x00ea
                boolean r0 = android.text.TextUtils.isEmpty(r4)
                if (r0 == 0) goto L_0x00f2
            L_0x00ea:
                java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
                java.lang.String r1 = "fortumo service-id attribute and service-inapp-secret values must be non-empty!"
                r0.<init>(r1)
                throw r0
            L_0x00f2:
                org.onepf.oms.appstore.FortumoBillingService$FortumoProductParser$FortumoDetails r0 = new org.onepf.oms.appstore.FortumoBillingService$FortumoProductParser$FortumoDetails
                java.lang.String r2 = "consumable"
                java.lang.String r2 = r11.getAttributeValue(r10, r2)
                boolean r2 = java.lang.Boolean.parseBoolean(r2)
                r0.<init>(r1, r2, r3, r4, r5, r6)
                r1 = r7
                goto L_0x0031
            L_0x0104:
                java.lang.String r2 = "product"
                boolean r2 = r1.equals(r2)
                if (r2 == 0) goto L_0x0119
                if (r0 == 0) goto L_0x0125
                java.lang.String r1 = r0.getId()
                r12.put(r1, r0)
                r1 = r7
                r0 = r10
                goto L_0x0031
            L_0x0119:
                java.lang.String r2 = "fortumo-products"
                boolean r1 = r1.equals(r2)
                if (r1 == 0) goto L_0x0125
                r1 = r9
                goto L_0x0031
            L_0x0124:
                return r12
            L_0x0125:
                r1 = r7
                goto L_0x0031
            */
            throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.appstore.FortumoBillingService.FortumoProductParser.parse(android.content.Context, boolean):java.util.Map");
        }

        private static boolean serviceInfoIsComplete(String str, String str2) {
            return !TextUtils.isEmpty(str) || TextUtils.isEmpty(str2) || (TextUtils.isEmpty(str2) && !TextUtils.isEmpty(str));
        }
    }

    public FortumoBillingService(Context context2, boolean z) {
        this.context = context2;
        this.isNook = z;
    }

    static void addPendingPayment(@NotNull Context context2, String str, String str2) {
        Editor edit = getFortumoSharedPrefs(context2).edit();
        edit.putString(str, str2);
        edit.commit();
        Logger.m1026d(str, " was added to pending");
    }

    @NotNull
    static Map<String, FortumoProduct> getFortumoInapps(@NotNull Context context2, boolean z) throws IOException, XmlPullParserException, IabException {
        HashMap hashMap = new HashMap();
        List<InappBaseProduct> list = (List) new InappsXMLParser().parse(context2).first;
        Map parse = FortumoProductParser.parse(context2, z);
        int i = 0;
        for (InappBaseProduct inappBaseProduct : list) {
            String productId = inappBaseProduct.getProductId();
            FortumoDetails fortumoDetails = (FortumoDetails) parse.get(productId);
            if (fortumoDetails == null) {
                throw new IabException(-1000, "Fortumo inapp product details were not found");
            }
            String serviceId = z ? fortumoDetails.getNookServiceId() : fortumoDetails.getServiceId();
            String serviceInAppSecret = z ? fortumoDetails.getNookInAppSecret() : fortumoDetails.getServiceInAppSecret();
            List fetchedPriceData = MpUtils.getFetchedPriceData(context2, serviceId, serviceInAppSecret);
            List list2 = ((fetchedPriceData == null || fetchedPriceData.size() == 0) && MpUtils.isSupportedOperator(context2, serviceId, serviceInAppSecret)) ? MpUtils.getFetchedPriceData(context2, serviceId, serviceInAppSecret) : fetchedPriceData;
            String str = (list2 == null || list2.isEmpty()) ? null : (String) list2.get(0);
            if (TextUtils.isEmpty(str)) {
                str = inappBaseProduct.getPriceDetails();
                if (TextUtils.isEmpty(str)) {
                    Logger.m1026d(productId, " not available for this carrier and the price is not specified in the inapps_products.xml");
                    i++;
                }
            }
            hashMap.put(productId, new FortumoProduct(inappBaseProduct, fortumoDetails, str));
        }
        if (i != list.size()) {
            return hashMap;
        }
        throw new IabException(-1000, "No inventory available for this carrier/country.");
    }

    static SharedPreferences getFortumoSharedPrefs(@NotNull Context context2) {
        return context2.getSharedPreferences(SHARED_PREFS_FORTUMO, 0);
    }

    static String getMessageIdInPending(@NotNull Context context2, String str) {
        return getFortumoSharedPrefs(context2).getString(str, null);
    }

    private String getSkuPrice(@NotNull FortumoProduct fortumoProduct) throws IabException {
        String fortumoPrice = fortumoProduct.getFortumoPrice();
        if (!TextUtils.isEmpty(fortumoPrice)) {
            String serviceId = this.isNook ? fortumoProduct.getNookServiceId() : fortumoProduct.getServiceId();
            String inAppSecret = this.isNook ? fortumoProduct.getNookInAppSecret() : fortumoProduct.getInAppSecret();
            MpUtils.fetchPaymentData(this.context, serviceId, inAppSecret);
            List fetchedPriceData = MpUtils.getFetchedPriceData(this.context, serviceId, inAppSecret);
            if (fetchedPriceData != null && !fetchedPriceData.isEmpty()) {
                return (String) fetchedPriceData.get(0);
            }
        }
        return fortumoPrice;
    }

    @NotNull
    private static Purchase purchaseFromPaymentResponse(@NotNull Context context2, @NotNull PaymentResponse paymentResponse) {
        Purchase purchase = new Purchase(OpenIabHelper.NAME_FORTUMO);
        purchase.setSku(paymentResponse.getProductName());
        purchase.setPackageName(context2.getPackageName());
        purchase.setOrderId(paymentResponse.getPaymentCode());
        Date date = paymentResponse.getDate();
        if (date != null) {
            purchase.setPurchaseTime(date.getTime());
        }
        purchase.setItemType("inapp");
        return purchase;
    }

    static void removePendingProduct(@NotNull Context context2, String str) {
        Editor edit = getFortumoSharedPrefs(context2).edit();
        edit.remove(str);
        edit.commit();
        Logger.m1026d(str, " was removed from pending");
    }

    public void consume(@NotNull Purchase purchase) throws IabException {
        removePendingProduct(this.context, purchase.getSku());
    }

    public void dispose() {
        this.purchaseFinishedListener = null;
    }

    public boolean handleActivityResult(int i, int i2, @Nullable Intent intent) {
        Purchase purchase;
        int i3 = 6;
        if (this.activityRequestCode != i) {
            return false;
        }
        if (intent == null) {
            Logger.m1025d("handleActivityResult: null intent data");
            this.purchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1002, "Null data in Fortumo IAB result"), null);
        } else {
            String str = "Purchase error.";
            if (i2 == -1) {
                PaymentResponse paymentResponse = new PaymentResponse(intent);
                purchase = purchaseFromPaymentResponse(this.context, paymentResponse);
                purchase.setDeveloperPayload(this.developerPayload);
                if (paymentResponse.getBillingStatus() == 2) {
                    i3 = 0;
                } else if (paymentResponse.getBillingStatus() == 1) {
                    Logger.m1026d("handleActivityResult: status pending for ", paymentResponse.getProductName());
                    String str2 = "Purchase is pending";
                    if (((FortumoProduct) this.inappsMap.get(paymentResponse.getProductName())).isConsumable()) {
                        addPendingPayment(this.context, paymentResponse.getProductName(), String.valueOf(paymentResponse.getMessageId()));
                        str = str2;
                        purchase = null;
                    } else {
                        str = str2;
                    }
                }
            } else {
                purchase = null;
            }
            this.developerPayload = null;
            IabResult iabResult = new IabResult(i3, str);
            Logger.m1026d("handleActivityResult: ", iabResult);
            this.purchaseFinishedListener.onIabPurchaseFinished(iabResult, purchase);
        }
        return true;
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        IabResult iabResult;
        this.purchaseFinishedListener = onIabPurchaseFinishedListener;
        this.activityRequestCode = i;
        this.developerPayload = str3;
        FortumoProduct fortumoProduct = (FortumoProduct) this.inappsMap.get(str);
        if (fortumoProduct == null) {
            Logger.m1026d("launchPurchaseFlow: required sku ", str, " was not defined");
            this.purchaseFinishedListener.onIabPurchaseFinished(new IabResult(5, String.format("Required product %s was not defined in xml files.", new Object[]{str})), null);
            return;
        }
        String messageIdInPending = getMessageIdInPending(this.context, fortumoProduct.getProductId());
        if (!fortumoProduct.isConsumable() || TextUtils.isEmpty(messageIdInPending) || messageIdInPending.equals("-1")) {
            activity.startActivityForResult(new PaymentRequestBuilder().setService(this.isNook ? fortumoProduct.getNookServiceId() : fortumoProduct.getServiceId(), this.isNook ? fortumoProduct.getNookInAppSecret() : fortumoProduct.getInAppSecret()).setConsumable(fortumoProduct.isConsumable()).setProductName(fortumoProduct.getProductId()).setDisplayString(fortumoProduct.getTitle()).build().toIntent(activity), i);
            return;
        }
        PaymentResponse paymentResponse = MpUtils.getPaymentResponse(this.context, Long.valueOf(messageIdInPending).longValue());
        Purchase purchase = null;
        int billingStatus = paymentResponse.getBillingStatus();
        if (billingStatus == 2) {
            purchase = purchaseFromPaymentResponse(this.context, paymentResponse);
            iabResult = new IabResult(0, "Purchase was successful.");
            removePendingProduct(this.context, str);
        } else if (billingStatus == 3 || billingStatus == 4) {
            iabResult = new IabResult(6, "Purchase was failed.");
            removePendingProduct(this.context, str);
        } else {
            iabResult = new IabResult(6, "Purchase is in pending.");
        }
        this.purchaseFinishedListener.onIabPurchaseFinished(iabResult, purchase);
    }

    public Inventory queryInventory(boolean z, @Nullable List<String> list, List<String> list2) throws IabException {
        Inventory inventory = new Inventory();
        SharedPreferences sharedPreferences = this.context.getSharedPreferences(SHARED_PREFS_FORTUMO, 0);
        Map all = sharedPreferences.getAll();
        if (all != null) {
            Editor edit = sharedPreferences.edit();
            for (String str : all.keySet()) {
                String str2 = (String) all.get(str);
                if (str2 != null) {
                    PaymentResponse paymentResponse = MpUtils.getPaymentResponse(this.context, Long.valueOf(str2).longValue());
                    if (paymentResponse.getBillingStatus() == 2) {
                        inventory.addPurchase(purchaseFromPaymentResponse(this.context, paymentResponse));
                    } else if (paymentResponse.getBillingStatus() == 3) {
                        edit.remove(str);
                    }
                } else {
                    all.remove(str);
                }
            }
            edit.commit();
        }
        for (FortumoProduct fortumoProduct : this.inappsMap.values()) {
            if (!fortumoProduct.isConsumable()) {
                List purchaseHistory = MpUtils.getPurchaseHistory(this.context, fortumoProduct.getServiceId(), fortumoProduct.getInAppSecret(), 5000);
                if (purchaseHistory != null && purchaseHistory.size() > 0) {
                    Iterator it = purchaseHistory.iterator();
                    while (true) {
                        if (!it.hasNext()) {
                            break;
                        }
                        PaymentResponse paymentResponse2 = (PaymentResponse) it.next();
                        if (paymentResponse2.getProductName().equals(fortumoProduct.getProductId())) {
                            inventory.addPurchase(purchaseFromPaymentResponse(this.context, paymentResponse2));
                            if (z) {
                                inventory.addSkuDetails(fortumoProduct.toSkuDetails(getSkuPrice(fortumoProduct)));
                            }
                        }
                    }
                }
            }
        }
        if (z && list != null && list.size() > 0) {
            for (String str3 : list) {
                FortumoProduct fortumoProduct2 = (FortumoProduct) this.inappsMap.get(str3);
                if (fortumoProduct2 != null) {
                    inventory.addSkuDetails(fortumoProduct2.toSkuDetails(getSkuPrice(fortumoProduct2)));
                } else {
                    throw new IabException(5, String.format("Data %s not found", new Object[]{str3}));
                }
            }
        }
        return inventory;
    }

    /* access modifiers changed from: 0000 */
    public boolean setupBilling(boolean z) {
        try {
            this.inappsMap = getFortumoInapps(this.context, z);
            return true;
        } catch (Exception e) {
            Logger.m1026d("billing is not supported due to ", e.getMessage());
            return false;
        }
    }

    public void startSetup(@NotNull OnIabSetupFinishedListener onIabSetupFinishedListener) {
        IabResult iabResult = new IabResult(0, "Fortumo: successful setup.");
        Logger.m1026d("Setup result: ", iabResult);
        onIabSetupFinishedListener.onIabSetupFinished(iabResult);
    }

    public boolean subscriptionsSupported() {
        return false;
    }
}

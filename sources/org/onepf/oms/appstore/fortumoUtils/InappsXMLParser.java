package org.onepf.oms.appstore.fortumoUtils;

import java.util.regex.Pattern;

public class InappsXMLParser {
    private static final String ATTR_AUTOFILL = "autofill";
    private static final String ATTR_COUNTRY = "country";
    private static final String ATTR_ID = "id";
    private static final String ATTR_LOCALE = "locale";
    private static final String ATTR_PERIOD = "period";
    private static final String ATTR_PUBLISH_STATE = "publish-state";
    private static final String TAG_COMMON_DESCRIPTION = "description";
    private static final String TAG_COMMON_TITLE = "title";
    private static final String TAG_INAPP_PRODUCTS = "inapp-products";
    private static final String TAG_ITEM = "item";
    private static final String TAG_ITEMS = "items";
    private static final String TAG_PRICE = "price";
    private static final String TAG_PRICE_BASE = "price-base";
    private static final String TAG_PRICE_LOCAL = "price-local";
    private static final String TAG_SUBSCRIPTION = "subscription";
    private static final String TAG_SUBSCRIPTIONS = "subscriptions";
    private static final String TAG_SUMMARY = "summary";
    private static final String TAG_SUMMARY_BASE = "summary-base";
    private static final String TAG_SUMMARY_LOCALIZATION = "summary-localization";
    private static final Pattern countryPattern = Pattern.compile("[A-Z][A-Z]");
    private static final Pattern localePattern = Pattern.compile("[a-z][a-z]_[A-Z][A-Z]");
    private static final Pattern skuPattern = Pattern.compile("([a-z]|[0-9]){1}[a-z0-9._]*");

    private static void inWrongNode(String str, String str2) {
        throw new IllegalStateException(String.format("%s is not inside %s", new Object[]{str, str2}));
    }

    private static void inWrongNode(String str, String str2, String str3) {
        throw new IllegalStateException(String.format("%s is not inside %s or %s", new Object[]{str, str2, str3}));
    }

    /* JADX WARNING: Code restructure failed: missing block: B:158:0x03f8, code lost:
        r2 = r3;
     */
    @org.jetbrains.annotations.NotNull
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public android.util.Pair<java.util.List<org.onepf.oms.appstore.fortumoUtils.InappBaseProduct>, java.util.List<org.onepf.oms.appstore.fortumoUtils.InappSubscriptionProduct>> parse(@org.jetbrains.annotations.NotNull android.content.Context r25) throws org.xmlpull.v1.XmlPullParserException, java.io.IOException {
        /*
            r24 = this;
            org.xmlpull.v1.XmlPullParserFactory r2 = org.xmlpull.v1.XmlPullParserFactory.newInstance()
            r3 = 1
            r2.setNamespaceAware(r3)
            org.xmlpull.v1.XmlPullParser r20 = r2.newPullParser()
            android.content.res.AssetManager r2 = r25.getAssets()
            java.lang.String r3 = "inapps_products.xml"
            java.io.InputStream r2 = r2.open(r3)
            r3 = 0
            r0 = r20
            r0.setInput(r2, r3)
            java.util.ArrayList r21 = new java.util.ArrayList
            r21.<init>()
            java.util.ArrayList r22 = new java.util.ArrayList
            r22.<init>()
            r9 = 0
            r7 = 0
            r3 = 0
            r8 = 0
            r5 = 0
            r6 = 0
            r4 = 0
            r18 = 0
            r17 = 0
            r10 = 0
            r14 = 0
            r15 = 0
            r13 = 0
            r12 = 0
            r11 = 0
            r16 = 0
            int r2 = r20.getEventType()
            r19 = r2
        L_0x003f:
            r2 = 1
            r0 = r19
            if (r0 == r2) goto L_0x03ee
            java.lang.String r2 = r20.getName()
            switch(r19) {
                case 2: goto L_0x0052;
                case 3: goto L_0x02a1;
                case 4: goto L_0x029b;
                default: goto L_0x004b;
            }
        L_0x004b:
            r2 = r3
        L_0x004c:
            int r19 = r20.next()
            r3 = r2
            goto L_0x003f
        L_0x0052:
            java.lang.String r19 = "inapp-products"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0060
            r18 = 1
            r2 = r3
            goto L_0x004c
        L_0x0060:
            java.lang.String r19 = "items"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0079
            if (r18 != 0) goto L_0x0075
            java.lang.String r2 = "items"
            java.lang.String r17 = "inapp-products"
            r0 = r17
            inWrongNode(r2, r0)
        L_0x0075:
            r17 = 1
            r2 = r3
            goto L_0x004c
        L_0x0079:
            java.lang.String r19 = "subscriptions"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x008f
            if (r18 != 0) goto L_0x008c
            java.lang.String r2 = "subscriptions"
            java.lang.String r14 = "inapp-products"
            inWrongNode(r2, r14)
        L_0x008c:
            r14 = 1
            r2 = r3
            goto L_0x004c
        L_0x008f:
            java.lang.String r19 = "item"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 != 0) goto L_0x00a3
            java.lang.String r19 = "subscription"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0169
        L_0x00a3:
            java.lang.String r9 = "subscription"
            boolean r2 = r2.equals(r9)
            if (r2 == 0) goto L_0x011a
            if (r14 != 0) goto L_0x00b4
            java.lang.String r2 = "subscription"
            java.lang.String r4 = "subscriptions"
            inWrongNode(r2, r4)
        L_0x00b4:
            r2 = 0
            java.lang.String r4 = "period"
            r0 = r20
            java.lang.String r4 = r0.getAttributeValue(r2, r4)
            java.lang.String r2 = "oneMonth"
            boolean r2 = r2.equals(r4)
            if (r2 != 0) goto L_0x00e9
            java.lang.String r2 = "oneYear"
            boolean r2 = r2.equals(r4)
            if (r2 != 0) goto L_0x00e9
            java.lang.IllegalStateException r2 = new java.lang.IllegalStateException
            java.lang.String r3 = "Wrong \"period\" value: %s. Must be \"%s\" or \"%s\"."
            r5 = 3
            java.lang.Object[] r5 = new java.lang.Object[r5]
            r6 = 0
            r5[r6] = r4
            r4 = 1
            java.lang.String r6 = "oneMonth"
            r5[r4] = r6
            r4 = 2
            java.lang.String r6 = "oneYear"
            r5[r4] = r6
            java.lang.String r3 = java.lang.String.format(r3, r5)
            r2.<init>(r3)
            throw r2
        L_0x00e9:
            r15 = 1
        L_0x00ea:
            org.onepf.oms.appstore.fortumoUtils.InappBaseProduct r9 = new org.onepf.oms.appstore.fortumoUtils.InappBaseProduct
            r9.<init>()
            r2 = 0
            java.lang.String r19 = "id"
            r0 = r20
            r1 = r19
            java.lang.String r2 = r0.getAttributeValue(r2, r1)
            java.util.regex.Pattern r19 = skuPattern
            r0 = r19
            java.util.regex.Matcher r19 = r0.matcher(r2)
            boolean r19 = r19.matches()
            if (r19 != 0) goto L_0x0125
            java.lang.IllegalStateException r3 = new java.lang.IllegalStateException
            java.lang.String r4 = "Wrong SKU ID: %s. SKU must match \"([a-z]|[0-9]){1}[a-z0-9._]*\""
            r5 = 1
            java.lang.Object[] r5 = new java.lang.Object[r5]
            r6 = 0
            r5[r6] = r2
            java.lang.String r2 = java.lang.String.format(r4, r5)
            r3.<init>(r2)
            throw r3
        L_0x011a:
            if (r17 != 0) goto L_0x0123
            java.lang.String r2 = "items"
            java.lang.String r9 = "items"
            inWrongNode(r2, r9)
        L_0x0123:
            r10 = 1
            goto L_0x00ea
        L_0x0125:
            r9.setProductId(r2)
            r2 = 0
            java.lang.String r19 = "publish-state"
            r0 = r20
            r1 = r19
            java.lang.String r2 = r0.getAttributeValue(r2, r1)
            java.lang.String r19 = "unpublished"
            r0 = r19
            boolean r19 = r0.equals(r2)
            if (r19 != 0) goto L_0x0163
            java.lang.String r19 = "published"
            r0 = r19
            boolean r19 = r0.equals(r2)
            if (r19 != 0) goto L_0x0163
            java.lang.IllegalStateException r3 = new java.lang.IllegalStateException
            java.lang.String r4 = "Wrong publish state value: %s. Must be \"%s\" or \"%s\""
            r5 = 3
            java.lang.Object[] r5 = new java.lang.Object[r5]
            r6 = 0
            r5[r6] = r2
            r2 = 1
            java.lang.String r6 = "unpublished"
            r5[r2] = r6
            r2 = 2
            java.lang.String r6 = "published"
            r5[r2] = r6
            java.lang.String r2 = java.lang.String.format(r4, r5)
            r3.<init>(r2)
            throw r3
        L_0x0163:
            r9.setPublished(r2)
            r2 = r3
            goto L_0x004c
        L_0x0169:
            java.lang.String r19 = "summary"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0186
            if (r10 != 0) goto L_0x0182
            if (r15 != 0) goto L_0x0182
            java.lang.String r2 = "summary"
            java.lang.String r13 = "item"
            java.lang.String r19 = "subscription"
            r0 = r19
            inWrongNode(r2, r13, r0)
        L_0x0182:
            r13 = 1
            r2 = r3
            goto L_0x004c
        L_0x0186:
            java.lang.String r19 = "summary-base"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x019d
            if (r13 != 0) goto L_0x0199
            java.lang.String r2 = "summary-base"
            java.lang.String r12 = "summary"
            inWrongNode(r2, r12)
        L_0x0199:
            r12 = 1
            r2 = r3
            goto L_0x004c
        L_0x019d:
            java.lang.String r19 = "summary-localization"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x01db
            if (r13 != 0) goto L_0x01b0
            java.lang.String r2 = "summary-localization"
            java.lang.String r5 = "summary"
            inWrongNode(r2, r5)
        L_0x01b0:
            r2 = 0
            java.lang.String r5 = "locale"
            r0 = r20
            java.lang.String r5 = r0.getAttributeValue(r2, r5)
            java.util.regex.Pattern r2 = localePattern
            java.util.regex.Matcher r2 = r2.matcher(r5)
            boolean r2 = r2.matches()
            if (r2 != 0) goto L_0x01d7
            java.lang.IllegalStateException r2 = new java.lang.IllegalStateException
            java.lang.String r3 = "Wrong \"locale\" attribute value: %s. Must match [a-z][a-z]_[A-Z][A-Z]."
            r4 = 1
            java.lang.Object[] r4 = new java.lang.Object[r4]
            r5 = 0
            r4[r5] = r6
            java.lang.String r3 = java.lang.String.format(r3, r4)
            r2.<init>(r3)
            throw r2
        L_0x01d7:
            r11 = 1
            r2 = r3
            goto L_0x004c
        L_0x01db:
            java.lang.String r19 = "title"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x01f9
            if (r12 != 0) goto L_0x03f8
            if (r11 != 0) goto L_0x03f8
            java.lang.String r2 = "title"
            java.lang.String r19 = "summary-base"
            java.lang.String r23 = "summary-localization"
            r0 = r19
            r1 = r23
            inWrongNode(r2, r0, r1)
            r2 = r3
            goto L_0x004c
        L_0x01f9:
            java.lang.String r19 = "description"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0217
            if (r12 != 0) goto L_0x03f8
            if (r11 != 0) goto L_0x03f8
            java.lang.String r2 = "description"
            java.lang.String r19 = "summary-base"
            java.lang.String r23 = "summary-localization"
            r0 = r19
            r1 = r23
            inWrongNode(r2, r0, r1)
            r2 = r3
            goto L_0x004c
        L_0x0217:
            java.lang.String r19 = "price"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0249
            if (r10 != 0) goto L_0x0232
            if (r15 != 0) goto L_0x0232
            java.lang.String r2 = "price"
            java.lang.String r16 = "item"
            java.lang.String r19 = "subscription"
            r0 = r16
            r1 = r19
            inWrongNode(r2, r0, r1)
        L_0x0232:
            r2 = 0
            java.lang.String r16 = "autofill"
            r0 = r20
            r1 = r16
            java.lang.String r2 = r0.getAttributeValue(r2, r1)
            boolean r2 = java.lang.Boolean.parseBoolean(r2)
            r9.setAutoFill(r2)
            r16 = 1
            r2 = r3
            goto L_0x004c
        L_0x0249:
            java.lang.String r19 = "price-base"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0261
            if (r16 != 0) goto L_0x03f8
            java.lang.String r2 = "price-base"
            java.lang.String r19 = "price"
            r0 = r19
            inWrongNode(r2, r0)
            r2 = r3
            goto L_0x004c
        L_0x0261:
            java.lang.String r19 = "price-local"
            r0 = r19
            boolean r2 = r2.equals(r0)
            if (r2 == 0) goto L_0x03f8
            if (r16 != 0) goto L_0x0274
            java.lang.String r2 = "price-local"
            java.lang.String r6 = "price"
            inWrongNode(r2, r6)
        L_0x0274:
            r2 = 0
            java.lang.String r6 = "country"
            r0 = r20
            java.lang.String r6 = r0.getAttributeValue(r2, r6)
            java.util.regex.Pattern r2 = countryPattern
            java.util.regex.Matcher r2 = r2.matcher(r6)
            boolean r2 = r2.matches()
            if (r2 != 0) goto L_0x03f8
            java.lang.IllegalStateException r2 = new java.lang.IllegalStateException
            java.lang.String r3 = "Wrong \"country\" attribute value: %s. Must match [A-Z][A-Z]."
            r4 = 1
            java.lang.Object[] r4 = new java.lang.Object[r4]
            r5 = 0
            r4[r5] = r6
            java.lang.String r3 = java.lang.String.format(r3, r4)
            r2.<init>(r3)
            throw r2
        L_0x029b:
            java.lang.String r2 = r20.getText()
            goto L_0x004c
        L_0x02a1:
            java.lang.String r19 = "inapp-products"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x02b0
            r18 = 0
            r2 = r3
            goto L_0x004c
        L_0x02b0:
            java.lang.String r19 = "items"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x02bf
            r17 = 0
            r2 = r3
            goto L_0x004c
        L_0x02bf:
            java.lang.String r19 = "subscriptions"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x02cd
            r14 = 0
            r2 = r3
            goto L_0x004c
        L_0x02cd:
            java.lang.String r19 = "item"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x02e3
            r9.validateItem()
            r0 = r21
            r0.add(r9)
            r9 = 0
            r2 = r3
            goto L_0x004c
        L_0x02e3:
            java.lang.String r19 = "subscription"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0300
            org.onepf.oms.appstore.fortumoUtils.InappSubscriptionProduct r2 = new org.onepf.oms.appstore.fortumoUtils.InappSubscriptionProduct
            r2.<init>(r9, r4)
            r2.validateItem()
            r0 = r22
            r0.add(r2)
            r4 = 0
            r9 = 0
            r15 = 0
            r2 = r3
            goto L_0x004c
        L_0x0300:
            java.lang.String r19 = "summary"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x030e
            r13 = 0
            r2 = r3
            goto L_0x004c
        L_0x030e:
            java.lang.String r19 = "title"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x033d
            int r2 = r3.length()
            r7 = 1
            if (r2 < r7) goto L_0x0323
            r7 = 55
            if (r2 <= r7) goto L_0x0339
        L_0x0323:
            java.lang.IllegalStateException r3 = new java.lang.IllegalStateException
            java.lang.String r4 = "Wrong title length: %d. Must be 1-55 symbols"
            r5 = 1
            java.lang.Object[] r5 = new java.lang.Object[r5]
            r6 = 0
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
            r5[r6] = r2
            java.lang.String r2 = java.lang.String.format(r4, r5)
            r3.<init>(r2)
            throw r3
        L_0x0339:
            r2 = r3
            r7 = r3
            goto L_0x004c
        L_0x033d:
            java.lang.String r19 = "description"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x036c
            int r2 = r3.length()
            r8 = 1
            if (r2 < r8) goto L_0x0352
            r8 = 80
            if (r2 <= r8) goto L_0x0368
        L_0x0352:
            java.lang.IllegalStateException r3 = new java.lang.IllegalStateException
            java.lang.String r4 = "Wrong description length: %d. Must be 1-80 symbols"
            r5 = 1
            java.lang.Object[] r5 = new java.lang.Object[r5]
            r6 = 0
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)
            r5[r6] = r2
            java.lang.String r2 = java.lang.String.format(r4, r5)
            r3.<init>(r2)
            throw r3
        L_0x0368:
            r2 = r3
            r8 = r3
            goto L_0x004c
        L_0x036c:
            java.lang.String r19 = "summary-base"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0382
            r9.setBaseTitle(r7)
            r9.setBaseDescription(r8)
            r7 = 0
            r8 = 0
            r12 = 0
            r2 = r3
            goto L_0x004c
        L_0x0382:
            java.lang.String r19 = "summary-localization"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x0399
            r9.addTitleLocalization(r5, r7)
            r9.addDescriptionLocalization(r5, r8)
            r7 = 0
            r8 = 0
            r5 = 0
            r11 = 0
            r2 = r3
            goto L_0x004c
        L_0x0399:
            java.lang.String r19 = "price-base"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 != 0) goto L_0x03ad
            java.lang.String r19 = "price-local"
            r0 = r19
            boolean r19 = r2.equals(r0)
            if (r19 == 0) goto L_0x03df
        L_0x03ad:
            float r19 = java.lang.Float.parseFloat(r3)     // Catch:{ NumberFormatException -> 0x03c3 }
            java.lang.String r23 = "price-base"
            r0 = r23
            boolean r2 = r2.equals(r0)
            if (r2 == 0) goto L_0x03d6
            r0 = r19
            r9.setBasePrice(r0)
            r2 = r3
            goto L_0x004c
        L_0x03c3:
            r2 = move-exception
            java.lang.IllegalStateException r2 = new java.lang.IllegalStateException
            java.lang.String r4 = "Wrong price: %s. Must be decimal."
            r5 = 1
            java.lang.Object[] r5 = new java.lang.Object[r5]
            r6 = 0
            r5[r6] = r3
            java.lang.String r3 = java.lang.String.format(r4, r5)
            r2.<init>(r3)
            throw r2
        L_0x03d6:
            r0 = r19
            r9.addCountryPrice(r6, r0)
            r6 = 0
            r2 = r3
            goto L_0x004c
        L_0x03df:
            java.lang.String r19 = "price"
            r0 = r19
            boolean r2 = r2.equals(r0)
            if (r2 == 0) goto L_0x03f8
            r16 = 0
            r2 = r3
            goto L_0x004c
        L_0x03ee:
            android.util.Pair r2 = new android.util.Pair
            r0 = r21
            r1 = r22
            r2.<init>(r0, r1)
            return r2
        L_0x03f8:
            r2 = r3
            goto L_0x004c
        */
        throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.appstore.fortumoUtils.InappsXMLParser.parse(android.content.Context):android.util.Pair");
    }
}

package org.onepf.oms.appstore.fortumoUtils;

import android.content.Context;
import android.util.Pair;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.FortumoStore;
import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserException;
import org.xmlpull.v1.XmlPullParserFactory;

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

    @NotNull
    public Pair<List<InappBaseProduct>, List<InappSubscriptionProduct>> parse(@NotNull Context context) throws XmlPullParserException, IOException {
        XmlPullParserFactory newInstance = XmlPullParserFactory.newInstance();
        newInstance.setNamespaceAware(true);
        XmlPullParser newPullParser = newInstance.newPullParser();
        newPullParser.setInput(context.getAssets().open(FortumoStore.IN_APP_PRODUCTS_FILE_NAME), null);
        ArrayList arrayList = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        InappBaseProduct inappBaseProduct = null;
        String str = null;
        String str2 = null;
        String str3 = null;
        String str4 = null;
        String str5 = null;
        Object obj = null;
        Object obj2 = null;
        Object obj3 = null;
        Object obj4 = null;
        Object obj5 = null;
        Object obj6 = null;
        Object obj7 = null;
        Object obj8 = null;
        Object obj9 = null;
        String str6 = null;
        for (int eventType = newPullParser.getEventType(); eventType != 1; eventType = newPullParser.next()) {
            String name = newPullParser.getName();
            switch (eventType) {
                case 2:
                    if (!name.equals(TAG_INAPP_PRODUCTS)) {
                        if (!name.equals(TAG_ITEMS)) {
                            if (!name.equals(TAG_SUBSCRIPTIONS)) {
                                if (!name.equals(TAG_ITEM) && !name.equals(TAG_SUBSCRIPTION)) {
                                    if (!name.equals(TAG_SUMMARY)) {
                                        if (!name.equals(TAG_SUMMARY_BASE)) {
                                            if (!name.equals(TAG_SUMMARY_LOCALIZATION)) {
                                                if (!name.equals("title")) {
                                                    if (!name.equals("description")) {
                                                        if (!name.equals("price")) {
                                                            if (!name.equals(TAG_PRICE_BASE)) {
                                                                if (name.equals(TAG_PRICE_LOCAL)) {
                                                                    if (obj9 == null) {
                                                                        inWrongNode(TAG_PRICE_LOCAL, "price");
                                                                    }
                                                                    str4 = newPullParser.getAttributeValue(null, ATTR_COUNTRY);
                                                                    if (countryPattern.matcher(str4).matches()) {
                                                                        break;
                                                                    }
                                                                    throw new IllegalStateException(String.format("Wrong \"country\" attribute value: %s. Must match [A-Z][A-Z].", new Object[]{str4}));
                                                                }
                                                                continue;
                                                            } else if (obj9 != null) {
                                                                break;
                                                            } else {
                                                                inWrongNode(TAG_PRICE_BASE, "price");
                                                                break;
                                                            }
                                                        }
                                                        if (obj3 == null && r15 == null) {
                                                            inWrongNode("price", TAG_ITEM, TAG_SUBSCRIPTION);
                                                        }
                                                        inappBaseProduct.setAutoFill(Boolean.parseBoolean(newPullParser.getAttributeValue(null, ATTR_AUTOFILL)));
                                                        obj9 = 1;
                                                        break;
                                                    } else if (obj7 == null && r11 == null) {
                                                        inWrongNode("description", TAG_SUMMARY_BASE, TAG_SUMMARY_LOCALIZATION);
                                                        break;
                                                    }
                                                } else if (obj7 == null && r11 == null) {
                                                    inWrongNode("title", TAG_SUMMARY_BASE, TAG_SUMMARY_LOCALIZATION);
                                                    break;
                                                }
                                            }
                                            if (obj6 == null) {
                                                inWrongNode(TAG_SUMMARY_LOCALIZATION, TAG_SUMMARY);
                                            }
                                            str3 = newPullParser.getAttributeValue(null, ATTR_LOCALE);
                                            if (localePattern.matcher(str3).matches()) {
                                                obj8 = 1;
                                                break;
                                            }
                                            throw new IllegalStateException(String.format("Wrong \"locale\" attribute value: %s. Must match [a-z][a-z]_[A-Z][A-Z].", new Object[]{str4}));
                                        }
                                        if (obj6 == null) {
                                            inWrongNode(TAG_SUMMARY_BASE, TAG_SUMMARY);
                                        }
                                        obj7 = 1;
                                        break;
                                    }
                                    if (obj3 == null && r15 == null) {
                                        inWrongNode(TAG_SUMMARY, TAG_ITEM, TAG_SUBSCRIPTION);
                                    }
                                    obj6 = 1;
                                    break;
                                }
                                if (name.equals(TAG_SUBSCRIPTION)) {
                                    if (obj4 == null) {
                                        inWrongNode(TAG_SUBSCRIPTION, TAG_SUBSCRIPTIONS);
                                    }
                                    str5 = newPullParser.getAttributeValue(null, ATTR_PERIOD);
                                    if (InappSubscriptionProduct.ONE_MONTH.equals(str5) || InappSubscriptionProduct.ONE_YEAR.equals(str5)) {
                                        obj5 = 1;
                                    } else {
                                        throw new IllegalStateException(String.format("Wrong \"period\" value: %s. Must be \"%s\" or \"%s\".", new Object[]{str5, InappSubscriptionProduct.ONE_MONTH, InappSubscriptionProduct.ONE_YEAR}));
                                    }
                                }
                                if (obj2 == null) {
                                    inWrongNode(TAG_ITEMS, TAG_ITEMS);
                                }
                                obj3 = 1;
                                inappBaseProduct = new InappBaseProduct();
                                String attributeValue = newPullParser.getAttributeValue(null, "id");
                                if (skuPattern.matcher(attributeValue).matches()) {
                                    inappBaseProduct.setProductId(attributeValue);
                                    attributeValue = newPullParser.getAttributeValue(null, ATTR_PUBLISH_STATE);
                                    if (InappBaseProduct.UNPUBLISHED.equals(attributeValue) || InappBaseProduct.PUBLISHED.equals(attributeValue)) {
                                        inappBaseProduct.setPublished(attributeValue);
                                        break;
                                    }
                                    throw new IllegalStateException(String.format("Wrong publish state value: %s. Must be \"%s\" or \"%s\"", new Object[]{attributeValue, InappBaseProduct.UNPUBLISHED, InappBaseProduct.PUBLISHED}));
                                }
                                throw new IllegalStateException(String.format("Wrong SKU ID: %s. SKU must match \"([a-z]|[0-9]){1}[a-z0-9._]*\"", new Object[]{attributeValue}));
                            }
                            if (obj == null) {
                                inWrongNode(TAG_SUBSCRIPTIONS, TAG_INAPP_PRODUCTS);
                            }
                            obj4 = 1;
                            break;
                        }
                        if (obj == null) {
                            inWrongNode(TAG_ITEMS, TAG_INAPP_PRODUCTS);
                        }
                        obj2 = 1;
                        break;
                    }
                    obj = 1;
                    break;
                    break;
                case 3:
                    if (!name.equals(TAG_INAPP_PRODUCTS)) {
                        if (!name.equals(TAG_ITEMS)) {
                            if (!name.equals(TAG_SUBSCRIPTIONS)) {
                                if (!name.equals(TAG_ITEM)) {
                                    if (!name.equals(TAG_SUBSCRIPTION)) {
                                        if (!name.equals(TAG_SUMMARY)) {
                                            if (!name.equals("title")) {
                                                if (!name.equals("description")) {
                                                    if (!name.equals(TAG_SUMMARY_BASE)) {
                                                        if (!name.equals(TAG_SUMMARY_LOCALIZATION)) {
                                                            if (!name.equals(TAG_PRICE_BASE) && !name.equals(TAG_PRICE_LOCAL)) {
                                                                if (!name.equals("price")) {
                                                                    break;
                                                                }
                                                                obj9 = null;
                                                                break;
                                                            }
                                                            try {
                                                                float parseFloat = Float.parseFloat(str6);
                                                                if (!name.equals(TAG_PRICE_BASE)) {
                                                                    inappBaseProduct.addCountryPrice(str4, parseFloat);
                                                                    str4 = null;
                                                                    break;
                                                                }
                                                                inappBaseProduct.setBasePrice(parseFloat);
                                                                break;
                                                            } catch (NumberFormatException e) {
                                                                throw new IllegalStateException(String.format("Wrong price: %s. Must be decimal.", new Object[]{str6}));
                                                            }
                                                        }
                                                        inappBaseProduct.addTitleLocalization(str3, str);
                                                        inappBaseProduct.addDescriptionLocalization(str3, str2);
                                                        str = null;
                                                        str2 = null;
                                                        str3 = null;
                                                        obj8 = null;
                                                        break;
                                                    }
                                                    inappBaseProduct.setBaseTitle(str);
                                                    inappBaseProduct.setBaseDescription(str2);
                                                    str = null;
                                                    str2 = null;
                                                    obj7 = null;
                                                    break;
                                                }
                                                int length = str6.length();
                                                if (length >= 1 && length <= 80) {
                                                    str2 = str6;
                                                    break;
                                                }
                                                throw new IllegalStateException(String.format("Wrong description length: %d. Must be 1-80 symbols", new Object[]{Integer.valueOf(length)}));
                                            }
                                            int length2 = str6.length();
                                            if (length2 >= 1 && length2 <= 55) {
                                                str = str6;
                                                break;
                                            }
                                            throw new IllegalStateException(String.format("Wrong title length: %d. Must be 1-55 symbols", new Object[]{Integer.valueOf(length2)}));
                                        }
                                        obj6 = null;
                                        break;
                                    }
                                    InappSubscriptionProduct inappSubscriptionProduct = new InappSubscriptionProduct(inappBaseProduct, str5);
                                    inappSubscriptionProduct.validateItem();
                                    arrayList2.add(inappSubscriptionProduct);
                                    str5 = null;
                                    inappBaseProduct = null;
                                    obj5 = null;
                                    break;
                                }
                                inappBaseProduct.validateItem();
                                arrayList.add(inappBaseProduct);
                                inappBaseProduct = null;
                                break;
                            }
                            obj4 = null;
                            break;
                        }
                        obj2 = null;
                        break;
                    }
                    obj = null;
                    break;
                    break;
                case 4:
                    str6 = newPullParser.getText();
                    break;
                default:
                    break;
            }
        }
        return new Pair(arrayList, arrayList2);
    }
}

.class final Lcom/facebook/appevents/suggestedevents/FeatureExtractor;
.super Ljava/lang/Object;
.source "FeatureExtractor.java"


# static fields
.field private static final NUM_OF_FEATURES:I = 0x1e

.field private static final REGEX_ADD_TO_CART_BUTTON_TEXT:Ljava/lang/String; = "(?i)add to(\\s|\\Z)|update(\\s|\\Z)|cart"

.field private static final REGEX_ADD_TO_CART_PAGE_TITLE:Ljava/lang/String; = "(?i)add to(\\s|\\Z)|update(\\s|\\Z)|cart|shop|buy"

.field private static final REGEX_CR_HAS_CONFIRM_PASSWORD_FIELD:Ljava/lang/String; = "(?i)(confirm.*password)|(password.*(confirmation|confirm)|confirmation)"

.field private static final REGEX_CR_HAS_LOG_IN_KEYWORDS:Ljava/lang/String; = "(?i)(sign in)|login|signIn"

.field private static final REGEX_CR_HAS_SIGN_ON_KEYWORDS:Ljava/lang/String; = "(?i)(sign.*(up|now)|registration|register|(create|apply).*(profile|account)|open.*account|account.*(open|creation|application)|enroll|join.*now)"

.field private static final REGEX_CR_PASSWORD_FIELD:Ljava/lang/String; = "password"

.field private static eventInfo:Ljava/util/Map; = null
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static initialized:Z = false

.field private static languageInfo:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static rules:Lorg/json/JSONObject;

.field private static textTypeInfo:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static getDenseFeatures(Lorg/json/JSONObject;Ljava/lang/String;)[F
    .locals 5
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 113
    :cond_0
    :try_start_0
    sget-boolean v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->initialized:Z

    if-nez v0, :cond_1

    return-object v1

    :cond_1
    const/16 v0, 0x1e

    .line 116
    new-array v0, v0, [F

    const/4 v2, 0x0

    .line 117
    invoke-static {v0, v2}, Ljava/util/Arrays;->fill([FF)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    .line 119
    :try_start_1
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 120
    new-instance v2, Lorg/json/JSONObject;

    const-string v3, "view"

    .line 121
    invoke-virtual {p0, v3}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v3, "screenname"

    .line 123
    invoke-virtual {p0, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 124
    new-instance v3, Lorg/json/JSONArray;

    invoke-direct {v3}, Lorg/json/JSONArray;-><init>()V

    .line 126
    invoke-static {v2, v3}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->pruneTree(Lorg/json/JSONObject;Lorg/json/JSONArray;)Z

    .line 127
    invoke-static {v2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->parseFeatures(Lorg/json/JSONObject;)[F

    move-result-object v4

    .line 128
    invoke-static {v0, v4}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->sum([F[F)V

    .line 130
    invoke-static {v2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->getInteractedNode(Lorg/json/JSONObject;)Lorg/json/JSONObject;

    move-result-object v4

    if-nez v4, :cond_2

    return-object v1

    .line 135
    :cond_2
    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v2

    .line 134
    invoke-static {v4, v3, p0, v2, p1}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->nonparseFeatures(Lorg/json/JSONObject;Lorg/json/JSONArray;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)[F

    move-result-object p0

    .line 136
    invoke-static {v0, p0}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->sum([F[F)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    return-object v0

    :catch_0
    return-object v0

    :catch_1
    move-exception p0

    .line 142
    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method private static getInteractedNode(Lorg/json/JSONObject;)Lorg/json/JSONObject;
    .locals 3
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    :try_start_0
    const-string v0, "is_interacted"

    .line 404
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    return-object p0

    :cond_1
    const-string v0, "childviews"

    .line 408
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p0

    if-nez p0, :cond_2

    return-object v1

    :cond_2
    const/4 v0, 0x0

    .line 413
    :goto_0
    invoke-virtual {p0}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge v0, v2, :cond_4

    .line 414
    invoke-virtual {p0, v0}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v2

    invoke-static {v2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->getInteractedNode(Lorg/json/JSONObject;)Lorg/json/JSONObject;

    move-result-object v2
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    if-eqz v2, :cond_3

    return-object v2

    :cond_3
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :catch_0
    move-exception p0

    .line 423
    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    :catch_1
    :cond_4
    return-object v1
.end method

.method static getTextFeature(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 107
    :cond_0
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, " | "

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ", "

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method static initialize(Ljava/io/File;)V
    .locals 3

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 62
    :cond_0
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    sput-object v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->rules:Lorg/json/JSONObject;

    .line 63
    new-instance v0, Ljava/io/FileInputStream;

    invoke-direct {v0, p0}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    .line 64
    invoke-virtual {v0}, Ljava/io/InputStream;->available()I

    move-result p0

    .line 65
    new-array p0, p0, [B

    .line 66
    invoke-virtual {v0, p0}, Ljava/io/InputStream;->read([B)I

    .line 67
    invoke-virtual {v0}, Ljava/io/InputStream;->close()V

    .line 68
    new-instance v0, Lorg/json/JSONObject;

    new-instance v1, Ljava/lang/String;

    const-string v2, "UTF-8"

    invoke-direct {v1, p0, v2}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    sput-object v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->rules:Lorg/json/JSONObject;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    .line 75
    :try_start_1
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    sput-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->languageInfo:Ljava/util/Map;

    .line 76
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->languageInfo:Ljava/util/Map;

    const-string v0, "ENGLISH"

    const-string v1, "1"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 77
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->languageInfo:Ljava/util/Map;

    const-string v0, "GERMAN"

    const-string v1, "2"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 78
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->languageInfo:Ljava/util/Map;

    const-string v0, "SPANISH"

    const-string v1, "3"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 79
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->languageInfo:Ljava/util/Map;

    const-string v0, "JAPANESE"

    const-string v1, "4"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 81
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    sput-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    .line 82
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "VIEW_CONTENT"

    const-string v1, "0"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 83
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "SEARCH"

    const-string v1, "1"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 84
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "ADD_TO_CART"

    const-string v1, "2"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 85
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "ADD_TO_WISHLIST"

    const-string v1, "3"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 86
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "INITIATE_CHECKOUT"

    const-string v1, "4"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 87
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "ADD_PAYMENT_INFO"

    const-string v1, "5"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 88
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "PURCHASE"

    const-string v1, "6"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 89
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "LEAD"

    const-string v1, "7"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 90
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    const-string v0, "COMPLETE_REGISTRATION"

    const-string v1, "8"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 92
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    sput-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->textTypeInfo:Ljava/util/Map;

    .line 93
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->textTypeInfo:Ljava/util/Map;

    const-string v0, "BUTTON_TEXT"

    const-string v1, "1"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 94
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->textTypeInfo:Ljava/util/Map;

    const-string v0, "PAGE_TITLE"

    const-string v1, "2"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 95
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->textTypeInfo:Ljava/util/Map;

    const-string v0, "RESOLVED_DOCUMENT_LINK"

    const-string v1, "3"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 96
    sget-object p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->textTypeInfo:Ljava/util/Map;

    const-string v0, "BUTTON_ID"

    const-string v1, "4"

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const/4 p0, 0x1

    .line 98
    sput-boolean p0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->initialized:Z
    :try_end_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_0

    return-void

    :catch_0
    move-exception p0

    .line 99
    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void

    :catch_1
    return-void
.end method

.method private static isButton(Lorg/json/JSONObject;)Z
    .locals 2

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    :cond_0
    :try_start_0
    const-string v0, "classtypebitmask"

    .line 372
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;)I

    move-result p0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    and-int/lit8 p0, p0, 0x20

    if-lez p0, :cond_1

    const/4 v1, 0x1

    :cond_1
    return v1

    :catch_0
    move-exception p0

    .line 373
    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return v1
.end method

.method static isInitialized()Z
    .locals 3

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 102
    :cond_0
    :try_start_0
    sget-boolean v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->initialized:Z
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception v0

    const-class v2, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0, v2}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return v1
.end method

.method private static matchIndicators([Ljava/lang/String;[Ljava/lang/String;)Z
    .locals 7

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 309
    :cond_0
    :try_start_0
    array-length v0, p0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_3

    aget-object v3, p0, v2

    .line 310
    array-length v4, p1

    const/4 v5, 0x0

    :goto_1
    if-ge v5, v4, :cond_2

    aget-object v6, p1, v5

    .line 311
    invoke-virtual {v6, v3}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v6
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    if-eqz v6, :cond_1

    const/4 p0, 0x1

    return p0

    :cond_1
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :cond_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_3
    return v1

    :catch_0
    move-exception p0

    .line 317
    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return v1
.end method

.method private static nonparseFeatures(Lorg/json/JSONObject;Lorg/json/JSONArray;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)[F
    .locals 7

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/16 v0, 0x1e

    .line 226
    :try_start_0
    new-array v0, v0, [F

    const/4 v2, 0x0

    .line 227
    invoke-static {v0, v2}, Ljava/util/Arrays;->fill([FF)V

    .line 229
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v3

    const/4 v4, 0x3

    const/4 v5, 0x0

    const/4 v6, 0x1

    if-le v3, v6, :cond_1

    sub-int/2addr v3, v6

    goto :goto_0

    :cond_1
    const/4 v3, 0x0

    :goto_0
    int-to-float v3, v3

    aput v3, v0, v4
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    :goto_1
    const/high16 v3, 0x3f800000    # 1.0f

    .line 233
    :try_start_1
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v4

    if-ge v5, v4, :cond_3

    .line 234
    invoke-virtual {p1, v5}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v4

    invoke-static {v4}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->isButton(Lorg/json/JSONObject;)Z

    move-result v4

    if-eqz v4, :cond_2

    const/16 v4, 0x9

    .line 235
    aget v6, v0, v4

    add-float/2addr v6, v3

    aput v6, v0, v4
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    :cond_2
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :catch_0
    :cond_3
    const/16 p1, 0xd

    const/high16 v4, -0x40800000    # -1.0f

    :try_start_2
    aput v4, v0, p1

    const/16 p1, 0xe

    aput v4, v0, p1

    .line 245
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 p2, 0x7c

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {p1, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 247
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    .line 248
    new-instance p4, Ljava/lang/StringBuilder;

    invoke-direct {p4}, Ljava/lang/StringBuilder;-><init>()V

    .line 249
    invoke-static {p0, p4, p2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->updateHintAndTextRecursively(Lorg/json/JSONObject;Ljava/lang/StringBuilder;Ljava/lang/StringBuilder;)V

    .line 250
    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    .line 251
    invoke-virtual {p4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    const/16 p4, 0xf

    const-string v4, "ENGLISH"

    const-string v5, "COMPLETE_REGISTRATION"

    const-string v6, "BUTTON_TEXT"

    .line 254
    invoke-static {v4, v5, v6, p2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_4

    const/high16 v4, 0x3f800000    # 1.0f

    goto :goto_2

    :cond_4
    const/4 v4, 0x0

    :goto_2
    aput v4, v0, p4

    const/16 p4, 0x10

    const-string v4, "ENGLISH"

    const-string v5, "COMPLETE_REGISTRATION"

    const-string v6, "PAGE_TITLE"

    .line 256
    invoke-static {v4, v5, v6, p1}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_5

    const/high16 v4, 0x3f800000    # 1.0f

    goto :goto_3

    :cond_5
    const/4 v4, 0x0

    :goto_3
    aput v4, v0, p4

    const/16 p4, 0x11

    const-string v4, "ENGLISH"

    const-string v5, "COMPLETE_REGISTRATION"

    const-string v6, "BUTTON_ID"

    .line 258
    invoke-static {v4, v5, v6, p0}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_6

    const/high16 p0, 0x3f800000    # 1.0f

    goto :goto_4

    :cond_6
    const/4 p0, 0x0

    :goto_4
    aput p0, v0, p4

    const/16 p0, 0x12

    const-string p4, "password"

    .line 262
    invoke-virtual {p3, p4}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result p4

    if-eqz p4, :cond_7

    const/high16 p4, 0x3f800000    # 1.0f

    goto :goto_5

    :cond_7
    const/4 p4, 0x0

    :goto_5
    aput p4, v0, p0

    const/16 p0, 0x13

    const-string p4, "(?i)(confirm.*password)|(password.*(confirmation|confirm)|confirmation)"

    .line 264
    invoke-static {p4, p3}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p4

    if-eqz p4, :cond_8

    const/high16 p4, 0x3f800000    # 1.0f

    goto :goto_6

    :cond_8
    const/4 p4, 0x0

    :goto_6
    aput p4, v0, p0

    const/16 p0, 0x14

    const-string p4, "(?i)(sign in)|login|signIn"

    .line 266
    invoke-static {p4, p3}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p4

    if-eqz p4, :cond_9

    const/high16 p4, 0x3f800000    # 1.0f

    goto :goto_7

    :cond_9
    const/4 p4, 0x0

    :goto_7
    aput p4, v0, p0

    const/16 p0, 0x15

    const-string p4, "(?i)(sign.*(up|now)|registration|register|(create|apply).*(profile|account)|open.*account|account.*(open|creation|application)|enroll|join.*now)"

    .line 268
    invoke-static {p4, p3}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p3

    if-eqz p3, :cond_a

    const/high16 p3, 0x3f800000    # 1.0f

    goto :goto_8

    :cond_a
    const/4 p3, 0x0

    :goto_8
    aput p3, v0, p0

    const/16 p0, 0x16

    const-string p3, "ENGLISH"

    const-string p4, "PURCHASE"

    const-string v4, "BUTTON_TEXT"

    .line 272
    invoke-static {p3, p4, v4, p2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p3

    if-eqz p3, :cond_b

    const/high16 p3, 0x3f800000    # 1.0f

    goto :goto_9

    :cond_b
    const/4 p3, 0x0

    :goto_9
    aput p3, v0, p0

    const/16 p0, 0x18

    const-string p3, "ENGLISH"

    const-string p4, "PURCHASE"

    const-string v4, "PAGE_TITLE"

    .line 274
    invoke-static {p3, p4, v4, p1}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p3

    if-eqz p3, :cond_c

    const/high16 p3, 0x3f800000    # 1.0f

    goto :goto_a

    :cond_c
    const/4 p3, 0x0

    :goto_a
    aput p3, v0, p0

    const/16 p0, 0x19

    const-string p3, "(?i)add to(\\s|\\Z)|update(\\s|\\Z)|cart"

    .line 278
    invoke-static {p3, p2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p3

    if-eqz p3, :cond_d

    const/high16 p3, 0x3f800000    # 1.0f

    goto :goto_b

    :cond_d
    const/4 p3, 0x0

    :goto_b
    aput p3, v0, p0

    const/16 p0, 0x1b

    const-string p3, "(?i)add to(\\s|\\Z)|update(\\s|\\Z)|cart|shop|buy"

    .line 280
    invoke-static {p3, p1}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p3

    if-eqz p3, :cond_e

    const/high16 p3, 0x3f800000    # 1.0f

    goto :goto_c

    :cond_e
    const/4 p3, 0x0

    :goto_c
    aput p3, v0, p0

    const/16 p0, 0x1c

    const-string p3, "ENGLISH"

    const-string p4, "LEAD"

    const-string v4, "BUTTON_TEXT"

    .line 285
    invoke-static {p3, p4, v4, p2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_f

    const/high16 p2, 0x3f800000    # 1.0f

    goto :goto_d

    :cond_f
    const/4 p2, 0x0

    :goto_d
    aput p2, v0, p0

    const/16 p0, 0x1d

    const-string p2, "ENGLISH"

    const-string p3, "LEAD"

    const-string p4, "PAGE_TITLE"

    .line 287
    invoke-static {p2, p3, p4, p1}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_10

    const/high16 v2, 0x3f800000    # 1.0f

    :cond_10
    aput v2, v0, p0
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_1

    return-object v0

    :catch_1
    move-exception p0

    .line 290
    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method private static parseFeatures(Lorg/json/JSONObject;)[F
    .locals 14

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/16 v0, 0x1e

    .line 146
    :try_start_0
    new-array v0, v0, [F

    const/4 v2, 0x0

    .line 147
    invoke-static {v0, v2}, Ljava/util/Arrays;->fill([FF)V

    const-string v2, "text"

    .line 148
    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v2

    const-string v3, "hint"

    .line 149
    invoke-virtual {p0, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v3

    const-string v4, "classname"

    .line 150
    invoke-virtual {p0, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v4

    const-string v5, "inputtype"

    const/4 v6, -0x1

    .line 151
    invoke-virtual {p0, v5, v6}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v5

    const/4 v6, 0x2

    .line 153
    new-array v7, v6, [Ljava/lang/String;

    const/4 v8, 0x0

    aput-object v2, v7, v8

    const/4 v9, 0x1

    aput-object v3, v7, v9

    const-string v3, "$"

    const-string v10, "amount"

    const-string v11, "price"

    const-string v12, "total"

    .line 155
    filled-new-array {v3, v10, v11, v12}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v3, v7}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->matchIndicators([Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v3

    const-wide/high16 v10, 0x3ff0000000000000L    # 1.0

    if-eqz v3, :cond_1

    .line 157
    aget v3, v0, v8
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    float-to-double v12, v3

    invoke-static {v12, v13}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v3, 0x0

    add-double/2addr v12, v10

    double-to-float v3, v12

    :try_start_1
    aput v3, v0, v8

    :cond_1
    const-string v3, "password"

    const-string v12, "pwd"

    .line 160
    filled-new-array {v3, v12}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v3, v7}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->matchIndicators([Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 162
    aget v3, v0, v9
    :try_end_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    float-to-double v12, v3

    invoke-static {v12, v13}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v3, 0x0

    add-double/2addr v12, v10

    double-to-float v3, v12

    :try_start_2
    aput v3, v0, v9

    :cond_2
    const-string v3, "tel"

    const-string v12, "phone"

    .line 165
    filled-new-array {v3, v12}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v3, v7}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->matchIndicators([Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_3

    .line 167
    aget v3, v0, v6
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_1

    float-to-double v12, v3

    invoke-static {v12, v13}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v3, 0x0

    add-double/2addr v12, v10

    double-to-float v3, v12

    :try_start_3
    aput v3, v0, v6

    :cond_3
    const-string v3, "search"

    .line 170
    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v3, v7}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->matchIndicators([Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_4

    const/4 v3, 0x4

    .line 172
    aget v7, v0, v3
    :try_end_3
    .catch Ljava/lang/Throwable; {:try_start_3 .. :try_end_3} :catch_1

    float-to-double v12, v7

    invoke-static {v12, v13}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v7, 0x0

    add-double/2addr v12, v10

    double-to-float v7, v12

    :try_start_4
    aput v7, v0, v3

    :cond_4
    if-ltz v5, :cond_5

    const/4 v3, 0x5

    .line 177
    aget v7, v0, v3
    :try_end_4
    .catch Ljava/lang/Throwable; {:try_start_4 .. :try_end_4} :catch_1

    float-to-double v12, v7

    invoke-static {v12, v13}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v7, 0x0

    add-double/2addr v12, v10

    double-to-float v7, v12

    :try_start_5
    aput v7, v0, v3

    :cond_5
    const/4 v3, 0x3

    if-eq v5, v3, :cond_6

    if-ne v5, v6, :cond_7

    :cond_6
    const/4 v3, 0x6

    .line 182
    aget v6, v0, v3
    :try_end_5
    .catch Ljava/lang/Throwable; {:try_start_5 .. :try_end_5} :catch_1

    float-to-double v6, v6

    invoke-static {v6, v7}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v12, 0x0

    add-double/2addr v6, v10

    double-to-float v6, v6

    :try_start_6
    aput v6, v0, v3

    :cond_7
    const/16 v3, 0x20

    if-eq v5, v3, :cond_8

    .line 186
    sget-object v3, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    .line 187
    invoke-virtual {v3, v2}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/regex/Matcher;->matches()Z

    move-result v3

    if-eqz v3, :cond_9

    :cond_8
    const/4 v3, 0x7

    .line 188
    aget v5, v0, v3
    :try_end_6
    .catch Ljava/lang/Throwable; {:try_start_6 .. :try_end_6} :catch_1

    float-to-double v5, v5

    invoke-static {v5, v6}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v7, 0x0

    add-double/2addr v5, v10

    double-to-float v5, v5

    :try_start_7
    aput v5, v0, v3

    :cond_9
    const-string v3, "checkbox"

    .line 192
    invoke-virtual {v4, v3}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v3

    if-eqz v3, :cond_a

    const/16 v3, 0x8

    .line 193
    aget v5, v0, v3
    :try_end_7
    .catch Ljava/lang/Throwable; {:try_start_7 .. :try_end_7} :catch_1

    float-to-double v5, v5

    invoke-static {v5, v6}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v7, 0x0

    add-double/2addr v5, v10

    double-to-float v5, v5

    :try_start_8
    aput v5, v0, v3

    :cond_a
    const-string v3, "complete"

    const-string v5, "confirm"

    const-string v6, "done"

    const-string v7, "submit"

    .line 196
    filled-new-array {v3, v5, v6, v7}, [Ljava/lang/String;

    move-result-object v3

    new-array v5, v9, [Ljava/lang/String;

    aput-object v2, v5, v8

    invoke-static {v3, v5}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->matchIndicators([Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_b

    const/16 v2, 0xa

    .line 199
    aget v3, v0, v2
    :try_end_8
    .catch Ljava/lang/Throwable; {:try_start_8 .. :try_end_8} :catch_1

    float-to-double v5, v3

    invoke-static {v5, v6}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v3, 0x0

    add-double/2addr v5, v10

    double-to-float v3, v5

    :try_start_9
    aput v3, v0, v2

    :cond_b
    const-string v2, "radio"

    .line 203
    invoke-virtual {v4, v2}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v2

    if-eqz v2, :cond_c

    const-string v2, "button"

    invoke-virtual {v4, v2}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v2

    if-eqz v2, :cond_c

    const/16 v2, 0xc

    .line 204
    aget v3, v0, v2
    :try_end_9
    .catch Ljava/lang/Throwable; {:try_start_9 .. :try_end_9} :catch_1

    float-to-double v3, v3

    invoke-static {v3, v4}, Ljava/lang/Double;->isNaN(D)Z

    const/4 v5, 0x0

    add-double/2addr v3, v10

    double-to-float v3, v3

    :try_start_a
    aput v3, v0, v2
    :try_end_a
    .catch Ljava/lang/Throwable; {:try_start_a .. :try_end_a} :catch_1

    :cond_c
    :try_start_b
    const-string v2, "childviews"

    .line 208
    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p0

    .line 209
    invoke-virtual {p0}, Lorg/json/JSONArray;->length()I

    move-result v2

    :goto_0
    if-ge v8, v2, :cond_d

    .line 211
    invoke-virtual {p0, v8}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    invoke-static {v3}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->parseFeatures(Lorg/json/JSONObject;)[F

    move-result-object v3

    invoke-static {v0, v3}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->sum([F[F)V
    :try_end_b
    .catch Lorg/json/JSONException; {:try_start_b .. :try_end_b} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_b .. :try_end_b} :catch_1

    add-int/lit8 v8, v8, 0x1

    goto :goto_0

    :catch_0
    :cond_d
    return-object v0

    :catch_1
    move-exception p0

    .line 217
    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method private static pruneTree(Lorg/json/JSONObject;Lorg/json/JSONArray;)Z
    .locals 8

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    :cond_0
    :try_start_0
    const-string v0, "is_interacted"

    .line 322
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v0

    const/4 v2, 0x1

    if-eqz v0, :cond_1

    return v2

    :cond_1
    const-string v0, "childviews"

    .line 330
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    const/4 v3, 0x0

    .line 331
    :goto_0
    invoke-virtual {v0}, Lorg/json/JSONArray;->length()I

    move-result v4

    if-ge v3, v4, :cond_3

    .line 332
    invoke-virtual {v0, v3}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v4

    const-string v5, "is_interacted"

    .line 333
    invoke-virtual {v4, v5}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_2

    const/4 v3, 0x1

    const/4 v4, 0x1

    goto :goto_1

    :cond_2
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_3
    const/4 v3, 0x0

    const/4 v4, 0x0

    .line 340
    :goto_1
    new-instance v5, Lorg/json/JSONArray;

    invoke-direct {v5}, Lorg/json/JSONArray;-><init>()V

    if-eqz v3, :cond_4

    const/4 p0, 0x0

    .line 342
    :goto_2
    invoke-virtual {v0}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge p0, v2, :cond_7

    .line 343
    invoke-virtual {v0, p0}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v2

    .line 344
    invoke-virtual {p1, v2}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    add-int/lit8 p0, p0, 0x1

    goto :goto_2

    :cond_4
    const/4 v3, 0x0

    .line 347
    :goto_3
    invoke-virtual {v0}, Lorg/json/JSONArray;->length()I

    move-result v6

    if-ge v3, v6, :cond_6

    .line 348
    invoke-virtual {v0, v3}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v6

    .line 349
    invoke-static {v6, p1}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->pruneTree(Lorg/json/JSONObject;Lorg/json/JSONArray;)Z

    move-result v7

    if-eqz v7, :cond_5

    .line 351
    invoke-virtual {v5, v6}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    const/4 v4, 0x1

    :cond_5
    add-int/lit8 v3, v3, 0x1

    goto :goto_3

    :cond_6
    const-string p1, "childviews"

    .line 354
    invoke-virtual {p0, p1, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :cond_7
    return v4

    :catch_0
    move-exception p0

    .line 362
    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    :catch_1
    return v1
.end method

.method private static regexMatched(Ljava/lang/String;Ljava/lang/String;)Z
    .locals 2

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 305
    :cond_0
    :try_start_0
    invoke-static {p0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object p0

    invoke-virtual {p0, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    invoke-virtual {p0}, Ljava/util/regex/Matcher;->find()Z

    move-result p0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return p0

    :catch_0
    move-exception p0

    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return v1
.end method

.method private static regexMatched(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 3

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 295
    :cond_0
    :try_start_0
    sget-object v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->rules:Lorg/json/JSONObject;

    const-string v2, "rulesForLanguage"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    sget-object v2, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->languageInfo:Ljava/util/Map;

    .line 296
    invoke-interface {v2, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/String;

    invoke-virtual {v0, p0}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    const-string v0, "rulesForEvent"

    .line 297
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    sget-object v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->eventInfo:Ljava/util/Map;

    .line 298
    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    const-string p1, "positiveRules"

    .line 299
    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    sget-object p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->textTypeInfo:Ljava/util/Map;

    .line 300
    invoke-interface {p1, p2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 301
    invoke-static {p0, p3}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->regexMatched(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return p0

    :catch_0
    move-exception p0

    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return v1
.end method

.method private static sum([F[F)V
    .locals 3

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 366
    :goto_0
    :try_start_0
    array-length v1, p0

    if-ge v0, v1, :cond_1

    .line 367
    aget v1, p0, v0

    aget v2, p1, v0

    add-float/2addr v1, v2

    aput v1, p0, v0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    return-void

    :catch_0
    move-exception p0

    .line 369
    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method private static updateHintAndTextRecursively(Lorg/json/JSONObject;Ljava/lang/StringBuilder;Ljava/lang/StringBuilder;)V
    .locals 3

    const-class v0, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    :try_start_0
    const-string v0, "text"

    const-string v1, ""

    .line 378
    invoke-virtual {p0, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v0

    const-string v1, "hint"

    const-string v2, ""

    .line 379
    invoke-virtual {p0, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v1

    .line 380
    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_1

    .line 381
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 383
    :cond_1
    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_2

    .line 384
    invoke-virtual {p2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_2
    const-string v0, "childviews"

    .line 387
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p0

    if-nez p0, :cond_3

    return-void

    :cond_3
    const/4 v0, 0x0

    .line 391
    :goto_0
    invoke-virtual {p0}, Lorg/json/JSONArray;->length()I

    move-result v1
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    if-ge v0, v1, :cond_4

    .line 393
    :try_start_1
    invoke-virtual {p0, v0}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v1

    .line 394
    invoke-static {v1, p1, p2}, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;->updateHintAndTextRecursively(Lorg/json/JSONObject;Ljava/lang/StringBuilder;Ljava/lang/StringBuilder;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    :catch_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_4
    return-void

    :catch_1
    move-exception p0

    .line 399
    const-class p1, Lcom/facebook/appevents/suggestedevents/FeatureExtractor;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

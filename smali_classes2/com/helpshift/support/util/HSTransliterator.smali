.class public Lcom/helpshift/support/util/HSTransliterator;
.super Ljava/lang/Object;
.source "HSTransliterator.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_Transliteratr"

.field private static hsCharacters:Lcom/helpshift/support/util/HSCharacters; = null

.field private static initDone:Z = false


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static deinit()V
    .locals 1

    const/4 v0, 0x0

    .line 37
    sput-object v0, Lcom/helpshift/support/util/HSTransliterator;->hsCharacters:Lcom/helpshift/support/util/HSCharacters;

    const/4 v0, 0x0

    .line 38
    sput-boolean v0, Lcom/helpshift/support/util/HSTransliterator;->initDone:Z

    return-void
.end method

.method public static init()V
    .locals 3

    .line 21
    sget-boolean v0, Lcom/helpshift/support/util/HSTransliterator;->initDone:Z

    if-nez v0, :cond_0

    .line 23
    :try_start_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "hs__data"

    invoke-static {v0, v1}, Lcom/helpshift/util/AssetsUtil;->readFileAsString(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 24
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v0, "HSCharacters"

    invoke-virtual {v1, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 26
    new-instance v1, Lcom/helpshift/support/util/HSCharacters;

    invoke-direct {v1, v0}, Lcom/helpshift/support/util/HSCharacters;-><init>(Lorg/json/JSONObject;)V

    sput-object v1, Lcom/helpshift/support/util/HSTransliterator;->hsCharacters:Lcom/helpshift/support/util/HSCharacters;

    const/4 v0, 0x1

    .line 27
    sput-boolean v0, Lcom/helpshift/support/util/HSTransliterator;->initDone:Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_Transliteratr"

    const-string v2, "Error reading json : "

    .line 31
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    :goto_0
    return-void
.end method

.method public static isLoaded()Z
    .locals 1

    .line 17
    sget-boolean v0, Lcom/helpshift/support/util/HSTransliterator;->initDone:Z

    return v0
.end method

.method public static unidecode(Ljava/lang/String;)Ljava/lang/String;
    .locals 8

    .line 42
    sget-boolean v0, Lcom/helpshift/support/util/HSTransliterator;->initDone:Z

    if-nez v0, :cond_0

    .line 43
    invoke-static {}, Lcom/helpshift/support/util/HSTransliterator;->init()V

    :cond_0
    if-eqz p0, :cond_8

    .line 45
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_1

    goto :goto_4

    :cond_1
    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 49
    :goto_0
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    const/16 v3, 0x80

    if-ge v1, v2, :cond_4

    .line 50
    invoke-virtual {p0, v1}, Ljava/lang/String;->charAt(I)C

    move-result v2

    if-le v2, v3, :cond_2

    goto :goto_1

    .line 54
    :cond_2
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    if-lt v1, v2, :cond_3

    return-object p0

    :cond_3
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 58
    :cond_4
    :goto_1
    invoke-virtual {p0}, Ljava/lang/String;->toCharArray()[C

    move-result-object p0

    .line 59
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    .line 61
    array-length v2, p0

    :goto_2
    if-ge v0, v2, :cond_7

    aget-char v4, p0, v0

    if-ge v4, v3, :cond_5

    .line 63
    invoke-virtual {v1, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_3

    :cond_5
    shr-int/lit8 v5, v4, 0x8

    and-int/lit16 v4, v4, 0xff

    .line 69
    sget-object v6, Lcom/helpshift/support/util/HSTransliterator;->hsCharacters:Lcom/helpshift/support/util/HSCharacters;

    if-eqz v6, :cond_6

    sget-object v6, Lcom/helpshift/support/util/HSTransliterator;->hsCharacters:Lcom/helpshift/support/util/HSCharacters;

    invoke-static {v5}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7, v4}, Lcom/helpshift/support/util/HSCharacters;->containsKey(Ljava/lang/String;I)Z

    move-result v6

    if-eqz v6, :cond_6

    .line 70
    sget-object v6, Lcom/helpshift/support/util/HSTransliterator;->hsCharacters:Lcom/helpshift/support/util/HSCharacters;

    invoke-static {v5}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v6, v5, v4}, Lcom/helpshift/support/util/HSCharacters;->get(Ljava/lang/String;I)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_3

    :cond_6
    const-string v4, ""

    .line 73
    invoke-virtual {v1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :goto_3
    add-int/lit8 v0, v0, 0x1

    goto :goto_2

    .line 77
    :cond_7
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_8
    :goto_4
    const-string p0, ""

    return-object p0
.end method

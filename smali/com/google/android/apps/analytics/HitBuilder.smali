.class Lcom/google/android/apps/analytics/HitBuilder;
.super Ljava/lang/Object;


# static fields
.field static final FAKE_DOMAIN_HASH:Ljava/lang/String; = "1"

.field private static final GOOGLE_ANALYTICS_GIF_PATH:Ljava/lang/String; = "/__utm.gif"

.field private static final X10_PROJECT_NAMES:I = 0x8

.field private static final X10_PROJECT_SCOPES:I = 0xb

.field private static final X10_PROJECT_VALUES:I = 0x9


# direct methods
.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static appendCurrencyValue(Ljava/lang/StringBuilder;Ljava/lang/String;D)V
    .locals 4

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "="

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-wide v0, 0x412e848000000000L    # 1000000.0

    mul-double p2, p2, v0

    const-wide/high16 v2, 0x3fe0000000000000L    # 0.5

    add-double/2addr p2, v2

    invoke-static {p2, p3}, Ljava/lang/Math;->floor(D)D

    move-result-wide p1

    div-double/2addr p1, v0

    const-wide/16 v0, 0x0

    cmpl-double p3, p1, v0

    if-eqz p3, :cond_0

    invoke-static {p1, p2}, Ljava/lang/Double;->toString(D)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_0
    return-void
.end method

.method private static appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "="

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-eqz p2, :cond_0

    invoke-virtual {p2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result p1

    if-lez p1, :cond_0

    invoke-static {p2}, Lcom/google/android/apps/analytics/AnalyticsParameterEncoder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_0
    return-void
.end method

.method private static constructEventRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;
    .locals 9

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "5(%s*%s"

    const/4 v4, 0x2

    new-array v5, v4, [Ljava/lang/Object;

    iget-object v6, p0, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-static {v6}, Lcom/google/android/apps/analytics/HitBuilder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const/4 v7, 0x0

    aput-object v6, v5, v7

    iget-object v6, p0, Lcom/google/android/apps/analytics/Event;->action:Ljava/lang/String;

    invoke-static {v6}, Lcom/google/android/apps/analytics/HitBuilder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const/4 v8, 0x1

    aput-object v6, v5, v8

    invoke-static {v3, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/google/android/apps/analytics/Event;->label:Ljava/lang/String;

    if-eqz v3, :cond_0

    const-string v3, "*"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/google/android/apps/analytics/Event;->label:Ljava/lang/String;

    invoke-static {v3}, Lcom/google/android/apps/analytics/HitBuilder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_0
    const-string v3, ")"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v3, p0, Lcom/google/android/apps/analytics/Event;->value:I

    const/4 v5, -0x1

    if-le v3, v5, :cond_1

    const-string v3, "(%d)"

    new-array v5, v8, [Ljava/lang/Object;

    iget v6, p0, Lcom/google/android/apps/analytics/Event;->value:I

    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v3, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_1
    invoke-static {p0}, Lcom/google/android/apps/analytics/HitBuilder;->getCustomVariableParams(Lcom/google/android/apps/analytics/Event;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "/__utm.gif"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "?utmwv=4.8.1ma"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "&utmn="

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getRandomVal()I

    move-result v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, "&utmt=event"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "&utme="

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "&utmcs=UTF-8"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "&utmsr=%dx%d"

    new-array v3, v4, [Ljava/lang/Object;

    iget v5, p0, Lcom/google/android/apps/analytics/Event;->screenWidth:I

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    aput-object v5, v3, v7

    iget v5, p0, Lcom/google/android/apps/analytics/Event;->screenHeight:I

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    aput-object v5, v3, v8

    invoke-static {v2, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "&utmul=%s-%s"

    new-array v3, v4, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v7

    invoke-virtual {v0}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v3, v8

    invoke-static {v2, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "&utmac="

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/google/android/apps/analytics/Event;->accountId:Ljava/lang/String;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "&utmcc="

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->getEscapedCookieString(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getAdHitId()I

    move-result p1

    if-eqz p1, :cond_2

    const-string p1, "&utmhid="

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getAdHitId()I

    move-result p0

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    :cond_2
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static constructHitRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;
    .locals 3

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "__##GOOGLEPAGEVIEW##__"

    iget-object v2, p0, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->constructPageviewRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p1

    :goto_0
    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1

    :cond_0
    const-string v1, "__##GOOGLEITEM##__"

    iget-object v2, p0, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->constructItemRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_1
    const-string v1, "__##GOOGLETRANSACTION##__"

    iget-object v2, p0, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->constructTransactionRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_2
    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->constructEventRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :goto_1
    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getAnonymizeIp()Z

    move-result p1

    if-eqz p1, :cond_3

    const-string p1, "&aip=1"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_3
    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getUseServerTime()Z

    move-result p0

    if-nez p0, :cond_4

    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    const-string p1, "&utmht="

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-virtual {p0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_4
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static constructItemRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;
    .locals 7

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/__utm.gif"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "?utmwv=4.8.1ma"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "&utmn="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getRandomVal()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, "&utmt=item"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getItem()Lcom/google/android/apps/analytics/Item;

    move-result-object v1

    if-eqz v1, :cond_0

    const-string v2, "&utmtid"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Item;->getOrderId()Ljava/lang/String;

    move-result-object v3

    invoke-static {v0, v2, v3}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "&utmipc"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Item;->getItemSKU()Ljava/lang/String;

    move-result-object v3

    invoke-static {v0, v2, v3}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "&utmipn"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Item;->getItemName()Ljava/lang/String;

    move-result-object v3

    invoke-static {v0, v2, v3}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "&utmiva"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Item;->getItemCategory()Ljava/lang/String;

    move-result-object v3

    invoke-static {v0, v2, v3}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "&utmipr"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Item;->getItemPrice()D

    move-result-wide v3

    invoke-static {v0, v2, v3, v4}, Lcom/google/android/apps/analytics/HitBuilder;->appendCurrencyValue(Ljava/lang/StringBuilder;Ljava/lang/String;D)V

    const-string v2, "&utmiqt="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Item;->getItemCount()J

    move-result-wide v2

    const-wide/16 v4, 0x0

    cmp-long v6, v2, v4

    if-eqz v6, :cond_0

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Item;->getItemCount()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    :cond_0
    const-string v1, "&utmac="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/google/android/apps/analytics/Event;->accountId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "&utmcc="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->getEscapedCookieString(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static constructPageviewRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;
    .locals 9

    const-string v0, ""

    iget-object v1, p0, Lcom/google/android/apps/analytics/Event;->action:Ljava/lang/String;

    if-eqz v1, :cond_0

    iget-object v0, p0, Lcom/google/android/apps/analytics/Event;->action:Ljava/lang/String;

    :cond_0
    const-string v1, "/"

    invoke-virtual {v0, v1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "/"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    :cond_1
    invoke-static {v0}, Lcom/google/android/apps/analytics/HitBuilder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p0}, Lcom/google/android/apps/analytics/HitBuilder;->getCustomVariableParams(Lcom/google/android/apps/analytics/Event;)Ljava/lang/String;

    move-result-object v1

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "/__utm.gif"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "?utmwv=4.8.1ma"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "&utmn="

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getRandomVal()I

    move-result v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v4

    if-lez v4, :cond_2

    const-string v4, "&utme="

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_2
    const-string v1, "&utmcs=UTF-8"

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "&utmsr=%dx%d"

    const/4 v4, 0x2

    new-array v5, v4, [Ljava/lang/Object;

    iget v6, p0, Lcom/google/android/apps/analytics/Event;->screenWidth:I

    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    const/4 v7, 0x0

    aput-object v6, v5, v7

    iget v6, p0, Lcom/google/android/apps/analytics/Event;->screenHeight:I

    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    const/4 v8, 0x1

    aput-object v6, v5, v8

    invoke-static {v1, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "&utmul=%s-%s"

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v2}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v7

    invoke-virtual {v2}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v2

    aput-object v2, v4, v8

    invoke-static {v1, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "&utmp="

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "&utmac="

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/google/android/apps/analytics/Event;->accountId:Ljava/lang/String;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "&utmcc="

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->getEscapedCookieString(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getAdHitId()I

    move-result p1

    if-eqz p1, :cond_3

    const-string p1, "&utmhid="

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getAdHitId()I

    move-result p0

    invoke-virtual {v3, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    :cond_3
    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static constructTransactionRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;
    .locals 5

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/__utm.gif"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "?utmwv=4.8.1ma"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "&utmn="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getRandomVal()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, "&utmt=tran"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getTransaction()Lcom/google/android/apps/analytics/Transaction;

    move-result-object v1

    if-eqz v1, :cond_0

    const-string v2, "&utmtid"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Transaction;->getOrderId()Ljava/lang/String;

    move-result-object v3

    invoke-static {v0, v2, v3}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "&utmtst"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Transaction;->getStoreName()Ljava/lang/String;

    move-result-object v3

    invoke-static {v0, v2, v3}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "&utmtto"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Transaction;->getTotalCost()D

    move-result-wide v3

    invoke-static {v0, v2, v3, v4}, Lcom/google/android/apps/analytics/HitBuilder;->appendCurrencyValue(Ljava/lang/StringBuilder;Ljava/lang/String;D)V

    const-string v2, "&utmttx"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Transaction;->getTotalTax()D

    move-result-wide v3

    invoke-static {v0, v2, v3, v4}, Lcom/google/android/apps/analytics/HitBuilder;->appendCurrencyValue(Ljava/lang/StringBuilder;Ljava/lang/String;D)V

    const-string v2, "&utmtsp"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/Transaction;->getShippingCost()D

    move-result-wide v3

    invoke-static {v0, v2, v3, v4}, Lcom/google/android/apps/analytics/HitBuilder;->appendCurrencyValue(Ljava/lang/StringBuilder;Ljava/lang/String;D)V

    const-string v1, "&utmtci"

    const-string v2, ""

    invoke-static {v0, v1, v2}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "&utmtrg"

    const-string v2, ""

    invoke-static {v0, v1, v2}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "&utmtco"

    const-string v2, ""

    invoke-static {v0, v1, v2}, Lcom/google/android/apps/analytics/HitBuilder;->appendStringValue(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    const-string v1, "&utmac="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/google/android/apps/analytics/Event;->accountId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "&utmcc="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p0, p1}, Lcom/google/android/apps/analytics/HitBuilder;->getEscapedCookieString(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static createX10Project([Lcom/google/android/apps/analytics/CustomVariable;Ljava/lang/StringBuilder;I)V
    .locals 5

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v0, "("

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/4 v0, 0x0

    const/4 v1, 0x1

    const/4 v1, 0x0

    const/4 v2, 0x1

    :goto_0
    array-length v3, p0

    if-ge v1, v3, :cond_2

    aget-object v3, p0, v1

    if-eqz v3, :cond_1

    aget-object v3, p0, v1

    if-nez v2, :cond_0

    const-string v4, "*"

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1

    :cond_0
    const/4 v2, 0x0

    :goto_1
    invoke-virtual {v3}, Lcom/google/android/apps/analytics/CustomVariable;->getIndex()I

    move-result v4

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v4, "!"

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    packed-switch p2, :pswitch_data_0

    :pswitch_0
    goto :goto_3

    :pswitch_1
    invoke-virtual {v3}, Lcom/google/android/apps/analytics/CustomVariable;->getScope()I

    move-result v3

    invoke-virtual {p1, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    goto :goto_3

    :pswitch_2
    invoke-virtual {v3}, Lcom/google/android/apps/analytics/CustomVariable;->getValue()Ljava/lang/String;

    move-result-object v3

    goto :goto_2

    :pswitch_3
    invoke-virtual {v3}, Lcom/google/android/apps/analytics/CustomVariable;->getName()Ljava/lang/String;

    move-result-object v3

    :goto_2
    invoke-static {v3}, Lcom/google/android/apps/analytics/HitBuilder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Lcom/google/android/apps/analytics/HitBuilder;->x10Escape(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_1
    :goto_3
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_2
    const-string p0, ")"

    invoke-virtual {p1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x8
        :pswitch_3
        :pswitch_2
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method

.method private static encode(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    invoke-static {p0}, Lcom/google/android/apps/analytics/AnalyticsParameterEncoder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static getCustomVariableParams(Lcom/google/android/apps/analytics/Event;)Ljava/lang/String;
    .locals 2

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getCustomVariableBuffer()Lcom/google/android/apps/analytics/CustomVariableBuffer;

    move-result-object p0

    if-nez p0, :cond_0

    const-string p0, ""

    return-object p0

    :cond_0
    invoke-virtual {p0}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->hasCustomVariables()Z

    move-result v1

    if-nez v1, :cond_1

    const-string p0, ""

    return-object p0

    :cond_1
    invoke-virtual {p0}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->getCustomVariableArray()[Lcom/google/android/apps/analytics/CustomVariable;

    move-result-object p0

    const/16 v1, 0x8

    invoke-static {p0, v0, v1}, Lcom/google/android/apps/analytics/HitBuilder;->createX10Project([Lcom/google/android/apps/analytics/CustomVariable;Ljava/lang/StringBuilder;I)V

    const/16 v1, 0x9

    invoke-static {p0, v0, v1}, Lcom/google/android/apps/analytics/HitBuilder;->createX10Project([Lcom/google/android/apps/analytics/CustomVariable;Ljava/lang/StringBuilder;I)V

    const/16 v1, 0xb

    invoke-static {p0, v0, v1}, Lcom/google/android/apps/analytics/HitBuilder;->createX10Project([Lcom/google/android/apps/analytics/CustomVariable;Ljava/lang/StringBuilder;I)V

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static getEscapedCookieString(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;
    .locals 3

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "__utma="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "1"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "."

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getUserId()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, "."

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getTimestampFirst()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, "."

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getTimestampPrevious()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, "."

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getTimestampCurrent()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, "."

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/Event;->getVisits()I

    move-result p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p0, ";"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-eqz p1, :cond_0

    const-string p0, "+__utmz="

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "1"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "."

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Referrer;->getTimeStamp()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p0, "."

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Referrer;->getVisit()I

    move-result p0

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Integer;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "."

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Referrer;->getIndex()I

    move-result p0

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Integer;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "."

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Referrer;->getReferrerString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ";"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_0
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/google/android/apps/analytics/HitBuilder;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static x10Escape(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-string v0, "\'"

    const-string v1, "\'0"

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p0

    const-string v0, ")"

    const-string v1, "\'1"

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p0

    const-string v0, "*"

    const-string v1, "\'2"

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p0

    const-string v0, "!"

    const-string v1, "\'3"

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method
